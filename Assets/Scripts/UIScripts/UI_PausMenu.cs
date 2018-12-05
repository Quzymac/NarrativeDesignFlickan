using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PausMenu : MonoBehaviour {
    public static bool GameIsPaused = false;
    
    [SerializeField]
    private GameObject pauseMenuUI;

    [SerializeField]
    private GameObject howToPlayUI;

    [SerializeField]
    private GameObject inventoryUI;

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    public void Resume()
    {
        FindObjectOfType<CH_PlayerCamera>().FreeMouse(false);
        pauseMenuUI.SetActive(false);
        inventoryUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void HowToPlayActivate()
    {
        howToPlayUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void HowToPlayDeactivate()
    {
        pauseMenuUI.SetActive(true);
        howToPlayUI.SetActive(false);
    }

    void Pause()
    {
        FindObjectOfType<CH_PlayerCamera>().FreeMouse(true);
        inventoryUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void TimeScaleOne()
    {
        Time.timeScale = 1f;
    }
}
