using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CH_PlayerMovement : MonoBehaviour
{
    [SerializeField]
    Animator myAnimator;
    [SerializeField]
    private Rigidbody body;
    [SerializeField]
    private float defaultMoveSpeed = 3, defaultRotationSpeed = 10, runMultiplier = 2;
    private float moveSpeed, rotationSpeed;
    [SerializeField]
    private Transform cameraTrans;
    private bool stop;
    private bool jump, jumping, grounded, running, idle, pushing, pickup, dancing;
    private float jumpTime;

    public Animator MyAnimator { get { return myAnimator; } }
    public bool Pushing { set { pushing = value; } }
    public bool Pickup { set { pickup = value; } }
    public bool Jumping { get { return jumping; } }

    private void Start()
    {
        moveSpeed = defaultMoveSpeed;
        rotationSpeed = defaultRotationSpeed;
        running = false;
        pushing = false;
        pickup = false;
        dancing = false;
    }

    public void SetStop(bool b) //Starta/Stoppa spelarens normala movement (till exempel under dialog)
    {
        stop = b;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            running = !running;
            if (running)
                SetSpeed(defaultMoveSpeed * runMultiplier, defaultRotationSpeed);
            else
                SetSpeed();
        }
        if (!stop && !dancing && Input.GetKey(KeyCode.Space) && Time.time > jumpTime)
        {
            jump = true;
        }
        if (myAnimator.GetBool("Jump") && Time.time > jumpTime -0.3f)
        {
            myAnimator.SetBool("Jump", false);
        }
        if (Input.GetKeyDown(KeyCode.F1))
        {
            dancing = !dancing;
            if(dancing)
                myAnimator.SetBool("Dance", true);
            else
                myAnimator.SetBool("Dance", false);
        }
    }
    void FixedUpdate()
    {
        if (!stop && !dancing)
        {
            if (jump)
            {
                if (grounded)
                {
                    jumpTime = Time.time + 1f;
                    body.AddForce(Vector3.up * 3, ForceMode.Impulse);
                    grounded = false;
                    jumping = true;
                    myAnimator.SetBool("Jump", true);
                    myAnimator.speed = 1;
                }
                jump = false;
            }
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
            if (!running && speedForward > 0 && !pushing)
            {
                myAnimator.SetBool("Walk", true);
                myAnimator.SetBool("Idle", false);
                myAnimator.SetBool("Run", false);
                myAnimator.speed = speedForward;
            }
            else if (running && speedForward > 0 && !pushing)
            {
                myAnimator.SetBool("Run", true);
                myAnimator.SetBool("Idle", false);
                myAnimator.SetBool("Walk", false);
                myAnimator.speed = speedForward;
            }
            else if(speedForward == 0)
            {
                myAnimator.SetBool("Idle", true);
                myAnimator.SetBool("Run", false);
                myAnimator.SetBool("Walk", false);
                myAnimator.speed = 1;
            }
            if (pickup)
            {
                pickup = false;
                myAnimator.speed = 1;
                IEnumerator waitForPickup = WaitForAnimation(1.0f, "Pickup");
                speedForward = 0;
                StartCoroutine(waitForPickup);
            }

            //TODO Add running animation here. (Beroende på velocity så att den inte sprintar full speed instantly)

            Vector3 velocityVector = speedForward * moveDirection;
            velocityVector *= moveSpeed; //Gångra med movespeed
            velocityVector.y = body.velocity.y;   //Vi vill inte ändra velocity i y led eftersom det skulle påvärka gravitationen
            body.velocity = velocityVector;   //GO! BLAH!
        }
    }

    private IEnumerator WaitForAnimation(float seconds, string name)
    {
        myAnimator.SetBool(name, true);
        SetStop(true);
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;
        yield return new WaitForSeconds(seconds);
        myAnimator.SetBool(name, false);
        SetStop(false);
    }

    public void SetSpeed(float moveSpeed = 0, float rotationSpeed = 0)
    {
        if (moveSpeed == 0)
        {
            moveSpeed = defaultMoveSpeed;
            rotationSpeed = defaultRotationSpeed;
        }
        this.moveSpeed = moveSpeed;
        this.rotationSpeed = rotationSpeed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            if (jumping)
            {
                Debug.Log("kakor");
                float shakeIntencity = collision.relativeVelocity.y;
                if (shakeIntencity > 1)
                    shakeIntencity = 1;
                CameraShake.Instance.ShakeCamera(shakeIntencity / 20, shakeIntencity / 10, 4);
            }
            jumping = false;
            grounded = true;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = false;
        }
    }
}