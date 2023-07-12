using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBound : MonoBehaviour
{
    void Update()
    {
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, -29.38f, 29.38f),
            Mathf.Clamp(transform.position.y, -29.4f, 29.4f));
    }
}
