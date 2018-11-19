using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is a data container for a loaded dialogue file. TODO: Change the text data to be an array? maybe yes maybe no?
public class Dialogue
{
    //Data
    private string text;                                                  
    private string name;
    private List<string> dialogueOptions = new List<string>();      //This should be on the buttons to display what the differnet choices for the player to say are. Name element fo these are always player character.
    private List<int> dialogueOptionsIndexes = new List<int>();     //This should link to the next non player dialogue in the DialogueManager.

    //Properties
    public string Name { get { return name; } set { name = value; } }
    public string Text { get { return text; } set { text = value; } }
    public List<string> DialogueOptions { get { return dialogueOptions; } set { dialogueOptions = value; } }
    public List<int> DialogueOptionsIndexes { get { return dialogueOptionsIndexes; } set { dialogueOptionsIndexes = value; } }
}
