using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    private string text;
    [SerializeField]
    private Text textDisplay;

    [SerializeField]
    private GameObject climbable;

    public void Interact (GameObject player)
    {
        //if there is a textdisplay play text
        if (textDisplay != null)
        {
            PlayText();
        }
        //if there is a climbing call Climbing
        if (climbable != null)
        {
            climbable.GetComponent<Climbing>().ClimbUp(player);
        }
    }

    private void PlayText()
    {
        textDisplay.text = text;

        if (Input.GetKeyDown("e"))
        {
            RemoveText();
        }
    }

    private void RemoveText()
    {
        textDisplay.text = "";
    }

}