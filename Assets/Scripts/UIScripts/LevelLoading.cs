using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoading : MonoBehaviour {

    public static LevelLoading SceneSwitching;
    int buildIndex;
    UI_FadingEffect fadeCanvas;

    private void Awake()
    {
        SceneSwitching = this;
        fadeCanvas = FindObjectOfType<UI_FadingEffect>();
    }

    public void SceneSwitch(int _buildIndex)
    {
        buildIndex = _buildIndex;
        fadeCanvas.ActivateFading();
        StartCoroutine(SceneSwitchFadeTimer());
    }

    IEnumerator SceneSwitchFadeTimer()
    {
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
