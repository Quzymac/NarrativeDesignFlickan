using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CH_PlayerCamera : MonoBehaviour { //Scriptet kan ligga vart som helst

    [SerializeField] //Target är ett empty gameObject som sitter vid karaktärens huvud
    private Transform target, cameraUpDown, cameraLeftRight, cameraRayOrigin; //Target = spelaren, cameraUpDown och leftRight är päron till kameran
    [SerializeField] 
    private float sensitivity = 0.5f;
    private float maxAngleX = .6f, minAngleX = 0, maxDist = 9, minDist = 1.5f;
    private float cameraDist = 4;
    private bool stop, lockedScroll;
    [SerializeField]
    LayerMask mask;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        LockCamera(true);
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
    private void AvoidStuff()
    {
        RaycastHit wallHit = new RaycastHit();
        //linecast from your player (targetFollow) to your cameras mask (camMask) to find collisions.
        if (Physics.Linecast(target.position, cameraRayOrigin.position - cameraRayOrigin.forward, out wallHit, mask))
        {
            Vector3 camPosition = cameraUpDown.GetChild(0).position;
            camPosition = new Vector3(wallHit.point.x + wallHit.normal.x * 0.5f, camPosition.y, wallHit.point.z + wallHit.normal.z * 0.5f);
            cameraUpDown.GetChild(0).position = Vector3.Lerp(cameraUpDown.GetChild(0).position, camPosition, Time.deltaTime * cameraDist);
        } else
        {
            cameraUpDown.GetChild(0).localPosition = Vector3.Lerp(cameraUpDown.GetChild(0).localPosition, Vector3.back * cameraDist, Time.deltaTime * 2);
        }

            cameraUpDown.GetChild(0).LookAt(target);

    }
    private void CameraScroll()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            cameraDist -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 200;
            if (cameraDist > maxDist)
                cameraDist = maxDist;
            Vector3 rayOrigin = cameraRayOrigin.localPosition;
            rayOrigin.z = -cameraDist;
            cameraRayOrigin.localPosition = rayOrigin;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            cameraDist -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 200;
            if (cameraDist < minDist)
                cameraDist = minDist;
            Vector3 rayOrigin = cameraRayOrigin.localPosition;
            rayOrigin.z = -cameraDist;
            cameraRayOrigin.localPosition = rayOrigin;
        }
    }
    void Update()
    {
        if (!stop)
        {
            if (!lockedScroll)
                CameraScroll();
            Quaternion newRotation = cameraUpDown.localRotation * Quaternion.Euler(-Input.GetAxis("Mouse Y") * sensitivity, 0, 0);
            if (newRotation.x < minAngleX)
            {
                newRotation.x = minAngleX;
            } else if (newRotation.x > maxAngleX)
            {
                newRotation.x = maxAngleX;
            }
            newRotation.y = 0;
            newRotation.z = 0;
            cameraUpDown.localRotation = Quaternion.RotateTowards(cameraUpDown.localRotation, newRotation, sensitivity * 4);
            cameraLeftRight.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);
            cameraLeftRight.position = target.position;
            AvoidStuff();
        }
    }
    public void LockCamera(bool locked)
    {
        lockedScroll = locked;
        if (lockedScroll)
        {
            cameraDist = 2;
            Vector3 rayOrigin = cameraRayOrigin.localPosition;
            rayOrigin.z = -cameraDist;
            cameraRayOrigin.localPosition = rayOrigin;
            cameraUpDown.GetChild(0).localPosition = Vector3.back * cameraDist;
        } else
        {
            cameraDist = 4;
            Vector3 rayOrigin = cameraRayOrigin.localPosition;
            rayOrigin.z = -cameraDist;
            cameraRayOrigin.localPosition = rayOrigin;
            cameraUpDown.GetChild(0).localRotation = Quaternion.identity;
            cameraUpDown.GetChild(0).localPosition = Vector3.back * cameraDist;
            cameraLeftRight.rotation = Quaternion.Euler(0, 20, 0);
            cameraUpDown.rotation = Quaternion.Euler(20, 20, 0);
        }
    }
    public void LookAtSomething(Transform something, float duration)
    {
        StartCoroutine(LookTowards(cameraLeftRight, cameraUpDown, something, duration));
    }
    private IEnumerator LookTowards(Transform cameraLeftRight, Transform cameraUpDown, Transform target, float duration)
    {
        stop = true;
        float lookTime = Time.time + duration;
        while (Time.time < lookTime)
        {
            Quaternion newRotation = Quaternion.LookRotation(target.position - cameraLeftRight.position);
            Quaternion updown = newRotation;
            updown.x = .1f;
            newRotation.x = 0;
            updown.z = 0;
            updown.y = 0;
            newRotation.z = 0;
            cameraLeftRight.rotation = Quaternion.RotateTowards(cameraLeftRight.rotation, newRotation, Time.deltaTime * 60);
            cameraUpDown.localRotation = Quaternion.RotateTowards(cameraUpDown.localRotation, updown, Time.deltaTime * 60);
            cameraLeftRight.position = this.target.position;
            yield return new WaitForEndOfFrame();
        }
        stop = false;
    }
}