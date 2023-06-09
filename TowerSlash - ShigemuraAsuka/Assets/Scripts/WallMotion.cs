using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMotion : MonoBehaviour
{
    public float scrollSpeed;
    private SpriteRenderer spriteRenderer;
    private float offset;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        offset += scrollSpeed * Time.deltaTime;
        spriteRenderer.material.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }
}
