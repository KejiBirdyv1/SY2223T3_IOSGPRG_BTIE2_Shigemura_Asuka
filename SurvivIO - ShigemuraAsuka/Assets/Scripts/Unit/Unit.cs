using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Unit : MonoBehaviour
{
    [SerializeField] protected string unitName;
    [SerializeField] protected float speed;
    private Health health;
    private Rigidbody2D rb2d;
    public Gun currentGun;

    public virtual void Initialize(string name, int maxHealth, float unitSpeed)
    {
        unitName = name;
        health = GetComponent<Health>();
        health.Initialize(maxHealth);
        rb2d = GetComponent<Rigidbody2D>();
        speed = unitSpeed;

        Debug.Log($"{unitName} has been initialized");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (bullet != null)
        {
            health.TakeDamage(bullet.damage);
            if (health.CurrentHealth <= 0)
            {
                DoDeath();
            }
            Destroy(collision.gameObject);
            rb2d.velocity = Vector2.zero;
        }
    }

    public virtual void Shoot()
    {
        Debug.Log($"Unit is shooting");
    }

    public virtual float GetSpeed()
    {
        return speed;
    }

    public virtual void DoDeath()
    {

    }
}
