using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTempo : MonoBehaviour
{

    public float tempo = 1f;
    private float test = 0;
    EnemyAI movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movement = GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(test < tempo)
            test += Time.deltaTime;
        else
            test = 0;
    }
}
