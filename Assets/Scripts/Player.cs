using System.IO;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Attributes")]
    
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GUIManager GUI;
    
    private readonly System.Random rnd = new System.Random();
    private int maxHealth = 100;
    private AudioSource source;

    public bool isDead { get; set; } = false;
    public int pHealing { get; set; } = 10;
    public int mHealing { get; set; } = 40;
    public int currentHealth { get; set; }
    public int currentMoney { get; set; }
    
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
        if (Input.GetKeyDown(KeyCode.Space)) TakeDamage(10); //for example
        if (Input.GetKeyDown(KeyCode.M)) TakeMoney(2); //for example
    }
    
    internal void TakeDamage(int damage)
    {
        currentHealth -= damage;
        GUI.SetHealth(currentHealth);
        
        if (currentHealth <= 0) Die();
    }
    
    internal void TakeHealing(int health)
    {
        if (currentHealth + health <= maxHealth) currentHealth += health;
        else currentHealth = maxHealth;
        
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
        GetComponent<PolygonCollider2D>().enabled = false;
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
