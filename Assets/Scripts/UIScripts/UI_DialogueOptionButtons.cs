using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class handles the player making choices in dialogues by pressing buttons.
//PLACE THIS ON A DIALOGUE OPTION BUTTON.
public class UI_DialogueOptionButtons : MonoBehaviour
{
    //Set in inspector.
    [Header("DialogueOptionId")]
    [SerializeField]
    int id;         //Set to match the requested index in dialogueoptions. Starts at 0.

    [Header("ThisButton")]
    [SerializeField]
    private Button button;

    [Header("Keybind")]
    [SerializeField]
    private KeyCode key;

    //<Summary>
    //This method is linked to the button in the inspector and initiates a dialogue choice when pressed.
    //Arguments: void.
    //Return: void.
    //<Summary>
    public void OnPress()
    {
        if(DialogueManager.Instance.Message().ChoiceNames[id] != null)
        {
            OptionsManager.Instance.SetOptionArea1(DialogueManager.Instance.Message().ChoiceNames[id], DialogueManager.Instance.Message().WorldChoices[id]);
        }
        UI_DialogueController.Instance.SetDialogue(DialogueManager.Instance.DialogueOption(id));
        DialogueManager.Instance.DialogueIndex = DialogueManager.Instance.ActiveDialogues[DialogueManager.Instance.DialogueIndex].DialogueOptionsIndexes[id];   //Sets the next index. Split this into its own
        DialogueManager.Instance.IsResponding = true;//more descriptive method.
        UI_DialogueController.Instance.DisableOptionButtons();
        UI_DialogueController.Instance.SetNextPageText();
    }

    //This chesks for keypresses matching the serialized key every frame and invokes onpress
    private void Update()   //Check for key presses
    {
        if (DialogueManager.Instance.HasOptions() && Input.GetKeyDown(key))
        {
            // Click on a button.

            FadeToColor(button.colors.pressedColor);
            button.onClick.Invoke();
        }
        else if (Input.GetKeyUp(key))
            FadeToColor(button.colors.normalColor);
    }

    //<Summary>
    //This method fakes the colour effect of a button press when using a keybind instead.
    //Arguments: A colour to fade to.
    //Return: void.
    //<Summary>
    private void FadeToColor(Color color)   //Fake button press with keys.
    {
        Graphic graphic = GetComponent<Graphic>();
        graphic.CrossFadeColor(color, button.colors.fadeDuration, true, true);
    }
}
