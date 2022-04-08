using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using Random = UnityEngine.Random;

public class Ammo : MonoBehaviour
{
    public float speed;
    public float destroyTime;
    private float random;
    void Start()
    {
        random = Random.Range(-10, 10) / 300f;
        Invoke("DestroyAmmo", destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag != "Player" && other.gameObject.tag != "bortik")
            Destroy(gameObject);
    }

    void FixedUpdate()
    {
        transform.Translate((Vector2.right + new Vector2(0, random)) * speed * Time.fixedDeltaTime);
    }

    void DestroyAmmo()
    {
        Destroy(gameObject);
    }
}
