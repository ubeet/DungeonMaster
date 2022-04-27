using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class Chest : Interactable
{
    [SerializeField] Item[] contents;
    [SerializeField] SpriteRenderer receivedItemSprite;
    [SerializeField] private float hideTime;

    private Item item;
    private bool isOpen;
    private Animator anim;
    private readonly Random rand = new Random();
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        item = contents[rand.Next(0, contents.Length)];
        item.number = rand.Next(1, 11);
        //if (isOpen)
            //anim.Play("idle_opened");
    }

    private void HideItem()
    {
        receivedItemSprite.sprite = null;
        
    }
    
    private void ShowItem()
    {
        receivedItemSprite.sprite = item.itemSprite;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            if (!isOpen)
            {
                anim.Play("open");
                Invoke(nameof(ShowItem), 1);
                Invoke(nameof(HideItem), hideTime);
                //Player.UseBuff(item);
                isOpen = true;
            }
        }
    }
}
