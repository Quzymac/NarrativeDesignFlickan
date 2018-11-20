﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CH_PlayerPushing : MonoBehaviour {

    private Rigidbody bodyToMove;
    private float moveSpeed;
    [SerializeField]
    private Rigidbody body;
    [SerializeField]
    private Transform cameraTrans;

    public void Push(Rigidbody bodyToMove, float moveSpeed)
    {
        this.bodyToMove = bodyToMove;
        this.moveSpeed = moveSpeed;
        GetComponent<CH_PlayerMovement>().SetStop(true);
        /*Vector3 positionToLookAt = bodyToMove.transform.position;
        positionToLookAt.y = transform.position.y;*/
        float xDif = bodyToMove.transform.position.x - transform.position.x;
        float zDif = bodyToMove.transform.position.z - transform.position.z;
        Vector3 positionToLookAt = transform.position;
        if (Mathf.Abs(xDif) > Mathf.Abs(zDif))
        {
            positionToLookAt += Vector3.right * xDif;
        } else
        {
            positionToLookAt += Vector3.forward * zDif;
        }
        transform.LookAt(positionToLookAt);
    }
    public void StopPushing(Rigidbody bodyToMove)
    {
        if (this.bodyToMove == bodyToMove)
        {
            this.bodyToMove = null;
            GetComponent<CH_PlayerMovement>().SetStop(false);
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (bodyToMove != null)
        {
            Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));    //Input vector
            if (inputDirection.magnitude > 1)
                inputDirection = inputDirection.normalized;
            float cameraAngle = -Vector3.Angle(cameraTrans.forward, Vector3.forward);   //Kamerans vinkel jämfört med världen
            if (Vector3.Angle(cameraTrans.right, Vector3.forward) > 90f)                //Om kameran är åt vänster, 360 - vinkeln (Pga anledningar)
                cameraAngle = 360 - cameraAngle;
            Vector3 moveDirection = Quaternion.AngleAxis(cameraAngle, Vector3.up) * inputDirection; //Hållet spelaren vill gå
                                                                                                    /*** Gå ***/
            float speedForward = Vector3.Dot(transform.forward, moveDirection);
            /*if (speedForward < 0)
                speedForward = 0;*/
            //TODO Add running animation here. (Beroende på velocity så att den inte sprintar full speed instantly)
            Vector3 velocityVector = speedForward * transform.forward;
            velocityVector *= moveSpeed; //Gångra med movespeed
            velocityVector.y = body.velocity.y;   //Vi vill inte ändra velocity i y led eftersom det skulle påvärka gravitationen
            body.velocity = velocityVector;   //GO! BLAH!
            velocityVector.y = bodyToMove.velocity.y;
            bodyToMove.velocity = velocityVector;
        }
    }
}
