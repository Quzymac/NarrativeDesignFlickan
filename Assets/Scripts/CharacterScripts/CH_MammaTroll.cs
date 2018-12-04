using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CH_MammaTroll : MonoBehaviour {

    Transform mammaGoal;
    NavMeshAgent troll;
    CH_MammaTroll mamma;

    void Start()
    {
        troll = GetComponent<NavMeshAgent>();
        troll.destination = mammaGoal.position;      
    }

    private void Update()
    {
        if (Vector3.Distance(gameObject.transform.position,mammaGoal.transform.position) <= 1f)
        {
            Destroy(gameObject);
        }
    }

    public void SetMammaGoal(Transform transform)
    {
        mammaGoal = transform;
    }
}