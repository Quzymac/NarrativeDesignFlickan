using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FadingEffect : MonoBehaviour {

    public GameObject Fade;

	public void ActivateFading()
    {
        Fade.SetActive(true);
    }

    public void DeactivateFading()
    {
        Fade.SetActive(false);
    }

}
