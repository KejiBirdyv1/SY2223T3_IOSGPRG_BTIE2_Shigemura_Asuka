using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBound : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, -49.5f, 49.5f),
            Mathf.Clamp(transform.position.y, -37f, 37f));
    }
}
