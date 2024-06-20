using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // open variables
    public GameObject player;
    public float moveSpeed = 5.0f;
    public float tempo = 1f;

    // simple pathfinding to player
    private float eX;
    private float eZ;
    enum _direction {left, right, forward, back, stop};
    private _direction direct;

    // movement
    private float distance;
    private GridManager gridManager;
    private EnemyToadSpawner ToadSpawner;
    private Vector3 targetPosition;
    private float yPosition;
    private bool AutoMove = true;

    private float test = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created   

    void Start()
    {
        gridManager = FindFirstObjectByType<GridManager>();
        ToadSpawner = GetComponent<EnemyToadSpawner>();
        targetPosition = gridManager.GetNearestPointOnGrid(transform.position);
        transform.position = targetPosition;
        yPosition = transform.position.y; // Store the initial Y position
    }
    
    // Update is called once per frame
    void Update() 
    {
        //Add in a method to check for surrounding enemies to prevent them from stacking later
        /*
        * Unsure what would be the best way to handle it
        *   Have the enemy stall in place
        *   They move in a diffrent direction?
        * Detection
        *   Should it be the enemy sends a collision in the direction they will move into and change if it colides into another box?
        *   or allow the enemies to be spaced out
        */

        if(AutoMove) {
            eX = transform.position.x - player.transform.position.x;
            eZ = transform.position.z - player.transform.position.z;

            // Searches for the player and moves closer
            // Will need to add in obstical avoidence when we add them in. this will do for now
            if (Mathf.Abs(eX) > Mathf.Abs(eZ))
                if(eX > 0)
                    direct = _direction.left;
                else
                    direct = _direction.right;
            else
                if(eZ < 0)
                    direct = _direction.forward;
                else
                    direct = _direction.back;


            //make the moddle rotate, then confirm direction?
            switch(direct) {
                case _direction.left:
                    targetPosition += Vector3.left * gridManager.gridSize;
                    break;
                case _direction.right:
                    targetPosition += Vector3.right * gridManager.gridSize;
                    break;
                case _direction.forward:
                    targetPosition += Vector3.forward * gridManager.gridSize;
                    break;
                case _direction.back:
                    targetPosition += Vector3.back * gridManager.gridSize;
                    break;
                default:
                    break;
            }

            targetPosition.y = yPosition;
            
            test = 0;
            this.AutoMove = !this.AutoMove;
        }
        else {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            test += Time.deltaTime;
            if(test > tempo) 
                this.AutoMove = !this.AutoMove;
        }

    }

    // Difficulty ramp
    public void TempoUp() 
    {
        /*
        * We ramp up the tempo the frogs move here
        * increace frog count?
        * Figure out parameters how it should speed up
        * Speed cap?
        */
        tempo -= 0.01f;
        moveSpeed += 0.01f;

    }

    // Destroy enemy method
    /**
    void OnCollisionEnter (Collision col)
    {
        if(col.GameObject.tag.Equals ("player"))
            Destroy(this.GameObject);
    }
    /**/
}
