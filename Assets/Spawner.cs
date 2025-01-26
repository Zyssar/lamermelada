using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int IA = 1;
    public bool active = true;
    public Enemy[] enemies;
    private int roll;

    // Start is called before the first frame update
    void Start()
    {
        IA = Random.Range(1, 10);
        Debug.Log("IA: " +  IA.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(30/IA);
        roll = Random.Range(0, 10);
        Debug.Log(roll.ToString());
        if (IA + roll >=15)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.difficulty == 4)
                    Instantiate(enemy, transform.position, Quaternion.identity);
            }
        }
        else
        {
            Debug.Log("XD");
        }
    }
}
