using UnityEngine;
using TMPro;

public class KeyIndicator : MonoBehaviour
{
    public int id;
    public TextMeshProUGUI tmp;

    void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        if (tmp == null)
        {
            Debug.LogError("TextMeshProUGUI component not found!");
        }
    }

    public void SetID(int newID)
    {
        id = newID;
    }

    public void SetText(string text)
    {
        if (tmp != null)
        {
            tmp.text = text;
        }
    }

    public void SetColor(Color color)
    {
        if (tmp != null)
        {
            tmp.color = color;
        }
    }
}
