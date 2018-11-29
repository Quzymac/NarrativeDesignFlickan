using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_FadingEffect : MonoBehaviour {

    [SerializeField] Image fadeCanvas;

    private void Start()
    {
        fadeCanvas.color = new Color(0, 0, 0, 1);
        DeactivateFading();
    }

    public void ActivateFading()
    {
        StartCoroutine(FadeImage(false));
    }

    public void DeactivateFading()
    {
        StartCoroutine(FadeImage(true));
    }
    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                fadeCanvas.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                fadeCanvas.color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
    }
}
