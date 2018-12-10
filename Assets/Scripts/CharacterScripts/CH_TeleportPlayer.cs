using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CH_TeleportPlayer : OB_Interactable {

    [SerializeField] Transform position1;
    [SerializeField]
    bool lockCamera;
    AudioSource audio;


    UI_FadingEffect fadeCanvas;

    private void Start()
    {
        fadeCanvas = FindObjectOfType<UI_FadingEffect>();
        audio = GetComponent<AudioSource>();

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
        StartCoroutine(ActivateDoor(player));
    }

    IEnumerator ActivateDoor(GameObject player)
    {
        fadeCanvas.ActivateFading();
        yield return new WaitForSeconds(1f);
        audio.Play();
        player.transform.position = position1.position;
        player.GetComponent<CH_PlayerCamera>().LockCamera(lockCamera);
        fadeCanvas.DeactivateFading();
    }
}