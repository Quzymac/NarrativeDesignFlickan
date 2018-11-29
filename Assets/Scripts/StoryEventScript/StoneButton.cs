using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneButton : MonoBehaviour {

    [SerializeField]
    private GameObject korok;
    private bool complete;

    public void Complete()
    {
        if (!complete)
        {
            korok.SetActive(true);
            complete = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "KorokStone")
        {
            Complete();
        }
    }
}
