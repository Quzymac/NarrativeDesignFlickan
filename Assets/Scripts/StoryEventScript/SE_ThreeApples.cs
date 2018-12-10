using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SE_ThreeApples : MonoBehaviour {

	private enum Status { Waiting, Dialog, Active, Ready, Complete };
    private Status currentState = Status.Waiting;
    [SerializeField]
    private string[] mainDialogs, appleDialogs, alreadyApplesDialog;
    private int curDialog = 0;
    private CH_Inventory inventory;
    int apples = 0;
    [SerializeField]
    CH_MonsterPatrol monster;
    [SerializeField]
    SE_Gloson gloson;
    [SerializeField]
    private GameObject osynligVägg;
    //Referens till osynliga väggar

    private void OnEnable()
    {
        OptionsManager.NewChoice += PickUpApple;
    }

    private void OnDestroy()
    {
        OptionsManager.NewChoice -= PickUpApple;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (currentState == Status.Waiting)
            {
                other.GetComponent<CH_PlayerCamera>().LookAtSomething(transform, 3);
                currentState = Status.Dialog;
                Dialog();
            }
        }
    }
    private void StartQuest()
    {
        inventory = FindObjectOfType<CH_Inventory>();
        Dialog();
        currentState = Status.Active;
    }
    private void FoundThreeApples()
    {
        //Stoppa grisen och gör så att man kan prata med den
        currentState = Status.Ready;
        monster.GetComponent<NavMeshAgent>().isStopped = true;
        monster.enabled = false;
        gloson.CanGive(this);
    }
    public void CompleteQuest()
    {
        //YAY!
        currentState = Status.Complete;
        Destroy(osynligVägg);
        //Destroy(gloson.gameObject);
        //Stänga av osynliga väggar
    }
    public void PickUpApple(object sender, OptionsEventArgs e)
    {
        if (currentState == Status.Active && e.Option == "Items")
        {
            if (apples != inventory.NumberOfSpecificItem(Item.Apple))
            {
                Dialog();
            }
        }
    }
    //När man går nära grisen så säger spelaren saker,
    //några sekunder senare så säger den lite mer,
    //några sekunder senare så aktiveras questet och om man redan har några äpplen så säger spelaren saker
    //Metod som får reda på när man plockar upp något
    //Dialog beroende på antal äpplen
    //If 3 äpplen så stannar gloson och man kan prata med den, äpplena 
    private void Dialog()
    {
        switch (curDialog)
        {
            case 0:
                UI_DialogueController.Instance.DisplayMessage("Tyra", mainDialogs[curDialog]);
                curDialog++;
                Invoke("Dialog", 5);
                break;
            case 1:
                UI_DialogueController.Instance.DisplayMessage("Tyra", mainDialogs[curDialog]);
                curDialog++;
                Invoke("Dialog", 5);
                break;
            case 2:
                UI_DialogueController.Instance.DisplayMessage("Tyra", mainDialogs[curDialog]);
                curDialog++;
                Invoke("Dialog", 2);
                break;
            case 3:
                UI_DialogueController.Instance.DisplayMessage("Tyra", mainDialogs[curDialog], 8);
                curDialog++;
                Invoke("StartQuest", 9);
                break;
            case 4:
                apples = inventory.NumberOfSpecificItem(Item.Apple);
                Debug.Log(apples);
                if (apples > 0)
                {
                    if (apples < 3)
                    {
                        Debug.Log("kakor");
                        UI_DialogueController.Instance.DisplayMessage("Tyra", alreadyApplesDialog[apples-1], 5);
                    } else
                    {
                        UI_DialogueController.Instance.DisplayMessage("Tyra", alreadyApplesDialog[3-1], 5);
                        FoundThreeApples();
                    }
                }
                curDialog++;
                break;
            case 5:
                apples = inventory.NumberOfSpecificItem(Item.Apple);
                if (apples <= 3)
                {
                    UI_DialogueController.Instance.DisplayMessage("Tyra", appleDialogs[apples - 1], 5);
                    if (apples == 3)
                        FoundThreeApples();
                }
                break;
        }
    }
}
