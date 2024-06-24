using UnityEngine;

public class Blinky : MonoBehaviour
{
    public GameObject player;

    private EnemyAI EnemyAI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyAI = GetComponent<EnemyAI>();
        EnemyAI.gridManager = FindFirstObjectByType<GridManager>();
        EnemyAI.targetPosition = EnemyAI.gridManager.GetNearestPointOnGrid(transform.position);
        transform.position = EnemyAI.targetPosition;
        EnemyAI.yPosition = transform.position.y; // Store the initial Y position
    }

    // Update is called once per frame
    void Update()
    {
        if(EnemyAI.AutoMove) {
            EnemyAI.eX = transform.position.x - player.transform.position.x;
            EnemyAI.eZ = transform.position.z - player.transform.position.z;

            // Searches for the player and moves closer
            // Will need to add in obstical avoidence when we add them in. this will do for now
            EnemyAI.direction();

            switch(EnemyAI.direct) {
                case EnemyAI._direction.left:
                    EnemyAI.tiltY = EnemyAI.rotation*2f;
                    EnemyAI.targetPosition += Vector3.left * EnemyAI.gridManager.gridSize;
                    break;
                case EnemyAI._direction.right:
                    EnemyAI.tiltY = EnemyAI.rotation*0f;
                    EnemyAI.targetPosition += Vector3.right * EnemyAI.gridManager.gridSize;
                    break;
                case EnemyAI._direction.forward:
                    EnemyAI.tiltY = EnemyAI.rotation*1f;
                    EnemyAI.targetPosition += Vector3.forward * EnemyAI.gridManager.gridSize;
                    break;
                case EnemyAI._direction.back:
                    EnemyAI.tiltY = EnemyAI.rotation*-1f;
                    EnemyAI.targetPosition += Vector3.back * EnemyAI.gridManager.gridSize;
                    break;
                default:
                    break;
            }
            EnemyAI.targetRotation = Quaternion.Euler(0, EnemyAI.tiltY, 0);
            EnemyAI.targetPosition.y = EnemyAI.yPosition;
            
            EnemyAI.counterM = 0;
            EnemyAI.AutoMove = !EnemyAI.AutoMove;
            if(EnemyAI.counterS > EnemyAI.scatter)
                EnemyAI.scatterMode = false;
        }
        else {
            transform.rotation = Quaternion.Slerp(transform.rotation, EnemyAI.targetRotation,  Time.deltaTime * EnemyAI.moveSpeed * 2);
            transform.position = Vector3.MoveTowards(transform.position, EnemyAI.targetPosition, EnemyAI.moveSpeed * Time.deltaTime);
            EnemyAI.counterM += Time.deltaTime;
            if(EnemyAI.counterM > EnemyAI.tempo) 
                EnemyAI.AutoMove = !EnemyAI.AutoMove;
            if(EnemyAI.scatterMode) {
                EnemyAI.counterS += Time.deltaTime;
            }
        }
    }
}
