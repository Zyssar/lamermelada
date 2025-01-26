using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class VaryingSizeBubbleBehaviour : MonoBehaviour
{
    private float direction;
    private float speed;
    private float range;
    private float livetime;

    private int frameCount;
    private float elapsedTime;
    private float speedMultiplier;
    private int randomSize;

    private void Update()
    {
     randomSize= (int)Random.Range(1f, 5f);
    }
    public void InitializeBubble(float speed, float range, float livetime)
    {

        switch (randomSize) {
            case 2:
                transform.localScale = new Vector3(0.6f, 0.6f);
                break;
            case 3:
                transform.localScale = new Vector3(0.8f, 0.8f);
                break;
            case 4:
                transform.localScale = new Vector3(1f, 1f);
                break;
        }

        this.speed = speed;
        this.range = range;
        this.livetime = livetime;
        StartCoroutine(MoveBubble());
        
    }

    IEnumerator MoveBubble()
    {
        while (elapsedTime <= livetime)
        {
            elapsedTime += Time.deltaTime;
            frameCount++;

            speedMultiplier += Time.deltaTime * 0.1f;

            direction = Mathf.Sin(frameCount * speed * speedMultiplier);
            transform.position += new Vector3(direction * range, 2f * Time.fixedDeltaTime, 0);
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
