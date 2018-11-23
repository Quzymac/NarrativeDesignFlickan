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
    

    void StartGame ()
    {
        countdownCanvas.GetComponent<CountdownTimer>().StartTimer();
        GetComponent<FairyFollowingPlayer>().FairyFollowToggle(true);
        Debug.Log("s");

    }

    public void GameTimerFinished()
    {
        GetComponent<FairyFollowingPlayer>().FairyFollowToggle(false);
        StartCoroutine(TeleportToFairies());
        Debug.Log("f");
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
