using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCursor : MonoBehaviour {

	void Start () {
        Cursor.lockState = CursorLockMode.None;
    }
}
