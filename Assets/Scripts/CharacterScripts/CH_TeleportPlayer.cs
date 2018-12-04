using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CH_TeleportPlayer : OB_Interactable {

    [SerializeField] Transform position1;
    [SerializeField]
    bool lockCamera;

    UI_FadingEffect fadeCanvas;

    private void Start()
    {
        fadeCanvas = FindObjectOfType<UI_FadingEffect>();
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
        StartCoroutine(Bla(player));
        

    }

    IEnumerator Bla(GameObject player)
    {
        fadeCanvas.ActivateFading();
        yield return new WaitForSeconds(1);
        player.transform.position = position1.position;
        player.GetComponent<CH_PlayerCamera>().LockCamera(lockCamera);
        fadeCanvas.DeactivateFading();
    }
}