using UnityEngine;
using TMPro;

public class TextHPandpoints : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI tmpText;

    void Start()
    {
        tmpText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(player != null)
        {
            tmpText.text = player.GetComponent<PlayerHealth>().curHealth.ToString() + "/" + player.GetComponent<PlayerHealth>().maxHealth;
        }
    }
}
