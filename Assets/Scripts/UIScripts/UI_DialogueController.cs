using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_DialogueController : MonoBehaviour {    //TODO: Set up interaction with buttons.

    //Set in inspector
    [Header("TextFields")]
    [SerializeField]
    private Text nextTextPage;
    [SerializeField]
    private Text nameField;
    [SerializeField]
    private Text textBox;
    [SerializeField]
    private Text textBoxFirstChar;  //Unity only supports one font per text object. This one will be used to apply a different font to the first character in the textbox.

    [Header("DialogueOptionButtons")]
    [SerializeField]
    private Button[] optionButtons= new Button[3];

    [Header("TextboxSprites")]
    [SerializeField]
    private Image arrow;
    [SerializeField]
    private Image panel;    //BackgroundSprite

    [Header("Blinkinterval & delay")]
    [SerializeField]
    private float interval;
    [SerializeField]
    private float startDelay;

    private bool isActive = false;
    private bool isBlinking = false;


    //Do something when waking up.
    private void Awake()
    {
        //We do not want multiple controllers in one scene. THERE CAN BE ONLY ONE!
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        // DontDestroyOnLoad(this);    //Carry the same dialogue manager (this object) over to the next scene instead of destroying it. This needs to be called on root object: probably a good idea to move it!
        //
    }

    //This region sets up the singelton pattern for easy access and safety. The instance is set in awake since this is a monobehaviour. This also means the field cannot be marked as readonly.    
    #region Singelton
    private static UI_DialogueController instance;

    public static UI_DialogueController Instance
    {
        get { return instance; }
    }

    #endregion

    //This region handles displaying simple messageboxes with strings aquired at runtime.
    #region Display Message string

    //<Summary>
    //This method opens up the dialogbox and displays a simple message based on strings instead of the dialogue container. Use to display single messageboxes with runtime information.
    //Arguments: a title and text to display.
    //Return: void.
    //<Summary>
    public void DisplayMessage(string title, string message, float time = 0)
    {
        panel.enabled = true;
        isActive = true;
        SetTitle(title);
        SetText(message);
        if(time > 0.001)
        {
            Wait(time);
            Closemessage();
        }
    }

    //<Summary>
    //This method closes the dialogbox. Exists for clarity putposes and is to be used together with the DisplayMessage method.
    //Arguments: void.
    //Return: void.
    //<Summary>
    public void Closemessage()
    {
        EndDialogue();
    }

    #endregion

    //This Region handles diplaying the dialogue on screen.
    #region Display Dialogue

    //<Summary>
    //This method handles displaying the textbox and dialogue on the UI.
    //Arguments: A dialogue to display.
    //Return: void.
    //<Summary>
    public void DisplayTextBox(Dialogues dialogue) //This needs more cleanup. Maybe make a switch statement too?
    {
        if (!isActive && Input.GetKeyDown(KeyCode.E))   //Initiate dialogue
        {
            DialogueManager.Instance.ActiveDialogues = DialogueManager.Instance.LoadDialogues(dialogue);
            OpenDialogue(DialogueManager.Instance.Message());
            SetNextPageText();
            //SetButtonsState();
        }
        else if (isActive && !DialogueManager.Instance.IsResponding && !DialogueManager.Instance.HasOptions() && DialogueManager.Instance.HasRemainingMessages() && Input.GetKeyDown(KeyCode.E)) //Jump to the dialogue at index+1 if there are no options.
        {
            DialogueManager.Instance.NextDialogue();
            SetDialogue(DialogueManager.Instance.Message());
            SetNextPageText();
            for(int i =0; i < DialogueManager.Instance.Message().ChoiceNames.Count; i++)
            {
                OptionsManager.Instance.SetOptionArea1(DialogueManager.Instance.Message().ChoiceNames[i], DialogueManager.Instance.Message().WorldChoices[i]);
            }
            //SetButtonsState();
        }
        else if (isActive && DialogueManager.Instance.IsResponding && Input.GetKeyDown(KeyCode.E))  //If we are responding, set the next dialogue to be the current index.
        {
            SetDialogue(DialogueManager.Instance.Message());
            //SetButtonsState();
            DialogueManager.Instance.IsResponding = false;
            SetNextPageText();
        }
        else if (isActive && DialogueManager.Instance.HasOptions()) //If we have options, take one.
        {
            ActivateOptionButtons(DialogueManager.Instance.Message());
            SetTitle("");
            SetText("");
            DisableNextPageText();
        }
        else if (isActive && !DialogueManager.Instance.HasOptions() && !DialogueManager.Instance.HasRemainingMessages() && Input.GetKeyDown(KeyCode.E)) //if there are no remaining messages and no options, end.
        {
            EndDialogue();
            DisableOptionButtons();
            DisableNextPageText();
            Debug.Log(OptionsManager.Instance.GetOptionArea1("B1_Alf_1"));
        }
    }

    //<Summary>
    //This method opens up the dialogbox and displays a dialogue if a message can be found.
    //Arguments: A dialogue to display.
    //Return: void.
    //<Summary>
    private void OpenDialogue(Dialogue nextDialogue)
    {
        if (nextDialogue != null)
        {
            panel.enabled = true;
            SetDialogue(DialogueManager.Instance.Message());
            isActive = true;
        }
    }

    //<Summary>
    //This method closes the dialogbox and resets the dialogue indexer.
    //Arguments: void.
    //Return: void.
    //<Summary>
    private void EndDialogue()
    {
        StopAllCoroutines();
        Time.timeScale = 1;
        DialogueManager.Instance.DialogueIndex = 0;
        panel.enabled = false;
        nameField.text = null;
        textBox.text = null;
        textBoxFirstChar.text = null;
        isActive = false;
        arrow.enabled = false;
        StopBlink();
    }

    //<Summary>
    //This method sets the title and textbox values to a new string.
    //Arguments: A dialogue to display.
    //Return: void.
    //<Summary>
    public void SetDialogue(Dialogue nextDialogue)
    {
        SetTitle(nextDialogue.Name);
        SetText(nextDialogue.Text);
    }

    //<Summary>
    //This method gets the dialogue option at the specified index and returns it as a string.
    //Arguments: A dialogue containing the options and the index of the option.
    //Return: A string containing the option text to display on the optionbutton.
    //<Summary>
    private string GetOptionString(Dialogue dialogue, int index)
    {
        return (index + 1).ToString() +" "+ dialogue.DialogueOptions[index];
    }

    //<Summary>
    //This method is a helper function for SetDialogue.
    //Arguments: A string to set the title value to.
    //Return: void.
    //<Summary>
    private void SetTitle(string text)
    {
        nameField.text = text;
    }

    //<Summary>
    //This method is a helper function for SetDialogue.
    //Arguments: A string to set the text value to.
    //Return: void.
    //<Summary>
    private void SetText(string text)
    {
        textBox.text = "     ";         //Change this if text font size is changed
        textBoxFirstChar.text = "";
        StopAllCoroutines();
        StartCoroutine(EffectTypeText(text));
    }

    public void SetNextPageText()
    {
        nextTextPage.text = "Press E to continue";
        nextTextPage.enabled = true;
        StartBlink(arrow);
    }

    private void DisableNextPageText()
    {
        nextTextPage.enabled = false;
        //StopBlink();
    }
    #endregion

    //This region adds effects to the text in the currently displayed dialogue.
    #region Text Effects

    //<Summary>
    //This Coroutine adds a typewriter effect to the text.
    //Arguments: A string to type out character for character.
    //Return: void.
    //<Summary>
    private IEnumerator EffectTypeText(string message)
    {
        char[] charMessage = message.ToCharArray();
        for (int i = 0; i < charMessage.Length; i++)
        {
            //Add the first character to its own text component. This is nessecary to display two different fonts.
            if(i == 0)
            {
                textBoxFirstChar.text += charMessage[i];
            }
            else
            {
                textBox.text += charMessage[i];
            }
            yield return null;
        }
    }

    private IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
    }

    //<Summary>
    //This Method starts aplying a blink effect to an image.
    //Arguments: A image to start blinking.
    //Return: void.
    //<Summary>
    private void StartBlink(Image image)
    {
        if (isBlinking)
            return;
        if (image != null)
        {
            isBlinking = true;
            InvokeRepeating("ToggleState", startDelay, interval);
        }
    }

    //<Summary>
    //This method stops the blink effect on the image.
    //Arguments: void.
    //Return: void.
    //<Summary>
    private void StopBlink()
    {
        CancelInvoke("ToggleState");
        isBlinking = false;
    }

    //<Summary>
    //This method toggles the next page image on and off.
    //Arguments: void.
    //Return: void.
    //<Summary>
    private void ToggleState()
    {
        arrow.enabled = !arrow.enabled;
    }

    #endregion

    //This region activates and deactivates options buttons.
    #region Buttons

    //<Summary>
    //This method determines wether the option buttons should be active or not.
    //Arguments: void.
    //Return: void.
    //<Summary>
    private void SetButtonsState()
    {
         if (DialogueManager.Instance.HasOptions())
         {
            ActivateOptionButtons(DialogueManager.Instance.Message());
         }
         else
         {
            DisableOptionButtons();
         }            
    }

    //<Summary>
    //This method displays the optionbuttons.
    //Arguments: The dialogue containing the different options.
    //Return: void.
    //<Summary>
    private void ActivateOptionButtons(Dialogue dialogue)
    {
        for(int index=0; index < dialogue.DialogueOptions.Count; index++)
        {
            optionButtons[index].gameObject.SetActive(true);
            optionButtons[index].GetComponentInChildren<Text>().text = GetOptionString(dialogue, index);
        }
    }

    //<Summary>
    //This method disables the optionbuttons
    //Arguments: void.
    //Return: void.
    //<Summary>
    public void DisableOptionButtons()
    {
        foreach(Button button in optionButtons)
        {
            button.gameObject.SetActive(false);
            button.GetComponentInChildren<Text>().text = null;
        }
    }

    #endregion
}

