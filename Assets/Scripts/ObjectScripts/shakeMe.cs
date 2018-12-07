using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shakeMe : MonoBehaviour {

    [SerializeField] float offset;
	void Awake () {
        origin = transform.position;
        newPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + offset);
	}

    private void Start()
    {
        bushSound.Play();
        StartCoroutine(ShakeMeDaddy());
    }

    void Update () {
		
	}

    [SerializeField] AudioSource bushSound;
    Vector3 origin;
    Vector3 newPos;
    IEnumerator ShakeMeDaddy()
    {
        yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));
        transform.position = origin;
        if ((int)Random.Range(0, 2) == 0)
        {
            yield return new WaitForSeconds(Random.Range(0.01f, 0.1f));
            transform.position = origin;
            yield return new WaitForSeconds(0.1f);
            transform.position = newPos;
        }
        yield return new WaitForSeconds(Random.Range(0.5f, 1.0f));
        transform.position = newPos;
        StartCoroutine(ShakeMeDaddy());
    }
}
