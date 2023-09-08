using System.Collections;
using UnityEngine;

public class WalkEye : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float spawnDelayTime;
    [SerializeField]
    private float destroyTime;
    [SerializeField]
    private float spawnX;
    [SerializeField]
    private int bouncyChance;
    [SerializeField]
    private float bounceForce;
    [SerializeField]
    private float bounceDelayMax;
    [SerializeField]
    private float gravityScale;

    private float bounceDelay;
    private bool isBouncy = false;
    private bool isSpawnDelay = true;
    private bool isFacingRight = true;
    private Rigidbody2D rb;

    private void Awake() 
    {
        transform.position = new Vector2(spawnX, 0);
        rb = GetComponent<Rigidbody2D>();

        // Random gen number to determine if eye will bounce
        int randNum = Random.Range(0, bouncyChance);
        if (randNum == bouncyChance / 2)
        {
            isBouncy = true;
            bounceDelay = Random.Range(1f, bounceDelayMax);
        }

        // Random gen number to determine if eye will spawn left or right
        randNum = Random.Range(0,2);
        if (randNum == 1)
        {
            transform.position = new Vector2(transform.position.x * -1, transform.position.y);
            transform.localScale = new Vector2(transform.localScale.x *-1, 1);
            isFacingRight = false;
        }

        // Call caroutines
        StartCoroutine("SpawnDelay");
        Invoke("DestroyObj", destroyTime);    
    }

    private void Update() 
    {
        // Handle Horizontal Movement
        if (!isSpawnDelay)
        {
            if (isFacingRight)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(speed * -1, rb.velocity.y);
            }
        }
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(spawnDelayTime);

        // Start carotine for bouncing
        if (isBouncy)
        {
            StartCoroutine("BounceHandlerLoop");
        }
        
        isSpawnDelay = false;
    }

    IEnumerator BounceHandlerLoop()
    {
        yield return new WaitForSeconds(bounceDelay);

        // Apply Gravity so eye will jump and fall endlessly
        rb.gravityScale = gravityScale;
        rb.velocity = new Vector2(rb.velocity.x, bounceForce);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage();
        }
    }

    private void DestroyObj()
    {
        Destroy(gameObject);
    }
}
