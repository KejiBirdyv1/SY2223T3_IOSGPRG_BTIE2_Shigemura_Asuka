using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float swipeDirection;

    private Vector3 firstPos;
    private Vector3 lastPos;
    private float swipeRange = 2f;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0f;

            if (touch.phase == TouchPhase.Began)
            {
                firstPos = touchPosition;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                lastPos = touchPosition;
                float swipeDistance = Vector3.Distance(firstPos, lastPos);

                Vector2 distance = lastPos - firstPos;

                float distanceX = Mathf.Abs(distance.x);
                float distanceY = Mathf.Abs(distance.y);

                // Swipe Down
                if (firstPos.y > lastPos.y && distanceY > distanceX && swipeDistance > swipeRange)
                {
                    swipeDirection = 1f;
                    Debug.Log("Swipe down");
                }

                // Swipe Up
                if (firstPos.y < lastPos.y && distanceY > distanceX && swipeDistance > swipeRange)
                {
                    swipeDirection = 2f;
                    Debug.Log("Swipe up");
                }

                // Swipe Right
                if (firstPos.x < lastPos.x && distanceX > distanceY && swipeDistance > swipeRange)
                {
                    swipeDirection = 3f;
                    Debug.Log("Swipe right");
                }

                // Swipe Left
                if (firstPos.x > lastPos.x && distanceX > distanceY && swipeDistance > swipeRange)
                {
                    swipeDirection = 4f;
                    Debug.Log("Swipe left");
                }
            }
        }
    }
}
