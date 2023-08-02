using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform player;
    public Camera miniMapCamera;
    public float mapScale = 0.2f;

    private void LateUpdate()
    {
        if (player == null)
            return;

        Vector3 newPosition = player.position;
        newPosition.y = miniMapCamera.transform.position.y;
        miniMapCamera.transform.position = newPosition;

        miniMapCamera.transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }
}
