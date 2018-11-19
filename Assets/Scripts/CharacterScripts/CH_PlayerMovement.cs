﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CH_PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private Rigidbody body;
    [SerializeField]
    private float defaultMoveSpeed = 3, defaultRotationSpeed = 10;
    private float moveSpeed, rotationSpeed;
    [SerializeField]
    private Transform cameraTrans;
    private bool stop;

    private void Start()
    {
        moveSpeed = defaultMoveSpeed;
        rotationSpeed = defaultRotationSpeed;
    }

    public void SetStop(bool b) //Starta/Stoppa spelarens normala movement (till exempel under dialog)
    {
        stop = b;
    }

    void FixedUpdate()
    {
        if (!stop)
        {
            /*** Vända sig ***/
            Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));    //Input vector
            if (inputDirection.magnitude > 1)
                inputDirection = inputDirection.normalized;
            float cameraAngle = -Vector3.Angle(cameraTrans.forward, Vector3.forward);   //Kamerans vinkel jämfört med världen
            if (Vector3.Angle(cameraTrans.right, Vector3.forward) > 90f)                //Om kameran är åt vänster, 360 - vinkeln (Pga anledningar)
                cameraAngle = 360 - cameraAngle;
            Vector3 moveDirection = Quaternion.AngleAxis(cameraAngle, Vector3.up) * inputDirection; //Hållet spelaren vill gå
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, moveDirection, Time.deltaTime * rotationSpeed, 0);  //Den nya riktningen karaktären ska kolla mot
            transform.rotation = Quaternion.LookRotation(newDirection); //Rotera karaktären så den kollar mot den nya riktningen

            /*** Gå ***/
            float speedForward = Vector3.Dot(transform.forward, moveDirection);
            if (speedForward < 0)
                speedForward = 0;
            //TODO Add running animation here. (Beroende på velocity så att den inte sprintar full speed instantly)
            Vector3 velocityVector = speedForward * moveDirection;
            velocityVector *= moveSpeed; //Gångra med movespeed
            velocityVector.y = body.velocity.y;   //Vi vill inte ändra velocity i y led eftersom det skulle påvärka gravitationen
            body.velocity = velocityVector;   //GO! BLAH!
        }
    }
    public void SetSpeed(float moveSpeed, float rotationSpeed)
    {
        if (moveSpeed == 0)
        {
            moveSpeed = defaultMoveSpeed;
            rotationSpeed = defaultRotationSpeed;
        }
        this.moveSpeed = moveSpeed;
        this.rotationSpeed = rotationSpeed;
    }
}