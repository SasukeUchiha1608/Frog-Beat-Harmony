using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // open variables
    public GameObject player;
    public float moveSpeed = 5.0f;
    public float tempo = 1f;
    public float scatter = 10f;

    // simple pathfinding to player
    public float eX;
    public float eZ;
    public enum _direction {left, back, right, forward};
    public _direction direct;
    public float tiltY = 0f;
    public float rotation = 90f;

    // movement
    public float distance;
    public GridManager gridManager;
    public Vector3 targetPosition;
    public Quaternion targetRotation;
    public float yPosition;
    public bool AutoMove = true;
    public bool scatterMode = false;
    public float counterM = 0;
    public float counterS = 0;

    public float test = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created   

    public void direction() {
        if(scatterMode) {
            if (Mathf.Abs(eX) > Mathf.Abs(eZ)) // will move away from the player
                    if(eX < 0)
                        direct = _direction.left;
                    else
                        direct = _direction.right;
                else
                    if(eZ > 0)
                        direct = _direction.forward;
                    else
                        direct = _direction.back;
        } else {
            if (Mathf.Abs(eX) > Mathf.Abs(eZ)) // chases the player
                if(eX > 0)
                    direct = _direction.left;
                else
                    direct = _direction.right;
            else
                if(eZ < 0)
                    direct = _direction.forward;
                else
                    direct = _direction.back;
        }
    }

    // Difficulty ramp
    public void TempoUp(float ramp) 
    {
        /*
        * We ramp up the tempo the frogs move here
        * increace frog count?
        * Figure out parameters how it should speed up
        * Speed cap?
        */
        tempo -= ramp;
        moveSpeed += ramp;

    }

    /*
    * scatter
    *   orb pick up
    *   player hit
    *   parry
    * inky blinky pinky clyde
    * 
    */

    // Destroy enemy method
    /**
    void OnCollisionEnter (Collision col)
    {
        // if tag enemy, turn.
        // if tag player, destroy.
    }
    /**/
}
