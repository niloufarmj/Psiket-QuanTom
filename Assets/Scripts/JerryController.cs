using UnityEngine;
using System.Collections;

public class JerryController : MonoBehaviour
{
    public SpeedSystem speed;
    private Vector2 targetPosition;
    private bool isMoving = false;
    GameManager gameManager;

    public Animator animator;


    private void Start()
    {
        gameManager = GameManager.instance;
        StartCoroutine(ChangeMovementTarget());
    }

    private void Update()
    {
        speed = gameManager.currentSpeed;
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        if (isMoving)
        {
            // Move towards the target position
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed.moveSpeed * Time.deltaTime);

            // Check if we've reached the target position
            if (transform.position == (Vector3)targetPosition)
            {
                isMoving = false;
            }
        }

        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        if (isMoving)
        {
            animator.SetBool("Walking", true);

            // Calculate the direction vector towards the target position
            Vector2 direction = targetPosition - (Vector2)transform.position;

            // Normalize the direction vector to get the unit vector
            direction.Normalize();

            // Set the horizontal and vertical values for the animator
            animator.SetFloat("Horizontal", direction.x);
            animator.SetFloat("Vertical", direction.y);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    private IEnumerator ChangeMovementTarget()
    {
        while (true)
        {
            // Wait for a random amount of time before moving to a new location
            yield return new WaitForSeconds(Random.Range(0, speed.maxWaitTime));

            // Set a new random target position within the defined range
            targetPosition = new Vector2(Random.Range(-speed.range, speed.range), Random.Range(-speed.range, speed.range));
            isMoving = true;
        }
    }
}

public struct SpeedSystem
{
    public float moveSpeed;
    public float maxWaitTime;
    public float range;
}