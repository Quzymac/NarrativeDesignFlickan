using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OB_Pushable : OB_Interactable {

    private float moveSpeed = 1.5f, rotationSpeed = 1; //Hur snabb spelaren är när den flyttar på objektet
    private bool pushing = false;
    private Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        OnEnter(other);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform == transform.parent)
        {
            Activate(other.gameObject);
        }
        OnExit(other);
    }
    private void Update()
    {
        DoThings();
        if (pushing)
        {
            body.position = transform.position;
        }
    }
    public override void Activate(GameObject player)
    {
        if (!pushing)
        {
            pushing = true;
            transform.parent = player.transform;
            Vector3 fulFix = transform.position - player.transform.position; //Om spelaren är för nära objektet så kan man inte gå, kommer inte på ett snyggare sätt att fixa problemet på
            transform.position += fulFix.normalized * .05f;
            player.GetComponent<CH_PlayerMovement>().SetSpeed(moveSpeed, rotationSpeed);
        } else
        {
            pushing = false;
            transform.parent = null;
            player.GetComponent<CH_PlayerMovement>().SetSpeed(0, 0);
        }
    }
}
