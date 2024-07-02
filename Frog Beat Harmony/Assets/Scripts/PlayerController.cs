using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private GridManager gridManager;
    private Vector3 targetPosition;
    private KeyIndicatorManager keyIndicatorManager;

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager == null)
        {
            Debug.LogError("GridManager not found!");
        }
        keyIndicatorManager = FindObjectOfType<KeyIndicatorManager>();
        if (keyIndicatorManager == null)
        {
            Debug.LogError("KeyIndicatorManager not found!");
        }
        targetPosition = gridManager.GetNearestPointOnGrid(transform.position);
        transform.position = targetPosition;
        keyIndicatorManager.UpdateKeyIndicators(); // Add this line to initialize indicators
    }

    void Update()
    {
        bool moved = false;

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W Key Pressed");
            targetPosition += Vector3.forward * gridManager.gridSize;
            moved = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("S Key Pressed");
            targetPosition += Vector3.back * gridManager.gridSize;
            moved = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A Key Pressed");
            targetPosition += Vector3.left * gridManager.gridSize;
            moved = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("D Key Pressed");
            targetPosition += Vector3.right * gridManager.gridSize;
            moved = true;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (moved)
        {
            keyIndicatorManager.UpdateKeyIndicators();
        }
    }
}
