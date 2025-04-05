using UnityEngine;


public class CameraFollowing : MonoBehaviour
{
    public Transform target;
    private Vector3 previousPosition;
    public float smoothSpeed = 0.125f;
    public int camBuffer = 2;
    void Start ()
    {
        
        Camera.main.orthographicSize = 10f;
        previousPosition = target.position;
    }
    void LateUpdate()
    {
        previousPosition.y = target.position.y;

        if (previousPosition.x < target.position.x - camBuffer)
        {
            previousPosition.x = target.position.x - camBuffer - 0.01f;
        }else if (previousPosition.x > target.position.x + camBuffer)
        {
            previousPosition.x = target.position.x + camBuffer + - 0.01f;
        }
        Vector3 desiredPosition = previousPosition + new Vector3(0f, -5.08f, -10f);

        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = desiredPosition;
    }
}
