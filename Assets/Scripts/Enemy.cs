using System.IO;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject loot;

    internal bool AI = false;
    private Vector3 position;
    private int currentHealth;
    
    private int maxHealth = 100;
    private int damage;
    
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
        Destroy(transform.parent.parent.gameObject);
        position = transform.position;
        Instantiate(loot, new Vector3(position.x, position.y, position.z), Quaternion.identity);
    }
}
