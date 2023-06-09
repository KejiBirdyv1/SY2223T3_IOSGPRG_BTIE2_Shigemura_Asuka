using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerLives = 3;
    [HideInInspector] public bool isDead;
    [HideInInspector] public bool enemyInRange;
    public float gravityToWall;
    [HideInInspector] public Rigidbody2D rb;
    public bool wrongDirection;
    public PlayerController swipeDir;
    [HideInInspector] public Enemy enemy;
    [HideInInspector] public WallMotion wall;
    [HideInInspector] public PowerUp powerUp;
    [HideInInspector] public Dash dash;
    [HideInInspector] public CircleCollider2D circleCollider;
    public float dashPointsIncrement;

    void Start()
    {
        isDead = false;
        dashPointsIncrement = 0.05f;
        wrongDirection = false;
        rb = GetComponent<Rigidbody2D>();
        powerUp = GetComponent<PowerUp>();
        dash = GetComponent<Dash>();
        swipeDir = GetComponent<PlayerController>();
        swipeDir.swipeDirection = 0;
        circleCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (isDead)
            return;

        if (playerLives <= 0)
        {
            playerLives = 0;
        }

        if (dash.isDash)
        {
            circleCollider.radius = 15f;
        }
        else
        {
            circleCollider.radius = 6f;
        }

        if (enemy != null)
        {
            if (wrongDirection)
            {
                playerLives--;
                wrongDirection = false;
            }

            if (swipeDir.swipeDirection != 0 && enemy.deathDirection != swipeDir.swipeDirection && swipeDir.swipeDirection != 0.05f)
            {
                wrongDirection = true;
                swipeDir.swipeDirection = 0;
            }

            if (enemy.deathDirection == swipeDir.swipeDirection)
            {
                enemy.enemyHp = 0;
            }

            if (dash.isDash)
            {
                enemy.speed = Time.unscaledTime;
            }

            if (enemy.enemyHp == 0)
            {
                enemyInRange = false;
                powerUp.PowerupChance();
                IncreaseDashGauge(dashPointsIncrement);
                UIController.score += Random.Range(15, 30);
            }
        }
        else
        {
            wrongDirection = false;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = Vector2.right * gravityToWall * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            Debug.Log("Enemy in range");
            enemyInRange = true;

            swipeDir.swipeDirection = 0.05f;

            enemy.secondArrow.SetActive(true);
            enemy.arrow.SetActive(false);
        }
        wall = other.GetComponent<WallMotion>();
    }

    void IncreaseDashGauge(float value)
    {
        dash.dashGauge += value;
    }
}
