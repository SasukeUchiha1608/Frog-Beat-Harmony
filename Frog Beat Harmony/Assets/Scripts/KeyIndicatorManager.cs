using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyIndicatorManager : MonoBehaviour
{
    public GameObject keyIndicatorPrefab;
    public Transform player;
    public int gridSize = 1;
    private GridManager gridManager;

    private Dictionary<Vector3, string> keyDirectionMap = new Dictionary<Vector3, string>
    {
        { Vector3.forward, "W" },
        { Vector3.back, "S" },
        { Vector3.left, "A" },
        { Vector3.right, "D" }
    };

    private List<GameObject> indicators = new List<GameObject>();

    void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
        if (gridManager == null)
        {
            Debug.LogError("GridManager not found!");
        }
        UpdateKeyIndicators();
    }

    public void UpdateKeyIndicators()
    {
        Debug.Log("Updating Key Indicators");

        // Clear previous indicators
        foreach (GameObject indicator in indicators)
        {
            Destroy(indicator);
        }
        indicators.Clear();

        // Create new indicators
        foreach (KeyValuePair<Vector3, string> entry in keyDirectionMap)
        {
            Vector3 direction = entry.Key;
            string key = entry.Value;
            Vector3 indicatorPosition = player.position + direction * gridSize;
            indicatorPosition = gridManager.GetNearestPointOnGrid(indicatorPosition);

            Debug.Log($"Creating indicator at {indicatorPosition} with key {key}");

            GameObject indicator = Instantiate(keyIndicatorPrefab, indicatorPosition, Quaternion.identity);
            if (indicator != null)
            {
                TextMeshPro tmp = indicator.GetComponent<TextMeshPro>();
                if (tmp != null)
                {
                    tmp.text = key;
                    Debug.Log($"TextMeshPro component found and set: {key}");
                }
                else
                {
                    Debug.LogError("TextMeshPro component not found on keyIndicatorPrefab!");
                }
                indicator.transform.SetParent(transform, false); // Set the parent to ensure it is within the canvas
                indicator.transform.localScale = Vector3.one * 0.1f; // Adjust the scale as needed
                indicators.Add(indicator);
            }
            else
            {
                Debug.LogError("Failed to instantiate keyIndicatorPrefab!");
            }
        }
    }
}
