using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml;

//This enum is slightly clunky, but works. Set on interactable object you wish to examine/ talk to. Might need to be changed later on if things get complicated.
public enum Dialogues { NONE, TestDialogue }

//This class handles the currently active dialogue. It stores all indivdual Dialogue data containers in a list. 
//TODO: Add data manipulation abbilities such as determining which line of dialogue to display next. Perhaps LoadData region should be made into its own script?
public class DialogueManager
{
    private List<Dialogue> activeDialogues = new List<Dialogue>();
    public List<Dialogue> ActiveDialogues { get { return activeDialogues; } set { activeDialogues = value; } }

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
    //IMPORTANT: Make sure the xml file is named according to "dialogueObject.xml" else a the file wont be found.
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

    public Dialogue NextDialogue()  //WORK IN PROGRESS
    {
        Dialogue nextDialogue = null;
        if (/*isFirstSentence*/ true)   //Use a a switch here instead. might be a few ifs..
        {
            /*isFirstSentence = false;*/
            if(activeDialogues.Count > 0)
            {
                nextDialogue = activeDialogues[0];
                activeDialogues.RemoveAt(0);
            }
        }
        return nextDialogue;
    }
}
