using UnityEngine;

public class TankCharacter : Player
{
    void Start()
    {
        playerLives = 5;
        dashPointsIncrement = 0.05f;
        isDead = false;
        wrongDirection = false;

        rb = GetComponent<Rigidbody2D>();
        powerUp = GetComponent<PowerUp>();
        dash = GetComponent<Dash>();
        swipeDir = GetComponent<PlayerController>();
        circleCollider = GetComponent<CircleCollider2D>();
    }
}
