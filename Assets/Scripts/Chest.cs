using System.Collections;
using UnityEngine;
using Random = System.Random;

public class Chest : Interactable
{
    [Header("Attributes")]
    
    [SerializeField] private GameObject[] contents;
    
    private readonly Random rand = new Random();
    private AudioSource source;
    private Vector3 position;
    private GameObject loot;
    private Animator anim;
    
    public bool IsOpen { get; set; } = false;

    private void Start()
    {
        Initialize();
    }

    internal void Initialize()
    {
        loot = contents[rand.Next(contents.Length)];
        source = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        position = transform.position;
    }
    
    private IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Instantiate(loot, new Vector3(position.x - 2, position.y, position.z), Quaternion.identity);
    }

    internal void OpenChest()
    {
        anim.Play("open");
        source.Play();
        IsOpen = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange && !IsOpen)
        {
            OpenChest();
            StartCoroutine(Wait(0.8f));
        }
    }
}
