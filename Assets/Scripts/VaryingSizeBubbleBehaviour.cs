using System.Collections;
using UnityEngine;

public class VaryingSizeBubbleBehaviour : MonoBehaviour
{
    private float direction;
    private float speed;
    private float range;
    private float livetime;
    public int level=1;

    private float elapsedTime;
    private float speedMultiplier;
    private int randomSize;

    public void InitializeBubble(float speed, float range, float livetime)
    {
        this.speed = speed;
        this.range = range;
        this.livetime = livetime;

        elapsedTime = 0f;
        speedMultiplier = 0f;

        randomSize = (int)Random.Range(1f, 5f);

        switch (randomSize)
        {
            case 2:
                level = 2;
                transform.localScale = new Vector3(0.6f, 0.6f);
                break;
            case 3:
                level = 3;
                transform.localScale = new Vector3(0.8f, 0.8f);
                break;
            case 4:
                level = 4;
                transform.localScale = new Vector3(1f, 1f);
                break;
            default:
                level = 1;
                transform.localScale = new Vector3(0.4f, 0.4f);
                break;
        }
        StartCoroutine(MoveBubble());
        
    }

    IEnumerator MoveBubble()
    {
        Debug.Log("started corutine");
        while (elapsedTime <= livetime)
        {
            elapsedTime += Time.deltaTime;

            speedMultiplier += Time.deltaTime * 0.1f;

            direction = Mathf.Sin(elapsedTime * speed * speedMultiplier);

            transform.position += new Vector3(direction * range, 2f * Time.fixedDeltaTime, 0);

            yield return null;
        }
        Destroy(this.gameObject);
    }

}
