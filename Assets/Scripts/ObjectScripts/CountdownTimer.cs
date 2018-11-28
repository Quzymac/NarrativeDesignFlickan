using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CountdownTimer : MonoBehaviour
{


    [SerializeField] float countDownTime = 30f;
    [SerializeField] string CountdownFinishedText;
    [SerializeField] GameObject countdownCanvas;
    [SerializeField] Text countDownText;
    float timer;
    int timeDisplayed;
    bool timerActive;
    bool halfTimePassed = false;

    //Call this method to start timer. 
    public void StartTimer()
    {
        countdownCanvas.SetActive(true);
        timer = countDownTime;
        timerActive = true;
    }

    void Update()
    {

        if (timerActive)
        {
            //counts down if timer is active
            timer -= Time.deltaTime;

            if (timer < (countDownTime * 0.5f) && !halfTimePassed)
            {
                halfTimePassed = true;
                FindObjectOfType<UI_DialogueController>().DisplayMessage("Älvor", "Halva tiden för ceremonin har löpt.", 4);
            }

            //Displays text set in inspector and calls selected method also set in inspector when timer reaches 0.
            if (timer < 0f)
            {
                FindObjectOfType<UI_DialogueController>().DisplayMessage("Älvor", "Ceremonin är fullbordad.", 4);
                FindObjectOfType<FairyFoodCollecting>().GameTimerFinished();
                timerActive = false;
                countdownCanvas.SetActive(false);
            }

            //displays seconds left on timer
            else if (timer > 0f)
            {
                if (Mathf.RoundToInt(timer + 0.5f) != timeDisplayed)
                {
                    timeDisplayed = Mathf.RoundToInt(timer + 0.5f);
                    countDownText.text = timeDisplayed.ToString();
                }
            }
        }
    }
}
