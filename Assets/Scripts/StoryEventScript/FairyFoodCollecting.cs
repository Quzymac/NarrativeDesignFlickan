using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyFoodCollecting : MonoBehaviour {

    [SerializeField] GameObject player;
    [SerializeField] Canvas countdownCanvas; //UI_Canvas
    [SerializeField] Transform fairyGameOverPos;
    [SerializeField] GameObject scriptManager;
    [SerializeField] GameObject fadeInCanvas;
    


    void StartGame ()
    {
        countdownCanvas.GetComponent<CountdownTimer>().StartTimer();
        FairyFollow(true);
        Debug.Log("s");

    }

    public void GameTimerFinished()
    {
        FairyFollow(false);
        StartCoroutine(TeleportToFairies());
        Debug.Log("f");
    }

    IEnumerator TeleportToFairies()
    {
        //Fade WIP
        scriptManager.GetComponent<UI_FadingEffect>().ActivateFading();
        yield return new WaitForSeconds(1);
        player.transform.position = fairyGameOverPos.position;
        yield return new WaitForSeconds(0.8f);
        fadeInCanvas.SetActive(true);
        yield return new WaitForSeconds(1.5f);

    }
    void FairyFollow(bool following)
    {
        if (following)
        {
            //follow player
        }
        else
        {
            //stop following player
        }
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P)){
            StartGame();
        }
	}
}
