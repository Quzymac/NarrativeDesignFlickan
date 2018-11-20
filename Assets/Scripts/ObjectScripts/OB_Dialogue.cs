using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OB_Dialogue : OB_Interactable
{
    [SerializeField]
    Dialogues dialogueFile;

    private void OnTriggerEnter(Collider other)
    {
        OnEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnExit(other);
    }
    //This needs to be changed from hardcoded keycodes. 
    public override void Activate(GameObject player)    //Change this to switch statement
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UI_DialogueController.Instance.DisplayTextBox(dialogueFile);
        }
    }
}
