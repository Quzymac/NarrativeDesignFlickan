using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Ska testa så att alla items har en gemensam beskriving så alla cloner av ett item kan användas för quest.
public enum Item {Apple, Blueberry, Lingonberry, Chanterelle, Falukorv, Birch_polypore, Fly_agaric, Gulfotshatta, Pine_cone, Fir_cone, Wine, BlueberryBush, LingonBush } //Beskriver item type, lägg till med nya sorters items
public class OB_Item : OB_Interactable {

    [SerializeField]    //Ställ in i inspektorn vilket sorts item detta är.
    Item type;
    [SerializeField]    //Lägg till den sprite som objektet ska ha i UI Inventorys
    Sprite invPicture;
    [SerializeField]
    Sounds sound;
    AudioHandler audioHandler;
    public bool Respawn { get; set; }

    // Till den punkt där objektet ska flyttas. Rekommenderar ett tomt objekt. Objektet måste ha en transform. Används i B3
    Transform target;
    float startTime = 0f;
    GameObject scriptManager;
    

    private void Start()
    {
        audioHandler = FindObjectOfType<AudioHandler>();
        scriptManager = FindObjectOfType<UI_FadingEffect>().gameObject;
        target = FindObjectOfType<FairyFoodCollecting>().GetFairyChest();
        if (type == Item.Apple || type == Item.BlueberryBush || type == Item.LingonBush)
            Respawn = true;
    }

    public Item GetItemType()
    {
        return type;
    }

    public Sprite GetInvImage()
    {
        return invPicture;
    }

    private void OnTriggerEnter(Collider other)
    {
        OnEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnExit(other);
    }

    public override void Activate(GameObject player)
    {
        if (scriptManager.GetComponent<FairyFoodCollecting>().B3MiniGameActive) //Only in use during b3 mini game
        {
            StartCoroutine(MoveObject(player));
        }
        else // Normal item pickup
        {
            CH_PlayerMovement ch_movement = player.GetComponent<CH_PlayerMovement>();
            if (player.GetComponent<CH_Inventory>().AddItem(gameObject))
            {
                ch_movement.Pickup = true;
     
                if (audioHandler != null)
                    audioHandler.PlaySound(sound);

                if (gameObject.GetComponent<Rigidbody>() != null && gameObject.GetComponent<Rigidbody>().isKinematic == true)
                {
                    gameObject.GetComponent<Rigidbody>().isKinematic = false;
                }
                player.GetComponent<CH_Interact>().RemoveInteractable(this);
                gameObject.SetActive(false);
            }
        }
    }

    //Coroutinen körs tills objektet har flyttats fram till target.
    IEnumerator MoveObject(GameObject player)
    {
        startTime = 0f;
        gameObject.GetComponent<Collider>().enabled = false;
        while (true)
        {
            startTime += Time.deltaTime;
            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target.position, startTime);
            if (gameObject.transform.position == target.position)
            {
                player.GetComponent<CH_Interact>().RemoveInteractable(this);

                target.gameObject.GetComponent<OB_TransportTarget>().SetObject(gameObject);
                gameObject.GetComponent<Collider>().enabled = true;
                gameObject.SetActive(false);

                yield break;
            }

            yield return null;
        }
    }
}
