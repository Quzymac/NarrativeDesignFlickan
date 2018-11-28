using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OB_Dialogue : OB_Interactable
{
    [SerializeField]
    Dialogues dialogueFile;
    [SerializeField]
    Dialogues exhausteddialogueFile;
    [SerializeField]
    string exhaustDialogueVar;

    private void OnEnable()
    {
        OptionsManager.NewChoice += OptionsScript_ValueChanged;
    }

    private void OnDisable()
    {
        OptionsManager.NewChoice -= OptionsScript_ValueChanged;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(enabled)
            OnEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnExit(other);
    }

    //Maybe a keybind manager should be created?
    public override void Activate(GameObject player)    
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            UI_DialogueController.Instance.DisplayTextBox(dialogueFile);
        }
    }

    private void OptionsScript_ValueChanged(object sender, OptionsEventArgs e)
    {
        if(e.Option == exhaustDialogueVar)
        {
            if(e.Value == 1)
            {
                dialogueFile = exhausteddialogueFile;
            }
            if(e.Value == 2)
            {
                enabled = false;
            }
        }
    }
}
