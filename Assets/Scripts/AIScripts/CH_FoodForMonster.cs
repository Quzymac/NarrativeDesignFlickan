using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CH_FoodForMonster : MonoBehaviour {
    Collider col;
    CH_MonsterPatrol monster;
    private void Start()
    {
        col = GetComponent<Collider>();
        monster = GetComponentInParent<CH_MonsterPatrol>();
    }

    bool eaten;
    [SerializeField] List<GameObject> foods;
    float CD;
    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Food")
        {
            if (!eaten)
            {
                col.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                eaten = true;
                foods.Remove(c.gameObject);
                Destroy(c.gameObject);
                if (foods.Count < 1)
                    StartCoroutine(CheckForFood(c.transform.position));
                else
                    StartCoroutine(CheckForFood(foods[0].transform.position));
            }
            else
            {
                if (!foods.Contains(c.gameObject))
                    foods.Add(c.gameObject);
            }

            if (foods.Count > 0)
            {
                monster.NewPosition(foods[0].transform.position);
            }
        }
    }
    [Header("Search for food size")]
    [SerializeField] float growthSize;
    IEnumerator CheckForFood(Vector3 newPos)
    {
        monster.NewPosition(newPos);
        if (foods.Count < 1)
        {
            for (int i = 0; i < 10; i++)
            {
                col.transform.localScale += new Vector3(growthSize, growthSize, growthSize);
                yield return new WaitForSeconds(0.1f);
            }
        }
        eaten = false;
        col.transform.localScale = new Vector3(1, 1, 1);
    }
}
