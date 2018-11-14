using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CH_PlayerMovement : MonoBehaviour {

    [SerializeField]
    private Rigidbody body;
    [SerializeField]
    private float moveSpeed;

    void FixedUpdate()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (direction.magnitude > 1)
            Vector3.Normalize(direction);
        direction *= moveSpeed;
        direction.y = body.velocity.y;
        body.velocity = direction;
    }
}
