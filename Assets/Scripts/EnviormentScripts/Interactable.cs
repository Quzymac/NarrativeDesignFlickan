using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    private string text;
    public Text textDisplay;
    public void Start()
    {

    }

    public void PlayText()
    {
        textDisplay.text = text;
    }

    public void RemoveText()
    {
        textDisplay.text = "";
    }

}
