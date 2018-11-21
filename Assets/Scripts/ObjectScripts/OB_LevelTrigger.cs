using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OB_LevelTrigger : MonoBehaviour {

    public string loadName;
    public string unloadName;

    private void OnTriggerEnter(Collider col)
    {
        
        if (loadName != "")
            LevelManager.Instance.Load(loadName);

        if (unloadName != "")
            StartCoroutine("UnloadScene");
    }

    IEnumerator UnloadScene()
    {
        yield return new WaitForSeconds(0.10f);
        LevelManager.Instance.Unload(unloadName);
    }
}
