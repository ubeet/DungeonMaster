using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Attributes")]
    
    [SerializeField] private GameObject[] loot;
    
    private int maxHealth = 100;
    private AudioSource source;
    private int currentHealth;
    private Vector3 position;
    
    public bool AI { get; set; } = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0) Die();
    }
    
    private IEnumerator DeathCoroutine()
    {
        while (source.isPlaying)
            yield return null;
        
        System.Random rnd = new System.Random();
        Destroy(transform.parent.parent.gameObject);
        position = transform.position;
        Instantiate(loot[rnd.Next(0, loot.Length)], new Vector3(position.x, position.y, position.z), Quaternion.identity);
    }

    private void Die()
    {
        source = GetComponent<AudioSource>();
        source.Play();
        
        StartCoroutine(DeathCoroutine());
    }
}
