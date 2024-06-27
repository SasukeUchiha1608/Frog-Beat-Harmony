using UnityEngine;

public class FloatScript : MonoBehaviour
{
    public GameObject[] TheHeart = new GameObject[4];
    public float lifeTime = 5f;
    private float counter = 0;

    private Vector3 temp;
    private Vector3 floatingH;
    private Vector3 floatingL;
    private bool up = true;

    private EnemyToadSpawner toggle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        toggle = FindFirstObjectByType<EnemyToadSpawner>();
        TheHeart = GameObject.FindGameObjectsWithTag("Orb");
        floatingH = transform.position;
        floatingL = transform.position;
        floatingH.y += .5f;
        floatingL.y -= .5f;
        Debug.Log("Hellow World");
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y == floatingH.y) 
            up = false;
        if(transform.position.y == floatingL.y) 
            up = true;

        if (up)
            transform.position = Vector3.MoveTowards(transform.position, floatingH, 0.5f*Time.deltaTime);
        else
            transform.position = Vector3.MoveTowards(transform.position, floatingL, 0.5f*Time.deltaTime);

        if(counter < lifeTime) {
            counter += Time.deltaTime;
        } else {
            Debug.Log("I should be dead");
            //toggle.orbCollected();
            Object.Destroy(this.gameObject);
        }
    }

}
