using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float yOffset;
    public float zOffset;
    public float damping;

    private Vector3 positionVelocity;

    void FixedUpdate ()
    {
        Vector3 newPosition = target.position + (target.forward * zOffset);
        newPosition.y = newPosition.y + yOffset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref positionVelocity, damping);

        Vector3 focalPoint = target.position + (target.forward * 5);
        transform.LookAt(focalPoint);
    }
}
