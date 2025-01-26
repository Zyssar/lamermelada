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
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            roll = Random.Range(0, 10);

            if (roll >= IA)
            {

            }

            Debug.Log(roll + IA);
            if (IA + roll >= 18)
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
}
