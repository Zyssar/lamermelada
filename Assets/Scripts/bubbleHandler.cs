using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbleHandler : MonoBehaviour
{
    public GameObject bubbleGameObject;
    public GameObject varyingBubble;
    public float minRange = -5f;
    public float maxRange = 5f;
    public float speedMinRange = 0.05f;
    public float speedMaxRange = 0.2f;
    public float rangeMinRange = 1f;
    public float rangeMaxRange = 3f;
    public float randMinRange = 0f;
    public float randMaxRange = 5f;
    private float spawnInterval = 1f;

    public float varyingSpawnInterval = 1f;
    public float liveTime = 5f;

    private void Start()
    {
        StartCoroutine(SpawnBubbles());
        StartCoroutine(SpawnVaryingBubbles());
    }

    IEnumerator SpawnBubbles()
    {
        while (true)
        {
            spawnInterval = Random.Range(randMinRange, randMaxRange);
            GameObject currentInstance = Instantiate(bubbleGameObject);
            basicBubbleBehaviour bubbleBehaviour = currentInstance.GetComponent<basicBubbleBehaviour>();

            float startX = Random.Range(minRange, maxRange);
            currentInstance.transform.position = new Vector3(startX, -11f, 0f);

            float speed = Random.Range(speedMinRange, speedMaxRange);
            float range = Random.Range(rangeMinRange, rangeMaxRange);
            bubbleBehaviour.InitializeBubble(speed, range, liveTime);

            yield return new WaitForSeconds(spawnInterval);
        }

    }
    IEnumerator SpawnVaryingBubbles()
    {
        while (true)
        {
            spawnInterval = Random.Range(randMinRange, randMaxRange);
            GameObject currentInstance = Instantiate(varyingBubble);
            VaryingSizeBubbleBehaviour bubbleBehaviour = currentInstance.GetComponent<VaryingSizeBubbleBehaviour>();

            float startX = Random.Range(minRange, maxRange);
            currentInstance.transform.position = new Vector3(startX, -11f, 0f);

            float speed = Random.Range(speedMinRange, speedMaxRange);
            float range = Random.Range(rangeMinRange, rangeMaxRange);
            bubbleBehaviour.InitializeBubble(speed, range, liveTime);

            yield return new WaitForSeconds(varyingSpawnInterval);
        }

    }
}
