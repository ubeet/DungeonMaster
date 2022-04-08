using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    public float offset;
    public SpriteRenderer gun;
    public GameObject ammo;
    public Transform shutDir;
    public float speed;
    
    private float startTime = 0;
    private float timeShot = 0;
    
    void FixedUpdate()
    {
        if (Input.GetButton("Fire1"))
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);
            
            if (timeShot <= 0)
            {
                Instantiate(ammo, shutDir.position, transform.rotation);
                timeShot = startTime;
            }
            
            if (transform.rotation.z >= -0.707 && transform.rotation.z <= 0.707)
                gun.flipY = true;
            else
                gun.flipY = false;
        }
        timeShot -= Time.fixedDeltaTime;
        startTime = speed;
        
    }

    
}    
    
