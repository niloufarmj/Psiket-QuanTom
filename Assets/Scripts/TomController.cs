using UnityEngine;

public class TomController : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 velocity;
    private Rigidbody2D rb;
    private float collisionTimer = 0f;
    private bool isColliding = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocity = Vector2.zero;
    }

    private void Update()
    {
        velocity.x = Input.GetAxisRaw("Horizontal");
        velocity.y = Input.GetAxisRaw("Vertical");

        // Update the collision timer
        if (isColliding)
        {
            collisionTimer += Time.deltaTime;
            if (collisionTimer >= 5f)
            {
                Debug.Log("Tom has been within Jerry's collider for 5 seconds!");
                // Reset the timer
                collisionTimer = 0f;
            }
        }
        else
        {
            // Reset the timer if the collision ends
            collisionTimer = 0f;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = velocity * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MainJerry")
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "MainJerry")
        {
            isColliding = false;
        }
    }
}