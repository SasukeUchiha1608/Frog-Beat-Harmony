using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private GridManager gridManager;
    private Vector3 targetPosition;
    private float yPosition;

    void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();
        targetPosition = gridManager.GetNearestPointOnGrid(transform.position);
        transform.position = targetPosition;
        yPosition = transform.position.y; // Store the initial Y position
    }

    void Update()
    {
        // User Input
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W Key Pressed");
            targetPosition += Vector3.forward * gridManager.gridSize;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("S Key Pressed");
            targetPosition += Vector3.back * gridManager.gridSize;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A Key Pressed");
            targetPosition += Vector3.left * gridManager.gridSize;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("D Key Pressed");
            targetPosition += Vector3.right * gridManager.gridSize;
        }

        // Ensure the Y coordinate is locked
        targetPosition.y = yPosition;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Enemy Collision
        /*
        * When frog colides with Enemy, game ends/loses a health
        * Does the enemy dissapear?
        * 
        */
    }
}
