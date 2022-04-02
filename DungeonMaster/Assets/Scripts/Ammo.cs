using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public float speed;
    public float destroyTime;
    void Start()
    {
        Invoke("DestroyAmmo", destroyTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag != "Player")
            Destroy(gameObject);
    }

    void FixedUpdate()
    {
        transform.Translate(Vector2.right * speed * Time.fixedDeltaTime);
    }

    void DestroyAmmo()
    {
        Destroy(gameObject);
    }
}
