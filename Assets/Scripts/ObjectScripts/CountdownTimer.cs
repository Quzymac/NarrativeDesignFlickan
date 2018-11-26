﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CountdownTimer : MonoBehaviour {


    [SerializeField] float countDownTime = 3f;
    [SerializeField] string CountdownFinishedText;
    [SerializeField] UnityEvent TimedEvent;

    [SerializeField] GameObject countdownCanvas;
    [SerializeField] Text countDownText;
    float timer;
    int timeDisplayed;
    bool timerActive;
    
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

            //Displays text set in inspector and calls selected method also set in inspector when timer reaches 0.
            if (timer < 0f)
            {
                countDownText.text = CountdownFinishedText;
                TimedEvent.Invoke();

                if(timer < -1.5f)
                {
                    timerActive = false;
                    countdownCanvas.SetActive(false);
                }
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
