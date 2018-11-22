using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is a data container for a loaded dialogue file. TODO: Change the text data to be an array? maybe yes maybe no?
public class Dialogue
{
    //Data
    private string text;                                                  
    private string name;
    private List<string> choiceNames = new List<string>();           //if the dialogue option should have an effect on the gameworld this needs to be used to determine which choice to set values on.
    private List<string> dialogueOptions = new List<string>();      //This should be on the buttons to display what the differnet choices for the player to say are. Name element for these are always player character.
    private List<int> dialogueOptionsIndexes = new List<int>();     //This should link to the next non player dialogue in the DialogueManager.
    private List<int> worldChoices = new List<int>();               //This represents the valuse to set on the different option variables as defined in choicenames.

    //Properties
    public string Name { get { return name; } set { name = value; } }
    public string Text { get { return text; } set { text = value; } }
    public List<string> ChoiceNames { get { return choiceNames; } set { choiceNames = value; } }
    public List<string> DialogueOptions { get { return dialogueOptions; } set { dialogueOptions = value; } }
    public List<int> DialogueOptionsIndexes { get { return dialogueOptionsIndexes; } set { dialogueOptionsIndexes = value; } }
    public List<int> WorldChoices { get { return worldChoices; } set { worldChoices = value; } }
}
