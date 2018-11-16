using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Objektet måste ha en till collider som är en trigger som är större än objektet.
//Skriptet som ska användas behöver ärva från detta skript. Exempel OB_Items : OB_Interactable
//Kolla på OB_Item eller OB_PlaceItem för exempel
public abstract class OB_Interactable : MonoBehaviour {

    bool interactable = false;
    GameObject player;

    protected void OnEnter(Collider other)
    {
        if (other.GetComponent<CH_PlayerMovement>() != null)
        {
            interactable = true;
            player = other.gameObject;
            Debug.Log("Can Interact");
        }
    }

    protected void OnExit(Collider other)
    {
        if (other.GetComponent<CH_PlayerMovement>() != null)
        {
            interactable = false;
            player = null;
            Debug.Log("Can't interact");
        }
    }

    public abstract void Activate(GameObject player); //Skript som använder detta behöver en metod vid namn Activate() som gör det som ska göras.

    public void DoThings()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactable == true)
            Activate(player);
    }
}
