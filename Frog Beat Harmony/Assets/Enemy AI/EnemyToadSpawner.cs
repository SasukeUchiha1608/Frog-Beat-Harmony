using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyToadSpawner : MonoBehaviour
{
    // Spawn area Variables
    public GameObject TheEnemy;
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
    private int capEnemies = 5;
    private int totalEnemies = 0;
    private int set = 2;
    private EnemyAI eAI;
    private int enemyID = 0;

    // Spawn timer control
    public float spawnTime = 6f;
    private float counter = 0;  
    private bool spawn = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        eAI = GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(spawn && (totalEnemies < capEnemies)) {

            // Spawns in enemies
            while (set < 2)
            {
                xPos = Random.Range(spawnXmin, spawnXmax);
                zPos = Random.Range(spawnZmin, spawnZmax);

                Instantiate(TheEnemy, new Vector3(xPos, 1, zPos), Quaternion.identity);
                enemyID += 1;
                totalEnemies += 1;
                
                //spawns in the orb that destroys the enemy.
                // possiable to set every X enemies defeated does damage to the boss
                xPos = Random.Range(0, boardX+1);
                zPos = Random.Range(0, boardZ+1);
                Instantiate(TheHeart, new Vector3(xPos, 1, zPos), Quaternion.identity);

                set += 1;
            }   
            counter = 0;
            set = 0;
            this.spawn = !this.spawn;
        } else {
            counter += Time.deltaTime;
            if(counter > spawnTime)
                this.spawn = !this.spawn;
        }

        /**
        if(dificulty ramp value) 
        {
            TempoUp(); //increaces the speed of the enemy toads
            spawnTime += 0.1f; //minimim spawn rate of the frogs?
            if (capEnemies < maxEnemies) // raises the total enemies avle to be smawned in
                capEnemies += 1;
        }
        /**/
    }

    public int getID() 
    {
        return enemyID++;
    }

    // Updates when an enemy dies
    public void enemyDown() {
        totalEnemies -= 1;
    }
}
