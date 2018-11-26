using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VadisScriptB2 : OB_Interactable {

    bool B2Area = false;

    string[] Conversation = new string[19] { "Vem gömmer sej där? Är du den store vitormen så ska du veta att jag inte är snopen att du är här - jag såg dina spår tidigare!",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        "",
        ""       
    };

    public override void Activate(GameObject player)
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        OnEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnExit(other);
    }
}
