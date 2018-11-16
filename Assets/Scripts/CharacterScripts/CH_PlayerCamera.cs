using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CH_PlayerCamera : MonoBehaviour { //Scriptet kan ligga vart som helst

    [SerializeField]
    private Transform target, cameraUpDown, cameraLeftRight; //Target = spelaren, cameraUpDown och leftRight är päron till kameran
    [SerializeField]
    private float sensitivity = 0.5f;
    private float maxAngleX = 65, minAngleX = 20;
    private bool stop;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void FreeMouse(bool b) //True = låst kamera och olåst mus
    {
        stop = b;
        if (stop)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        } else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void Update()
    {
        if (!stop)
        {
            cameraLeftRight.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);
            cameraUpDown.Rotate(-Input.GetAxis("Mouse Y") * sensitivity, 0, 0);
            if (cameraUpDown.localRotation.eulerAngles.x < minAngleX)
            {
                cameraUpDown.localRotation = Quaternion.Euler(new Vector3(minAngleX, 0, 0));
            }
            else if (cameraUpDown.localRotation.eulerAngles.x > maxAngleX)
            {
                cameraUpDown.localRotation = Quaternion.Euler(new Vector3(maxAngleX, 0, 0));
            }
            cameraLeftRight.position = target.position;
        }
    }
}