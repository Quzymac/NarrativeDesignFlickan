using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CH_PlayerMovement : MonoBehaviour {

    [SerializeField]
    private Rigidbody body;
    [SerializeField]
    private float moveSpeed;

    bool climbing;

    private GameObject interactable;

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
        Vector3 direction;
        

        if (climbing)
        {
            //TODO Add climbing animation here.
            direction = new Vector3(0f, Input.GetAxis("Vertical"), 0f);
            if (direction.magnitude > 1)
                Vector3.Normalize(direction);
            direction *= moveSpeed;
            direction.z = body.velocity.z;
        }
        else
        {
            //TODO Add running animation here.
            direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            if (direction.magnitude > 1)
                Vector3.Normalize(direction);
            direction *= moveSpeed;
            direction.y = body.velocity.y;
        }
        body.velocity = direction;

        if (Input.GetKeyDown("e") && interactable != null)
        {
            interactable.GetComponent<Interactable>().PlayText();
        }
    }
}
