using UnityEngine;
using Random = UnityEngine.Random;

public class Ammo : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float destroyTime;
    [SerializeField] private int damage;
    [SerializeField] private bool isEnemy;
    private Player actionTarget;
    private Enemy actionTargetE;
    private float random;
    
    private void Start()
    {
        random = Random.Range(-10, 10) / 300f;
        Invoke(nameof(DestroyAmmo), destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isEnemy)
        {
            if (!other.gameObject.CompareTag("Enemy") && !other.gameObject.CompareTag("bortik") && !other.isTrigger)
            {
                Destroy(gameObject);
                if (other.gameObject.CompareTag("Player"))
                {
                    actionTarget = other.gameObject.GetComponent<Player>();
                    actionTarget.TakeDamage(damage);
                }
            }
        }
        else
        {
            if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("bortik") && !other.isTrigger)
            {
                Destroy(gameObject);
                if (other.gameObject.CompareTag("Enemy"))
                {
                    actionTargetE = other.gameObject.GetComponent<Enemy>();
                    actionTargetE.TakeDamage(damage);   
                }
            }
        }
    }

    private void FixedUpdate()
    {
        transform.Translate((Vector2.right + new Vector2(0, random)) * speed * Time.fixedDeltaTime);
    }

    void DestroyAmmo()
    {
        Destroy(gameObject);
    }
}
