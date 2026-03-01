using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public ObstacleData data; //ссылка на стат данные
    void OnTriggerEnter(Collider other) //обработка касания 
    {
        if (other.CompareTag("Player") && data != null)
        {
            Destroy(gameObject);
        }
    }
}