using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Spawner : MonoBehaviour
{
    public int IA = 1;
    public float timer = 40;
    public float depth = 0;
    private float storedDepth = 0f;
    [SerializeField] DepthController depthController;
    public GameObject[] enemiesGO;
    public Enemy[] enemies;
    public float waitTimer;
    private int roll;


    // Start is called before the first frame update
    void Start()
    {
        IA = Random.Range(0, 20);

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

    private float spawnRateDepths()
    { 
        depth = depthController.KM;

        if (Mathf.Round(depth) >= storedDepth)
        {
            IA += 1;
            storedDepth = Mathf.Round(depth);
        }

        return Mathf.Clamp(depth, 0, timer);
    }


    IEnumerator Spawn()
    {
        while (true)
        {
            waitTimer = Mathf.Clamp(timer / spawnRateDepths(), 0, 50 - IA);
            Debug.Log(waitTimer);
            yield return new WaitForSeconds(waitTimer);
            roll = Random.Range(0, 20);
            Debug.Log("Tiro: " + roll.ToString());
            Debug.Log("IA " + IA.ToString());
            if (roll >= IA)
            {
                if (IA + roll >= Mathf.Clamp(20/depth, 0, 20))
                {

                    foreach (Enemy enemy in enemies)
                    {
                        if (enemy.difficulty == 4)
                            Instantiate(enemy, transform.position, Quaternion.identity);
                    }
                }
                else
                {
                    Debug.Log("troleo");
                }
            }

            Debug.Log(roll + IA);
            
        }
    }
}
