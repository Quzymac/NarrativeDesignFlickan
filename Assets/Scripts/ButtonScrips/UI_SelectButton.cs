using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SelectButton : MonoBehaviour {

    public Button slot1;
    public Button slot2;
    public Button slot3;
    public Button slot4;
    public Button slot5;
    public Button slot6;
    public Button slot7;
    public Button slot8;

	
	// Update is called once per frame
	private void Update () {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            slot1.Select();
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            slot2.Select();
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            slot3.Select();
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            slot4.Select();
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            slot5.Select();
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            slot6.Select();
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            slot7.Select();
        else if (Input.GetKeyDown(KeyCode.Alpha8))
            slot8.Select();

    }
}
