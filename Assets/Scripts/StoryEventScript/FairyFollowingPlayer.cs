using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyFollowingPlayer : MonoBehaviour {

    GameObject player;
    GameObject fairy;
    Vector3 fairyStartPos;
    bool fairyFollowing = false;
    [SerializeField] float followDistance = 3f;

    private void Start()
    {
        fairy = this.gameObject;
        player = FindObjectOfType<CH_PlayerMovement>().gameObject;
        fairyStartPos = transform.position;
    }


    public void FairyFollowToggle(bool follow) // true to start following, false to stop and teleport fairy back
    {
        if (follow)
        {
            fairyFollowing = true;
            fairy.GetComponent<UI_InteractionText>().SetTextActive(false);
            player.GetComponent<CH_Interact>().RemoveInteractable(fairy.GetComponent<OB_Interactable>());
            fairy.GetComponent<Collider>().enabled = false;
        }
        else
        {
            fairyFollowing = false;
            TeleportFairyToStartPos();
        }
    }
    void TeleportFairyToStartPos()
    {
        fairy.transform.position = fairyStartPos;
    }

    void Update()
    {
        if (fairyFollowing && Vector3.Distance(player.transform.position, fairy.transform.position) > followDistance)
        {
            fairy.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * (Vector3.Distance(player.transform.position, fairy.transform.position) - 1));
        }
    }
}
