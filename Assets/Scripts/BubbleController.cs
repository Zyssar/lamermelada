using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public int maxHp = 15;


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
        if (Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(damageBubble(2));
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            regenerateBubble(3);
        }

    }
    private void Start()
    {
        for (int i = 0; i < maxHp; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        StartCoroutine(startPop());
    }

    private IEnumerator startPop()
    {
        hp = GetActiveBubbles().Count;
        if (hp <= 0 || GetActiveBubbles().Count == 0)
        {
            Debug.Log("No hay burbujas para procesar.");
            yield break;
        }
        while (hp > 0)
        {
            List<Transform> bubbles = GetActiveBubbles();
            for (int i = 0; i < bubbles.Count; i++)
            {
                bubbles[i] = transform.GetChild(i);
            }

            for (int i = bubbles.Count - 1; i >= 0; i--)
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
        List<Transform> activeBubbles = GetActiveBubbles();
        amount = activeBubbles.Count;

        if (amount == 0 || hp <=0)
        {
            Debug.Log("No hay burbujas activas para continuar con el estallido.");
            yield break;
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
        List<Transform> activeBubbles = GetActiveBubbles();
        index = activeBubbles.Count;
        if (index > 0)
        {
            ResetBubble(activeBubbles[index - 1]);
        }

    }

    public void restart()
    {
        StopAllCoroutines();
        Transform[] bubbles = new Transform[transform.childCount];
        for (int i = 0; i < bubbles.Length; i++)
        {
            Transform currentBubble = transform.GetChild(i);
            currentBubble.gameObject.SetActive(true);
            ResetBubble(currentBubble);
        }
        StartCoroutine(startPop());
    }

    public bool isDead()
    {
        return (hp <= 0);
    }

    public IEnumerator damageBubble(int amount)
    {
        stopPop();

        List<Transform> activeBubbles = new List<Transform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform bubble = transform.GetChild(i);
            if (bubble.gameObject.activeSelf)
            {
                activeBubbles.Add(bubble);
            }
        }


        if (activeBubbles.Count == 0)
        {
            Debug.Log("No hay burbujas activas para dañar.");
        }

        amount = Mathf.Min(amount, activeBubbles.Count);

        for (int i = activeBubbles.Count - 1; i >= activeBubbles.Count - amount; i--)
        {
            StartCoroutine(PlayPopEffect(activeBubbles[i]));
            hp--;
        }
        StartCoroutine(waitAndPop());
        yield return null;
    }

    IEnumerator waitAndPop()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(continuePop());
    }


    public void regenerateBubble(int amount)
    {
        stopPop();
        int additional = amount;
        Transform[] activeBubbles = new Transform[transform.childCount];
        if (additional + hp >= maxHp) additional = maxHp - hp; // capear la vida maxhp uwu
        for (int i = 0; i < activeBubbles.Length; i++)
        {
            if(additional != 0 && !transform.GetChild(i).gameObject.activeSelf)
            {
                Transform currentBubble = transform.GetChild(i);
                currentBubble.gameObject.SetActive(true);
                ResetBubble(currentBubble);
                activeBubbles[i] = transform.GetChild(i);
                additional--;
                hp++;
            }
        }
        StartCoroutine(continuePop());
    }

    private IEnumerator PlayPopEffect(Transform bubble)
    {
        Image bubbleImage = bubble.GetComponent<Image>();
        if (bubbleImage != null)
        {
            bubbleImage.sprite = bubblePop;
        }

        yield return new WaitForSeconds(0.2f);

        bubble.gameObject.SetActive(false);
    }

    private List<Transform> GetActiveBubbles()
    {
        List<Transform> activeBubbles = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform bubble = transform.GetChild(i);
            if (bubble.gameObject.activeSelf)
            {
                activeBubbles.Add(bubble);
            }
        }
        return activeBubbles;
    }


    private void ResetBubble(Transform bubble)
    {
        bubble.localScale = Vector3.one;
        bubble.GetComponent<Image>().sprite = bubbleSprite;
    }


}
