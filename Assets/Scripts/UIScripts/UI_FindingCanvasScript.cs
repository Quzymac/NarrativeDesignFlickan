using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FindingCanvasScript : MonoBehaviour {

    [SerializeField]
    GameObject fadeInCanvas;
    float deleteTime = 2f;

    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("FadeInCanvas") == false)
        {
            fadeInCanvas.SetActive(true);
        }
    }

    //IEnumerator

    private void Update()
    {
        deleteTime -= Time.deltaTime;
        if (deleteTime <= 0)
        {
            fadeInCanvas.SetActive(false);
        }
    }
}
