using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private string playerTag;
    [SerializeField] private float movingSpeed;

    private void Awake()
    {
        if (playerTransform == null)
        {
            if (playerTag == "")
                playerTag = "Player";
            playerTransform = GameObject.FindGameObjectWithTag(playerTag).transform;
        }

        playerTransform.position = new Vector3()
        {
            x = playerTransform.position.x,
            y = playerTransform.position.y,
            z = playerTransform.position.z
        };
        
    }

    
    private void FixedUpdate()
    {
        if (playerTransform)
        {
            Vector3 target = new Vector3()
            {
                x = playerTransform.position.x,
                y = playerTransform.position.y,
                z = playerTransform.position.z - 10
            };
            Vector3 pos = Vector3.Lerp(transform.position, target, movingSpeed * Time.fixedDeltaTime);
            transform.position = pos;
        }
    }
}
