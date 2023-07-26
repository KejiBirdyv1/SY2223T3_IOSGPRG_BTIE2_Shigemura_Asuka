using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionParticleSystem;
    [SerializeField] private float speed;
    public int damage;
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Unit>() != null ||
            collision.gameObject.GetComponent<Obstacle>() != null)
        {
            speed = 0;
            rb2d.velocity = Vector2.zero;
            explosionParticleSystem.Play();
            spriteRenderer.enabled = false;
            this.GetComponent<BoxCollider2D>().enabled = false;
            this.GetComponent<CircleCollider2D>().enabled = true;
            Destroy(gameObject, 1f);
        }

        if (collision.gameObject.GetComponent<Health>() != null)
        {
            Health healthScript = collision.gameObject.GetComponent<Health>();
            Unit gameUnitScript = collision.gameObject.GetComponent<Unit>();
            healthScript.TakeDamage(damage);
            if (healthScript.CurrentHealth <= 0)
            {
                gameUnitScript.DoDeath();
            }
        }
    }

    private void Start()
    {
        speed = 10f;
        damage = 100;
        this.GetComponent<CircleCollider2D>().enabled = false;
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }
}
