using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Spawner : MonoBehaviour
{
    public int IA = 1;
    public float timer = 5;
    public float depth = 0;
    private float storedDepth = 0f;
    private int spawningDifficulty = 0;
    [SerializeField] DepthController depthController;

    [SerializeField] private GameObject[] enemiesdf1;
    [SerializeField] private GameObject[] enemiesdf2;
    [SerializeField] private GameObject[] enemiesdf3;
    [SerializeField] private GameObject[] enemiesdf4;
    private GameObject[][] enemiesByDifficulty;
    private float waitTimer;
    private int roll;


    // Start is called before the first frame update
    void Start()
    {
        IA = Random.Range(0, 2);
        Debug.Log(IA);
        enemiesByDifficulty = new GameObject[][] { enemiesdf1, enemiesdf2, enemiesdf3, enemiesdf4 };
        StartCoroutine(Spawn());
    }

    private float getSpawnRate()
    { 
        depth = depthController.KM;

        if (Mathf.Round(depth) > storedDepth)
        {
            IA = Mathf.Clamp(IA+1, 0, 20);
            storedDepth = Mathf.Round(depth);
        }

        return Mathf.Clamp(depth, 1, timer);
    }

    private int CalculateDifficulty()
    {
        float adjustedDepth = Mathf.Max(depth, 1f); // Avoid division by zero
        int depthFactor = Mathf.Clamp((int)(40f / adjustedDepth), 1, 4);

        if (IA + roll >= depthFactor * 4) return 3; // Index for `enemiesdf4`
        if (IA + roll >= depthFactor * 3) return 2; // Index for `enemiesdf3`
        if (IA + roll >= depthFactor * 2) return 1; // Index for `enemiesdf2`
        return 0; // Index for `enemiesdf1`
    }


    IEnumerator Spawn()
    {
        while (true)
        {
            waitTimer = Mathf.Clamp(timer / getSpawnRate(), 0.5f, 5);
            yield return new WaitForSecondsRealtime(waitTimer);
            roll = Mathf.Clamp((int)Random.Range(0, 5+depth), 0, 20);
            Debug.Log(roll);
            if (roll <= IA)
            {
                spawningDifficulty = CalculateDifficulty();
                Debug.Log(spawningDifficulty);
                GameObject[] enemiesToSpawn = enemiesByDifficulty[spawningDifficulty];

                if (enemiesToSpawn == null || enemiesToSpawn.Length == 0)
                {
                    Debug.LogWarning($"No enemies available for difficulty {spawningDifficulty}!");
                }
                else
                {
                    GameObject enemyPrefab = enemiesToSpawn[Random.Range(0, enemiesToSpawn.Length)];

                    if (enemyPrefab != null)
                    {
                        Instantiate(enemyPrefab);
                    }
                    else
                    {
                        Debug.LogWarning($"Null enemy found in difficulty {spawningDifficulty} array!");
                    }
                }

            }
        }
    }
}
