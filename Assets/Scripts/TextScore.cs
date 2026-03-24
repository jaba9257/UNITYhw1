using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class TextScore : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI tmpText;
    private float score = 0;

    void Start()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        
        if (player != null)
        {
            score += Time.deltaTime * player.GetComponent<PlayerController>().forwardSpeed;
            tmpText.text = "Score: " + (int)score;
        }
    }
}
