using UnityEngine;
using Random = UnityEngine.Random;

public class Ammo : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float destroyTime;
    private float random;
    private void Start()
    {
        random = Random.Range(-10, 10) / 300f;
        Invoke(nameof(DestroyAmmo), destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("bortik") && !other.isTrigger)
            Destroy(gameObject);
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
