using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyIndicatorManager : MonoBehaviour
{
    public GameObject keyIndicatorPrefab;
    public Transform player;
    public int gridSize = 1;
    private GridManager gridManager;
    public float indicatorHeight = 1.0f; // Adjust this value as needed to position the indicators above the terrain

    public Transform canvas;

    private List<GameObject> indicators = new List<GameObject>();

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager == null)
        {
            Debug.LogError("GridManager not found!");
        }
        CreateKeyIndicators();
        UpdateKeyIndicators();
    }

    void Update()
    {
        UpdateKeyIndicators();
    }

    private void CreateKeyIndicators()
    {
        // Create new indicators
        Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
        string[] keys = { "W", "S", "A", "D" };

        for (int i = 0; i < directions.Length; i++)
        {
            GameObject indicator = Instantiate(keyIndicatorPrefab);
            indicator.transform.SetParent(canvas, false);
            indicator.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            TextMeshProUGUI tmp = indicator.GetComponent<TextMeshProUGUI>();
            if (tmp != null)
            {
                tmp.text = keys[i];
                tmp.alignment = TextAlignmentOptions.Center;
                tmp.fontSize = 9.0f;

                switch (keys[i]) {
                    case "W":
                    tmp.color = Color.yellow;
                    break;
                    
                    case "A":
                    tmp.color = Color.blue;
                    break;
                    
                    case "S":
                    tmp.color = Color.green;
                    break;

                    case "D":
                    tmp.color = Color.red;
                    break;
                }
            }
            indicators.Add(indicator);
        }
    }

    public void UpdateKeyIndicators()
    {
        Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };

        for (int i = 0; i < indicators.Count; i++)
        {
            Vector3 direction = directions[i];
            GameObject indicator = indicators[i];
            Vector3 indicatorPosition = player.position + direction * gridSize;

            indicatorPosition = gridManager.GetNearestPointOnGrid(indicatorPosition);
            indicatorPosition.y = player.position.y; // Keep y consistent with player's y
            indicatorPosition.z = player.position.z + direction.z * gridSize; // Ensure z is adjusted correctly

            indicator.transform.position = indicatorPosition;
        }
    }
}
