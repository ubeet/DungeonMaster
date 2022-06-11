using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject[] loot;

    public bool AI { get; set; } = false;
    
    private AudioSource source;
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
    
    private IEnumerator deathCoroutine()
    {
        System.Random rnd = new System.Random();
        source = GetComponent<AudioSource>();
        source.Play();
        
        while (source.isPlaying)
            yield return null;
        
        Destroy(transform.parent.parent.gameObject);
        position = transform.position;
        Instantiate(loot[rnd.Next(0, loot.Length)], new Vector3(position.x, position.y, position.z), Quaternion.identity);
    }

    private void Die()
    {
        StartCoroutine(deathCoroutine());
    }
}
