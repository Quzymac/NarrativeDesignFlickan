using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Ska testa så att alla items har en gemensam beskriving så alla cloner av ett item kan användas för quest.
public enum Item {Mushroom, Ball} //Describes item type
public class OB_Item : MonoBehaviour {

    [SerializeField]
    Item type;

    [SerializeField]
    Sprite invPicture;

    public Item GetItemType()
    {
        return type;
    }

    public Sprite GetInvImage()
    {
        return invPicture;
    }
}
