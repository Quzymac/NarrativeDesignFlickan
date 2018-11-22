using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CH_RequestItems : OB_Interactable {

    [SerializeField]
    UnityEvent character;
    [SerializeField]
    Item[] itemsWanted;

    private void OnTriggerEnter(Collider other)
    {
        OnEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnExit(other);
    }

    public void GiveItems(List<GameObject> inItems)
    {
        items.AddRange(inItems);
        character.Invoke();
        character = null;
    }

    public override void Activate(GameObject player)
    {
        if (character != null)
        {
            player.GetComponent<CH_Inventory>().RequestItems(gameObject, itemsWanted);
        }
    }

    public bool Tomte()
    {
        return true;
    }

    List<GameObject> items = new List<GameObject>();
    public void Troll()
    {
        bool falukorv = false;
        bool berry = false;
        bool mushroom = false;

        foreach (GameObject item in items)
        {
            if (item.GetComponent<OB_Item>().GetItemType() == Item.Falukorv)
            {
                falukorv = true;
            }
            else if (item.GetComponent<OB_Item>().GetItemType() == Item.Blueberry || item.GetComponent<OB_Item>().GetItemType() == Item.Lingonberry)
            {
                berry = true;
            }
            else if (item.GetComponent<OB_Item>().GetItemType() == Item.Chanterelle)
            {
                mushroom = true;
            }
        }

        if (falukorv == true && berry == false && mushroom == false)
        {
            Debug.Log("Usch va salt dehär va! magen min kommer vara arg senare!");
            //return true;
        }
        else if (falukorv == true && berry == true && mushroom == false)
        {
            //De va gött, men mera mat hade vart härligt!
            //return true;
        }
        else if (falukorv  == true && berry == true && mushroom == true)
        {
            //Det här va det bäste jag äti på länge, låt mig hjälp dig ned, när du kommer närmare vattnet så ska du se till att hålla dig borta från Näcken,
            //han spelar fiol så du vet! Du ska också se till att kolla under alla stenar som ser spännande ut!return true;
            //return true;
        }
        else if (falukorv == true && berry == false && mushroom == true)
        {
            //Suck, det här va smaskit, men något som drar på kinderna och något annat mums hade gjort de goare
            //return true;
        }
        else if (falukorv == false && berry == true && mushroom == true)
        {
            //Det här var gott, men det var något med mer smak som jag hade velat ha, spring bort och hämta något mer till mej.
            //return true;
        }
        else if (falukorv == false && berry == true && mushroom == false)
        {
            //Det här fick kinderna att dra iväg och var mums, men något mer krävs för att hålla mig mätt
            //return true;
        }
        else if (falukorv == false && berry == false && mushroom == true)
        {
            //Det här var alldeles för lite, Spring iväg och ge mig mer mat
            //return true;
        }
        else
        {
            Debug.Log(" Jag tänker inte hjälpa dig");
            //return false;
        }
    }
        
}
