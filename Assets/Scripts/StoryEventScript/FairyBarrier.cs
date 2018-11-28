using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyBarrier : MonoBehaviour {

    [SerializeField] Transform pushOutEndPos; //Where the player will be pushed to
    [SerializeField] GameObject barrier;//gameobject with collider to keep player out and particleEffect forceField
     GameObject player;

    [SerializeField] float pushSpeed = 10f;
   
	void Update ()
    {
        //Only for testing 000000_____------000000_____------000000_____------000000_____------000000_____------000000_____------000000_____------000000_____------
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    StartCoroutine(PushPlayerAway());
        //}
    }
    private void Start()
    {
        player = FindObjectOfType<CH_PlayerMovement>().gameObject;
    }
    public void Pushplayer()
    {
        StartCoroutine(PushPlayerAway());
    }

    private void OnEnable()
    {
        OptionsManager.NewChoice += PushPlayer;
    }
    void PushPlayer(object sender, OptionsEventArgs e)
    {
        if (e.Option == "B3_Alvor_1" && e.Value == 1)
        {
            StartCoroutine(PushPlayerAway());
        }
    }
    IEnumerator PushPlayerAway() //Call this when fairy is pushing player out of B3
    {
        float startTime = 0f;
        player.GetComponent<CH_PlayerMovement>().SetStop(true);

        while (true)
        {
            startTime += Time.deltaTime * (0.01f * pushSpeed);
            player.transform.position = Vector3.Lerp(player.transform.position, pushOutEndPos.transform.position, startTime);
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, pushOutEndPos.transform.rotation, startTime * 10);

            if (Vector3.Distance(player.transform.position, pushOutEndPos.transform.position) < 0.1f)    //player.transform.position == endPos.transform.position)
            {
                barrier.SetActive(true);
                player.GetComponent<CH_PlayerMovement>().SetStop(false);

                yield break;
            }

            yield return null;
        }
    }
}
