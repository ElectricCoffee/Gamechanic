using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const float JUMPING_TIME = 0.5f;
    private const float JUMP_HEIGHT = 13.5f;
    private const float SLERP_TIME = 0.15f;

    public float speed;
    public float gravityModifier;
    public float jumpForce;
    public float timeSpeed = 1f;
    public float despawnDelay = 3;

    public bool mechanicJump;
    public bool mechanicRotation;
    public bool mechanicMovement;
    public bool mechanicHealth;

    private Rigidbody rb;
    private bool isJumping;
    private bool isDead;

    private Vector3 moveInput;
    private Vector3 currentRotation;
    private uint health = 1;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = timeSpeed; // Set time speed

        // Kill player if health is 0 or mechanic is off
        if (health == 0 || !mechanicHealth)
        {
            Kill();
        }
    }

    void FixedUpdate()
    {
        rb.velocity += Physics.gravity * gravityModifier * Time.fixedDeltaTime;

        moveInput = new Vector3(
            Input.GetAxisRaw("Horizontal"),
            0f,
            Input.GetAxisRaw("Vertical"));

        // Method for setting facing direction
        Rotate(moveInput);

        HandleMovement(mechanicMovement);

        HandleRotation(mechanicRotation);

        HandleJumping(mechanicJump);
    }

    private void Rotate(Vector3 vector)
    {
        
        if (vector == new Vector3(1, 0f, 1))
        {
            currentRotation = new Vector3(1, 0f, 1);
        }
        else if (vector == Vector3.right)
        {
            currentRotation = Vector3.right;
        }
        else if (vector == new Vector3(1f, 0f, -1f))
        {
            currentRotation = new Vector3(1f, 0f, -1f);
        }
        else if (vector == Vector3.forward)
        {
            currentRotation = Vector3.forward;
        }
        else if (vector == Vector3.back)
        {
            currentRotation = Vector3.back;
        }
        else if (vector == new Vector3(-1f, 0f, 1f))
        {
            currentRotation = new Vector3(-1f, 0, 1f);
        }
        else if (vector == Vector3.left)
        {
            currentRotation = Vector3.left;
        }
        else if (vector == new Vector3(-1f, 0f, -1f))
        {
            currentRotation = new Vector3(-1f, 0f, -1f);
        }
    }

    public void Kill()
    {
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        isDead = true;
        yield return new WaitForSeconds(despawnDelay);
        Destroy(gameObject);
    }

    private void HandleMovement(bool active)
    {
        if (active)
        {
            // Velocity before new velocity is created
            Vector3 past = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            // Jumping uses a different velocity from moving on the ground
            if (isJumping && time >= JUMPING_TIME)
            {
                rb.velocity = new Vector3(0f, rb.velocity.y, 0f) + past;
            }
            else if (!isJumping && (time <= 0.55f || Math.Abs(time) < Mathf.Epsilon))
            {
                rb.velocity = new Vector3(
                    moveInput.x * speed,
                    rb.velocity.y,
                    moveInput.z * speed);
            }
        }
    }

    private void HandleRotation(bool active)
    {
        if (active)
        {
            // Vector showing where is the Player facing
            Vector3 orientation = new Vector3(currentRotation.x, 0.0f, currentRotation.z);

            // Rotating the Player
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(orientation),
                SLERP_TIME);
        }
    }

    private void HandleJumping(bool active)
    {
        if (active)
        {
            var canJump = Physics.Raycast(rb.position, Vector3.down, out var _, JUMP_HEIGHT);
            var keyPressed = Input.GetKeyDown(KeyCode.Space);
            // Jumping when there is a hole in front
            if (canJump && keyPressed && !isJumping)
            {
                time = 0f;
                isJumping = true;
                rb.velocity = new Vector3(
                    currentRotation.x * jumpForce,
                    jumpForce / 2,
                    currentRotation.z * jumpForce);
            }
        }

        isJumping &= (time < JUMPING_TIME);

        // Timer for jumping only for 0.5 seconds
        if (isJumping)
        {
            if (time < JUMPING_TIME)
            {
                time += Time.deltaTime;
            }
        }
    }
}
