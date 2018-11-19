using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class handles the player making choices in dialogues by pressing buttons.
public class UI_DialogueOptionButtons : MonoBehaviour
{
    //Set in inspector.
    [Header("DialogueOptionId")]
    [SerializeField]
    int id;         //Set to match the requested index in dialogueoptions. Starts at 0.

    public void OnPress()
    {
        UI_DialogueController.Instance.SetDialogue(DialogueManager.Instance.DialogueOption(id));
        DialogueManager.Instance.DialogueIndex = DialogueManager.Instance.ActiveDialogues[DialogueManager.Instance.DialogueIndex].DialogueOptionsIndexes[id];   //Sets the next index. Split this into its own
        DialogueManager.Instance.IsResponding = true;//more descriptive method.
    }                                                                                                                                                           
}
