using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is a data container for a loaded dialogue file. TODO: Change the text data to be an array? maybe yes maybe no?
public class Dialogue
{
    private string text;
    private string name;
    private List<string> choices = new List<string>();

    public string Name { get { return name; } set { name = value; } }
    public string Text { get { return text; } set { text = value; } }
    public List<string> Choises { get { return choices; } set { choices = value; } }

}
