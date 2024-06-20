using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class KeyIndicatorManager : MonoBehaviour
{
    public GameObject wIndicatorPrefab;
    public GameObject aIndicatorPrefab;
    public GameObject sIndicatorPrefab;
    public GameObject dIndicatorPrefab;
    public Transform player;
    public float gridSize = 1.0f;
    private Dictionary<string, GameObject> keyIndicators = new Dictionary<string, GameObject>();

    void Start()
    {
        CreateKeyIndicator("W", wIndicatorPrefab, Vector3.forward);
        CreateKeyIndicator("A", aIndicatorPrefab, Vector3.left);
        CreateKeyIndicator("S", sIndicatorPrefab, Vector3.back);
        CreateKeyIndicator("D", dIndicatorPrefab, Vector3.right);
    }

    void Update()
    {
        UpdateKeyIndicators();
    }

    void CreateKeyIndicator(string key, GameObject prefab, Vector3 direction)
    {
        GameObject indicator = Instantiate(prefab, transform);
        indicator.GetComponent<TextMeshProUGUI>().text = key;  // Ensure this uses TextMeshProUGUI
        keyIndicators[key] = indicator;
    }

    void UpdateKeyIndicators()
    {
        Vector3 playerPosition = player.position;

        // Calculate the world positions for the key indicators
        Vector3 wPosition = playerPosition + Vector3.forward * gridSize;
        Vector3 aPosition = playerPosition + Vector3.left * gridSize;
        Vector3 sPosition = playerPosition + Vector3.back * gridSize;
        Vector3 dPosition = playerPosition + Vector3.right * gridSize;

        // Convert the world positions to screen space
        keyIndicators["W"].transform.position = Camera.main.WorldToScreenPoint(wPosition);
        keyIndicators["A"].transform.position = Camera.main.WorldToScreenPoint(aPosition);
        keyIndicators["S"].transform.position = Camera.main.WorldToScreenPoint(sPosition);
        keyIndicators["D"].transform.position = Camera.main.WorldToScreenPoint(dPosition);
    }
}
