using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonRopeVisual : MonoBehaviour
{
    [SerializeField] Texture2D spriteTexture;
    public LineRenderer ropeRenderer;
    public Transform harpoonBase;
    void Start()
    {
        ropeRenderer.material.mainTexture = spriteTexture;
        ropeRenderer.material.mainTextureScale = new Vector2(1, 1);
    }

    void Update()
    {
            ropeRenderer.SetPosition(0, harpoonBase.position + harpoonBase.right*0.5f);
            ropeRenderer.SetPosition(1, transform.position);

    }
}
