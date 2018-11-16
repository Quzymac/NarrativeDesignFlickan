using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;

//This enum is slightly clunky, but works. Set on interactable object you wish to examine/ talk to. Might need to be changed later on if things get complicated.
public enum Dialogues { NONE, TestDialogue }

//This class handles the currently active dialogue. It stores all indivdual Dialogue data containers in a list. 
//TODO: Perhaps LoadData region should be made into its own script?
public class DialogueManager
{
    private List<Dialogue> activeDialogues = new List<Dialogue>();
    public List<Dialogue> ActiveDialogues { get { return activeDialogues; } set { activeDialogues = value; } }

    private int dialogueIndex = 0;
    public int DialogueIndex { set { dialogueIndex = value; } }

    //private bool isFirstSentence;
 
    //This region sets up the singelton pattern for easy access and safety.
    #region Singelton
    private static readonly DialogueManager instance = new DialogueManager();

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static DialogueManager()
    {
    }

    private DialogueManager()
    {
    }

    public static DialogueManager Instance{
        get { return instance; } }
    #endregion

    //Maybe this should be in its own script.
    #region LoadData 
    //<Summary>
    //This method loads dialogue data from an xml file and adds it to a list. 
    //--------------------------------------------------------------------------------
    //IMPORTANT: Make sure the xml file is named according to "dialogueObject.xml" else the file wont be found.
    //--------------------------------------------------------------------------------
    //Arguments: An enum corresponding to the file to read.
    //Return: A list containing all received Dialogue data containers.
    //<Summary>
    public List<Dialogue> LoadDialogues(Dialogues dialogueObject)
    {
        //isFirstSentence = true;
        List<Dialogue> dialogues = new List<Dialogue>();
        Dialogue dialogue = null; 
        TextAsset file = Resources.Load<TextAsset>(dialogueObject.ToString());
        if (file != null)
        {
            using (StringReader stringReader = new StringReader(file.text))
            {
                using (XmlReader reader = XmlReader.Create(stringReader))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name)                                
                            {
                                case "Name":
                                    dialogue = new Dialogue();
                                    dialogues.Add(dialogue);
                                    dialogue.Name = reader["name"];
                                    break;
                                case "Text":
                                    dialogue.Text = reader["text"];
                                    break;
                                case "Choices":
                                    dialogue.Choises.Add(reader["choice"]);
                                    break;
                            }
                        }
                    }
                    reader.Close();
                }
                stringReader.Close();
            }
        }
        return dialogues;
    }
    #endregion

    public void NextDialogue()  //WORK IN PROGRESS
    {
        if (HasRemaningMessages())   
        {
            NextMessage();
        }
    }

    //<Summary>
    //This method retrievs the dialogue elemtn at the current dialogueindex.
    //Arguments: void.
    //Return: The dialog element at the current index.
    //<Summary>
    public Dialogue Message()
    {
        return ActiveDialogues[dialogueIndex];
    }

    //<Summary>
    //This method checks if there are any remaining dialogue elements waiting to be displayed.
    //Arguments: void.
    //Return: True if there are more elements to display, false if there are none.
    //<Summary>
    public bool HasRemaningMessages()
    {
        return dialogueIndex + 1 < ActiveDialogues.Count;
    }

    //<Summary>
    //This method moves the dialogue index one step.
    //Arguments: void.
    //Return: void.
    //<Summary>
    private void NextMessage()
    {
        dialogueIndex = (dialogueIndex + 1) % ActiveDialogues.Count;
    }
}
