using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isDead;
    public float deathDirection;
    public float enemyHp;
    public float speed;

    public List<GameObject> arrows;
    public GameObject arrow;
    public GameObject secondArrow;

    private SpriteRenderer spriteRenderer;
    private Dash dash;

    private void Start()
    {
        deathDirection = Random.Range(1, 5);
        int randomIdx = Random.Range(0, arrows.Count);

        Quaternion arrowRotation = Quaternion.identity;
        switch (deathDirection)
        {
            case 1: // Down
                arrowRotation = Quaternion.Euler(0f, 0f, -90f); 
                break;
            case 2: // Up
                arrowRotation = Quaternion.Euler(0f, 0f, 90f); 
                break;
            case 3: // Right
                arrowRotation = Quaternion.Euler(0f, 0f, 0f); 
                break;
            case 4: // Left
                arrowRotation = Quaternion.Euler(0f, 0f, 180f); 
                break;
        }

        GameObject selectedArrow = arrows[randomIdx];
        arrow = Instantiate(selectedArrow, transform.position + new Vector3(-1f, -1f, 0f), arrowRotation);
        arrow.transform.SetParent(transform, true);

        if (selectedArrow == arrows[1])
        {
            spriteRenderer = arrow.GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = true;
        }
        else if (selectedArrow == arrows[2])
        {
            secondArrow = Instantiate(arrows[0], transform.position + new Vector3(-1f, -1f, 0f), arrowRotation);
            secondArrow.transform.SetParent(transform, true);
            secondArrow.SetActive(false);
        }
    }

    private void Update()
    {
        if (dash != null)
        {
            speed = dash.isDash ? 20f : 5f;
        }

        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (enemyHp <= 0)
        {
            isDead = true;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        dash = other.collider.GetComponent<Dash>();

        if (dash != null)
        {
            Destroy(gameObject);
        }

        Player player = other.collider.GetComponent<Player>();

        if (player != null)
        {
            if (!dash.isDash)
            {
                player.playerLives--;
            }
            else
            {
                UIController.score += Random.Range(15, 30);
            }
        }
    }
}
