using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadB2Interact : OB_Interactable {

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
        UI_DialogueController.Instance.DisplayMessage("Tyra", "Snok har jag sett förut men aldrig skulle jag fått för mej att det fanns ormar så stora uti Ärlaskogen.", 6);
            
    }

}
