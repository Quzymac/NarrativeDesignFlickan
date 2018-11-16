using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FadingEffect : MonoBehaviour {

    public GameObject Fade;
    public static UI_FadingEffect FadeActivation;
    private void Start()
    {
        FadeActivation = this;
    }
    public void ActivateFading()
    {
        Fade.SetActive(true);
    }

    public void DeactivateFading()
    {
        Fade.SetActive(false);
    }

    public GameObject Returnfade()
    {
        return Fade;
    }
}
