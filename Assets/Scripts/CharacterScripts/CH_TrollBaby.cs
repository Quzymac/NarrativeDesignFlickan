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
        UI_DialogueController.Instance.DisplayMessage("Tyra", "Den här ungen är inte min Lillebror! Detta är ett litet trollbarn! Har någon bytt ut min lillebror mot ett litet trollbarn?", 5f);
    }
}
