using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    void Update() 
    {
        //Camera World Boundary 
        transform.position = new Vector3(
            Mathf.Clamp(target.position.x, -21f, 21f),
            Mathf.Clamp(target.position.y, -25f, 25f),
            transform.position.z);
    }
}