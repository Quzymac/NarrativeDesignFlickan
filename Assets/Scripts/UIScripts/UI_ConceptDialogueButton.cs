using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ConceptDialogueButton : MonoBehaviour
{
    [SerializeField]
    private Dialogues dialogue;

    public void OnPress()
    {
        UI_ConceptSceneDialogue.Instance.CurrentDialogue(dialogue);
        UI_DialogueController.Instance.Closemessage();
    }
}
