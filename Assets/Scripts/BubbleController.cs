using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BubbleController : MonoBehaviour
{
    public float timeToPop = 1.5f; 
    public Sprite bubblePop;
    public bool multiplierActive = false;
    public Sprite bubbleSprite;
    public int multiplier = 2;
    public int maxHp = 15;
    public int hp;
    private List<IEnumerator> PopCoroutines;
    private IEnumerator FirstPopCoroutine;
    private IEnumerator LoopPopCoroutine;
    private List<IEnumerator> PopAnimations;
    private List<int> PopAnimationsIndex;

    private void Start()
    {
        PopCoroutines = new List<IEnumerator>();
        PopAnimations = new List<IEnumerator>();
        PopAnimationsIndex = new List<int>();
        for (int i = 0; i < maxHp; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        FirstPopCoroutine = StartPop();
        PopCoroutines.Add(FirstPopCoroutine);
        StartCoroutine(FirstPopCoroutine);
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

    private IEnumerator StartPop()
    {
        int amount=0;
        List<Transform> activeBubbles = GetActiveBubbles();
        hp = activeBubbles.Count;
        amount = activeBubbles.Count;

        if (amount == 0 || hp <=0)
        {
            Debug.Log("No hay burbujas activas para continuar con el estallido.");
            yield break;
        }
        
        for (int i = amount - 1; i >= 0; i--)
        {
            LoopPopCoroutine = PopBubble(activeBubbles[i]);
            PopCoroutines.Add(LoopPopCoroutine);
            yield return StartCoroutine(LoopPopCoroutine);
        }

    }

    private void StopPop()
    {
        int index=0;
        foreach (IEnumerator I in PopCoroutines)
        {
            StopCoroutine(I);
        }
        PopCoroutines.Clear();
        List<Transform> activeBubbles = GetActiveBubbles();
        index = activeBubbles.Count;
        if (index > 0)
        {
            ResetBubble(activeBubbles[index - 1]);
        }

    }

    public bool IsDead()
    {
        return (hp <= 0);
    }

    public IEnumerator damageBubble(int amount)
    {
        StopPop();

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
            IEnumerator popEffect = PlayPopEffect(activeBubbles[i]);
            PopAnimations.Add(popEffect);
            PopAnimationsIndex.Add(i);
            StartCoroutine(popEffect);
            hp--;
        }
        StartCoroutine(WaitAndPop());
        yield return null;
    }

    IEnumerator WaitAndPop()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(StartPop());
    }


    public void RegenerateBubble(int amount)
    {
        StopPop();
        foreach(IEnumerator i in PopAnimations)
        {
            StopCoroutine(i);
        }
        foreach(int i in PopAnimationsIndex)
        {
            ResetBubble(transform.GetChild(i));
            transform.GetChild(i).gameObject.SetActive(false);
        }
        PopAnimations.Clear();
        PopAnimationsIndex.Clear();
        int additional = amount;
        Transform[] activeBubbles = new Transform[transform.childCount];
        if (additional + hp >= maxHp) additional = maxHp - hp;
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
        StartCoroutine(StartPop());
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
