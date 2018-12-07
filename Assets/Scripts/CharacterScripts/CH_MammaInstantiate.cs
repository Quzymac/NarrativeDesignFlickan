using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CH_MammaInstantiate : MonoBehaviour {

    [SerializeField]
    GameObject MammaTroll;
    bool firstSpawn = false;
    CH_MammaTroll mamma;

    public 

    Vector3 spawnposition;

    private void Awake()
    {
        spawnposition = new Vector3(149, 2, 149);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (firstSpawn == false && other.CompareTag("Player"))
        {
            mamma = Instantiate(MammaTroll, spawnposition, Quaternion.identity).GetComponent<CH_MammaTroll>();          
            mamma.SetMammaGoal(GameObject.FindGameObjectWithTag("MammaGoal").transform);
            firstSpawn = true;
            UI_DialogueController.Instance.DisplayMessage("Tyra", "Springer det där trollet iväg med Hilding?! Jag måste hinna ikapp!", 8f);
            FindObjectOfType<CH_PlayerCamera>().LookAtSomething(mamma.transform, 3);
        }
        else
        {
            Debug.Log("Cannot spawn again");
        }
    }
}
