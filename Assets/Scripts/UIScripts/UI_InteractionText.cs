using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InteractionText : MonoBehaviour {

    [SerializeField] Canvas interactionTextCanvas;
    [SerializeField] Transform cam;
    [SerializeField] Text displayedText;

    string itemName;
    string interactionName;

    bool showCanvas;

    //What distance from center of object the text will appear
    [SerializeField] float offsetPosX = 1f;

    //Call this method with "true" when interaction is possible to display text.
    //Call this method with "false" when interaction is not possible to stop displaying text
	public void SetTextActive(bool isActive)
    {
        interactionTextCanvas.enabled = isActive;
        showCanvas = isActive;
    }

    void Start()
    {
        itemName = GetComponentInParent<OB_Item>().GetItemType().ToString();
        displayedText.text = "E to pick up " + itemName;

        //Offsets text closer to camera
        interactionTextCanvas.GetComponentInChildren<Text>().rectTransform.localPosition = new Vector3(0, 0, -offsetPosX);
    }

    void Update () {
        if (showCanvas)
        {
            //Rotates text towards camera when active
            interactionTextCanvas.transform.LookAt(cam);
            interactionTextCanvas.transform.rotation *= Quaternion.Euler(0, 180f, 0);
        }
    }
}
