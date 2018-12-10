using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OB_DoorMP : OB_Interactable {

    UI_DialogueController dialogController;
    private void Start()
    {
        dialogController = FindObjectOfType<UI_DialogueController>();
    }

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
        dialogController.DisplayMessage("Tyra", "Mor och far blir tokarga om jag väcker dom såhär pass på morgonen.", 5);
    }
}
