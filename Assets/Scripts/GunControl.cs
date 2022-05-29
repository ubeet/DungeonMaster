using UnityEngine;

public class GunControl : MonoBehaviour
{
    [SerializeField] private GameObject circle;
    [SerializeField] private float offset;
    [SerializeField] private SpriteRenderer gun;
    [SerializeField] private GameObject ammo;
    [SerializeField] private Transform shotDir;
    [SerializeField] private float speed;
    [SerializeField] private int count;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform GunPosChange;
    
    private float startTime = 0;
    private float timeShot = 0;
    private Vector2 direction;
    private States position = States.idle_down;

    
    private void FixedUpdate()
    {
        if (Input.GetButton("Fire1"))
        {
            gun.enabled = true;
            direction.x = Input.GetAxis("Horizontal");
            direction.y = Input.GetAxis("Vertical");

            bool stay = direction.y == 0 && direction.x == 0;
            
            var pos = GunPosChange.rotation.z;
            
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
            
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - circle.transform.position;
            float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            circle.transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);
            
            if (timeShot <= 0)
            {
                for (int i = count / -2; i <= count / 2; i++)
                {
                    if (count % 2 == 0 && i != 0 || count % 2 != 0)
                        Instantiate(ammo, shotDir.position, Quaternion.Euler(0f, 0f, rotateZ + offset + i * 7));
                }
                
                timeShot = startTime;
            }
            if (circle.transform.rotation.z >= -0.707 && circle.transform.rotation.z <= 0.707)
                gun.flipY = true;
            else
                gun.flipY = false;
        }
        else
            gun.enabled = false;
        
        timeShot -= Time.fixedDeltaTime;
        startTime = speed;
        
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
    
