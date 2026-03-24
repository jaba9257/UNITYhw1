using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public float invincibilityDuration = 1f;

    private int currentHealth;
    private bool isInvincible = false;
    private float invincibilityTimer;
    private float score;
    public Animator animator;
    public int curHealth { get  { return currentHealth; } }

    void Start() //устанавливаем хп
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        score += Time.deltaTime * gameObject.GetComponent<PlayerController>().forwardSpeed;
        if (isInvincible) // Если действуют кадры неузявимости 
        {
            invincibilityTimer -= Time.deltaTime;
            if (invincibilityTimer <= 0)
            {
                isInvincible = false;
                animator.SetBool("IsShield", false);
            }//выключаем неузявимость
            
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
        animator.ResetTrigger("Hit");
        animator.SetTrigger("Hit");
        if (currentHealth <= 0) Die();
        else
        {
            isInvincible = true;
            invincibilityTimer = invincibilityDuration;
        }
    }

    public void TakeHeal(int amount)
    {
        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public void ActivateShield(float duration)
    {
        isInvincible = true;
        invincibilityTimer = duration;
        animator.SetBool("IsShield", true);
    }

    void Die() //если умерли то загружаем заново сцену
    {
        int record = PlayerPrefs.GetInt("Record", 0);

        if (score > record)
        {
            PlayerPrefs.SetInt("Record", (int)score);
        }

        PlayerPrefs.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}