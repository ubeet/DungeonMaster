using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject[] loot;

    internal bool AI = false;
    
    private int maxHealth = 100;
    private Vector3 position;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }

    private void Die()
    {
        System.Random rnd = new System.Random();
        Destroy(transform.parent.parent.gameObject);
        position = transform.position;
        Instantiate(loot[rnd.Next(0, loot.Length)], new Vector3(position.x, position.y, position.z), Quaternion.identity);
    }
}
