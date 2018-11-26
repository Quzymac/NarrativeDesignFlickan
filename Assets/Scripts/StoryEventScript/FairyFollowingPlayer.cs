﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyFollowingPlayer : MonoBehaviour {

    [SerializeField] GameObject player;
    [SerializeField] GameObject fairy;
    [SerializeField] Transform fairyStartPos; //position fairy will teleport back to after minigame is finished (same position as before the game), where the player will talk to it
    bool fairyFollowing = false;
    [SerializeField] float followDistance = 3f;



    public void FairyFollowToggle(bool follow) // true to start following, false to stop and teleport fairy back
    {
        if (follow)
        {
            fairyFollowing = true;
        }
        else
        {
            fairyFollowing = false;
            TeleportFairyToStartPos();
        }
    }
    void TeleportFairyToStartPos()
    {
        fairy.transform.position = fairyStartPos.position;
    }

    void Update()
    {
        if (fairyFollowing && Vector3.Distance(player.transform.position, fairy.transform.position) > followDistance)
        {
            fairy.transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Time.deltaTime * (Vector3.Distance(player.transform.position, fairy.transform.position) - 1));
        }

        if (Input.GetKeyDown(KeyCode.F)) //__===000__===0000 TESTING ONLY ===000___===000
        {
            FairyFollowToggle(!fairyFollowing);
        }
    }
}