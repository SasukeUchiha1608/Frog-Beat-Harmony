using UnityEngine;

public class FloatScript : MonoBehaviour
{
    public GameObject TheHeart;
    public float spawnTime = 5f;
    private float counter = 0;
    public float spawnXmax = 11;
    public float spawnZmax = 10;
    private EnemyToadSpawner toggle;
    private bool orbSpawn = false;

    private float xPos;
    private float zPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        toggle = GetComponent<EnemyToadSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        Mathf.Sin(Time.deltaTime);

        if(orbSpawn) {
            xPos = Random.Range(0, spawnXmax);
            zPos = Random.Range(0, spawnZmax);

            Instantiate(TheHeart, new Vector3(xPos, 1, zPos), Quaternion.identity);       
            
            /**
            OnCollisionEnter(collision col) {
                if(tag == player)
                toggle.orbCollected();
                
                //destroy self
                destroy.destroy(this.GameObject);
            }
            /**/
        } else {
            counter += Time.deltaTime;
            if(counter > spawnTime)
                orbSpawn = !orbSpawn;
        }
    }

}
