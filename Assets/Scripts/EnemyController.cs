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

    private void Start () 
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        if (GetComponent<Enemy>().AI)
        {
            agent = GetComponent<NavMeshAgent>();
            if(!agent.isOnNavMesh) 
            {
                transform.position = transform.position;
                agent.enabled = false;
                agent.enabled = true;
            }
            agent.updateRotation = false; 
            agent.updateUpAxis = false;
            goalPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            agent.SetDestination(goalPosition);

            direction.x = goalPosition.x;
            direction.y = goalPosition.y;
            
            /*if (Vector2.Distance(goalPosition, rb.transform.position) > distance)
            {
                agent.SetDestination(goalPosition);
                rb.MovePosition(rb.transform.position + goalPosition * speed * Time.deltaTime);
            }
            else
            {
                agent.SetDestination(-goalPosition);
                rb.MovePosition(rb.transform.position - goalPosition * speed * Time.deltaTime);
            }*/
        }
    }
}
