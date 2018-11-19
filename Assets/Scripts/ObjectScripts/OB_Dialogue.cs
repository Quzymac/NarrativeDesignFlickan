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

    public override void Activate(GameObject player)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UI_DialogueController.Instance.DisplayTextBox(dialogueFile);
        }
    }

    private void Update()
    {
        DoThings();
    }
}
