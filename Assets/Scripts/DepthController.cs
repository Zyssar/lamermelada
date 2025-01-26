using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DepthController : MonoBehaviour
{
    [SerializeField] public TMP_Text depths;
    [SerializeField] public float divingSpeed = 5f;
    [SerializeField] public float timer = 0f;
    public float meters = 0f;
    public float KM = 0f;

    void FixedUpdate()
    {
        if (KM > 0)
            depths.text = KM.ToString("F1") + " km";
        else
            depths.text = meters.ToString("F0") + " m";
    }

    IEnumerator addDepth()
    {
        while (true)
        {
            yield return null;

            meters += divingSpeed * Time.deltaTime;

            if (meters >= 1000)
            {
                KM += 1f;
                meters -= 1000f;
            }

            if (KM > 0)
            {
                if (meters >= 100)
                {
                    KM += 0.1f;
                    meters -= 100f;
                }
            }
        }
    }

    void Start()
    {
        StartCoroutine(addDepth());
        depths.text = meters.ToString() + " m";
    }
}
