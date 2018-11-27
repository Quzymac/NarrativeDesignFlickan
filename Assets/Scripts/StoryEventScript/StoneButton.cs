using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneButton : MonoBehaviour {

    [SerializeField]
    private GameObject reward;
    private bool complete;

    public void Complete()
    {
        if (!complete)
        {
            Instantiate(reward, transform.position, Quaternion.identity);
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
