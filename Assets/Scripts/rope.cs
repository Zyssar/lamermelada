using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rope : MonoBehaviour
{
    [SerializeField] public Vector3 centerPoint; // The center point the player should stay near
    public float midDistance = 5f; // Max distance from the center point
    public float maxDistance = 8f;
    public float pullSpeed = 5f; // Speed at which the player is pulled back
    private bool isPulledBack=false;

    void Update()
    {
        // Calculate the distance from the center point
        float distance = Vector3.Distance(transform.position, centerPoint);

        // If the distance exceeds the max allowed distance
        if (distance > midDistance) {
            // Calculate the direction to pull the player back
            Vector3 directionToCenter = (centerPoint - transform.position).normalized;

            // Move the player back toward the center at the pull speed
            transform.position += directionToCenter * pullSpeed * Time.deltaTime;
        }
        
        if (distance > maxDistance)
        {
            // Set the flag to indicate the player surpassed the distance
            if (!isPulledBack)
            {
                isPulledBack = true;
            }

            // Calculate the direction to pull the player back to the center
            Vector3 directionToCenter = (centerPoint - transform.position).normalized;

            // Move the player back toward the center at the pull speed
            transform.position += directionToCenter * 4 * pullSpeed * Time.deltaTime;
        }
        else
        {
            // If the player comes back within the radius, pull them back to the center anyway
            if (isPulledBack)
            {
                // Calculate the direction to pull the player back to the center
                Vector3 directionToCenter = (centerPoint - transform.position).normalized;

                // Move the player back toward the center at the pull speed
                transform.position += directionToCenter *  2 * pullSpeed * Time.deltaTime;

                // Ensure the player is fully pulled back before allowing normal movement
                if (Vector3.Distance(transform.position, centerPoint) <= 0.1f)
                {
                    isPulledBack = false;
                }
            }
        }
    }
}
