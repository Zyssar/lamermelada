using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 2f; 
    private float backgroundHeight; 
    private Transform[] backgrounds;
    private void Start()
    {
        backgrounds = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            backgrounds[i] = transform.GetChild(i);
        }

        if (backgrounds.Length > 0)
        {
            SpriteRenderer sr = backgrounds[0].GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                backgroundHeight = sr.bounds.size.y;
            }
        }
    }
     

    private void Update()
    {
        foreach (var background in backgrounds)
        {
            background.position += Vector3.up * scrollSpeed * Time.deltaTime;

               if (background.position.y > backgroundHeight)
            {
                float lowestY = GetLowestBackgroundY();
                background.position = new Vector3(background.position.x, lowestY - backgroundHeight, background.position.z);
            }
        }
    }

    private float GetLowestBackgroundY()
    {
        float lowestY = float.MaxValue;
        foreach (var background in backgrounds)
        {
            if (background.position.y < lowestY)
            {
                lowestY = background.position.y;
            }
        }
        return lowestY;
    }

}
