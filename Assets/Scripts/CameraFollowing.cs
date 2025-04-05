using UnityEngine;


public class CameraFollowing : MonoBehaviour
{
    public Transform target;
    private Transform previousTarget;
    public float smoothSpeed = 0.125f;

    void Start ()
    {
        
        Camera.main.orthographicSize = 10f;
    }
    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + new Vector3(0f, -5.08f, -10f);
        //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = desiredPosition;
    }
}
