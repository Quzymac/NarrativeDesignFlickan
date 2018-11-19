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
    [SerializeField]    //För nuvarandeplacerade jag en audiosource på playercameran och lade till en ljudfil där.
    AudioSource source;

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
            if(source != null)
                source.Play();

            if (gameObject.GetComponent<Rigidbody>() != null && gameObject.GetComponent<Rigidbody>().isKinematic == true)
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
            gameObject.SetActive(false);
        }

    }

    private void Update()
    {
        DoThings();
    }
    /*
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
