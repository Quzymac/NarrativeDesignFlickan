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
    int itemSlot = 0;
    float strength = 0f;

    //Kollar inputs. Om 1-8 byter itemslot i items arrayen. X gör att man droppar ett item i sitt inventory framför sig.
    private void Update()
    {

        if (Input.GetKey(KeyCode.X) && items[itemSlot] != null && strength < 5)
            strength += Time.deltaTime * 4;
        if (Input.GetKeyUp(KeyCode.X) && items[itemSlot] != null)
        {
            if (strength < 1f)
            {
                DropItem();
                strength = 0f;
            }
            else
            {
                ThrowItem();
            }
        }

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
        if (Physics.CheckBox(gameObject.transform.position + gameObject.transform.forward, new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity, -1, QueryTriggerInteraction.Ignore) == false)
        {
            items[itemSlot].transform.position = gameObject.transform.position + gameObject.transform.forward;
            items[itemSlot].SetActive(true);
            if (items[itemSlot].GetComponent<Rigidbody>())
            {
                items[itemSlot].GetComponent<Rigidbody>().Sleep();
            }
            items[itemSlot] = null;
            itemImages[itemSlot].sprite = null;
            itemImages[itemSlot].gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Can't drop items here");
        }
    }

    void ThrowItem()
    {
        if (items[itemSlot].GetComponent<Rigidbody>() != null && Physics.CheckBox(gameObject.transform.position + gameObject.transform.forward, new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity, -1, QueryTriggerInteraction.Ignore) == false)
        {
            items[itemSlot].transform.position = gameObject.transform.position + gameObject.transform.forward;
            items[itemSlot].SetActive(true);
            items[itemSlot].GetComponent<Rigidbody>().velocity = transform.forward * strength + transform.up * (strength * 2);
            items[itemSlot] = null;
            itemImages[itemSlot].sprite = null;
            itemImages[itemSlot].gameObject.SetActive(false);
            strength = 0f;
        }
        else
        {
            strength = 0f;
            Debug.Log("Can't throw this here");
        }
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
    public GameObject SearchInventory(Item itemToFind)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null && items[i].GetComponent<OB_Item>().GetItemType() == itemToFind)
            {
                return items[i];
            }
        }
        return null;
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
