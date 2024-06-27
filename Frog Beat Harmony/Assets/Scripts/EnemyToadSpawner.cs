using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyToadSpawner : MonoBehaviour
{
    // Spawgn area Variables
    public GameObject player;
    public GameObject Toad_Blinky; // chases the player
    public GameObject Toad_Pinky; // flanks the player
    public GameObject Toad_Clyde; // chases the player, but scared
    public GameObject Toad_Buzz; // patrolls a route. follows the player if close, returns to closest node of route if player escapes
    public GameObject Toad_Clatter; // Always tries to be near the orb
    public GameObject TheHeart;
    public float boardX = 10;
    public float boardZ = 10;
    public float spawnXmin = 9;
    public float spawnZmin = 1;
    public float spawnXmax = 11;
    public float spawnZmax = 10;
    public Vector3 playerSpawn;

    private Vector3 temp;

    // possiable to make this code spawn in the orbs too?

    // Enemy control
    public int maxEnemies = 3;
    private int totalEnemies = 0;

    // Spawn timer control
    public float ramp = 0.1f;
    public float spawnTime = 6f;
    public float scatterTime = 5f;
    public float scatterIntervul = 20f;
    public float orbCooldown = 10f;
    private float counterSpawn = 0;
    private float counterIntervul = 0;
    private float counterSTime = 0;   
    private float counterOrb = 0;
    private bool toadSpawn = false;
    private bool orbSpawn = false;
    private bool orbExists = false;
    private bool scatter;

    private GridManager gridManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Instantiate(player, playerSpawn, Quaternion.identity);

        gridManager = FindFirstObjectByType<GridManager>();
        temp.x = Random.Range(spawnXmin, spawnXmax);
        temp.z = Random.Range(spawnZmin, spawnZmax);
        if (temp.z == player.transform.position.x || temp.z == player.transform.position.z)
            temp.z += 1;
        temp.y = 1;
        temp = gridManager.GetNearestPointOnGrid(temp);
        Toad_Blinky = Instantiate(Toad_Blinky, temp, Quaternion.identity);

        totalEnemies = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // spawn enemies
        if (totalEnemies < maxEnemies && toadSpawn) {
            
            temp.x = Random.Range(spawnXmin, spawnXmax);
            temp.z = Random.Range(spawnZmin, spawnZmax);
            if (temp.z == player.transform.position.x || temp.z == player.transform.position.z)
                temp.z += 1;
            temp.y = 1;
            temp = gridManager.GetNearestPointOnGrid(temp);

            Toad_Pinky = Instantiate(Toad_Blinky, temp, Quaternion.identity);
            totalEnemies += 1;
            counterSpawn = 0;
            toadSpawn = false;
        } else {
            counterSpawn += Time.deltaTime;
            if(counterSpawn > spawnTime)
                toadSpawn = true;
        }

        //allows the player some breathing room
        if(counterIntervul < scatterIntervul) {
            counterIntervul += Time.deltaTime;
        } else {
            if(!scatter)
                scatter = true;
            counterSTime += Time.deltaTime;
            if(counterSTime > scatterTime){
                if(scatterTime > 0)
                    scatterIntervul -= 1f;
                counterIntervul = 0;
                counterSTime = 0;
                scatter = false;
            }
        }

        /**/
        if(orbSpawn && !orbExists)
        {
            temp.x = Random.Range(0, boardX+1);
            temp.z = Random.Range(0, boardZ+1);
            temp = gridManager.GetNearestPointOnGrid(temp);
            TheHeart = Instantiate(TheHeart, temp, Quaternion.identity);
            orbSpawn = !orbSpawn;
            orbExists = true;
            counterOrb = 0;
        } else {
            counterOrb += Time.deltaTime;
            if(counterOrb > orbCooldown) {
                orbSpawn = !orbSpawn;
                orbExists = !orbExists;
            }
        }
        /**/

        /**
        if(dificulty ramp value) 
        {
            TempoUp(); //increaces the speed of the enemy toads
            spawnTime += 0.1f; //minimim spawn rate of the frogs?
            if (capEnemies < maxEnemies) // raises the total enemies avle to be smawned in
                capEnemies += 1;
            spawn in the next toad
            scatter timer decreased
        }
        /**/
    }

    public bool scatterMode() {
        return scatter;
    }

    public void orbCollected() {
        orbExists = false;
        // scatter = true;
        
        // set all toads to scatter
        // tick up collection
    }

    // Updates when an enemy dies
    public void enemyDown() {
        totalEnemies -= 1;
    }

    public GameObject getPlayer() {
        return player;
    }

    public Vector3 getEnemy() {
        return temp;
    }
}
