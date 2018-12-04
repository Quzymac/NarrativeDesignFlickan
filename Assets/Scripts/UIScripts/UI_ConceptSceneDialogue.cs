using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ConceptSceneDialogue : MonoBehaviour
{
    private Dialogues dialogue;

    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    private void Awake()
    {
        //We do not want multiple controllers in one scene. THERE CAN BE ONLY ONE!
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        // DontDestroyOnLoad(this);    //Carry the same dialogue manager (this object) over to the next scene instead of destroying it. This needs to be called on root object: probably a good idea to move it!
        //
    }

    //This region sets up the singelton pattern for easy access and safety. The instance is set in awake since this is a monobehaviour. This also means the field cannot be marked as readonly.    
    #region Singelton
    private static UI_ConceptSceneDialogue instance;

    public static UI_ConceptSceneDialogue Instance
    {
        get { return instance; }
    }

    #endregion

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E) && dialogue != Dialogues.NONE)
        {
            UI_DialogueController.Instance.DisplayTextBox(dialogue);
        }
	}

    public void CurrentDialogue(Dialogues dialogue)
    {
        this.dialogue = dialogue;
    }

    public void StartDialogue()
    {

    }
}
