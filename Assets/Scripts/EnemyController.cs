using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float distance;
    
    private NavMeshAgent agent;
    private Rigidbody2D rb;
    private Vector3 goalPosition;
    private Vector2 direction;
    private Animator animator;
    private States state = States.down;

    void Start () 
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false; 
        agent.updateUpAxis = false;
        rb = GetComponent<Rigidbody2D>();
        goalPosition = GetComponent<Vector3>();
        animator = GetComponent<Animator>();
    }
    
    private void FixedUpdate()
    {
        goalPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        agent.SetDestination(goalPosition);

        direction.x = goalPosition.x;
        direction.y = goalPosition.y;
        if (Vector3.Distance(goalPosition, rb.position) > distance)
        {
            agent.SetDestination(goalPosition);
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }
        else
        {
            agent.SetDestination(-goalPosition);
            rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
        }

        if(direction.y == 0 && direction.x == 0)
            State = state;
        else if (direction.x > 0)
        {
            State = States.right;
            state = States.right;
        }
        else if (direction.x < 0)
        {
            State = States.left;
            state = States.left;
        }
        else if (direction.y < 0)
        {
            State = States.down;
            state = States.down;
        }
        else if (direction.y > 0)
        {
            State = States.up;
            state = States.up;
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
