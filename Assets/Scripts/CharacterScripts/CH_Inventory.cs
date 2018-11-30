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
    //[SerializeField]
    ParticleSystem particle;
    ParticleSystem activeParticle;
    [SerializeField]
    Sprite orgInvImage; //InvImage inactive
    [SerializeField]
    Sprite giveInvImgage; //InvImage given
    List<Item> consumables = new List<Item> { Item.Apple, Item.Blueberry, Item.Chanterelle, Item.Lingonberry, Item.Wine };
    string[] itemNames = {"Äpple", "Blåbär", "Lingon", "Kantarell", "Falukorv", "Björkticka", "Röd flugsvamp", "Gulfotshätta", "Tallkotte", "Grankotte", "Vinflaska" };
    string[] itemConsumeDesc = { "Syrligt och mättande!", "Söta blåbär är det bästa som finns!", "Beska, men ändå underbara!", "Skogens guld är både matig och god!",
    "Trollet kanske vill ha den här.", "Ser inte god ut. Jag är nog hellre hungrig.", "Jag blir fasligt sjuk om jag äter den här.", "Jag tror inte jag kan äta såna här svampar.",
    "Dom här kan jag bara äta omogna.", "Den här kommer jag nog bara få ont i munnen av att äta.", "Jättegott! Men jag känner mig lite konstig nu."};
    List<Button> changedButtons = new List<Button>();
    [Header("BlåbärBush prefab utan bär")]
    [SerializeField]
    GameObject blueberryBushPrefab;
    [Header("LingonBush prefab utan bär")]
    [SerializeField]
    GameObject lingonBushPrefab;
    [Header("Blueberry Prefab")]
    [SerializeField]
    GameObject blueberryPrefab;
    [Header("Lingonberry Prefab")]
    [SerializeField]
    GameObject lingonberryPrefab;
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

        if (Input.GetKeyDown(KeyCode.C))
        {
            ConsumeItem();
        }


        if (Input.GetKeyDown(KeyCode.R) &&  givingItems)
        {
            if(items[itemSlot] != null && reqItems.Contains(items[itemSlot].GetComponent<OB_Item>().GetItemType()) && !itemsToGive.Contains(items[itemSlot]) && itemsToGive.Count < maximumItems)
            {
                itemsToGive.Add(items[itemSlot]);
                changedButtons.Add(inventorySlots[itemSlot]);
                inventorySlots[itemSlot].image.sprite = giveInvImgage;
            }
            else if(itemsToGive.Count == maximumItems)
            {
                UI_DialogueController.Instance.DisplayMessage("Tyra", requester.name + " vill inte ha fler saker", 3f);
            }
            else if(items[itemSlot] != null)
            {
                UI_DialogueController.Instance.DisplayMessage("Tyra", requester.name + " vill inte ha detta föremål", 3f);
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) && givingItems)
        {
            for(int i = 0; i < items.Length; i++)
            {
                if(itemsToGive.Contains(items[i]))
                {
                    items[i] = null;
                    itemImages[i].sprite = null;
                    itemImages[i].gameObject.SetActive(false);
                }
            }
            GiveItems();
            foreach(Button b in changedButtons)
            {
                b.image.sprite = orgInvImage;
            }
            changedButtons.Clear();
            givingItems = false;
        }
        else if(Input.GetKeyDown(KeyCode.T) && givingItems)
        {
            foreach (Button b in changedButtons)
            {
                b.image.sprite = orgInvImage;
            }
            changedButtons.Clear();
            itemsToGive.Clear();
            givingItems = false;
            gameObject.GetComponent<CH_PlayerMovement>().SetStop(false);
        }
    }

    //Flyttar ett item i sitt inventory framför karaktären, aktiverar det och tar bort det från inventory.
    void DropItem()
    {
        if (Physics.CheckBox(gameObject.transform.position + gameObject.transform.forward + Vector3.up, new Vector3(0.2f, 0.2f, 0.2f), Quaternion.identity, -1, QueryTriggerInteraction.Ignore) == false)
        {
            items[itemSlot].transform.position = gameObject.transform.position + gameObject.transform.forward;
            items[itemSlot].SetActive(true);
            if (items[itemSlot].GetComponent<Rigidbody>())
            {
                items[itemSlot].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                items[itemSlot].GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
            items[itemSlot] = null;
            itemImages[itemSlot].sprite = null;
            itemImages[itemSlot].gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Can't drop");
        }
    }

    void ThrowItem()
    {
        if (items[itemSlot].GetComponent<Rigidbody>() == null)
        {
            Debug.Log("Can't throw this item");
            strength = 0f;
        }
        else if(Physics.CheckBox(gameObject.transform.position + gameObject.transform.forward + Vector3.up, new Vector3(0.1f, 0.1f, 0.1f), Quaternion.identity, -1, QueryTriggerInteraction.Ignore) == false)
        {
            items[itemSlot].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
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
        UI_DialogueController.Instance.DisplayMessage(itemNames[(int)items[itemSlot].GetComponent<OB_Item>().GetItemType()], itemConsumeDesc[(int)items[itemSlot].GetComponent<OB_Item>().GetItemType()],3f);
        if (consumables.Contains(items[itemSlot].GetComponent<OB_Item>().GetItemType()))
        {
            Destroy(items[itemSlot]);
            items[itemSlot] = null;
            itemImages[itemSlot].sprite = null;
            itemImages[itemSlot].gameObject.SetActive(false);
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
                if (itemToAdd.GetComponent<OB_Item>().GetItemType() == Item.BlueberryBush)
                {
                    items[i] = Instantiate(blueberryPrefab);
                    items[i].SetActive(false);
                }
                else if(itemToAdd.GetComponent<OB_Item>().GetItemType() == Item.LingonBush)
                {
                    items[i] = Instantiate(lingonberryPrefab);
                    items[i].SetActive(false);
                }
                else
                {
                    items[i] = itemToAdd;
                }

                itemImages[i].sprite = itemToAdd.GetComponent<OB_Item>().GetInvImage();
                itemImages[i].gameObject.SetActive(true);

                if(itemToAdd.GetComponent<OB_Item>().GetItemType() == Item.Apple && itemToAdd.GetComponent<OB_Item>().Respawn)
                {
                    itemToAdd.GetComponent<OB_Item>().Respawn = false;
                    StartCoroutine(SpawnNewItem(itemToAdd, itemToAdd.transform.position, 10f));
                }

                else if(itemToAdd.GetComponent<OB_Item>().GetItemType() == Item.BlueberryBush && itemToAdd.GetComponent<OB_Item>().Respawn)
                {
                    itemToAdd.GetComponent<OB_Item>().Respawn = false;
                    StartCoroutine(SpawnNewItem(blueberryBushPrefab, itemToAdd.transform.position, 0f));
                }

                else if (itemToAdd.GetComponent<OB_Item>().GetItemType() == Item.LingonBush && itemToAdd.GetComponent<OB_Item>().Respawn)
                {
                    itemToAdd.GetComponent<OB_Item>().Respawn = false;
                    StartCoroutine(SpawnNewItem(lingonBushPrefab, itemToAdd.transform.position, 0f));
                }

                OptionsManager.Instance.SetOptionArea1("Items", items.Length);
                return true;
            }
        }
        return false;
    }

    IEnumerator SpawnNewItem(GameObject copy, Vector3 position, float time)
    {
        GameObject newItem;
        yield return new WaitForSeconds(time);
        newItem = Instantiate(copy, position, Quaternion.identity);
        newItem.SetActive(true);
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
    //Returnar hur många av ett vist item man har
    public int NumberOfSpecificItem(Item item)
    {
        int n = 0;
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null && items[i].GetComponent<OB_Item>().GetItemType() == item)
            {
                n++;
            }
        }
        return n;
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
    //Tar bort flera items av en viss sort
    public void RemoveItems(Item itemToRemove, int amount)
    {
        int n = 0;
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null && items[i].GetComponent<OB_Item>().GetItemType() == itemToRemove && n < amount)
            {
                items[i] = null;
                itemImages[i].sprite = null;
                itemImages[i].gameObject.SetActive(false);
                n++;
            }
        }
    }

    List<GameObject> itemsToGive;
    GameObject requester;
    bool givingItems;
    int maximumItems;
    List<Item> reqItems = new List<Item>();
    public void RequestItems(GameObject inRequester, Item[] inItems, int maxNumberOfItems)
    {
        if (!givingItems)
        {
            requester = inRequester;
            maximumItems = maxNumberOfItems;
            reqItems.AddRange(inItems);
            itemsToGive = new List<GameObject>();
            givingItems = true;
            Debug.Log("Giving Items");
            gameObject.GetComponent<CH_PlayerMovement>().SetStop(true);
        }
    }

    public void GiveItems()
    {
        requester.GetComponent<CH_RequestItems>().GiveItems(itemsToGive);
        requester = null;
        gameObject.GetComponent<CH_PlayerMovement>().SetStop(false);
        itemsToGive.Clear();
    }
}
