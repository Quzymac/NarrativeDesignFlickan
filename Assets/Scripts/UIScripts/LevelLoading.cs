using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoading : MonoBehaviour {

    public static LevelLoading SceneSwitching;
    int buildIndex;
    private bool intro = false;
    public bool Intro { get { return intro; } set { intro = value; } }
    UI_FadingEffect fadeCanvas;

    private void Update()
    {
        
    }

    private void Awake()
    {
        SceneSwitching = this;
        fadeCanvas = FindObjectOfType<UI_FadingEffect>();
    }

    public void SceneSwitch(int _buildIndex)
    {
        buildIndex = _buildIndex;
        fadeCanvas.ActivateFading();
        if (buildIndex != 1)
        {
            StartCoroutine(SceneSwitchFadeTimer());
        }
        else
            intro = true;
    }

    public IEnumerator SceneSwitchFadeTimer()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(buildIndex);
    }

    public void QuitGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Application.Quit();
    }
}
