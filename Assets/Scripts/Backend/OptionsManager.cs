using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

//This class handles storing the different choices made by the player.
public class OptionsManager
{
    //DataArea1
    //Define all bools to keep track of options here.    
    //0 = false, 1 = true. If more options are nessecary they can be counted up as 3, 4 ,5 etc.
    private Dictionary<string, int> area1Bools = new Dictionary<string, int>() {
        { "test", 0}
    };

    //PropertiesArea1
    public Dictionary<string, int> Area1Bools{ get { return area1Bools; } set { area1Bools = value; } }

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
        if (Area1Bools.ContainsKey(optionName))
        {
            Area1Bools[optionName] = value;
        }
    }

    //<Summary>
    //This method Gets the value of the option specified by name.
    //Arguments: The options name as a string.
    //Return: The options value as an int.
    //<Summary>
    public int GetOptionArea1(string option)
    {
        return Area1Bools[option];
    }
}
