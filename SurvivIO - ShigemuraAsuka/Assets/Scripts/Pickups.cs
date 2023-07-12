using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public GameObject[] guns;
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacles"))
        {
            Destroy(gameObject);
        }
    }
}
