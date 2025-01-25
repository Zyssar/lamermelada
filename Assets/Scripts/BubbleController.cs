using System.Collections;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    public float timeToPop = 1.5f; 
    public Sprite bubblePop;

    private void Start()
    {
        StartCoroutine(Pop());
    }

    private IEnumerator Pop()
    {
        Transform[] bubbles = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            bubbles[i] = transform.GetChild(i);
        }

        for (int i = bubbles.Length - 1; i >= 0; i--)
        {
            yield return StartCoroutine(PopBubble(bubbles[i]));
        }

    }

    private IEnumerator PopBubble(Transform bubble)
    {
        float memeTimeToPop = timeToPop;
        Vector3 initialScale = bubble.localScale;
        Vector3 targetScale = Vector3.zero;
        float elapsedTime = 0f;

        switch (bubble.name)
        {
            case "Bubble0":
                {
                    memeTimeToPop *= 2f;
                    break;
                }
            case "Bubble1":
                {
                    memeTimeToPop *= 1.5f;
                    break;
                }
        }

        while (elapsedTime < memeTimeToPop)
        {
            bubble.localScale = Vector3.Lerp(initialScale, targetScale, elapsedTime / memeTimeToPop);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        bubble.localScale = targetScale; 
        bubble.GetComponent<SpriteRenderer>().sprite = bubblePop;
        bubble.localScale = new Vector3(initialScale.x / 2, initialScale.y / 2, 0);
        yield return new WaitForSeconds(0.5f); 
        bubble.gameObject.SetActive(false);
    }
}
