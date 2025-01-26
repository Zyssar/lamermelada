using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DepthController : MonoBehaviour
{
    [SerializeField] public TMP_Text depths;
    [SerializeField] public float divingSpeed=5f;
    [SerializeField] public float timer = 0f;
    public float meters = 0f;
    public float KM = 0f;

    void FixedUpdate()
    {
        if (KM > 0)
            depths.text = KM.ToString("F1") + " km";
        else
            depths.text = meters.ToString() + " m";
    }

    IEnumerator addDepth()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (KM > 0)
            {
                if (meters >= 100)
                {
                    KM += 0.1f;
                    meters -= 100;
                }
            }
            else
            {
                if (meters >= 1000)
                {
                    KM += 1f;
                    meters -= 1000;
                }
            }

            meters += divingSpeed;
        }
    }



    void Start()
    {
        StartCoroutine(addDepth());
        depths.text = meters.ToString() + " m";
    }
}
