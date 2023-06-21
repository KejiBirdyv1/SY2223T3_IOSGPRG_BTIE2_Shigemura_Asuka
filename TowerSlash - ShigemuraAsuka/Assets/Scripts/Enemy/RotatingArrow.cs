using UnityEngine;

public class RotatingArrow : MonoBehaviour
{
    private float rotateTimer = 0.5f;
    private float rotateInterval = 0.2f;
    private float rotationAngle = 90f;

    private void Update()
    {
        if (rotateTimer > 0)
        {
            rotateTimer -= Time.deltaTime;
        }

        if (rotateTimer <= 0)
        {
            transform.eulerAngles += new Vector3(0f, 0f, rotationAngle);

            if (transform.eulerAngles.z > 270f)
            {
                transform.eulerAngles = Vector3.zero;
            }

            rotateTimer = rotateInterval;
        }
    }
}
