using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Objektet måste ha en till collider som är en trigger som är större än objektet.
//Skriptet som ska användas behöver ärva från detta skript. Exempel OB_Items : OB_Interactable
//Kolla på OB_Item eller OB_PlaceItem för exempel
public abstract class OB_Interactable : MonoBehaviour {

    bool interactable = false;
    GameObject player;

    //Skript som ärver behöver en OnTriggerEnter som kallar på OnEnter(). Triggern bör vara större än objektet.
    protected void OnEnter(Collider other)
    {
        if (other.GetComponent<CH_PlayerMovement>() != null)
        {
            interactable = true;
            player = other.gameObject;
            Debug.Log("Can Interact");
            other.GetComponent<CH_Interact>().AddInteractable(gameObject);
            if (GetComponent<UI_InteractionText>() != null)
                GetComponent<UI_InteractionText>().SetTextActive(true);
        }
    }

    //Skript som ärver behöver en OnTriggerExit som kallar på OnExit(). Triggern bör vara större än objektet.
    protected void OnExit(Collider other)
    {
        if (other.GetComponent<CH_PlayerMovement>() != null)
        {
            interactable = false;
            player = null;
            Debug.Log("Can't interact");
            other.GetComponent<CH_Interact>().RemoveInteractable(gameObject);
            if (GetComponent<UI_InteractionText>() != null)
                GetComponent<UI_InteractionText>().SetTextActive(false);

        }
    }
    //Skript som använder detta behöver en metod vid namn public override void Activate() som gör det som ska göras.
    public abstract void Activate(GameObject player);

    //Skript som ärver behöver en update som kallar på DoThings().
    public void DoThings()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactable == true)
            Activate(player);
    }
}
