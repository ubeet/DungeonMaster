using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float distance;
    
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
            agent.speed = 2;
        }
        else
        {
            agent = GetComponent<NavMeshAgent>();
            agent.speed = 0;
        }
    }
}
