using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Chest : Interactable
{
    [SerializeField] GameObject[] contents;
    
    private readonly Random rand = new Random();
    private GameObject loot;
    private Animator anim;
    private bool isOpen;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        loot = contents[rand.Next(contents.Length)];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            if (!isOpen)
            {
                anim.Play("open");
                var position = transform.position;
                Instantiate(loot, new Vector3(position.x - 2, position.y, position.z), Quaternion.identity);
                isOpen = true;
            }
        }
    }
}
