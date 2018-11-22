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
    [SerializeField]
    ParticleSystem particle;
    ParticleSystem activeParticle;
    List<Item> consumables = new List<Item> { Item.Apple, Item.Blueberry, Item.Chanterelle, Item.Lingonberry, Item.Wine };
    string[] itemNames = {"Äpple", "Blåbär", "Kantarell", "Röd flugsvamp", "Lingon", "Gulfotshätta", "Vinflaska", "Falukorv", "Björkticka", "Tallkotte", "Grankotte" };
    string[] itemConsumeDesc = { "Syrligt och mättande!", "Söta blåbär är det bästa som finns!", "Beska, men ändå underbara!", "Skogens guld är både matig och god!",
    "Trollet kanske vill ha den här.", "Ser inte god ut. Jag är nog hellre hungrig.", "Jag blir fasligt sjuk om jag äter den här.", "Jag tror inte jag kan äta såna här svampar.",
    "Dom här kan jag bara äta omogna.", "Den här kommer jag nog bara få ont i munnen av att äta.", "Jättegott! Men jag känner mig lite konstig nu."};
    
    //Kollar inputs. Om 1-8 byter itemslot i items arrayen. X gör att man droppar ett item i sitt inventory framför sig.
    //Keycodes kommer bytas ut till input manager referenser senare antagligen.
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

        if (Input.GetKeyDown(KeyCode.C))
        {
            ConsumeItem();
        }


        if (Input.GetKeyDown(KeyCode.R) && reqItems.Contains(items[itemSlot].GetComponent<OB_Item>().GetItemType()) && givingItems)
        {
            if (!itemsToGive.Contains(items[itemSlot]))
            {
                itemsToGive.Add(items[itemSlot]);
            }
            Debug.Log("Added a " + items[itemSlot].GetComponent<OB_Item>().GetItemType().ToString() + " to give");
        }

        if (Input.GetKeyDown(KeyCode.Return) && givingItems)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (itemsToGive.Contains(items[i]))
                {
                    items[i] = null;
                }
            }
            GiveItems();
            givingItems = false;
        }
        else if(Input.GetKeyDown(KeyCode.T) && givingItems)
        {
            itemsToGive.Clear();
            givingItems = false;
            gameObject.GetComponent<CH_PlayerMovement>().SetStop(false);
        }
    }

    //Flyttar ett item i sitt inventory framför karaktären, aktiverar det och tar bort det från inventory.
    void DropItem()
    {
        if (Physics.CheckBox(gameObject.transform.position + gameObject.transform.forward, new Vector3(0.1f, 0.1f, 0.1f), Quaternion.identity, -1, QueryTriggerInteraction.Ignore) == false)
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
        if (items[itemSlot].GetComponent<Rigidbody>() == null)
        {
            Debug.Log("Can't throw this item");
            strength = 0f;
        }
        else if(Physics.CheckBox(gameObject.transform.position + gameObject.transform.forward, new Vector3(0.1f, 0.1f, 0.1f), Quaternion.identity, -1, QueryTriggerInteraction.Ignore) == false)
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

    void ConsumeItem()
    {
        if (items[itemSlot].GetComponent<OB_Item>().GetItemType() == Item.Wine)
        {
            activeParticle = Instantiate(particle, gameObject.transform.position + Vector3.up, particle.transform.rotation, gameObject.transform);
            StartCoroutine(DrunkEffect(5));
        }
        UI_DialogueController.Instance.DisplayMessage(itemNames[(int)items[itemSlot].GetComponent<OB_Item>().GetItemType()], itemConsumeDesc[(int)items[itemSlot].GetComponent<OB_Item>().GetItemType()]);
        if (consumables.Contains(items[itemSlot].GetComponent<OB_Item>().GetItemType()))
        {
            Destroy(items[itemSlot]);
            items[itemSlot] = null;
            itemImages[itemSlot].sprite = null;
            itemImages[itemSlot].gameObject.SetActive(false);
        }
        StartCoroutine(RemoveItemText());
    }

    IEnumerator RemoveItemText()
    {
        int time = 3;
        while(true)
        {
            yield return new WaitForSecondsRealtime(1f);
            if (time <= 0)
            {
                UI_DialogueController.Instance.Closemessage();
                yield break;
            }
            else
            {
                time--;
                Debug.Log(time);
                yield return null;
            }
        }

    }

    IEnumerator DrunkEffect(int drunkTime)
    {
        int time = drunkTime;

        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (time <= 0)
            {
                Destroy(activeParticle.gameObject);
                activeParticle = null;
                //Avaktivera saker som startas med drunk effect här.
                yield break;
            }
            else
            {
                time--;
                yield return null;
            }
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
                if(itemToAdd.GetComponent<OB_Item>().GetItemType() == Item.Apple)
                {
                    StartCoroutine(SpawnNewItem(itemToAdd));
                }
                return true;
            }
        }
        return false;
    }

    IEnumerator SpawnNewItem(GameObject copy)
    {
        int time = 10;
        GameObject newItem;
        while (true)
        {
            yield return new WaitForSecondsRealtime(1f);
            if (time <= 0)
            {
                newItem = Instantiate(copy);
                newItem.SetActive(true);
                yield break;
            }
            else
            {
                time--;
                yield return null;
            }
        }
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

    public List<GameObject> itemsToGive;
    GameObject requester;
    bool givingItems;
    List<Item> reqItems = new List<Item>();
    public void RequestItems(GameObject inRequester, Item[] inItems)
    {
        requester = inRequester;
        reqItems.AddRange(inItems);
        inRequester.GetComponent<OB_Interactable>().enabled = false;
        itemsToGive = new List<GameObject>();
        givingItems = true;
        Debug.Log("Giving Items");
        gameObject.GetComponent<CH_PlayerMovement>().SetStop(true);
    }

    public void GiveItems()
    {
        requester.GetComponent<CH_RequestItems>().GiveItems(itemsToGive);
        requester.GetComponent<OB_Interactable>().enabled = true;
        requester = null;
        gameObject.GetComponent<CH_PlayerMovement>().SetStop(false);
        itemsToGive.Clear();
    }
}
