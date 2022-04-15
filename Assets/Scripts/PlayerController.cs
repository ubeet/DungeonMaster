using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] SpriteRenderer lamp;
    [SerializeField] Transform light;
    [SerializeField] float lightSpeed;
    [SerializeField] GameObject GunPosChange;
    //Joystick joystick;
    [SerializeField] Renderer gun;
    
    private Rigidbody2D rb;
    private Vector2 direction;
    private Animator animator;
    private States position = States.idle_down;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gun.enabled = false;
    }
    
    void FixedUpdate() {
        //direction.x = joystick.Horizontal;
        //direction.y = joystick.Vertical;
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");
        
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        
        bool stay = direction.y == 0 && direction.x == 0;
        if (Input.GetButton("Fire1"))
        {
            gun.enabled = true;
            var pos = GunPosChange.transform.rotation.z;
            
            if (pos > 0.386 && pos < 0.922)
            {
                State = stay ? States.idle_up : States.up;
                position = States.idle_up;
                gun.sortingOrder = 0;
                lamp.sortingOrder = 0;
                lamp.flipX = true;
            }
            else if (pos > -0.922 && pos <= -0.386)
            {
                State = stay ? States.idle_down : States.down;
                position = States.idle_down;
                gun.sortingOrder = 2;
                lamp.sortingOrder = 2;
                lamp.flipX = false;
            }
            else if (pos > -0.386 && pos <= 0.386)
            {
                State = stay ? States.idle_right : States.right;
                position = States.idle_right;
                gun.sortingOrder = 2;
                lamp.sortingOrder = 0;
                lamp.flipX = false;
            }
            else if (!(pos > -0.922 && pos <= 0.922))
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
        if(position == States.idle_up || position == States.up)
            light.rotation = Quaternion.Lerp(light.rotation, 
                Quaternion.Euler(0f, 0f, 90f), 
                lightSpeed * Time.fixedDeltaTime);
        else if(position == States.idle_down || position == States.down)
            light.rotation = Quaternion.Lerp(light.rotation, 
                Quaternion.Euler(0f, 0f, 270f), 
                lightSpeed * Time.fixedDeltaTime);
        else if(position == States.idle_left || position == States.left)
            light.rotation = Quaternion.Lerp(light.rotation, 
                Quaternion.Euler(0f, 0f, 180f), 
                lightSpeed * Time.fixedDeltaTime);
        else if(position == States.idle_right || position == States.right)
            light.rotation = Quaternion.Lerp(light.rotation, 
                Quaternion.Euler(0f, 0f, 0f), 
                lightSpeed * Time.fixedDeltaTime);
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
