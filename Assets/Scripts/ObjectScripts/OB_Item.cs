using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Ska testa så att alla items har en gemensam beskriving så alla cloner av ett item kan användas för quest.
public enum Item {Mushroom, Ball} //Describes item type
public class OB_Item : OB_Interactable {

    [SerializeField]
    Item type;

    [SerializeField]
    Sprite invPicture;

    public Item GetItemType()
    {
        return type;
    }

    public Sprite GetInvImage()
    {
        return invPicture;
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

        if (player.GetComponent<CH_Inventory>().AddItem(gameObject))
        {
            if (gameObject.GetComponent<Rigidbody>().isKinematic == true)
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.SetActive(false);
        }

    }

    private void Update()
    {
        DoThings();
    }

    /*public void TakeItem()
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
        else if (recivedItem == false)
        {
            Debug.Log("I need a " + requestedItem.name);
        }
        else if (recivedItem == true)
        {
            Debug.Log("I have already recieved a" + requestedItem.name);
        }
    }*/
}
