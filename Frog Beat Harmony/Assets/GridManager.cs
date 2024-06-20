using UnityEngine;

public class GridManager : MonoBehaviour
{
    public float gridSize = 1.0f;

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        int xCount = Mathf.RoundToInt(position.x / gridSize);
        int zCount = Mathf.RoundToInt(position.z / gridSize);

        Vector3 result = new Vector3(
            xCount * gridSize,
            position.y,
            zCount * gridSize);

        return result;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (float x = -10; x < 10; x += gridSize)
        {
            for (float z = -10; z < 10; z += gridSize)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, 0, z));
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
    }
}
