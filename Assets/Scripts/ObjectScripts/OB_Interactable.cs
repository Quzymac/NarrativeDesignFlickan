using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Skriptet är för stunden bara för saker man kan ta upp. Kommer ändra till generella saker senare. 
//Objektet måste ha en till collider som är en trigger som är större än objektet.
public class OB_Interactable : MonoBehaviour {

    [SerializeField]
    GameObject givenItemPosition; //Lägg ett tomt object där man vill ett item ska hamna när man lämnar in det. Optional
    [SerializeField]
    UnityEvent method; //Välj vilken method som ska köras närman interagerar med ett objekt.

    bool recivedItem = false;
    bool interactable = false;
    CH_Inventory inventory;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<CH_PlayerMovement>() != null)
        {
            interactable = true;
            Debug.Log("Can Interact");
            inventory = other.GetComponent<CH_Inventory>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CH_PlayerMovement>() != null)
        {
            interactable = false;
            Debug.Log("Can't interact");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactable == true && method != null)
            method.Invoke(); //Aktiverar den method som är vald i inspektorn
    }

    public void TakeItem()
    {
        if (inventory.AddItem(gameObject))
        {
            if (gameObject.GetComponent<Rigidbody>().isKinematic == true)
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
            interactable = false;
            gameObject.SetActive(false);
        }
    }

    public void PlaceItem(GameObject item)
    {
        if (givenItemPosition != null && inventory.SearchInventory(item))
        {
            item.transform.position = givenItemPosition.transform.position;
            item.SetActive(true);
            inventory.RemoveItem(item);
            item.GetComponent<Rigidbody>().isKinematic = true;
            recivedItem = true;
        }
    }

    public void NeedItem(GameObject requestedItem)
    {
        if (inventory.SearchInventory(requestedItem) && recivedItem == false)
        {
            PlaceItem(requestedItem);
            inventory.RemoveItem(requestedItem);
            recivedItem = true;
            Debug.Log("Thanks");
        }
        else if(recivedItem == false)
        {
            Debug.Log("I need a " + requestedItem.name);
        }
        else if(recivedItem == true)
        {
            Debug.Log("I have already recieved a" + requestedItem.name);
        }
    }
}
