using System.Collections;
using UnityEngine;
using Random = System.Random;

public class Chest : Interactable
{
    [SerializeField] GameObject[] contents;
    
    private readonly Random rand = new Random();
    private GameObject loot;
    private Animator anim;
    private Vector3 position;
    private bool isOpen;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        loot = contents[rand.Next(contents.Length)];
        position = transform.position;
    }
    private IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Instantiate(loot, new Vector3(position.x - 2, position.y, position.z), Quaternion.identity);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            if (!isOpen)
            {
                anim.Play("open");
                StartCoroutine(Wait(0.8f));
                isOpen = true;
            }
        }
    }
}
