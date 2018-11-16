using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PausMenu : MonoBehaviour {
    public static bool GameIsPaused = false;
    
    [SerializeField]
    private GameObject pauseMenuUI;

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
        pauseMenuUI.SetActive(false);
        inventoryUI.SetActive(true);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
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
