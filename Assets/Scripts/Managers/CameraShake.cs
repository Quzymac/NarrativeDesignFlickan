using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {  //Sitter direct på kameran
                                            //Aktivera Camera Shake såhär: CameraShake.Instance.ShakeCamera(.1f, .05f, 5);
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void ShakeCamera(float amount, float duration, float speed = 1)
    {
        StartCoroutine(Shake(amount, duration, speed));
    }
    private IEnumerator Shake(float amount, float duration, float speed = 2)
    {
        float shakeTime = Time.time + duration;
        float xShake, yShake;
        while (Time.time < shakeTime)
        {
            xShake = Random.Range(-1f, 1f) * amount;
            yShake = Random.Range(-1f, 1f) * amount;
            transform.localPosition = new Vector3(xShake, yShake, transform.localPosition.z);
            yield return new WaitForSeconds(.1f / speed);
        }
        transform.localPosition = new Vector3(0, 0, transform.localPosition.z);
    }
    private static CameraShake instance;
    public static CameraShake Instance
    {
        get { return instance; }
    }
}
