using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DepthController : MonoBehaviour
{
    [SerializeField] public TMP_Text depths;
    public int meters = 0;
    public float KM = 0;
    private int frameCount = 0;

    void Update()
    {
        frameCount++;

        if (frameCount % 180 == 0)
        {
            addDepth();
            if (KM > 0)
                depths.text = KM.ToString("F1") + " km";
            else
                depths.text = meters.ToString() + " m";
        }
    }

    void addDepth()
    {
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

        meters += 1;  
        frameCount = 0; 
    }


    void Start()
    {
        depths.text = meters.ToString() + " m";
    }
}
