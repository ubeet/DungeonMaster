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
        if (GetComponent<Enemy>().AI)
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
        
        
    }
    
    
}
