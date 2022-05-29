using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] SpriteRenderer lamp;
    [SerializeField] Transform light;
    [SerializeField] float lightSpeed;

    private Rigidbody2D rb;
    private Vector2 direction;
    private Animator animator;
    private States position = States.down;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
    
    private States State
    {
        get { return (States) animator.GetInteger("state"); }
        set{ animator.SetInteger("state", (int)value); }
    }
    
    public enum States
    {
        up,
        down,
        right,
        left
    }
}
