using UnityEngine;

public class SpeedCharacter : Player
{
    void Start()
    {
        playerLives = 3;
        dashPointsIncrement = 0.1f;
        isDead = false;
        wrongDirection = false;

        rb = GetComponent<Rigidbody2D>();
        powerUp = GetComponent<PowerUp>();
        dash = GetComponent<Dash>();
        swipeDir = GetComponent<PlayerController>();
        circleCollider = GetComponent<CircleCollider2D>();
    }
}
