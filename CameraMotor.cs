using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform target;           // The target the camera should follow (e.g., the player)
    public float smoothSpeed = 0.07f; // The speed of the smoothing
    public Vector3 offset;             // The offset from the target position

    void LateUpdate()
    {
        // Desired position is the target's position plus the offset
        Vector3 desiredPosition = target.position + offset;

        // Smoothly interpolate between the current position and the desired position
        Vector3 smoothedPosition = Vector3.Slerp(transform.position, desiredPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;
    }
}
