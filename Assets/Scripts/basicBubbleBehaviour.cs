using System.Collections;
using UnityEngine;

public class basicBubbleBehaviour : MonoBehaviour
{
    private float direction;
    private float speed;
    private float range;
    private float livetime;

    private int frameCount;
    private float elapsedTime;
    public void InitializeBubble(float speed, float range, float livetime)
    {
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
            direction = Mathf.Sin(frameCount * speed);
            transform.position += new Vector3(direction * range, 0.01f, 0);
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
