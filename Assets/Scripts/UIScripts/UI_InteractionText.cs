using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InteractionText : MonoBehaviour {


    //Add interact canvas to item prefab 
    //Add Camera in inspector to canvas event camera

    Canvas interactionTextCanvas;
    Transform cam;
    Item itemName;
    bool canvasActive;
    [SerializeField]
    string customText;

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

        interactionTextCanvas.GetComponentInChildren<Text>().text = "\"E\" för att använda";
        //Sets text to match itemType
        if (GetComponent<OB_Item>())
        {
            itemName = GetComponentInParent<OB_Item>().GetItemType();
            interactionTextCanvas.GetComponentInChildren<Text>().text = "\"E\" för att ta " + TranslateItemName(itemName);
        }

        if (GetComponent<OB_Dialogue>())
        {
            interactionTextCanvas.GetComponentInChildren<Text>().text = "\"E\" för att prata";
        }
        if (customText != "")
            interactionTextCanvas.GetComponentInChildren<Text>().text = "\"E\" " + customText;

    }
    string TranslateItemName(Item item)
    {
        switch (item)
        {
            case Item.Blueberry:
                return "blåbär";
            case Item.BlueberryBush:
                return "blåbär";
            case Item.LingonBush:
                return "lingon";
            case Item.Lingonberry:
                return "lingon";
            case Item.Apple:
                return "äpple";
            case Item.Chanterelle:
                return "kantarell";
            case Item.Falukorv:
                return "falukorv";
            case Item.Birch_polypore:
                return "björkticka";
            case Item.Fly_agaric:
                return "flugsvamp";
            case Item.Gulfotshatta:
                return "gulfotshätta";
            case Item.Pine_cone:
                return "tallkotte";
            case Item.Fir_cone:
                return "grankotte";
            case Item.Wine:
                return "vin";
        }

        return "SomeString";
    }
    void Update ()
    {
        if (canvasActive)
        {
            //Rotates text towards camera when active
            interactionTextCanvas.transform.LookAt(cam);
            interactionTextCanvas.transform.rotation *= Quaternion.Euler(0, 180f, 0);
        }
    }
}
