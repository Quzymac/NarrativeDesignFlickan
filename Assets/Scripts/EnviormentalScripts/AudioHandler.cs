using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Beskrivning vilkét ljud som ska köras. Ska ha samma position som sitt ljud i sounds arrayen t.ex. Pickup ska ha position 0 i sounds
public enum Sounds {Pickup, DropDown, Throw, Eat}

public class AudioHandler : MonoBehaviour {

    //Objekter som ska spela ljud måste ha en audiosource utan play on awake och detta skript.
    AudioSource source;
    [SerializeField] //Lägg till olika ljud i inspektorn som andra skript ska kunna spela up.
    AudioClip[] sounds;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }


    public void PlaySound(Sounds sound)
    {
        source.clip = sounds[(int)sound];
        source.PlayOneShot(source.clip);
    }

}
