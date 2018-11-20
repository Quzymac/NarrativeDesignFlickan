using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class OB_Pushable : OB_Interactable {

    private float pushSpeed = 1.5f; //Hur snabb spelaren är när den flyttar på objektet
    private bool pushing = false;
    private Rigidbody body;
    private Vector3 localPosition; //Position jämfört med spelaren när den blir puttad
    private Quaternion localRotation;

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
        if (pushing && other.tag == "Player")
        {
            Activate(other.gameObject);
        }
        OnExit(other);
    }
    public override void Activate(GameObject player)
    {
        if (!pushing)
        {
            pushing = true;
            body.constraints = RigidbodyConstraints.FreezeRotation;
            player.GetComponent<CH_PlayerPushing>().Push(body, pushSpeed);
            /*transform.parent = player.transform;
            Vector3 fulFix = transform.position - player.transform.position; //Om spelaren är för nära objektet så kan man inte gå, kommer inte på ett snyggare sätt att fixa problemet på
            transform.position += fulFix.normalized * .05f;
            localPosition = transform.localPosition;
            localRotation = transform.localRotation;
            player.GetComponent<CH_PlayerMovement>().SetSpeed(moveSpeed, rotationSpeed);*/
        } else
        {
            pushing = false;
            body.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            player.GetComponent<CH_PlayerPushing>().StopPushing(body);
        }
    }
}
