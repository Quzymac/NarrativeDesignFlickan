using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CH_MammaTroll : MonoBehaviour {

    [SerializeField]
    Transform mammaGoal;

    NavMeshAgent troll;

    [SerializeField]
    GameObject mammaTroll;

    void Start()
    {
        troll = GetComponent<NavMeshAgent>();
        troll.destination = mammaGoal.position;      
    }
    private void Update()
    {
        if (troll.transform.position == mammaGoal.transform.position)
        {
            Destroy(mammaTroll);
        }
    }
}