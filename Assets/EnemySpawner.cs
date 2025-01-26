using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] public GameObject[] Enemies;
    [SerializeField] public Transform playerPos;
    public Spawner[] Spawners;
    public float minSpawnTime;
    public float maxSpawnTime;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Spawner spawner in Spawners) {

            spawner.difficulty = Random.Range(0,4);
        }
    }
}

public class Enemy : MonoBehaviour
{
    public float spawnRate = 1f;

    public void Start()
    {
        
    }

    public void Action()
    {

    }
}
