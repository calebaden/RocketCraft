using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float yOffset;
    public float zOffset;
    public float damping;

    private Vector3 positionVelocity;

    Rigidbody targetBody;

    // Used for initialization
    void Start ()
    {
        targetBody = target.GetComponent<Rigidbody>();
    }

    // Fixed update is called every fixed amount of time
    void FixedUpdate ()
    {
        Vector3 newPosition = target.position + (targetBody.velocity.normalized * zOffset);
        newPosition.y = target.position.y + yOffset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref positionVelocity, damping);

        Vector3 focalPoint = target.position + (targetBody.velocity.normalized * 5);
        focalPoint.y = target.position.y;
        transform.LookAt(focalPoint);
    }
}
