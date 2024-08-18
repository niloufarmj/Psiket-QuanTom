using UnityEngine;

public class TomController : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 velocity;
    private Rigidbody2D rb;
    private bool isColliding = false;
    public AudioSource jerrySound;
    private float collisionAudioTimer = 0f;
    public float collisionAudioDelay = 0.3f; // Delay between audio playbacks (in seconds)

    private float deltax, deltay;

    public Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocity = Vector2.zero;
    }

    private void Update()
    {
        if (Application.isMobilePlatform)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        deltax = touchPos.x - transform.position.x;
                        deltay = touchPos.y - transform.position.y;
                        break;
                    case TouchPhase.Moved:
                        rb.MovePosition(new Vector2(touchPos.x - deltax, touchPos.y - deltay));
                        break;
                    case TouchPhase.Ended:
                        rb.velocity = Vector2.zero;
                        break;
                }
            }
        }
        else
        {
            velocity.x = Input.GetAxisRaw("Horizontal");
            velocity.y = Input.GetAxisRaw("Vertical");
        }
        

        // Update the collision timer
        if (isColliding)
        {
            GameManager.instance.collisionTimer += Time.deltaTime;

            if (GameManager.instance.collisionTimer >= PlayerPrefs.GetFloat("difficulty"))
            {
                GameManager.instance.WIn();
                // Reset the timer
                GameManager.instance.collisionTimer = 0f;
            }

            if (collisionAudioTimer == 0 || collisionAudioTimer >= collisionAudioDelay)
            {
                jerrySound.PlayOneShot(jerrySound.clip);
                collisionAudioTimer = 0f;
            }

            // Play the audio with a delay
            collisionAudioTimer += Time.deltaTime;
            
        }
        else
        {
            // Reset the timer if the collision ends
            GameManager.instance.collisionTimer = 0f;
            collisionAudioTimer = 0f;
        }


    }

    void UpdateAnimation()
    {
        if (velocity != Vector2.zero)
        {
            animator.SetBool("Walks", true);

            
            // Set the horizontal and vertical values for the animator
            animator.SetFloat("Horizontal", velocity.x);
            animator.SetFloat("Vertical", velocity.y);
        }
        else
        {
            animator.SetBool("Walks", false);
        }
    }

    private void FixedUpdate()
    {
        if (!Application.isMobilePlatform)
            rb.velocity = velocity * moveSpeed;
        UpdateAnimation();
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