using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BubbleController : MonoBehaviour
{
    public float timeToPop = 1.5f; 
    public Sprite bubblePop;
    private int hp;
    public bool multiplierActive = false;
    public Sprite bubbleSprite;
    public int multiplier = 2;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            stopPop();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine(continuePop());
        }
    }
    private void Start()
    {
        StartCoroutine(startPop());
    }

    private IEnumerator startPop()
    {
        hp = transform.childCount;
        while (hp > 0)
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
        Debug.Log("dead");

        yield return null;

    }

    private IEnumerator PopBubble(Transform bubble)
    {
        float memeTimeToPop = timeToPop;
        Vector3 initialScale = bubble.localScale;
        Vector3 targetScale = Vector3.zero;
        float elapsedTime = 0f;

        if (multiplierActive)
        {
            memeTimeToPop /= multiplier;
        }

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
        Image bubbleImage = bubble.GetComponent<Image>();
        if (bubbleImage != null)
        {
            bubbleImage.sprite = bubblePop;
        }
        bubble.localScale = new Vector3(initialScale.x * 0.75f, initialScale.y * 0.75f, 0);
        yield return new WaitForSeconds(0.5f); 
        bubble.gameObject.SetActive(false);
        hp -= 1;
    }

    private IEnumerator continuePop()
    {
        int amount=0;
        Transform[] activeBubbles = new Transform[transform.childCount];
        for (int i = 0; i < activeBubbles.Length; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                amount++;
                activeBubbles[i] = transform.GetChild(i);
                Debug.Log(activeBubbles[i]);
            }
        }

        for (int i = amount - 1; i >= 0; i--)
        {
            Debug.Log(amount.ToString());
            yield return StartCoroutine(PopBubble(activeBubbles[i]));
        }

    }

    private void stopPop()
    {
        int index=0;
        StopAllCoroutines();
        Transform[] activeBubbles = new Transform[transform.childCount];
        for (int i = 0; i < activeBubbles.Length; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                index++;
                activeBubbles[i] = transform.GetChild(i);
                Debug.Log(activeBubbles[i]);
            }
        }
        activeBubbles[index - 1].localScale = new Vector3(1,1,1);
        activeBubbles[index - 1].GetComponent<Image>().sprite = bubbleSprite;
    }

    public void restart()
    {
        StopAllCoroutines();
        Transform[] bubbles = new Transform[transform.childCount];
        for (int i = 0; i < bubbles.Length; i++)
        {
            Transform currentBubble = transform.GetChild(i);
            currentBubble.gameObject.SetActive(true);
            currentBubble.localScale = new Vector3(1, 1, 1);
            currentBubble.GetComponent<Image>().sprite = bubbleSprite;
        }
        StartCoroutine(startPop());
    }

    public bool isDead()
    {
        return (hp <= 0);
    }

    public IEnumerator damageBubble(int amount)
    {
        int bubbleAmount = 0;
        stopPop();
        Transform[] activeBubbles = new Transform[transform.childCount];
        for(int i = 0; i< activeBubbles.Length-1; i++)
        {
            bubbleAmount++;
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                activeBubbles[i] = transform.GetChild(i);
            }
        }

        if (bubbleAmount <= amount)
        {
            for(int i= 0; i<bubbleAmount; i++)
            {
                activeBubbles[i].GetComponent<Image>().sprite = bubblePop;
            }
            yield return new WaitForSeconds(1);
            for (int i = 0; i < bubbleAmount; i++)
            {
                activeBubbles[i].gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = bubbleAmount; i >= bubbleAmount-amount ; i--)
            {
                activeBubbles[i].GetComponent<Image>().sprite = bubblePop;
                yield return new WaitForSeconds(0.05f);
                activeBubbles[i].gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(0.2f);
            yield return StartCoroutine(continuePop());
        }
    }

    public void regenerateBubble(int amount)
    {

    }
}
