using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CH_Inventory : MonoBehaviour {

    GameObject[] items = new GameObject[8];
    [SerializeField]
    Button[] inventorySlots = new Button[8];
    [SerializeField]
    Image[] itemImages = new Image[8];
    public int itemSlot = 0;

    //Kollar inputs. Om 1-8 byter itemslot i items arrayen. X gör att man droppar ett item i sitt inventory framför sig.
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X) && items[itemSlot] != null)
            DropItem();

        if (Input.GetKeyDown(KeyCode.Z) && items[itemSlot] != null)
            ThrowItem();

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            inventorySlots[0].Select();
            itemSlot = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            inventorySlots[1].Select();
            itemSlot = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            inventorySlots[2].Select();
            itemSlot = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            inventorySlots[3].Select();
            itemSlot = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            inventorySlots[4].Select();
            itemSlot = 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            inventorySlots[5].Select();
            itemSlot = 5;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            inventorySlots[6].Select();
            itemSlot = 6;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            inventorySlots[7].Select();
            itemSlot = 7;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && itemSlot < 7)
        {
            itemSlot++;
            inventorySlots[itemSlot].Select();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && itemSlot > 0)
        {
            itemSlot--;
            inventorySlots[itemSlot].Select();
        }

    }

    //Flyttar ett item i sitt inventory framför karaktären, aktiverar det och tar bort det från inventory.
    void DropItem()
    {
        items[itemSlot].transform.position = gameObject.transform.position + gameObject.transform.forward;
        items[itemSlot].SetActive(true);
        items[itemSlot] = null;
        itemImages[itemSlot].sprite = null;
        itemImages[itemSlot].gameObject.SetActive(false);
    }

    void ThrowItem()
    {
        if (items[itemSlot].GetComponent<Rigidbody>() != null)
        {
            items[itemSlot].transform.position = gameObject.transform.position + gameObject.transform.forward;
            items[itemSlot].SetActive(true);
            items[itemSlot].GetComponent<Rigidbody>().velocity = Vector3.forward*5 + Vector3.up*10;
            items[itemSlot] = null;
            itemImages[itemSlot].sprite = null;
            itemImages[itemSlot].gameObject.SetActive(false);
        }
        else
            Debug.Log("Can't throw this");
    }

    //Lägger till en referens till ett gameobject.
    public bool AddItem(GameObject itemToAdd)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = itemToAdd;
                itemImages[i].sprite = itemToAdd.GetComponent<OB_Item>().GetInvImage();
                itemImages[i].gameObject.SetActive(true);
                return true;
            }
        }
        return false;
    }
    
    //Hittar ett item i inventory och retunerar true om den hittas annars false. 
    public bool SearchInventory(GameObject itemToFind)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null && items[i].gameObject == itemToFind.gameObject)
            {
                return true;
            }
        }
        return false;
    }

    //Tar bort ett item från inventory.
    public bool RemoveItem(GameObject itemToRemove)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null && items[i].gameObject == itemToRemove.gameObject)
            {
                items[i] = null;
                itemImages[i].sprite = null;
                itemImages[i].gameObject.SetActive(false);
                return true;
            }
        }
        return false;
    }
}
