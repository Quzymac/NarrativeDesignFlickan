using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CountdownTimer : MonoBehaviour {


    [SerializeField] float countdownStart = 3f;
    [SerializeField] string CountdownFinishedText;
    [SerializeField] UnityEvent TimedEvent;

    [SerializeField] GameObject countdownCanvas;
    [SerializeField] Text countDownText;
    float timer;
    int timeDisplayed;
    bool timerActive;
    

    public void StartTimer()
    {
        countdownCanvas.SetActive(true);
        timer = countdownStart;
        timerActive = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartTimer();
        }

        if (timerActive)
        {
            timer -= Time.deltaTime;

            if (timer < 0f)
            {
                countDownText.text = CountdownFinishedText;
                TimedEvent.Invoke();

                if(timer < -1.5f)
                {
                    timerActive = false;
                    countDownText.text = "";
                    countdownCanvas.SetActive(false);
                }
            }

            if (timer > 0f)
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
