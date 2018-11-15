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

    private void DisplayTextBox(Dialogues dialogue) //TODO: Split this into smaller functions.
    {       
        if (!isActive && Input.GetKeyDown(KeyCode.E))
        {
            DialogueManager.Instance.ActiveDialogues = DialogueManager.Instance.LoadDialogues(dialogue);  //temp
            panel.enabled = true;
            Dialogue nextDialogue = DialogueManager.Instance.NextDialogue();
            nameField.text = nextDialogue.Name;
            textBox.text = nextDialogue.Text;
            isActive = true;
        }
        //else if (isActive &&)
        //{

        //}
        else if (isActive && Input.GetKeyDown(KeyCode.E))
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        panel.enabled = false;
        nameField.text = null;
        textBox.text = null;
        isActive = false;
    }
}

