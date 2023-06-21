using UnityEngine;

public class DefaultCharacter : Player
{
    void Start()
    {
        playerLives = 3;
        dashPointsIncrement = 0.05f;
        isDead = false;
        wrongDirection = false;

        rb = GetComponent<Rigidbody2D>();
        powerUp = GetComponent<PowerUp>();
        dash = GetComponent<Dash>();
        circleCollider = GetComponent<CircleCollider2D>();
    }
}
