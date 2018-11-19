using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Lägg på objektet som ska flyttas.
public class OB_TransportItem : OB_Interactable {

    // Till den punkt där objektet ska flyttas. Rekommenderar ett tomt objekt. Objektet måste ha en transform.
    [SerializeField]
    Transform target;

    float startTime = 0f;
    float delayTime = 1;

    private void OnTriggerEnter(Collider other)
    {
        OnEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnExit(other);
    }

    public override void Activate(GameObject player)
    {
        StartCoroutine(MoveObject(delayTime));
    }

    //Coroutinen körs tills objektet har flyttats fram till target.
    IEnumerator MoveObject(float delay)
    {
        startTime = 0f;
        gameObject.GetComponent<Collider>().enabled = false;
        while (true)
        {
            startTime += Time.deltaTime;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target.position, startTime);
            if (gameObject.transform.position == target.position)
            {
                target.gameObject.GetComponent<OB_TransportTarget>().SetObject(gameObject);
                gameObject.GetComponent<Collider>().enabled = true;
                yield break;
            }

            yield return null;
        }
    }

    private void Update()
    {
        DoThings();
    }

}
