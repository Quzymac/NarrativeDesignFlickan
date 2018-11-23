using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

//This class handles storing the different choices made by the player.
public class OptionsManager
{
    public static event EventHandler<OptionsEventArgs> NewChoice;   //Subscribe to this if you wish to receive events about a value chanhing.

    //DataArea1
    //Define all bools to keep track of options here.    
    //0 = false, 1 = true. If more options are nessecary they can be counted up as 3, 4 ,5 etc.
    private Dictionary<string, int> b1Bools = new Dictionary<string, int>() {
        { "test", 0}, { "B1_Alf_1", 0}
    };

    //PropertiesArea1
    public Dictionary<string, int> B1Bools{ get { return b1Bools; } set { b1Bools = value; } }

    //This region sets up the singelton pattern for easy access and safety.
    #region Singelton
    private static readonly OptionsManager instance = new OptionsManager();

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static OptionsManager()
    {
    }

    private OptionsManager()
    {
    }

    public static OptionsManager Instance
    {
        get { return instance; }
    }
    #endregion

    //<Summary>
    //This method sets the value of an option depending on player choices.
    //Arguments: The option to change value of as a string, and the value to change to as an integer.
    //Return: void.
    //<Summary>
    public void SetOptionArea1(string optionName, int value)
    {
        if (B1Bools.ContainsKey(optionName))
        {
            B1Bools[optionName] = value;
            NewChoice(this, new OptionsEventArgs(B1Bools, optionName, value));
        }
    }

    //<Summary>
    //This method Gets the value of the option specified by name.
    //Arguments: The options name as a string.
    //Return: The options value as an int.
    //<Summary>
    public int GetOptionArea1(string option)
    {
        return B1Bools[option];
    }
}

public class OptionsEventArgs : EventArgs
{
    public OptionsEventArgs(Dictionary<string, int> dict, string option, int value)
    {
        Value = value;
        Option = option;
        Dict = dict;
    }

    public int Value;
    public string Option;
    public Dictionary<string, int> Dict;
}
