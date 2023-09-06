using UnityEngine;

public class ProjectileEye : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float projectileDestroyTime;
    [SerializeField]
    private Animator animator;

    private Rigidbody2D rb;
    private float angle;
    private Transform _playerPos;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();

        // Get player pos, calculate angle, rotate projectile
        _playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        angle = Mathf.Atan2(_playerPos.position.y - transform.position.y, _playerPos.position.x - transform.position.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0,0, angle);
        
        Invoke("DestroyObj", projectileDestroyTime);
    }

    private void Update() 
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.name == "Player")
        {
            other.GetComponent<PlayerHealth>().TakeDamage();

            animator.SetTrigger("isHit");
        }
    }

    public void DestroyObj()
    {
        Destroy(gameObject);
    }
}
