using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] SpriteRenderer lamp;
    [SerializeField] Transform light;
    [SerializeField] float lightSpeed;

    private Rigidbody2D rb;
    private Vector2 direction;
    private Animator animator;
    private States position = States.idle_down;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    private void FixedUpdate()
    {
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");
        
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

        if(!Input.GetButton("Fire1"))
        {
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
        if(State == States.idle_up || State == States.up)
            light.rotation = Quaternion.Lerp(light.rotation, 
                Quaternion.Euler(0f, 0f, 90f), 
                lightSpeed * Time.fixedDeltaTime);
        else if(State == States.idle_down || State == States.down)
            light.rotation = Quaternion.Lerp(light.rotation, 
                Quaternion.Euler(0f, 0f, 270f), 
                lightSpeed * Time.fixedDeltaTime);
        else if(State == States.idle_left || State == States.left)
            light.rotation = Quaternion.Lerp(light.rotation, 
                Quaternion.Euler(0f, 0f, 180f), 
                lightSpeed * Time.fixedDeltaTime);
        else if(State == States.idle_right || State == States.right)
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
