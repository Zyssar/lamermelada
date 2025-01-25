using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reset : MonoBehaviour
{

    public BubbleController bubbleController;
    //falta la wbd de controlar la altura
    void Update()
    {
        if (bubbleController.isDead())
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            bubbleController.restart();
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
