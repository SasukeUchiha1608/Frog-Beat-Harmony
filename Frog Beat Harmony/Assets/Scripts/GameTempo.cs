using UnityEngine;

public class GameTempo : MonoBehaviour
{
    public float tempo;
    private float counter;
    public float rotationsPerMinute = 10;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /**
        sin wave to affect it's y position
        linked with the current tempo (tbd)

        /**/

        //Makes the orb rotate continuously
        transform.Rotate(0,6.0f*rotationsPerMinute*Time.deltaTime,0);
    }

    public void TempoUp(float ramp) {
        tempo -= ramp;
    }
}
