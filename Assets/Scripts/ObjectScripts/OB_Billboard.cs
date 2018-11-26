using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OB_Billboard : MonoBehaviour {

    GameObject player;
    Vector3 target;
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<CameraShake>().gameObject;
	}

    private void Update()
    {
        gameObject.transform.LookAt(new Vector3(player.transform.position.x, gameObject.transform.position.y, player.transform.position.z));
    }
}
