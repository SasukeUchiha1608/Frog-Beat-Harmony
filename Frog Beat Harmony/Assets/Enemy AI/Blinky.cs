using UnityEngine;

public class Blinky : MonoBehaviour
{
    public GameObject player;
    public float eX;
    public float eZ;

    private float tempo = 1f;
    private float counterM = 0;
    bool AutoMove = true;

    private EnemyAI EnemyAI;
    private EnemyToadSpawner ETS;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemyAI = GetComponent<EnemyAI>();
        ETS = GetComponent<EnemyToadSpawner>();
        EnemyAI.gridManager = FindFirstObjectByType<GridManager>();
        EnemyAI.targetPosition = EnemyAI.gridManager.GetNearestPointOnGrid(transform.position);
        transform.position = EnemyAI.targetPosition;
        EnemyAI.yPosition = transform.position.y; // Store the initial Y position
    }

    // Update is called once per frame
    void Update()
    {
        if(AutoMove) {
            eX = transform.position.x - player.transform.position.x;
            eZ = transform.position.z - player.transform.position.z;

            if(ETS.scatterMode())
                EnemyAI.directionScatter(eX, eZ);
            else
                EnemyAI.directionNorm(eX, eZ);
            // Searches for the player and moves closer
            // Will need to add in obstical avoidence when we add them in. this will do for now
            
            counterM = 0;
            AutoMove = !AutoMove;
        } else {
            transform.rotation = Quaternion.Slerp(transform.rotation, EnemyAI.targetRotation,  Time.deltaTime * EnemyAI.getMoveSpeed() * 2);
            transform.position = Vector3.MoveTowards(transform.position, EnemyAI.targetPosition, EnemyAI.getMoveSpeed() * Time.deltaTime);
            counterM += Time.deltaTime;
            if(counterM > tempo) 
                AutoMove = !AutoMove;
        }
    }

    public void TempoUp(float ramp) 
    {
        tempo -= ramp;
    }
}
