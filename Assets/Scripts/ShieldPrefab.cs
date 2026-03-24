using UnityEngine;

public class ShieldBuff : MonoBehaviour
{
    public float duration = 3f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth health = other.GetComponent<PlayerHealth>();

        if (health != null)
        {
            health.ActivateShield(duration);
            Destroy(gameObject);
        }
    }
}