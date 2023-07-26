using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    void Update() 
    {
        //Camera World Boundary 
        transform.position = new Vector3(
            Mathf.Clamp(target.position.x, -41f, 41f),
            Mathf.Clamp(target.position.y, -32.45f, 32.45f),
            transform.position.z);
    }
}