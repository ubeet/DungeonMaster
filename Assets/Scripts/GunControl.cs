using System;
using UnityEngine;

public class GunControl : MonoBehaviour
{
    [Header("Attributes")]
    
    [SerializeField] private SpriteRenderer gun;
    [SerializeField] private GameObject ammo;
    [SerializeField] private float offset;
    [SerializeField] private float speed;
    [SerializeField] private int count;
    
    private States position = States.idle_down;
    private Transform gunPosChange;
    private Vector3 goalPosition;
    private float startTime = 0;
    private float timeShot = 0;
    private AudioSource source;
    private Vector3 difference;
    private Vector2 direction;
    private GameObject circle;
    private Transform shotDir;
    private Animator animator;
    
    private void Start()
    {
        Initialize();
    }
    
    internal void Initialize()
    {
        source = GetComponent<AudioSource>();
        circle = transform.parent.gameObject;
        shotDir = transform.GetChild(0);
        
        if(transform.parent.parent.gameObject.TryGetComponent<Animator>(out animator))
            animator.transform.parent.gameObject.GetComponentInParent<Animator>();
        else animator = new GameObject().AddComponent<Animator>();
        
        gunPosChange = circle.GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        if (gameObject.GetComponent<Item>().isInInventory)
        {
            if (animator.gameObject.CompareTag("Enemy") && animator.gameObject.GetComponent<Enemy>().AI || Input.GetButton("Fire1") && animator.gameObject.CompareTag("Player"))
            {
                gun.enabled = true;
                bool stay;

                if (animator.gameObject.CompareTag("Enemy"))
                {
                    goalPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
                    stay = false;
                }
                else
                {
                    direction.x = Input.GetAxis("Horizontal");
                    direction.y = Input.GetAxis("Vertical");
                    stay = direction.y == 0 && direction.x == 0;
                }

                var pos = gunPosChange.rotation.z;

                if (pos > 0.386 && pos < 0.922)
                {
                    State = stay ? States.idle_up : States.up;
                    position = States.idle_up;
                    gun.sortingOrder = 0;
                }
                
                else if (pos > -0.922 && pos <= -0.386)
                {
                    State = stay ? States.idle_down : States.down;
                    position = States.idle_down;
                    gun.sortingOrder = 2;
                }
                
                else if (pos > -0.386 && pos <= 0.386)
                {
                    State = stay ? States.idle_right : States.right;
                    position = States.idle_right;
                    gun.sortingOrder = 2;
                }
                
                else if (!(pos > -0.922 && pos <= 0.922))
                {
                    State = stay ? States.idle_left : States.left;
                    position = States.idle_left;
                    gun.sortingOrder = 0;
                }
                
                var shotPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                if (animator.gameObject.CompareTag("Enemy")) shotPos = goalPosition;
                difference = shotPos - circle.transform.position;

                float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                circle.transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);

                if (timeShot <= 0)
                {
                    for (int i = count / -2; i <= count / 2; i++)
                        if (count % 2 == 0 && i != 0 || count % 2 != 0)
                            Instantiate(ammo, shotDir.position, Quaternion.Euler(0f, 0f, rotateZ + offset + i * 7));
                    timeShot = startTime;
                    source.Play();
                }

                if (circle.transform.rotation.z >= -0.707 && circle.transform.rotation.z <= 0.707) gun.flipY = true;
                else gun.flipY = false;
            }
            else gun.enabled = false;

            timeShot -= Time.fixedDeltaTime;
            startTime = speed;
        }

    }
    
    private States State
    {
        get { return (States) animator.GetInteger("state"); }
        set{ animator.SetInteger("state", (int)value); }
    }

    private enum States
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
    
