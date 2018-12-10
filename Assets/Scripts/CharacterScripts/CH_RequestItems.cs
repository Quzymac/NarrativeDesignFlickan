using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CH_RequestItems : OB_Interactable {

    [SerializeField]
    UnityEvent character;
    [SerializeField]
    Item[] itemsWanted;
    [SerializeField]
    int maxNumberOfItemsToBeGiven;
    List<GameObject> items = new List<GameObject>();
    int givingTomteItems = 1;
    int givingTrollItems = 1;
    int refusedTroll;
    SE_SopaScript sopaScript;
    CH_Interact tempPlayer;
    string characterName;
    bool needItems = true;

    private void OnEnable()
    {
        OptionsManager.NewChoice += Compare; 
    }

    private void OnSceneUnloaded(Scene current)
    {
        OptionsManager.NewChoice -= Compare;
    }

    private void Compare(object sender, OptionsEventArgs e)
    {
        if (e.Option == "B1_Alf_1" && character.GetPersistentMethodName(0) == "Tomte")
        {
            if (e.Value == givingTomteItems)
            {
                enabled = true;
                GetComponent<OB_Dialogue>().enabled = false;
            }
        }

        if (e.Option == "B1_Sopa_1" && character.GetPersistentMethodName(0) == "Troll")
        {
            if (e.Value == givingTrollItems)
            {
                enabled = true;
                tempPlayer.RemoveInteractable(GetComponent<OB_Dialogue>());
                tempPlayer.AddInteractable(this);
                GetComponent<OB_Dialogue>().enabled = false;
            }
        }

    }

    private void Start()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        enabled = false;
        if (character.GetPersistentMethodName(0) == "Troll")
        {
            sopaScript = GetComponent<SE_SopaScript>();
            tempPlayer = FindObjectOfType<CH_Interact>();
            characterName = "Trollet";
        }

        if(character.GetPersistentMethodName(0) == "Tomte")
        {
            characterName = "Tomten";
        }
    }

    public string GetCharacterName()
    {
        return characterName;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enabled)
        {
            OnEnter(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        OnExit(other);
    }

    public void GiveItems(List<GameObject> inItems)
    {
        items.AddRange(inItems);
        character.Invoke();
    }

    public override void Activate(GameObject player)
    {
        if(needItems && character.GetPersistentEventCount() > 0)
        {
            player.GetComponent<CH_Inventory>().RequestItems(gameObject, itemsWanted, maxNumberOfItemsToBeGiven);
        }
    }

    public void Tomte()
    {
        foreach (GameObject item in items)
        {
            if (item.GetComponent<OB_Item>().GetItemType() == Item.Apple)
            {
                StartCoroutine(SendMessage("Tomte", "Man tackar, man tackar, Jag ska nog se till att du har lite tur längre fram på ditt äventyr"));
                needItems = false;
            }
            else if (item.GetComponent<OB_Item>().GetItemType() == Item.Blueberry || item.GetComponent<OB_Item>().GetItemType() == Item.Lingonberry)
            {
                StartCoroutine(SendMessage("Tomte", "Tackar så himle mycke, flicka lilla. Nån gång när du behöve hjälp finns ja där."));
                needItems = false;
            }
            else if (item.GetComponent<OB_Item>().GetItemType() == Item.Chanterelle)
            {
                StartCoroutine(SendMessage("Tomte", "Svamp ä fullkomligt vidrigt. Hitte du icke nå bättre?"));
                needItems = false;
            }
            else
            {
                UI_DialogueController.Instance.DisplayMessage("Tomte", "Det var inte så snällt av dig, hoppas trollmamman tar dej som hon tog männskobarnet som sprang förbi nyss.", 5f);
            }
        }
    }

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
            StartCoroutine(SendMessage("Troll", "Usch, va salt dehär va! Magen min känner sig inte gla'!"));
            needItems = false;
            Invoke("CompletedPreAlpha", 7.5f);
        }
        else if (falukorv == true && berry == true && mushroom == false)
        {
            StartCoroutine(SendMessage("Troll", "De va alldeles lagom gött, men mera mat hade också vart väl mött!"));
            needItems = false;
            Invoke("CompletedPreAlpha", 7.5f);
        }
        else if (falukorv  == true && berry == true && mushroom == true)
        {
            StartCoroutine(SendMessage("Troll", "Det va det bäste ja äti på ett tag, låt mej hjälp dej ned. Om du är vid sjön senare idag, se till att lämna Näcken ifred.",true));
            needItems = false;
            Invoke("CompletedPreAlpha", 15);
        }
        else if (falukorv == true && berry == false && mushroom == true)
        {
            StartCoroutine(SendMessage("Troll", "Suck, det här va väl rätt smaskigt, men annat mums som drar på kinderna hade inte vart taskigt."));
            needItems = false;
            Invoke("CompletedPreAlpha", 7.5f);
        }
        else if (falukorv == false && berry == true && mushroom == true)
        {
            StartCoroutine(SendMessage("Troll", "Det här va gott, men nåt smakrikare hade suttit bra. Om du inte har brått' vill jag mer mat ha."));
        }
        else if (falukorv == false && berry == true && mushroom == false)
        {
            StartCoroutine(SendMessage("Troll", "Det här va delikat och magen min känns rätt, men det ä' väl klart att mer krävs för att ja ska bli mätt."));
        }
        else if (falukorv == false && berry == false && mushroom == true)
        {
            StartCoroutine(SendMessage("Troll", "Det här ä' på tok för lite mat för mej, om du ä' klok så hämtar du nån mer grej."));
        }
        else
        {
            StartCoroutine(SendMessage("Troll", "Fy va taskigt, jag tänker inte hjälpa dig"));
            refusedTroll++;
            if(refusedTroll == 5)
            {
                StartCoroutine(sopaScript.PushPlayer());
                StartCoroutine(SendMessage("Troll", "Nu har du vari på tok för taskig, jag tänker inte prata mer me dig"));
                needItems = false;
            }
        }
    }
    private void CompletedPreAlpha()
    {
        SceneManager.LoadScene(2);
    }

    IEnumerator SendMessage(string character, string text, bool extraText = false)
    {
        UI_DialogueController.Instance.DisplayMessage(character, text);
        yield return new WaitForSecondsRealtime(7.5f);
        UI_DialogueController.Instance.Closemessage();
        if(extraText)
        {
            StartCoroutine(SendMessage("Troll", "Näcken spelar violin, så skärp örona dina. Och om du än har energin, kolla under alla stenar fina!"));
        }
    } 
}
