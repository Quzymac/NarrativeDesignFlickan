using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CH_TeleportPlayer : MonoBehaviour {

    [SerializeField] Transform position1;
    //[SerializeField] Transform position2;

    private void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.tag == "Player")
        {
            c.gameObject.transform.position = position1.position;
        }
    }
}