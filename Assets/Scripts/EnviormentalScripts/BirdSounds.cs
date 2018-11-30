using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BirdSounds : MonoBehaviour
{

    [SerializeField] List<AudioClip> birdSounds = new List<AudioClip>();
    [SerializeField] AudioSource audioSource;
    SphereCollider soundRange;

    private void Start()
    {
        StartCoroutine(BirdsSoundsCO());
        soundRange = GetComponent<SphereCollider>();
        soundRange.radius = audioSource.maxDistance;
    }

    IEnumerator BirdsSoundsCO()
    {
        if (!isOutside)
        {
            int randomNumber = Random.Range(0, birdSounds.Count - 1);
            audioSource.clip = birdSounds[randomNumber];
            print(randomNumber);
            audioSource.volume = Random.Range(0.03f, 0.1f);
            audioSource.Play();
            yield return new WaitForSeconds(birdSounds[randomNumber].length + Random.Range(1, 5));
            StartCoroutine(BirdsSoundsCO());
        }
    }
    bool isOutside;
    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            isOutside = true;
            StartCoroutine(BirdLower());
        }
    }
    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            isOutside = false;
            StartCoroutine(BirdsSoundsCO());
        }
    }
    IEnumerator BirdLower()
    {
        for (int i = 0; i < 20; i++)
        {
            audioSource.volume -= 0.004f;
            yield return new WaitForSeconds(0.01f);
        }
        audioSource.volume = 0;
    }
}
