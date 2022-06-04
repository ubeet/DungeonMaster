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
    
    private void Update()
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
