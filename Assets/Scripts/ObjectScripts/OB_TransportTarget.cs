using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Placeras på det objekt som ska få ett annat objekt via transport
public class OB_TransportTarget : MonoBehaviour {

    [SerializeField] List<GameObject> transportedItems;
    [SerializeField] int itemsTier = 0;
    [SerializeField] Item dialogItem;
    int[] items = new int[10];

    public Item GetDialogItem()
    {
        Calculate();
        return dialogItem;
    }
    public int GetItemsTier()
    {
        Calculate();
        return itemsTier;
    }

    // Räknar ut vilket item det finns flest av och vilken tier det finns flest av
    void Calculate()
    {
        
        foreach (var item in transportedItems)
        {
            items[(int)item.GetComponent<OB_Item>().GetItemType()]++;
        }

        itemsTier = CalculateTier();
        dialogItem = CalculateItems();
    }

    int CalculateTier()
    {
        if(items[(int)Item.Falukorv] > 0) // falukorv = tier 3
        {
            return 3;
        }

        int tierOne = items[(int)Item.Apple] + items[(int)Item.Blueberry] + items[(int)Item.Lingonberry];
        int tierTwo = items[(int)Item.Chanterelle] + items[(int)Item.Birch_polypore] + items[(int)Item.Gulfotshatta];
        int tierThree = items[(int)Item.Fir_cone] + items[(int)Item.Pine_cone] + items[(int)Item.Fly_agaric];
        
        if (transportedItems.Count <= 3) //inte tillräckligt med items
        {
            return 3;
        }
        if (tierThree > tierTwo && tierThree > tierOne) // most of tier 3
        {
            return 3;
        }
        else if (tierTwo > tierOne && tierTwo >= tierThree) //most of tier 2
        {
            return 2;
        }
        else // most of tier 1
        {
            return 1;
        }
    }

    Item CalculateItems() // räknar ut vilket item det finns flest av
    {
        if(items[(int)Item.Falukorv] > 0)
        {
            return Item.Falukorv;
        }

        int maxValue = 0;
        int mostOfItem = 0;
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] >= maxValue)
            {
                if (items[i] == maxValue) // random om det finns lika många
                {
                    int random = (int)Random.Range(0f, 100f);
                    if (random % 2 == 0)
                    {
                        maxValue = items[i];
                        mostOfItem = i;
                    }
                }
                else
                {
                    maxValue = items[i];
                    mostOfItem = i;
                }
            }
        }
        return (Item)mostOfItem;
    }

    public bool SetObject(GameObject inObject)
    {
        if (inObject != null)
        {
            transportedItems.Add(inObject);

            Debug.Log("I have a " + inObject.name);
            return true;
        }
        else
        {
            return false;

        }
    }
}
//Item[] tierOneItems = new Item[] { Item.Apple, Item.Blueberry, Item.Lingonberry };
//Item[] tierTwoItems = new Item[] { Item.Chanterelle, Item.Birch_polypore, Item.Gulfotshatta };
//Item[] tierThreeItems = new Item[] { Item.Fir_cone, Item.Pine_cone, Item.Fly_agaric };

//switch (item.GetComponent<OB_Item>().GetItemType())
//{
//    case Item.Falukorv: // falukorv vinner alltid
//        mostOf = Item.Falukorv; 
//        return 3;
//    case Item.Apple:
//        apple++;
//        tierOne++;
//        break;
//    case Item.Blueberry:
//        blueBerry++;
//        tierOne++;
//        break;
//    case Item.Lingonberry:
//        lingonberry++;
//        tierOne++;
//        break;
//    case Item.Chanterelle:
//        chanterelle++;
//        tierTwo++;
//        break;
//    case Item.Birch_polypore:
//        birchPolypore++;
//        tierTwo++;
//        break;
//    case Item.Gulfotshatta:
//        gulfotshatta++;
//        tierTwo++;
//        break;
//    case Item.Fly_agaric:
//        flyAgaric++;
//        tierThree++;
//        break;
//    case Item.Pine_cone:
//        pineCone++;
//        tierThree++;
//        break;
//    case Item.Fir_cone:
//        firCone++;
//        tierThree++;
//        break;
//    default:
//        break;
//}