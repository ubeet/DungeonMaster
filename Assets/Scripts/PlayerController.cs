using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction;
    
    public SpriteRenderer lamp;

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

    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
    }
    // Update is called once per frame
    void FixedUpdate() {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        //direction.x = joystick.Horizontal;
        //direction.y = joystick.Vertical;
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");
        bool stay = direction.y == 0 && direction.x == 0;
            

        if (Input.GetButton("Fire1"))
        {
            gun.enabled = true;
            if (GunPosChange.transform.rotation.z > 0.386 && GunPosChange.transform.rotation.z < 0.922)
            {
                State = stay ? States.idle_up : States.up;
                position = States.idle_up;
                gun.sortingOrder = 0;
                lamp.sortingOrder = 0;
                lamp.flipX = true;
            }
            else if (GunPosChange.transform.rotation.z > -0.922 && GunPosChange.transform.rotation.z <= -0.386)
            {
                State = stay ? States.idle_down : States.down;
                position = States.idle_down;
                gun.sortingOrder = 2;
                lamp.sortingOrder = 2;
                lamp.flipX = false;
            }
            else if (GunPosChange.transform.rotation.z > -0.386 && GunPosChange.transform.rotation.z <= 0.386)
            {
                State = stay ? States.idle_right : States.right;
                position = States.idle_right;
                gun.sortingOrder = 2;
                lamp.sortingOrder = 0;
                lamp.flipX = false;
            }
            else if (!(GunPosChange.transform.rotation.z > -0.922 && GunPosChange.transform.rotation.z <= 0.922))
            {
                State = stay ? States.idle_left : States.left;
                position = States.idle_left;
                gun.sortingOrder = 0;
                lamp.sortingOrder = 2;
                lamp.flipX = true;
            }
        }
        else
        {
            gun.enabled = false;
            if(direction.y == 0 && direction.x == 0)
                State = position;
            else if (direction.x > 0)
            {
                State = States.right;
                position = States.idle_right;
                lamp.sortingOrder = 0;
                lamp.flipX = false;
            }
            else if (direction.x < 0)
            {
                State = States.left;
                position = States.idle_left;
                lamp.sortingOrder = 2;
                lamp.flipX = true;
            }
            else if (direction.y < 0)
            {
                State = States.down;
                position = States.idle_down;
                lamp.sortingOrder = 2;
                lamp.flipX = false;
            }
            else if (direction.y > 0)
            {
                State = States.up;
                position = States.idle_up;
                lamp.sortingOrder = 0;
                lamp.flipX = true;
            }
            
        }
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
