using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWallCollision : MonoBehaviour {

    string vatten = "I should not go in the water";
    string skog = "I should not stray to far in the forest";
    string fog = "I could get lost in the fog, i should stop here";
    string mammapappaRum = "Jag borde inte gå in i deras rum";
    string by = "Det finns ingenting för mig att göra i byn.";

    string textToDisplay;
    [SerializeField]
    private string customText;

    enum TypeOfWall
    {
        vatten, skog, fog, mammaPappaRum, by, custom
    }
    [SerializeField] TypeOfWall wallType;

    private void Start()
    {
        switch (wallType)
        {
            case TypeOfWall.vatten:
                textToDisplay = vatten;
                break;
            case TypeOfWall.skog:
                textToDisplay = skog;
                break;
            case TypeOfWall.fog:
                textToDisplay = fog;
                break;
            case TypeOfWall.mammaPappaRum:
                textToDisplay = mammapappaRum;
                break;
            case TypeOfWall.by:
                textToDisplay = by;
                break;
            case TypeOfWall.custom:
                textToDisplay = customText;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UI_DialogueController.Instance.DisplayMessage("Tyra: ", textToDisplay);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        UI_DialogueController.Instance.Closemessage();
    }
}
