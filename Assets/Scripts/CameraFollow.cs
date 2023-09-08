using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float verticalOffset;
    [SerializeField]
    private float minY;
    [SerializeField]
    private float maxY;
    
    public Transform target;

    [HideInInspector]
    public Vector3 newPos;

    private void FixedUpdate()
    {
        // Calculating new pos for camera with target position, clamping values to ensure camera doesn't break bounds
        newPos = new Vector3(target.position.x, Mathf.Clamp(target.position.y + verticalOffset, minY, maxY), -10);

        transform.position = Vector3.Slerp(transform.position, newPos, speed * Time.fixedDeltaTime);
    }
}
