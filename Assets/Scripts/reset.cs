using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reset : MonoBehaviour
{

    public BubbleController bubbleController;
    public playerMovement playerMovement;
    void Update()
    {
        if (bubbleController.IsDead())
        {
            playerMovement.isAlive = false;
            transform.GetChild(0).gameObject.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            transform.GetChild(0).gameObject.SetActive(false);
            string sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);

        }
    }
}
