using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Joystick movementJoystick;
    public Joystick aimJoystick;
    public GameObject player;

    private Player playerScript;
    private Vector2 move;

    private void Start()
    {
        playerScript = player.GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        // Movement
        Vector2 movementDirection = Vector2.up * movementJoystick.Vertical + Vector2.right * movementJoystick.Horizontal;
        transform.Translate(movementDirection * playerScript.GetSpeed());

        // Aim
        if (aimJoystick.Horizontal != 0 || aimJoystick.Vertical != 0)
        {
            move.x = aimJoystick.Horizontal;
            move.y = aimJoystick.Vertical;

            float xRotation = move.x;
            float yRotation = move.y;
            float zRotation = Mathf.Atan2(xRotation, yRotation) * Mathf.Rad2Deg;
            transform.eulerAngles = new Vector3(0, 0, -zRotation);
        }
    }
}
