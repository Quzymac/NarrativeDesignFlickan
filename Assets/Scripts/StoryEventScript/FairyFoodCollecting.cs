using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyFoodCollecting : MonoBehaviour {

    [SerializeField] GameObject player;
    [SerializeField] Canvas countdownCanvas; //UI_Canvas
    [SerializeField] Transform fairyGameOverPos;
    [SerializeField] GameObject scriptManager;
    [SerializeField] GameObject fadeInCanvas;
    bool fairyAreFollowing = false;

    bool _b3MiniGameActive = true;
    public bool B3MiniGameActive { get { return _b3MiniGameActive; } set { _b3MiniGameActive = value; } }


    void StartGame ()
    {
        countdownCanvas.GetComponent<CountdownTimer>().StartTimer();
        GetComponent<FairyFollowingPlayer>().FairyFollowToggle(true);
        B3MiniGameActive = true;

    }

    public void GameTimerFinished()
    {
        B3MiniGameActive = false;
        GetComponent<FairyFollowingPlayer>().FairyFollowToggle(false);
        StartCoroutine(TeleportToFairies());

    }

    IEnumerator TeleportToFairies()
    {
        //Fade WIP
        //scriptManager.GetComponent<UI_FadingEffect>().ActivateFading();
        yield return new WaitForSeconds(1);
        //player.GetComponent<CH_PlayerMovement>().SetStop(true); //to force player to talk to fairy after collecting
        player.transform.position = fairyGameOverPos.position;
        yield return new WaitForSeconds(0.8f);
        //fadeInCanvas.SetActive(true);
        yield return new WaitForSeconds(1.5f);

    }

    void Update () {

        if (Input.GetKeyDown(KeyCode.P)){
            StartGame();
        }
        
    }
}
