using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_DialogueController : MonoBehaviour {    

    //Set in inspector
    [SerializeField]
    private Text nameField;
    [SerializeField]
    private Text textBox;
    [SerializeField]
    private Text textBoxFirstChar;  //Unity only support one font per text object. This one will be used to apply a different font to the first character in the textbox.
    [SerializeField]
    private Image panel;

    private bool isActive = false;
	
	// TODO: Change this to initiate with ontriggerenter with interactable. 
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DisplayTextBox(Dialogues.TestDialogue);
        }
    }

    //<Summary>
    //This method handles displaying the textbox and dialogue on the UI.
    //Arguments: A dialogue to display.
    //Return: void.
    //<Summary>
    private void DisplayTextBox(Dialogues dialogue) 
    {
        if (!isActive && Input.GetKeyDown(KeyCode.E))
        {
            DialogueManager.Instance.ActiveDialogues = DialogueManager.Instance.LoadDialogues(dialogue);
            OpenDialogue(DialogueManager.Instance.Message());
        }
        else if (isActive && DialogueManager.Instance.HasRemaningMessages() && Input.GetKeyDown(KeyCode.E))
        {
            DialogueManager.Instance.NextDialogue();
            SetDialogue(DialogueManager.Instance.Message());
        }
        else if (isActive && Input.GetKeyDown(KeyCode.E))
        {
            EndDialogue();
        }
    }

    //<Summary>
    //This method opens up the dialogbox and displays a message if a message could be found.
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
    private void SetDialogue(Dialogue nextDialogue)
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

    //<Summary>
    //This Coroutine adds a typewriter effect to the text.
    //Arguments: A string to type out charachter for character.
    //Return: void.
    //<Summary>
    private IEnumerator EffectTypeText(string message)
    {
        char[] charMessage = message.ToCharArray();
        for (int i = 0; i < charMessage.Length; i++)
        {
            //Add the first character to it's own text component. This is nessecary to display two different fonts.
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
}

