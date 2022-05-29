using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] public Transform goal;
    [SerializeField] float speed = 5f;

    private NavMeshAgent agent;
    private Rigidbody2D rb;
    private Vector2 direction;
    private Animator animator;
    private States position = States.down;

    void Start () 
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; 
        agent.updateUpAxis = false;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
    private void FixedUpdate()
    {
        var goalPosition = goal.position;
        agent.SetDestination(goalPosition);

        direction.x = goalPosition.x;
        direction.y = goalPosition.y;
        
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        
        if(direction.y == 0 && direction.x == 0)
            State = position;
        else if (direction.x > 0)
        {
            State = States.right;
            position = States.right;
        }
        else if (direction.x < 0)
        {
            State = States.left;
            position = States.left;
        }
        else if (direction.y < 0)
        {
            State = States.down;
            position = States.down;
        }
        else if (direction.y > 0)
        {
            State = States.up;
            position = States.up;
        }
    }
    
    private States State
    {
        get => (States) animator.GetInteger("state");
        set => animator.SetInteger("state", (int)value);
    }
    
    public enum States
    {
        up,
        down,
        left,
        right,
    }
}
