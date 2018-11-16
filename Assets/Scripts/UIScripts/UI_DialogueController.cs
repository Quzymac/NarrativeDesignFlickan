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

    private void OpenDialogue(Dialogue nextDialogue)
    {
        if (nextDialogue != null)
        {
            panel.enabled = true;
            SetDialogue(DialogueManager.Instance.Message());
            isActive = true;
        }
    }

    private void EndDialogue()
    {
        DialogueManager.Instance.DialogueIndex = 0;
        panel.enabled = false;
        nameField.text = null;
        textBox.text = null;
        isActive = false;
    }

    private void SetDialogue(Dialogue nextDialogue)
    {
        SetTitle(nextDialogue.Name);
        SetText(nextDialogue.Text);
    }

    private void SetTitle(string text)
    {
        nameField.text = text;
    }

    private void SetText(string text)
    {
        textBox.text = "";
        StopAllCoroutines();
        StartCoroutine(EffectTypeText(text));
    }

    private IEnumerator EffectTypeText(string message)
    {
        foreach(char character in message.ToCharArray())
        {
            textBox.text += character;
            yield return null;
        }
    }
}

