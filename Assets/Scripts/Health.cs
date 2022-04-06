using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] int currentHealth;
    [SerializeField] GUIManager healthBar;
    
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //for example
            TakeDamage(10);
    }
    
    private void TakeDamage(int damage)
    {
        if (currentHealth <= 0) Die();
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    private void Die()
    {
        //DeathScreen
    }
}
