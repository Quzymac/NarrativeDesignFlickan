using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb_01 : OB_Interactable {

    [SerializeField]
    private GameObject climbBottom;
    [SerializeField]
    private GameObject climbTop;
    [SerializeField]
    private float climbSpeed = 5f;
    [SerializeField]
    bool climbingUp;
    [SerializeField]
    Transform lookTarget;

    private void OnTriggerEnter(Collider other)
    {
        OnEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnExit(other);
    }

    public void ClimbUp(GameObject player)
    {
        player.GetComponent<Rigidbody>().useGravity = false;// No gravety when climbing
        player.GetComponent<Rigidbody>().isKinematic = true;// Is kenetic so you don't fly away
        player.GetComponent<CH_PlayerMovement>().SetStop(true);// Restrict player movment during climb
        StartCoroutine(Climb(player));
    }

    IEnumerator Climb(GameObject player)
    {
        bool movingToStartPosition = true;
        Vector3 target;

        if (climbingUp)
            target = new Vector3(climbBottom.transform.position.x, player.transform.position.y, climbBottom.transform.position.z);
        else
            target = new Vector3(climbTop.transform.position.x, player.transform.position.y, climbBottom.transform.position.z);

        while (movingToStartPosition)
        {
            player.transform.LookAt(gameObject.transform, Vector3.up);
            if (climbingUp)
            {

                player.transform.LookAt(new Vector3(lookTarget.transform.position.x, player.transform.position.y, lookTarget.transform.position.z));
                player.transform.position = Vector3.MoveTowards(player.transform.position, target, Time.deltaTime * 4f);
                if (player.transform.position == target)
                    movingToStartPosition = false;
            }
            else
            {
                player.transform.LookAt(new Vector3(lookTarget.transform.position.x, player.transform.position.y, lookTarget.transform.position.z));
                player.transform.position = Vector3.MoveTowards(player.transform.position, target, Time.deltaTime * 4f);
                if (player.transform.position == target)
                    movingToStartPosition = false;
            }
            yield return null;

        }

        while (movingToStartPosition == false)
        {
            //climb up.
            if (climbingUp)
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position, climbTop.transform.position, Time.deltaTime * climbSpeed);
                if (Vector3.Distance(climbTop.transform.position, player.transform.position) < 1)
                {
                    player.GetComponent<Rigidbody>().useGravity = true;
                    player.GetComponent<Rigidbody>().isKinematic = false;
                    player.GetComponent<CH_PlayerMovement>().SetStop(false);
                    yield break;
                }
            }
            //climb down.
            else
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position, climbBottom.transform.position, Time.deltaTime * climbSpeed);
                if (Vector3.Distance(climbBottom.transform.position, player.transform.position) < 1)
                {
                    player.GetComponent<Rigidbody>().useGravity = true;
                    player.GetComponent<Rigidbody>().isKinematic = false;
                    player.GetComponent<CH_PlayerMovement>().SetStop(false);
                    yield break;
                }
            }
            yield return null;
        }
    }

    public override void Activate(GameObject player)
    {
        ClimbUp(player);
    }
}
