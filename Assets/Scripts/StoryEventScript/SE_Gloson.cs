using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_Gloson : OB_Interactable {

    private CH_Inventory inventory;
    private SE_ThreeApples storyEvent;
    private bool active = false;

    public void CanGive(SE_ThreeApples storyEvent)
    {
        inventory = FindObjectOfType<CH_Inventory>();
        this.storyEvent = storyEvent;
        active = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (active)
        OnEnter(other);
    }
    private void OnTriggerExit(Collider other)
    {
        OnExit(other);
    }
    public override void Activate(GameObject player)
    {
        if (active)
        {
            Debug.Log("GLOSNOMNOMNOMNOMNOM");
            inventory.RemoveItems(Item.Apple, 3);
            player.GetComponent<CH_Interact>().RemoveInteractable(this);
            storyEvent.CompleteQuest();
        }
    }
}
