using System.Collections;
using UnityEngine;

public class basicBubbleBehaviour : MonoBehaviour
{
    private float direction;
    private float speed;
    private float range;
    private float livetime;

    private float elapsedTime;
    private float speedMultiplier;
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

            speedMultiplier += Time.deltaTime * 0.1f;

            direction = Mathf.Sin(elapsedTime * speed * speedMultiplier);
            transform.position += new Vector3(direction * range, 2f * Time.fixedDeltaTime, 0);
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
