using System.IO;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GUIManager GUI;
    [SerializeField] GameObject player;
    [SerializeField] GameObject deathScreen;
    public int currentHealth;
    public int currentMoney;
    
    private System.Random rand = new System.Random();
    private int maxHealth = 100;
    
    private void Start()
    {
        if (File.Exists(SaveSystem.PlayerPath))
        {
            PlayerData data = SaveSystem.LoadPlayerData();
            currentHealth = data.health;
            currentMoney = data.money;
            GUI.SetMaxHealth(maxHealth);
            GUI.SetHealth(currentHealth);
            GUI.SetMoney(currentMoney);
            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];
            transform.position = position;
        }
        else
        {
            currentHealth = maxHealth;
            GUI.SetMaxHealth(currentHealth);
            GUI.SetHealth(currentHealth);
            GUI.SetMoney(0);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) //for example
            TakeDamage(10);
        if (Input.GetKeyDown(KeyCode.M)) //for example
            TakeMoney(2);
    }
    
    private void TakeDamage(int damage)
    {
        currentHealth -= damage;
        GUI.SetHealth(currentHealth);
        if (currentHealth <= 0) Die();
    }
    
    private void TakeHealing(int health)
    {
        if (currentHealth + health <= maxHealth)
            currentHealth += health;
        else
            currentHealth = maxHealth;
        GUI.SetHealth(currentHealth);
    }
    
    private void TakeMoney(int coins)
    {
        currentMoney += coins;
        GUI.SetMoney(currentMoney);
    }

    private void Die()
    {
        SaveSystem.DeleteData();
        player.SetActive(false);
        deathScreen.SetActive(true);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            TakeMoney(rand.Next(1, 6));
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Potion"))
        {
            TakeHealing(10);
            Destroy(other.gameObject);
        }
        
        if (other.CompareTag("Medicine"))
        {
            TakeHealing(20);
            Destroy(other.gameObject);
        }
    }

    public void SaveGame()
    {
        SaveSystem.SavePlayerData(this);
    }
}
