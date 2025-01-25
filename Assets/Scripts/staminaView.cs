using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class staminaView : MonoBehaviour
{
    private Image bar;


    private void Start()
    {
        bar = gameObject.GetComponentInChildren<Image>();
    }

    void Update()
    {
    }

    public IEnumerator barRefill(float time)
    {
        float filling = time / 50;
        bar.fillAmount = 0;
        while(bar.fillAmount < 1)
        {
            yield return new WaitForSeconds(filling);
            bar.fillAmount += 0.02f;
        }

    }
}
