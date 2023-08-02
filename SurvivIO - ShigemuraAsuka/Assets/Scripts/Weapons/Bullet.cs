using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    public int damage;

    private void Start()
    {
        speed = 15f;
        Destroy(gameObject, 3f); // Destroy the bullet object after 3 seconds
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    public void SetBulletDamage(int damage)
    {
        this.damage = damage;
    }
}
