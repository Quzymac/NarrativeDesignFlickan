using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_DialogueController : MonoBehaviour {    //TODO: Set up interaction with buttons.

    //Set in inspector
    [Header("TextFields")]
    [SerializeField]
    private Text nameField;
    [SerializeField]
    private Text textBox;
    [SerializeField]
    private Text textBoxFirstChar;  //Unity only supports one font per text object. This one will be used to apply a different font to the first character in the textbox.

    [Header("DialogueOptionButtons")]
    [SerializeField]
    private Button[] optionButtons= new Button[3];

    [Header("TextboxSprite")]
    [SerializeField]
    private Image panel;    //BackgroundSprite

    private bool isActive = false;

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
            SetButtonsState();
        }
        else if (isActive && !DialogueManager.Instance.IsResponding && !DialogueManager.Instance.HasOptions() && DialogueManager.Instance.HasRemainingMessages() && Input.GetKeyDown(KeyCode.E)) //Jump to the dialogue at index+1 if there are no options.
        {
            DialogueManager.Instance.NextDialogue();
            SetDialogue(DialogueManager.Instance.Message());
            SetButtonsState();
        }
        else if (isActive && DialogueManager.Instance.IsResponding && Input.GetKeyDown(KeyCode.E))  //If we are responding, set the next dialogue to be the current index.
        {
            SetDialogue(DialogueManager.Instance.Message());
            SetButtonsState();
            DialogueManager.Instance.IsResponding = false;
        }
        else if (isActive && DialogueManager.Instance.HasOptions()) //If we have options, take one.
        {
            ActivateOptionButtons(DialogueManager.Instance.Message());
        }
        else if (isActive && !DialogueManager.Instance.HasOptions() && !DialogueManager.Instance.HasRemainingMessages() && Input.GetKeyDown(KeyCode.E)) //if there are no remaining messages and no options, end.
        {
            EndDialogue();
            DisableOptionButtons();
        }
    }

    //<Summary>
    //This method opens up the dialogbox and displays a message if a message can be found.
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
        DialogueManager.Instance.DialogueIndex = 0;
        panel.enabled = false;
        nameField.text = null;
        textBox.text = null;
        textBoxFirstChar.text = null;
        isActive = false;
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
    #endregion

    //This region activates and deactivates options buttons.
    #region Buttons
        //Perahps a keybind manager should be set up to define all keybinds?

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

    private void ActivateOptionButtons(Dialogue dialogue)
    {
        for(int i=0; i < dialogue.DialogueOptions.Count; i++)
        {
            optionButtons[i].gameObject.SetActive(true);
        }
    }

    public void DisableOptionButtons()
    {
        foreach(Button button in optionButtons)
        {
            button.gameObject.SetActive(false); 
        }
    }

    #endregion
}

