using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    [SerializeField]
    AudioSource gameMusic;
    float thisMusic;
    //bool hasExit = false;

    private void OnTriggerExit(Collider other)
    {
        thisMusic = gameMusic.volume;
        StartCoroutine(FadeOut(gameMusic, 4));
        //hasExit = true;     
    }
  
    private void OnTriggerEnter(Collider other)
    {
        /*if (hasExit == true)
        {
            StartCoroutine(FadeIn(gameMusic, 3));
        }
        hasExit = false;*/
        StartCoroutine(FadeIn(gameMusic, 3));
    }
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeOutTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeOutTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public static IEnumerator FadeIn(AudioSource audioSource, float FadeInTime)
    {
        float startVolume = audioSource.volume;      
        audioSource.volume += startVolume * Time.deltaTime / FadeInTime;
        yield return null;        
        audioSource.Play();
    }

}
