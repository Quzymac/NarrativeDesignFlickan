using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class BirdSounds : MonoBehaviour {

    [SerializeField]
    AudioClip[] Bird = new AudioClip[9];
    AudioSource birdSounds;

    private void Start()
    {
        birdSounds = gameObject.GetComponent<AudioSource>();
        birdSounds.Play();
    }
}
