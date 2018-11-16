using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CH_PlayerMovement : MonoBehaviour {

    [SerializeField]
    private Rigidbody body;
    [SerializeField]
    private float moveSpeed = 3, rotationSpeed = 10;
    [SerializeField]
    private Transform cameraTrans;
    private bool stop;

    bool climbing;

    private GameObject interactable;

    public void SetStop(bool b) //Starta/Stoppa spelarens normala movement (till exempel under dialog)
    {
        stop = b;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Climbable"))
        {
            climbing = true;
        }
        if (other.tag.Equals("Interactable"))
        {
            interactable = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Climbable"))
        {
            climbing = false;
        }
        if (other.tag.Equals("Interactable"))
        {
            interactable.GetComponent<Interactable>().RemoveText();
            interactable = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            climbing = false;
        }
    }

    void FixedUpdate()
    {
        Vector3 direction = Vector3.zero;

        if (climbing)
        {
            //TODO Add climbing animation here.
            direction = new Vector3(0f, Input.GetAxis("Vertical"), 0f);
            if (direction.magnitude > 1)
                Vector3.Normalize(direction);
            direction *= moveSpeed;
            direction.z = body.velocity.z;
        }
        else if (!stop)
        {
            /*** Vända sig ***/
            Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));    //Input vector
            float cameraAngle = -Vector3.Angle(cameraTrans.forward, Vector3.forward);   //Kamerans vinkel jämfört med världen
            if (Vector3.Angle(cameraTrans.right, Vector3.forward) > 90f)                //Om kameran är åt vänster, 360 - vinkeln (Pga anledningar)
                cameraAngle = 360 - cameraAngle;
            Vector3 moveDirection = Quaternion.AngleAxis(cameraAngle, Vector3.up) * inputDirection; //Hållet spelaren vill gå
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, moveDirection, Time.deltaTime * rotationSpeed, 0);  //Den nya riktningen karaktären ska kolla mot
            transform.rotation = Quaternion.LookRotation(newDirection); //Rotera karaktären så den kollar mot den nya riktningen

            /*** Gå ***/ //Ska ändra movement lite senare (gå segare medans man roterar typ)
            float velocity = inputDirection.magnitude * moveSpeed; //Så att den typ accelererar lite + att den står stilla om man inte klickar något
            if (velocity > 0)
            {
                //TODO Add running animation here. (Beroende på velocity så att den inte sprintar full speed instantly)
                newDirection *= velocity; //Gångra med movespeed och ^ det där
                newDirection.y = body.velocity.y;   //Vi vill inte ändra velocity i y led eftersom det skulle påvärka gravitationen
                body.velocity = newDirection;   //GO! BLAH!
            }
        }

        if (Input.GetKeyDown("e") && interactable != null)
        {
            interactable.GetComponent<Interactable>().PlayText();
        }
    }
}
