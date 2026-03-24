using UnityEngine;
using TMPro;

public class DisplayRecord : MonoBehaviour
{
    public TextMeshProUGUI tmpText;

    void Start()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        tmpText.text = "Best Score: " + PlayerPrefs.GetInt("Record").ToString();
    }
}
