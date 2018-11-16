using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoading : MonoBehaviour {

    public static LevelLoading SceneSwitching;
    int buildIndex;

    private void Awake()
    {
        SceneSwitching = this;
    }

    public void SceneSwitch(int _buildIndex)
    {
        buildIndex = _buildIndex;
        UI_FadingEffect.FadeActivation.ActivateFading();
        StartCoroutine(SceneSwitchFadeTimer());
    }

    IEnumerator SceneSwitchFadeTimer()
    {
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene(buildIndex);
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
