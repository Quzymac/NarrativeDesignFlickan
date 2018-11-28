using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CH_Interact : MonoBehaviour
{

    List<OB_Interactable> interacables = new List<OB_Interactable>();

    public void AddInteractable(OB_Interactable gameObject)
    {
        if (gameObject != null)
        {
            interacables.Add(gameObject);
        }

    }

    public void RemoveInteractable(OB_Interactable gameObject)
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
                interacables[0].Activate(gameObject);
            }
        }
    }

}