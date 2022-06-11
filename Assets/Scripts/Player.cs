using System.IO;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GUIManager GUI;
    [SerializeField] private GameObject deathScreen;
    
    public int currentHealth { get; set; }
    public int currentMoney { get; set; }

    private AudioSource source;
    private readonly System.Random rnd = new System.Random();
    private int maxHealth = 100;

    public bool isDead { get; set; } = false;
    public int pHealing { get; set; } = 10;
    public int mHealing { get; set; } = 40;
    
    private void Start()
    {
        source = GetComponent<AudioSource>();
        
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
    
    internal void TakeDamage(int damage)
    {
        currentHealth -= damage;
        GUI.SetHealth(currentHealth);
        if (currentHealth <= 0) Die();
    }
    
    internal void TakeHealing(int health)
    {
        if (currentHealth + health <= maxHealth)
            currentHealth += health;
        else
            currentHealth = maxHealth;
        GUI.SetHealth(currentHealth);
    }
    
    internal void TakeMoney(int coins)
    {
        currentMoney += coins;
        GUI.SetMoney(currentMoney);
    }

    private void Die()
    {
        SaveSystem.DeleteData();
        isDead = true;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        deathScreen.SetActive(true);
        Time.timeScale = 0;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin") && other.GetComponent<Item>().isBuff)
        {
            TakeMoney(rnd.Next(1, 4));
            Destroy(other.gameObject);
            source.Play();
        }
        if (other.CompareTag("CoinSack") && other.GetComponent<Item>().isBuff)
        {
            TakeMoney(rnd.Next(10, 16));
            Destroy(other.gameObject);
            source.Play();
        }
        if (other.CompareTag("Potion") && other.GetComponent<Item>().isBuff)
        {
            TakeHealing(pHealing);
            Destroy(other.gameObject);
            source.Play();
        }
        if (other.CompareTag("Medicine") && other.GetComponent<Item>().isBuff)
        {
            TakeHealing(mHealing);
            Destroy(other.gameObject);
            source.Play();
        }
    }

    public void SaveGame()
    {
        SaveSystem.SavePlayerData(this);
    }
}
