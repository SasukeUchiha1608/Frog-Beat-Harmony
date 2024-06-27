using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    // open variables
    public float moveSpeed = 5.0f;
    public float scatterTime = 10f;

    // simple pathfinding to player

    public enum _direction {left, back, right, forward};
    public _direction direct;
    private float tiltY = 0f;
    private float rotation = 90f;

    // movement
    public Vector3 targetPosition;
    public Quaternion targetRotation;
    public float yPosition;

    public float test = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created   

    // allows the toad to chase the player
    public Vector3 directionNorm(float eX, float eZ, GridManager gridManager, Vector3 Pos) {
        targetPosition = gridManager.GetNearestPointOnGrid(Pos);

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

        switch(direct) {
            case _direction.left:
                tiltY = rotation*2f;
                targetPosition += Vector3.left * gridManager.gridSize;
                break;
            case _direction.right:
                tiltY = rotation*0f;
                targetPosition += Vector3.right * gridManager.gridSize;
                break;
            case _direction.forward:
                tiltY = rotation*1f;
                targetPosition += Vector3.forward * gridManager.gridSize;
                break;
            case _direction.back:
                tiltY = rotation*-1f;
                targetPosition += Vector3.back * gridManager.gridSize;
                break;
            default:
                break;
        }

        targetRotation = Quaternion.Euler(0, tiltY, 0);
        targetPosition.y = Pos.y;
        return targetPosition;
    }

    // Have the toad run from the player
    public Vector3 directionScatter(float eX, float eZ, GridManager gridManager, Vector3 Pos) {
        targetPosition = gridManager.GetNearestPointOnGrid(Pos);

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

        targetPosition.y = Pos.y;
        return targetPosition;
    }

    public Quaternion getOrientation(Vector3 currentPos, Vector3 nextPos) {
        Vector3 temp = currentPos - nextPos;
        if(temp.x != 0) {
            if(temp.x > 0)
                tiltY = rotation*2f;
            else
                tiltY = rotation*0f;
        } else {
            if(temp.z < 0)
                tiltY = rotation*1f;
            else
                tiltY = rotation*-1f;
        }

        targetRotation = Quaternion.Euler(0, tiltY, 0);

        return targetRotation;
    }

    public float getMoveSpeed() {
        return moveSpeed;
    }

}
