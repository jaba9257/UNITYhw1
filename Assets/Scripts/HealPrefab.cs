using UnityEngine;

public class HealBuff : MonoBehaviour
{
    public int healAmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth health = other.GetComponent<PlayerHealth>();

        if (health != null)
        {
            health.TakeHeal(healAmount);
            Destroy(gameObject);
        }
    }
}