using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction;

    public GameObject GunPosChange;
    //public Joystick joystick;
    private Rigidbody2D rb;
    public Renderer gun;
    private Animator animator;
    private States position = States.idle_down;
    

    // Start is called before the first frame update
    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gun.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //direction.x = joystick.Horizontal;
        //direction.y = joystick.Vertical;
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");
        if(direction.y == 0 && direction.x == 0)
            State = position;
        if (Input.GetButton("Fire1"))
        {
            gun.enabled = true;
            if (GunPosChange.transform.rotation.z > 0.386 && GunPosChange.transform.rotation.z < 0.922)
            {
                State = States.up;
                position = States.idle_up;
                gun.sortingOrder = 0;
                
            }
            else if (GunPosChange.transform.rotation.z > -0.922 && GunPosChange.transform.rotation.z <= -0.386)
            {
                State = States.down;
                position = States.idle_down;
                gun.sortingOrder = 2;
            }
            else if (GunPosChange.transform.rotation.z > -0.386 && GunPosChange.transform.rotation.z <= 0.386)
            {
                State = States.right;
                position = States.idle_right;
                gun.sortingOrder = 2;
            }
            else if (!(GunPosChange.transform.rotation.z > -0.922 && GunPosChange.transform.rotation.z <= 0.922))
            {
                State = States.left;
                position = States.idle_left;
                gun.sortingOrder = 0;
            }
        }
        else
        {
            gun.enabled = false;
            if (direction.x > 0)
            {
                State = States.right;
                position = States.idle_right;
                //gun.sortingOrder = 2;
            }
            else if (direction.x < 0)
            {
                State = States.left;
                position = States.idle_left;
                //gun.sortingOrder = 0;
            }
            else if (direction.y < 0)
            {
                State = States.down;
                position = States.idle_down;
                //gun.sortingOrder = 2;
            }
            else if (direction.y > 0)
            {
                State = States.up;
                position = States.idle_up;
                //gun.sortingOrder = 0;
            }
        }
        
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
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
        left,
        right,
        idle_up,
        idle_down,
        idle_left,
        idle_right
    }

}
