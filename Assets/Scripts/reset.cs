using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reset : MonoBehaviour
{

    public BubbleController bubbleController;
    public playerMovement playerMovement;
    //falta la wbd de controlar la altura
    void Update()
    {
        if (bubbleController.isDead())
        {
            playerMovement.isAlive = false;
            transform.GetChild(0).gameObject.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            bubbleController.restart();
            transform.GetChild(0).gameObject.SetActive(false);
            playerMovement.rb.transform.position = Vector3.zero;
            playerMovement.rb.rotation = -90;
            playerMovement.isAlive = true;
        }
    }
}
