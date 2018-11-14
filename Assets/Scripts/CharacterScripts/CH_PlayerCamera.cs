using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CH_PlayerCamera : MonoBehaviour {

    [SerializeField]
    private Transform camTransform;
    [SerializeField]
    private float sensitivity = 0.5f;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update () {
            camTransform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);
	}
}