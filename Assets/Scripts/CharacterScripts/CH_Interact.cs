using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CH_Interact : MonoBehaviour
{

    List<GameObject> interacables = new List<GameObject>();

    public void AddInteractable(GameObject gameObject)
    {
        if (gameObject != null)
        {
            interacables.Add(gameObject);
        }

    }

    public void RemoveInteractable(GameObject gameObject)
    {
        if (gameObject != null)
        {
            interacables.Remove(gameObject);
        }
    }

    private void Update()
    {
        if(interacables.Count > 0)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                interacables[0].GetComponent<OB_Interactable>().Activate(gameObject);
            }
        }
    }

}