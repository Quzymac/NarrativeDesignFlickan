using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CH_Inventory : MonoBehaviour {

    [SerializeField]
    GameObject[] items = new GameObject[8];
    [SerializeField]
    int itemSlot = 0;

    //Kollar inputs. Om 1-8 byter itemslot i items arrayen. X gör att man droppar ett item i sitt inventory framför sig.
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X) && items[itemSlot] != null)
        {
            DropItem();
        }

        if (Input.GetKeyDown(KeyCode.Z) && items[itemSlot] != null)
            ThrowItem();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            itemSlot = 0;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            itemSlot = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            itemSlot = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            itemSlot = 3;
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            itemSlot = 4;
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            itemSlot = 5;
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            itemSlot = 6;
        else if (Input.GetKeyDown(KeyCode.Alpha8))
            itemSlot = 7;

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && itemSlot < 7)
            itemSlot++;
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && itemSlot > 0)
            itemSlot--;



    }

    //Flyttar ett item i sitt inventory framför karaktären, aktiverar det och tar bort det från inventory.
    void DropItem()
    {
        items[itemSlot].transform.position = gameObject.transform.position + gameObject.transform.forward;
        items[itemSlot].SetActive(true);
        items[itemSlot] = null;
    }

    void ThrowItem()
    {
        if (items[itemSlot].GetComponent<Rigidbody>() != null)
        {
            items[itemSlot].transform.position = gameObject.transform.position + gameObject.transform.forward;
            items[itemSlot].SetActive(true);
            items[itemSlot].GetComponent<Rigidbody>().velocity = Vector3.forward*5 + Vector3.up*10;
            items[itemSlot] = null;
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
                return true;
            }
        }
        return false;
    }
}
