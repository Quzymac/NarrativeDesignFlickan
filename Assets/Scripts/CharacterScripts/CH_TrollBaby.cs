using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CH_TrollBaby : OB_Interactable {

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
        UI_DialogueController.Instance.DisplayMessage("Tyra", "Det ser ut som Hilding, men jag är säker på att det inte är det! Var har min lillebror tagit vägen?", 5);
    }
}
