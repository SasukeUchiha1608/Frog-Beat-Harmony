using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private GridManager gridManager;
    private Vector3 targetPosition;
    private float yPosition;
    private int direction = 0;

    public int HP = 3;
    private bool invinciable = false;
    private float counter = 0f;
    public float invinciableTimer = 3f;

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
        // direction is needed for pinky's AI
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W Key Pressed");
            direction = 1;
            targetPosition += Vector3.forward * gridManager.gridSize;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("S Key Pressed");
            direction = 2;
            targetPosition += Vector3.back * gridManager.gridSize;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A Key Pressed");
            direction = 3;
            targetPosition += Vector3.left * gridManager.gridSize;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("D Key Pressed");
            direction = 4;
            targetPosition += Vector3.right * gridManager.gridSize;
        }

        if(invinciable) {
            if(counter < invinciableTimer) {
                counter += Time.deltatime;
            } else {
                invinciable = false;
                counter = 0f;
            }
        } 

        // Ensure the Y coordinate is locked
        targetPosition.y = yPosition;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    public int facingDirection() {
        return direction;
    }
    /**
    private void oncollisionenter(collision col){
        if ((col.tag == "enemy") && !invinciable) {
            HP -= 1;
            scatter = true
        }
    }
    /**/

}
