using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OB_PlaceItem : OB_Interactable {

    [SerializeField]
    GameObject givenItemPosition; //Lägg ett tomt object där man vill ett item ska hamna när man lämnar in det.
    [SerializeField]
    Item itemToPlace;
    bool haveItem = false;
    GameObject item;

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
        if (givenItemPosition != null && haveItem == false)
        {
            item = player.GetComponent<CH_Inventory>().SearchInventory(itemToPlace);
            if(item != null)
            {
                item.transform.position = givenItemPosition.transform.position;
                item.SetActive(true);
                player.GetComponent<CH_Inventory>().RemoveItem(item);
                if (item.GetComponent<Rigidbody>())
                {
                    item.GetComponent<Rigidbody>().isKinematic = true;
                }
                player.GetComponent<CH_Interact>().RemoveInteractable(item);
                foreach(Collider c in item.GetComponents<Collider>())
                {
                    if(c.isTrigger)
                    {
                        c.enabled = false;
                    }
                }
                item.GetComponent<OB_Item>().enabled = false;
                haveItem = true;
            }
        }
        else if(haveItem == true)
        {
            item.SetActive(false);
            player.GetComponent<CH_Inventory>().AddItem(item);
            if (item.GetComponent<Rigidbody>())
            {
                item.GetComponent<Rigidbody>().isKinematic = false;
            }
            player.GetComponent<CH_Interact>().RemoveInteractable(item);
            foreach (Collider c in item.GetComponents<Collider>())
            {
                if (c.isTrigger)
                {
                    c.enabled = true;
                }
            }
            item.GetComponent<Collider>().enabled = true;
            item.GetComponent<OB_Item>().enabled = true;
            haveItem = false;
            item = null;
        }
    }
}
