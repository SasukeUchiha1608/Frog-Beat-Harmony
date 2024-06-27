using UnityEngine;

public class Blinky : MonoBehaviour
{
    public GameObject player;
    public Vector3 itself;
    private float eX;
    private float eZ;
    private Vector3 targetPosition;
    private Quaternion targetRotation;

    private float tempo = 1f;
    private float counterM = 0;
    bool AutoMove = true;

    private EnemyAI enemyAI;
    private EnemyToadSpawner ETS;
    private GridManager gridManager;

    public enum _direction {left, back, right, forward};
    public _direction direct;
    private float tiltY = 0f;
    private float rotation = 90f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        enemyAI = FindFirstObjectByType<EnemyAI>();
        ETS = FindFirstObjectByType<EnemyToadSpawner>();
        gridManager = FindFirstObjectByType<GridManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        eX = transform.position.x - player.transform.position.x;
        eZ = transform.position.z - player.transform.position.z;
        if(AutoMove) {
            // Section quarentiened as it's not working as intended. injevtin old code for now

            /**/
            if(ETS.scatterMode()) {
                targetPosition = b_directionScatter(eX, eZ);
                Debug.Log("Scatter mode");
            } else {
                 b_directionNorm(eX, eZ);
                Debug.Log("Nominal");
            }
            // Searches for the player and moves closer
            // Will need to add in obstical avoidence when we add them in. this will do for now
            /**/
            //targetRotation = b_getOrientation(transform.position, targetPosition);
            
            counterM = 0;
            AutoMove = !AutoMove;
        } else {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation,  Time.deltaTime * 5f * 2);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 5f * Time.deltaTime);
            counterM += Time.deltaTime;
            if(counterM > tempo) 
                AutoMove = !AutoMove;
        }
    }

    public void TempoUp(float ramp) 
    {
        tempo -= ramp;
    }

    /**/ //Bandaid fix. remove later when code works as it should
    // Have the toad run from the player
    Vector3 b_directionNorm(float eX, float eZ) {
        targetPosition = gridManager.GetNearestPointOnGrid(transform.position);

        if (Mathf.Abs(eX) > Mathf.Abs(eZ)) // chases the player
            if(eX > 0)
                direct = _direction.left;
            else
                direct = _direction.right;
        else
            if(eZ < 0)
                direct = _direction.forward;
            else
                direct = _direction.back;

        switch(direct) {
            case _direction.left:
                tiltY = rotation*2f;
                targetPosition += Vector3.left * gridManager.gridSize;
                break;
            case _direction.right:
                tiltY = rotation*0f;
                targetPosition += Vector3.right * gridManager.gridSize;
                break;
            case _direction.forward:
                tiltY = rotation*1f;
                targetPosition += Vector3.forward * gridManager.gridSize;
                break;
            case _direction.back:
                tiltY = rotation*-1f;
                targetPosition += Vector3.back * gridManager.gridSize;
                break;
            default:
                break;
        }

        targetRotation = Quaternion.Euler(0, tiltY, 0);
        targetPosition.y = 1;
        return targetPosition;
    }

    Vector3 b_directionScatter(float eX, float eZ) {
        targetPosition = gridManager.GetNearestPointOnGrid(transform.position);

        if (Mathf.Abs(eX) > Mathf.Abs(eZ)) // will move away from the player
                if(eX < 0)
                    direct = _direction.left;
                else
                    direct = _direction.right;
            else
                if(eZ > 0)
                    direct = _direction.forward;
                else
                    direct = _direction.back;

        switch(direct) {
            case _direction.left:
                tiltY = rotation*2f;
                targetPosition += Vector3.left * gridManager.gridSize;
                break;
            case _direction.right:
                tiltY = rotation*0f;
                targetPosition += Vector3.right * gridManager.gridSize;
                break;
            case _direction.forward:
                tiltY = rotation*1f;
                targetPosition += Vector3.forward * gridManager.gridSize;
                break;
            case _direction.back:
                tiltY = rotation*-1f;
                targetPosition += Vector3.back * gridManager.gridSize;
                break;
            default:
                break;
        }

        targetPosition.y = 1;
        return targetPosition;
    }
    /**
    public Quaternion b_getOrientation(Vector3 currentPos, Vector3 nextPos) {
        Vector3 temp = currentPos - nextPos;
        if(temp.x != 0) {
            if(temp.x > 0)
                tiltY = rotation*2f;
            else
                tiltY = rotation*0f;
        } else {
            if(temp.z < 0)
                tiltY = rotation*1f;
            else
                tiltY = rotation*-1f;
        }

        targetRotation = Quaternion.Euler(0, tiltY, 0);

        return targetRotation;
    }
    /**/
}
