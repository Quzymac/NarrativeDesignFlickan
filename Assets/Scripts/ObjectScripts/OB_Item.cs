using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ska testa så att alla items har en gemensam beskriving så alla cloner av ett item kan användas för quest.
public enum Item {Mushroom, Ball} //Describes item type
public class OB_Item : MonoBehaviour {

    [SerializeField]
    Item type;

    public Item GetItemType()
    {
        return type;
    }
}
