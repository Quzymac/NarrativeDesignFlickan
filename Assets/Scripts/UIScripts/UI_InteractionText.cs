using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InteractionText : MonoBehaviour {


    //Add interact canvas to item prefab 
    //Add Camera in inspector to canvas event camera

    Canvas interactionTextCanvas;
    Transform cam;
    string itemName;
    bool canvasActive;

    //Call this method with "true" when interaction is possible to display text.
    //Call this method with "false" when interaction is not possible to stop displaying text
	public void SetTextActive(bool isActive)
    {
        interactionTextCanvas.gameObject.SetActive(isActive);
        canvasActive = isActive;
    }

    void Start()
    {
        cam = FindObjectOfType<Camera>().transform;
        interactionTextCanvas = GetComponentInChildren<Canvas>();
        interactionTextCanvas.gameObject.SetActive(false);

        interactionTextCanvas.GetComponentInChildren<Text>().text = "\"E\" to interact";
        //Sets text to match itemType
        if (GetComponent<OB_Item>())
        {
            itemName = GetComponentInParent<OB_Item>().GetItemType().ToString();
            interactionTextCanvas.GetComponentInChildren<Text>().text = "\"E\" " + itemName;
        }

        if (GetComponent<OB_Dialogue>())
        {
            interactionTextCanvas.GetComponentInChildren<Text>().text = "\"E\" to speak";
        }
    }

    void Update ()
    {
        //for testing, remove when implemented in OB_Interactable
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetTextActive(!canvasActive);
        }

        if (canvasActive)
        {
            //Rotates text towards camera when active
            interactionTextCanvas.transform.LookAt(cam);
            interactionTextCanvas.transform.rotation *= Quaternion.Euler(0, 180f, 0);
        }
    }
}
