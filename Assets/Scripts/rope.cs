using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] public Vector3 centerPoint;
    public float centerRadius = 0.5f;
    public float midDistance = 5f;
    public float maxDistance = 8f;
    public float pullSpeed = 5f;
    private bool isPulledBack = false;
    [SerializeField] Texture2D spriteTexture;

    public LineRenderer ropeRenderer;

    void Start()
    {
        
        if (ropeRenderer == null)
        {
            ropeRenderer = gameObject.AddComponent<LineRenderer>();
        }

        ropeRenderer.startWidth = 0.1f;
        ropeRenderer.endWidth = 0.1f;
        ropeRenderer.positionCount = 2;
        ropeRenderer.material = new Material(Shader.Find("Sprites/Default"));
        ropeRenderer.startColor = Color.white;
        ropeRenderer.endColor = Color.white;

        if (spriteTexture != null)
        {
            ropeRenderer.material.mainTexture = spriteTexture;
            ropeRenderer.material.mainTextureScale = new Vector2(1, 1);
        }

        ropeRenderer.textureMode = LineTextureMode.Tile;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, centerPoint);

        if (distance <= centerRadius)
        {
            isPulledBack = false;
            UpdateRopeVisual(distance);
            return;
        }

        if (distance > midDistance && distance <= maxDistance)
        {
            Vector3 directionToCenter = (centerPoint - transform.position).normalized;
            transform.position += directionToCenter * pullSpeed * Time.deltaTime;
        }
        else if (distance > maxDistance)
        {
            Vector3 directionToCenter = (centerPoint - transform.position).normalized;
            transform.position += directionToCenter * 4 * pullSpeed * Time.deltaTime;
            isPulledBack = true;
        }

        if (isPulledBack)
        {
            Vector3 directionToCenter = (centerPoint - transform.position).normalized;
            transform.position += directionToCenter * 2 * pullSpeed * Time.deltaTime;
        }

        UpdateRopeVisual(distance);
    }

    void UpdateRopeVisual(float distance)
    {
        ropeRenderer.SetPosition(0, centerPoint);
        ropeRenderer.SetPosition(1, transform.position);

        if (distance >= maxDistance)
        {
            ropeRenderer.startColor = Color.red; 
            ropeRenderer.endColor = Color.red;
        }
        else if (distance > midDistance)
        {
            ropeRenderer.startColor = Color.yellow;
            ropeRenderer.endColor = Color.yellow;
        }
        else
        {
            ropeRenderer.startColor = Color.white;
            ropeRenderer.endColor = Color.white;
        }
    }
}
