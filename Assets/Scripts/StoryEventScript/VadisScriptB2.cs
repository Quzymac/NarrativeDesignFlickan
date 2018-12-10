using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Skriptet ska ligga på en trigger collider som täcker ån
// Property of John Martín
public class VadisScriptB2 : MonoBehaviour {
    
    bool recivedItemRecently = false;
    static bool minigameActive = false;
    CH_PlayerMovement player;
    [Header("Dra in VadisCharacter")]
    [SerializeField]
    GameObject vadis;

    private void Start()
    {
        player = FindObjectOfType<CH_PlayerMovement>();
    }

    private void OnEnable()
    {
        OptionsManager.NewChoice += ActivateMinigame;
    }

    private void OnDestroy()
    {
        OptionsManager.NewChoice -= ActivateMinigame;
    }

    private void ActivateMinigame(object sender, OptionsEventArgs e)
    {
        if (e.Option == "B2_Vadis_1")
        {
            minigameActive = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OB_Item item = other.GetComponent<OB_Item>();
        if (item == null)
            item = other.GetComponentInParent<OB_Item>();
        if(item != null)
        {
            foreach (Collider c in other.GetComponents<Collider>())
            {
                if (!c.isTrigger)
                {
                    c.enabled = false;
                    StartCoroutine(EnableCollider(c));
                }
            }

            if (!recivedItemRecently && minigameActive)
            {
                player.SetStop(true);
                recivedItemRecently = true;

                if (other.GetComponent<OB_Item>().GetItemType() == Item.Birch_polypore)
                {
                    StartCoroutine(BirchPolyPore());
                }
                else if (other.GetComponent<OB_Item>().GetItemType() == Item.Pine_cone)
                {
                    minigameActive = false;
                    StartCoroutine(PineCone());
                }
                else if (other.GetComponent<OB_Item>().GetItemType() == Item.Fir_cone)
                {
                    StartCoroutine(FirCone());
                }
                else
                {
                    StartCoroutine(OtherThings());
                }
            }
        }
    }

    IEnumerator EnableCollider(Collider collider)
    {
        yield return new WaitForSeconds(1f);
        collider.enabled = true;
        collider.gameObject.SetActive(false);
    }

    IEnumerator BirchPolyPore()
    {
        UI_DialogueController.Instance.DisplayMessage("Vådis", "Svamp från björk?");
        yield return new WaitForSeconds(2f);
        UI_DialogueController.Instance.DisplayMessage("Vådis", "Tyra vet väl redan att Trollmor Saga tycker att björkar har jättefula fläckar!");
        yield return new WaitForSeconds(5f);
        UI_DialogueController.Instance.DisplayMessage("Tyra", "Det visste jag väl redan. Den råkade jag bara knuffa i!");
        yield return new WaitForSeconds(4f);
        UI_DialogueController.Instance.Closemessage();
        recivedItemRecently = false;
        player.SetStop(false);
    }

    IEnumerator FirCone()
    {
        UI_DialogueController.Instance.DisplayMessage("Vådis", "Kotte från gran?");
        yield return new WaitForSeconds(2f);
        UI_DialogueController.Instance.DisplayMessage("Vådis", "Tyra vet väl redan att Trollmor Saga tycker att granar att alldeles för sköra!");
        yield return new WaitForSeconds(5f);
        UI_DialogueController.Instance.DisplayMessage("Tyra", "Det visste jag väl redan. Den råkade jag bara knuffa i!");
        yield return new WaitForSeconds(4f);
        UI_DialogueController.Instance.Closemessage();
        recivedItemRecently = false;
        player.SetStop(false);
    }

    IEnumerator OtherThings()
    {
        UI_DialogueController.Instance.DisplayMessage("Vådis", "Buuuu!");
        yield return new WaitForSeconds(2f);
        UI_DialogueController.Instance.DisplayMessage("Vådis", "Varför gav du mig en sån? Tramsas Tyra med Vådis?");
        yield return new WaitForSeconds(5f);
        UI_DialogueController.Instance.DisplayMessage("Tyra", "Ta det lugnt. Det var ju bara på skoj!");
        yield return new WaitForSeconds(4f);
        UI_DialogueController.Instance.Closemessage();
        recivedItemRecently = false;
        player.SetStop(false);
    }

    IEnumerator PineCone()
    {
        UI_DialogueController.Instance.DisplayMessage("Vådis", "Kotte från tall?");
        yield return new WaitForSeconds(2f);
        UI_DialogueController.Instance.DisplayMessage("Vådis", "Jaaa! Trollmor Saga säger ju jämt att tallar påminner om trollhemmet bland bergen!");
        yield return new WaitForSeconds(5f);
        UI_DialogueController.Instance.DisplayMessage("Tyra", "Nu vet Vådis att Tyra och Trollmor Saga verkligen är kompisar. Då är Tyra och Vådis också kompisar!");
        yield return new WaitForSeconds(5f);
        UI_DialogueController.Instance.Closemessage();
        recivedItemRecently = false;
        vadis.SetActive(false);
        player.SetStop(false);
    }
}
