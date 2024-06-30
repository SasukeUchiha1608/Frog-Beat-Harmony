using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public Vector3 offset;    // Offset distance between the player and camera

    void Start()
    {
        // Set the initial offset based on the current positions
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        // Update the camera position based on the player's position + offset
        transform.position = player.position + offset;
    }
}
