using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class handles the player making choices in dialogues by pressing buttons.
public class UI_DialogueOptionButtons : MonoBehaviour
{
    //Set in inspector.
    [Header("DialogueOptionId")]
    [SerializeField]
    int id;         //Set to match the requested index in dialogueoptions. Starts at 0.
    [SerializeField]
    [Header("ThisButton")]
    private Button button;
    [Header("Keybind")]
    [SerializeField]
    private KeyCode key;

    public void OnPress()
    {
        UI_DialogueController.Instance.SetDialogue(DialogueManager.Instance.DialogueOption(id));
        DialogueManager.Instance.DialogueIndex = DialogueManager.Instance.ActiveDialogues[DialogueManager.Instance.DialogueIndex].DialogueOptionsIndexes[id];   //Sets the next index. Split this into its own
        DialogueManager.Instance.IsResponding = true;//more descriptive method.
        UI_DialogueController.Instance.DisableOptionButtons();
    }

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

    private void FadeToColor(Color color)   //Fake button press with keys.
    {
        Graphic graphic = GetComponent<Graphic>();
        graphic.CrossFadeColor(color, button.colors.fadeDuration, true, true);
    }
}
