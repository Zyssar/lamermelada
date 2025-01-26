using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundScript : MonoBehaviour
{
    public float scrollSpeed = 2f;
    public float backgroundHeight;

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
            backgroundHeight = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.y;
        }
    }

    private void Update()
    {
        foreach (var background in backgrounds)
        {
            background.position += Vector3.down * scrollSpeed * Time.deltaTime;

            if (background.position.y < -backgroundHeight)
            {
                float highestY = GetHighestBackgroundY();
                background.position = new Vector3(background.position.x, highestY + backgroundHeight, background.position.z);
            }
        }
    }

    private float GetHighestBackgroundY()
    {
        float highestY = float.MinValue;
        foreach (var background in backgrounds)
        {
            if (background.position.y > highestY)
            {
                highestY = background.position.y;
            }
        }
        return highestY;
    }

}
