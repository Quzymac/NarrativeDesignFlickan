using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Placeras på det objekt som ska få ett annat objekt via transport
public class OB_TransportTarget : MonoBehaviour {

    [SerializeField] List<GameObject> transportedItems;
    GameObject transportedItem; //-----------

    [SerializeField] int itemsTier = 0;

    [SerializeField] Item mostOf;

    int[] items;


    int CalculateTier()
    {
        return 1; //REMOVE--+000000+---REMOVE--+000000+---REMOVE--+000000+---REMOVE--+000000+---REMOVE--+000000+---REMOVE--+000000+---REMOVE--+000000+---

        int numberOfItems = 0;

        //Tier1
        int apple = 0;
        int blueBerry = 0;
        int lingonberry = 0;
        //Tier2
        int chanterelle = 0;
        int birchPolypore = 0;
        int gulfotshatta = 0;

        //Tier3
        int flyAgaric = 0;
        int pineCone = 0;
        int firCone = 0;

        items = new int[9];

        foreach (var item in transportedItems)
        {

            items[(int)item.GetComponent<OB_Item>().GetItemType()]++;

            numberOfItems++;

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
        }


        int tierOne = items[(int)Item.Apple] + items[(int)Item.Blueberry] + items[(int)Item.Lingonberry];
        int tierTwo = items[(int)Item.Chanterelle] + items[(int)Item.Birch_polypore] + items[(int)Item.Gulfotshatta];
        int tierThree = items[(int)Item.Fir_cone] + items[(int)Item.Pine_cone] + items[(int)Item.Fly_agaric];

        int random3 = (int)Random.Range(0f, 3f);
        int random2 = (int)Random.Range(0f, 2f);


        if (transportedItems.Count <= 3) //inte tillräckligt med items
        {
            return 0;
        }

        Item[] tierOneItems = new Item[] { Item.Apple, Item.Blueberry, Item.Lingonberry };
        Item[] tierTwoItems = new Item[] { Item.Chanterelle, Item.Birch_polypore, Item.Gulfotshatta };
        Item[] tierThreeItems = new Item[] { Item.Fir_cone, Item.Pine_cone, Item.Fly_agaric };

        if (tierThree > tierTwo && tierThree > tierOne) // most of tier 3
        {
            int maxValue = 0;
            for (int i = 0; i < tierThreeItems.Length; i++)
            {


                //WIP

                //if (myFruits[i] > maxValue)
                //{
                //    maxValue = myFruits[i];
                //    maxIndex = i;
            }
        }
        else if (tierTwo > tierOne && tierTwo >= tierThree) //most of tier 2
        {
            //tierTwo
        }
        else // most of tier 1
        {
            //tierOne
        }
    }



    public bool SetObject(GameObject inObject)
    {
        if (inObject != null)
        {
            transportedItems.Add(inObject);

            //--------
            transportedItem = inObject;
            Debug.Log("I have a " + transportedItem.name);
            return true;
        }
        else
        {
            return false;

        }
    }
}
