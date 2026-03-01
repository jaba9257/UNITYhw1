using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public float invincibilityDuration = 1f;

    private int currentHealth;
    private bool isInvincible = false;
    private float invincibilityTimer;

    void Start() //устанавливаем хп
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isInvincible) // Если действуют кадры неузявимости 
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0) isInvincible = false; //выключаем неузявимость
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Obstacle obstacle = other.GetComponent<Obstacle>(); //Если коснулись препядствие 
        if (obstacle != null) TakeDamage(obstacle.data.damageAmount);
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return; // если неуязвимость выходим иначе уменьшаем хп
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
        else
        {
            isInvincible = true;
            invincibilityTimer = invincibilityDuration;
        }
    }

    void Die() //если умерли то загружаем заново сцену
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}