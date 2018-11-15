using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoading : MonoBehaviour {

    public static LevelLoading SceneSwitching;

    private void Awake()
    {
        SceneSwitching = this;
    }

    public void SceneSwitch(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
