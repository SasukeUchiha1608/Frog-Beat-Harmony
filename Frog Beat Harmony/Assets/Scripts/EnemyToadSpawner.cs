using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Add in a function to manage grid boundries
* prepare code to generate the tiles randomly
* check if the vsync works correctly
* try to savely merge the main and enemy branch together
* add in the sounds as soon as i can tomorrow
*/


public class EnemyToadSpawner : MonoBehaviour
{
    // Spawgn area Variables
    public GameObject player;
    public GameObject Toad_Blinky; // chases the player
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
    private Blinky blink;
    private Vector3[] locations;
    public int capEnemies = 6;
    private int maxEnemies = 1;
    private int totalEnemies = 0;

    // Spawn timer control
    public float ramp = 0.1f;
    public float spawnTime = 6f;
    public float scatterTime = 3f;
    public float scatterIntervul = 20f;
    public float rampIntervul = 10f;
    public float orbCooldown = 10f;
    private float counterSpawn = 0;
    private float counterIntervul = 0;
    private float counterSTime = 0;   
    private float counterOrb = 0;
    private float counterRamp = 0;
    private bool toadSpawn = false;
    private bool orbSpawn = false;
    private bool orbExists = false;
    private bool scatter;

    private int orbsCollect = 0;

    private GridManager gridManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // hopefully this works
        locations = new Vector3[6];
        QualitySettings.vSyncCount = 1;
        player = Instantiate(player, playerSpawn, Quaternion.identity);

        blink = FindFirstObjectByType<Blinky>();
        gridManager = FindFirstObjectByType<GridManager>();
        temp.x = Random.Range(spawnXmin, spawnXmax);
        temp.z = Random.Range(spawnZmin, spawnZmax);
        if (temp.z == player.transform.position.x || temp.z == player.transform.position.z)
            temp.z += 1;
        temp.y = 1;
        temp = gridManager.GetNearestPointOnGrid(temp);
        Toad_Blinky = Instantiate(Toad_Blinky, temp, Quaternion.identity);
        locations[0] = temp;

        totalEnemies += 1;
    }

    // Update is called once per frame
    void Update()
    {
            if(orbsCollect > 6) {
                Debug.Log("Player Won");
            }
        // spawn enemies
        if (totalEnemies < capEnemies && toadSpawn) {
            
            temp.x = Random.Range(spawnXmin, spawnXmax);
            temp.z = Random.Range(spawnZmin, spawnZmax);
            if (temp.z == player.transform.position.x || temp.z == player.transform.position.z)
                temp.z += 1;
            temp.y = 1;
            temp = gridManager.GetNearestPointOnGrid(temp);

            Toad_Blinky = Instantiate(Toad_Blinky, temp, Quaternion.identity);
            locations[totalEnemies] = temp;

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
                    scatterIntervul += 20f;
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

        if(counterRamp < rampIntervul) {
            counterRamp += Time.deltaTime;
        } else {
            blink.TempoUp(0.1f);
            counterRamp = 0;
        }

        
    }

    public bool scatterMode() {
        return scatter;
    }

    public void orbCollected() {
        orbExists = false;
        scatter = true;
        orbsCollect += 1;
        blink.TempoUp(0.3f);
        // set all toads to scatter
        // tick up collection
    }

    public GameObject getPlayer() {
        return player;
    }

    public int getID() {
        return totalEnemies;
    }

    public void updateTarget(int ID, Vector3 nextPos) {
        locations[ID] = nextPos;
    }

    public bool enemyColide(int ID) {
        for(int i = 0; i < 6; i++) {
            if(i != ID && locations[i] != null)
                if(locations[ID] == locations[i])
                    return true;
        }
        if(locations[ID].x >= boardX || locations[ID].z >= boardZ)
            return true;
        return false;
    }

}
