using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPt : MonoBehaviour
{
    public Transform player; // Reference to the player transform
    public float distanceFromPlayer = 0.5f; // Distance from the player

    void Update()
    {
        // Get the mouse position in the world
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Mathf.Abs(Camera.main.transform.position.z - player.position.z); // Set z to the distance from the camera to the player
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Calculate the direction from the player to the mouse position
        Vector3 direction = (worldMousePosition - player.position).normalized;

        // Calculate the new position for the point object
        Vector3 pointPosition = player.position + direction * distanceFromPlayer;

        // Set the position of the point object
        transform.position = pointPosition;

        // Rotate the point object to face the mouse cursor
        transform.right = direction;
    }
}
