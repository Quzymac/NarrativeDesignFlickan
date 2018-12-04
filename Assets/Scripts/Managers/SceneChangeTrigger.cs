using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CH_PlayerMovement>())
        {
            SceneManager.LoadScene(2);
        }
    }
}
