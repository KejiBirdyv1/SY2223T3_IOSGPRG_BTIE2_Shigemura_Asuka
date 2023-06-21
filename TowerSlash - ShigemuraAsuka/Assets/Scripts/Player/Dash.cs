using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dashGauge;
    public float dashTimer;
    public bool isDash;

    private Vector3 firstPos;
    private Vector3 lastPos;
    private Touch touch;
    private Vector3 touchPosition;
    private float swipeRange = 2;
    private Player player;

    private void Start()
    {
        dashTimer = 3f;
        dashGauge = 0f;
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (dashGauge >= 1f)
        {
            dashGauge = 1f;
        }

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;
        }

        if (Input.touchCount > 0 && touch.phase == TouchPhase.Began)
        {
            firstPos = touchPosition;
        }
        else if (Input.touchCount > 0 && touch.phase == TouchPhase.Ended)
        {
            lastPos = touchPosition;

            float swipeDistance = Vector3.Distance(firstPos, lastPos);

            // Only activates if no enemy in player range
            if (swipeDistance <= swipeRange && !player.enemyInRange && !player.isDead)
            {
                UIController.score += Random.Range(5,10);
            }
        }
    }
}
