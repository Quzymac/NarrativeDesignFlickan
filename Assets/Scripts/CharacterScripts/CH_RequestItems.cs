using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CH_RequestItems : OB_Interactable {

    [SerializeField]
    UnityEvent character;
    [SerializeField]
    Item[] itemsWanted;
    [SerializeField]
    int maxNumberOfItemsToBeGiven;

    private void OnTriggerEnter(Collider other)
    {
        OnEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnExit(other);
    }

    public void GiveItems(List<GameObject> inItems)//GameObject[] inItems
    {
        items.AddRange(inItems);
        character.Invoke();
        character = null;
    }

    public override void Activate(GameObject player)
    {
        if (character != null)
        {
            player.GetComponent<CH_Inventory>().RequestItems(gameObject, itemsWanted, maxNumberOfItemsToBeGiven);
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
            StartCoroutine(SendMessage("Troll", "Usch, va salt dehär va! Magen min känner sig inte gla'!",false));
        }
        else if (falukorv == true && berry == true && mushroom == false)
        {
            StartCoroutine(SendMessage("Troll", "De va alldeles lagom gött, men mera mat hade också vart väl mött!",false));
        }
        else if (falukorv  == true && berry == true && mushroom == true)
        {
            StartCoroutine(SendMessage("Troll", "Det va det bäste ja äti på ett tag, låt mej hjälp dej ned. Om du är vid sjön senare idag, se till att lämna Näcken ifred.",true));
        }
        else if (falukorv == true && berry == false && mushroom == true)
        {
            StartCoroutine(SendMessage("Troll", "Suck, det här va väl rätt smaskigt, men annat mums som drar på kinderna hade inte vart taskigt.",false));
        }
        else if (falukorv == false && berry == true && mushroom == true)
        {
            StartCoroutine(SendMessage("Troll", "Det här va gott, men nåt smakrikare hade suttit bra. Om du inte har brått' vill jag mer mat ha.",false));
        }
        else if (falukorv == false && berry == true && mushroom == false)
        {
            StartCoroutine(SendMessage("Troll", "Det här va delikat och magen min känns rätt, men det ä' väl klart att mer krävs för att ja ska bli mätt.",false));
        }
        else if (falukorv == false && berry == false && mushroom == true)
        {
            StartCoroutine(SendMessage("Troll", "Det här ä' på tok för lite mat för mej, om du ä' klok så hämtar du nån mer grej.",false));
        }
        else
        {
            StartCoroutine(SendMessage("Troll", "Jag tänker inte hjälpa dig",false));
        }
    }

    IEnumerator SendMessage(string character, string text, bool extraText)
    {
        UI_DialogueController.Instance.DisplayMessage(character, text);
        yield return new WaitForSecondsRealtime(5);
        UI_DialogueController.Instance.Closemessage();
        if(extraText)
        {
            StartCoroutine(SendMessage("Troll", "Näcken spelar violin, så skärp örona dina.Och om du än har energin, kolla under alla stenar fina!",false));
        }
    } 
}
