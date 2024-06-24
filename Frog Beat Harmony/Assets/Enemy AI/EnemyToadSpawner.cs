using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyToadSpawner : MonoBehaviour
{
    // Spawn area Variables
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

    private float xPos;
    private float zPos;

    // possiable to make this code spawn in the orbs too?

    // Enemy control
    public int maxEnemies = 5;
    private int capEnemies = 2;
    private int totalEnemies = 0;
    private int set = 2;


    // Spawn timer control
    public float ramp = 0.1f;
    public float spawnTime = 6f;
    public float orbCooldown = 10f;
    private float counter = 0;  
    private bool toadSpawn = true;
    private bool orbSpawn = false;
    private bool orbExists = false;
    private bool scatter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        xPos = Random.Range(spawnXmin, spawnXmax);
        zPos = Random.Range(spawnZmin, spawnZmax);

        Instantiate(Toad_Blinky, new Vector3(xPos, 1, zPos), Quaternion.identity);

        xPos = Random.Range(spawnXmin, spawnXmax);
        zPos = Random.Range(spawnZmin, spawnZmax);

        Instantiate(Toad_Pinky, new Vector3(xPos, 1, zPos), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if(toadSpawn && (totalEnemies < capEnemies)) {
            /*
            * random switch case for next spawn?
            * or random next added new ai type
            * notes if enemy is loaded
            */
        } else {
            counter += Time.deltaTime;
            if(counter > spawnTime)
                toadSpawn = !toadSpawn;
        }

        if(orbSpawn && !orbExists)
        {
            xPos = Random.Range(0, boardX+1);
            zPos = Random.Range(0, boardZ+1);
            Instantiate(TheHeart, new Vector3(xPos, 1, zPos), Quaternion.identity);
            orbSpawn = !orbSpawn;
            orbExists = true;
        } else {
            counter += Time.deltaTime;
            if(counter > orbCooldown)
                orbSpawn = !orbSpawn;
        }

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
        scatter = true;
        
        // set all toads to scatter
        // tick up collection
    }

    // Updates when an enemy dies
    public void enemyDown() {
        totalEnemies -= 1;
    }

    
}
