using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_SopaScript : MonoBehaviour {

    [SerializeField]
    Transform pushPoint;
    GameObject player;

    private void Start()
    {
        player = FindObjectOfType<CH_PlayerMovement>().gameObject;
    }

    public IEnumerator PushPlayer()
    {
        float startTime = 0f;
        player.GetComponent<CH_PlayerMovement>().SetStop(true);
        player.GetComponent<Rigidbody>().isKinematic = true;

        while(true)
        {
            startTime += Time.deltaTime;
            player.transform.position = Vector3.Lerp(player.transform.position, pushPoint.transform.position, startTime * 0.2f);

            if (Vector3.Distance(player.transform.position, pushPoint.transform.position) < 0.1f)
            {
                player.GetComponent<CH_PlayerMovement>().SetStop(false);
                player.GetComponent<Rigidbody>().isKinematic = false;
                yield break;
            }

            yield return null;
        }
    }
}
