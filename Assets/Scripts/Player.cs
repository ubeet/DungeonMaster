using System.IO;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    public int currentHealth;
    public int currentMoney;
    [SerializeField] GUIManager GUI;

    private void Start()
    {
        if (File.Exists(SaveSystem.path))
        {
            PlayerData data = SaveSystem.LoadData();
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
        if (currentHealth <= 0) Die();
        currentHealth -= damage;
        GUI.SetHealth(currentHealth);
    }
    
    private void TakeMoney(int coins)
    {
        currentMoney += coins;
        GUI.SetMoney(currentMoney);
    }

    private void Die()
    {
        //DeathScreen
    }

    public void SaveGame()
    {
        SaveSystem.SaveData(this);
    }
}
