using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour {

    [SerializeField]
    private GameObject climbBottom;
    [SerializeField]
    private GameObject climbTop;
    [SerializeField]
    private float climbSpeed = 5f;

    private GameObject player;

    private bool climbingUp = false;
    private bool climbingDown = false;
    private bool startClimbingDown = false;

    public void ClimbUp(GameObject Player)
    {
        player = Player;
        player.transform.position = climbBottom.transform.position; // Teleport player to climb start
        player.GetComponent<Rigidbody>().useGravity = false;// No gravety when climbing
        player.GetComponent<Rigidbody>().isKinematic = true;// Is kenetic so you don't fly away
        player.GetComponent<CH_PlayerMovement>().SetStop(true);// Restrict player movment during climb
        climbingUp = true;
    }  

    private void FixedUpdate()
    {
        //climb up.
        if (climbingUp)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, climbTop.transform.position, Time.deltaTime * climbSpeed);
            if (Vector3.Distance(climbTop.transform.position, player.transform.position) < 1)
            {
                climbingUp = false;
                climbingDown = true;
            }
        }      
        //If you are at the top of cimbling and press e you start clibing down.
        if (climbingDown && Input.GetKeyDown("e"))
        {
            startClimbingDown = true;
        }
        //climb down.
        if (startClimbingDown)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, climbBottom.transform.position, Time.deltaTime * climbSpeed);
            if (Vector3.Distance(climbBottom.transform.position, player.transform.position) < 1)
            {
                climbingDown = false;
                startClimbingDown = false;
                player.GetComponent<Rigidbody>().useGravity = true;
                player.GetComponent<Rigidbody>().isKinematic = false;
                player.GetComponent<CH_PlayerMovement>().SetStop(false);
            }
        }
    }
}
