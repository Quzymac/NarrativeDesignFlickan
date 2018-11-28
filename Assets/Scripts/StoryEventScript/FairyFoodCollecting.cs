using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FairyFoodCollecting : MonoBehaviour
{

    GameObject player;
    UI_DialogueController dialogController;
    CountdownTimer countdownTimer;
    [SerializeField] Transform endGamePos;
    [SerializeField] GameObject fadeInCanvas;
    GameObject fairyChest;
    GameObject fairy;
    public Transform GetFairyChest()
    {
        return fairyChest.transform;
    }

    bool fairyAreFollowing = false;

    [SerializeField] float dialogMessageDuration = 5f;
    int dialogTier;
    Item dialogItem;

    bool _b3MiniGameActive = false;
    public bool B3MiniGameActive { get { return _b3MiniGameActive; } set { _b3MiniGameActive = value; } }

    private void Awake()
    {
        player = FindObjectOfType<CH_PlayerMovement>().gameObject;
        fairy = FindObjectOfType<FairyFollowingPlayer>().gameObject;
        fairyChest = FindObjectOfType<OB_TransportTarget>().gameObject;
        dialogController = FindObjectOfType<UI_DialogueController>();
        countdownTimer = FindObjectOfType<CountdownTimer>();
    }
    private void OnEnable()
    {
        OptionsManager.NewChoice += StartGame;//subscribe 
    }

    void StartGame(object sender, OptionsEventArgs e)
    {
        if (e.Option == "B3_Alvor_1" && e.Value == 2)
        {
            countdownTimer.StartTimer();
            fairy.GetComponent<FairyFollowingPlayer>().FairyFollowToggle(true);
            B3MiniGameActive = true;
            OptionsManager.NewChoice -= StartGame;//unsubscribe
        }
    }

    public void GameTimerFinished() //Teleports fairy + player back and sets variables for items in chest
    {
        B3MiniGameActive = false;
        StartCoroutine(TeleportToFairies());
        dialogItem = fairyChest.GetComponent<OB_TransportTarget>().GetDialogItem();
        dialogTier = fairyChest.GetComponent<OB_TransportTarget>().GetItemsTier();
        StartCoroutine(EndingDialog());
    }

    IEnumerator EndingDialog()
    {
        string item = "";

        yield return new WaitForSeconds(dialogMessageDuration * 0.5f);

        if (dialogTier == 0)
        {
            dialogController.DisplayMessage("Älvor", "Du gör narr av oss, flicksnärta.", dialogMessageDuration);
            yield return new WaitForSeconds(dialogMessageDuration);
            dialogController.DisplayMessage("Älvor", "Vi älvor glömmer aldrig bort sådan hänsynslöshet.", dialogMessageDuration);
            yield return new WaitForSeconds(dialogMessageDuration);
            GetComponent<FairyBarrier>().Pushplayer();
            //OptionsManager.Instance.SetOptionArea1("B3_Alvor_1", 1); //pushplayeraway
            yield break;
        }

        dialogController.DisplayMessage("Älvor", "Intressant...", dialogMessageDuration);
        yield return new WaitForSeconds(dialogMessageDuration);
        switch (dialogItem)
        {
            case Item.Apple:
                item = "För oss älvor representerar äpplen visdom och gåtfullhet.";
                break;
            case Item.Blueberry:
                item = "För oss älvor representerar blåbär rikedom och värdighet.";
                break;
            case Item.Lingonberry:
                item = "För oss älvor representerar lingon renhet och passion.";
                break;
            case Item.Chanterelle:
                item = "För oss älvor representerar kantareller lycka och hederlighet.";
                break;
            case Item.Falukorv:
                item = "För oss älvor är människans tjusning i kött från elementära livsformer... svårbegriplig";
                break;
            case Item.Birch_polypore:
                item = "För oss älvor representerar björktickor ståndaktighet och tillit";
                break;
            case Item.Fly_agaric:
                item = "För oss älvor representerar röda flugsvampar egoism och melodrama.";
                break;
            case Item.Gulfotshatta:
                item = "För oss älvor representerar gulfotshättor vänskap och samförstånd.";
                break;
            case Item.Pine_cone:
                item = "För oss älvor representerar tallkottar feghet och enfald.";
                break;
            case Item.Fir_cone:
                item = "För oss älvor representerar grankottar okunnighet och apati.";
                break;
            default:
                break;
        }
        dialogController.DisplayMessage("Älvor", item, dialogMessageDuration);
        yield return new WaitForSeconds(dialogMessageDuration);

        switch (dialogTier)
        {
            case 1:
                dialogController.DisplayMessage("Älvor", "[TIER 1] PLACEHOLDER", dialogMessageDuration);
                yield return new WaitForSeconds(dialogMessageDuration);
                // Vidare till C1
                break;
            case 2:
                dialogController.DisplayMessage("Älvor", "[TIER 2] PLACEHOLDER", dialogMessageDuration);
                yield return new WaitForSeconds(dialogMessageDuration);
                // Vidare till C3
                break;
            case 3:
                dialogController.DisplayMessage("Älvor", "Du gör narr av oss, flicksnärta.", dialogMessageDuration);
                yield return new WaitForSeconds(dialogMessageDuration);
                dialogController.DisplayMessage("Älvor", "Vi älvor glömmer aldrig bort sådan hänsynslöshet.", dialogMessageDuration);
                yield return new WaitForSeconds(dialogMessageDuration);
                GetComponent<FairyBarrier>().Pushplayer();
                //OptionsManager.Instance.SetOptionArea1("B3_Alvor_1", 1); //pushplayeraway
                break;
            default:
                GetComponent<FairyBarrier>().Pushplayer();
                //OptionsManager.Instance.SetOptionArea1("B3_Alvor_1", 1);
                break;
        }
    }

    IEnumerator TeleportToFairies()
    {
        //Fade WIP
        //scriptManager.GetComponent<UI_FadingEffect>().ActivateFading();
        yield return new WaitForSeconds(1);
        player.GetComponent<CH_PlayerMovement>().SetStop(true); //to force player to talk to fairy after collecting
        player.transform.position = endGamePos.position;
        fairy.GetComponent<FairyFollowingPlayer>().FairyFollowToggle(false);
        yield return new WaitForSeconds(0.8f);
        //fadeInCanvas.SetActive(true);
        yield return new WaitForSeconds(1.5f);

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            countdownTimer.StartTimer();
            fairy.GetComponent<FairyFollowingPlayer>().FairyFollowToggle(true);
            B3MiniGameActive = true;
        }

    }
}
