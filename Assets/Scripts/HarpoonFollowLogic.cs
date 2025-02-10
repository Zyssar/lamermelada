using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonFollowLogic : MonoBehaviour
{
    public bool instaTrack = false;
    private Vector3 mousePos;
    public Camera mainCamera;
    private Vector3 smoothedDirection;
    public bool isShooting;

    void Update()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        mousePos = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.nearClipPlane));

        Vector3 direction = (mousePos - transform.position);
        direction.z = 0;

        if (instaTrack)
        {
            RotateTowards(direction);
        }
        else
        {
            float rotationSpeed = 6f;
            smoothedDirection = Vector3.Lerp(transform.right, direction.normalized, Time.deltaTime * rotationSpeed);
            RotateTowards(smoothedDirection);
        }
    }

    void RotateTowards(Vector3 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public Vector3 getDirection()
    {
        return transform.right;
    }
}
