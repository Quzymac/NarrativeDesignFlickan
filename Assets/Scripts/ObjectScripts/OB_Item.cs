using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Ska testa så att alla items har en gemensam beskriving så alla cloner av ett item kan användas för quest.
public enum Item {Apple, Blueberry, Lingonberry, Chanterelle, Falukorv, Birch_polypore, Fly_agaric, Gulfotshatta, Pine_cone, Fir_cone, Wine } //Beskriver item type, lägg till med nya sorters items
public class OB_Item : OB_Interactable {

    [SerializeField]    //Ställ in i inspektorn vilket sorts item detta är.
    Item type;
    [SerializeField]    //Lägg till den sprite som objektet ska ha i UI Inventorys
    Sprite invPicture;
    [SerializeField]
    Sounds sound;
    AudioHandler audioHandler;

    private void Start()
    {
        audioHandler = FindObjectOfType<AudioHandler>();
    }

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
            if (audioHandler != null)
                audioHandler.PlaySound(sound);

            if (gameObject.GetComponent<Rigidbody>() != null && gameObject.GetComponent<Rigidbody>().isKinematic == true)
            {
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
            player.GetComponent<CH_Interact>().RemoveInteractable(gameObject);
            gameObject.SetActive(false);
        }

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
