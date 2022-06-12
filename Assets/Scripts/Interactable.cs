using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected bool playerInRange;
    protected Collider2D other;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            this.other = other;
            playerInRange = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger) playerInRange = false;
    }
}
