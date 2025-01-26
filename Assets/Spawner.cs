using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public int IA = 1;
    public bool active = true;
    public GameObject[] enemiesGO;
    public Enemy[] enemies;
    private int roll;

    // Start is called before the first frame update
    void Start()
    {
        IA = Random.Range(1, 10);

        enemies = new Enemy[enemiesGO.Length];

        // Populate the enemies array with Enemies components
        for (int i = 0; i < enemiesGO.Length; i++)
        {
            Enemy enemyComponent = enemiesGO[i].GetComponent<Enemy>();
            if (enemyComponent != null)
            {
                enemies[i] = enemyComponent;
            }
            else
            {
                Debug.LogWarning($"GameObject {enemiesGO[i].name} does not have an Enemies component.");
            }
        }

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
                if (IA + roll >= 12)
                {

                    foreach (Enemy enemy in enemies)
                    {
                        if (enemy.difficulty == 4)
                            Instantiate(enemy, transform.position, Quaternion.identity);
                    }
                }
                else
                {
                }
            }

            Debug.Log(roll + IA);
            
        }
    }
}
