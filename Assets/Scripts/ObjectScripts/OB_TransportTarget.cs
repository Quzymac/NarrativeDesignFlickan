using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Placeras på det objekt som ska få ett annat objekt via transport
public class OB_TransportTarget : MonoBehaviour {

    GameObject transportedItem;

    public bool SetObject(GameObject inObject)
    {
        if (inObject != null)
        {
            transportedItem = inObject;
            Debug.Log("I have a " + transportedItem.name);
            return true;
        }
        else
            return false;
    }



}
