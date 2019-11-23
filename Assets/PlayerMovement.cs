using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private RaycastHit hit;

    public float speed;
    public float gravityModifier;
    public float jumpForce;
    public float timeSpeed = 1f;

    public bool mechanicJump;
    public bool mechanicRotation;
    public bool mechanicMovement;
    public bool mechanicHealth;

    private bool isJumping;
    private bool isDead;

    private Vector2 moveInput;
    private Vector2 currentRotation;
    private uint health = 1;
    private float time; // what the hell is 't'?

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

        if (mechanicJump)
        {
            var keyPressed = Input.GetKeyDown(KeyCode.Space);
            // Jumping when there is a hole in front
            if (keyPressed && !isJumping)
            {
                time = 0f;
                isJumping = true;
                rb.velocity = new Vector3 (
                    currentRotation.x * jumpForce,
                    jumpForce / 2,
                    currentRotation.y * jumpForce);
            }
        }

        isJumping &= (time < 0.5f);

        // Timer for jumping only for 0.5 seconds
        if (isJumping)
        {
            if (time < 0.5f)
            {
                time += Time.deltaTime;
            }
        }
    }

    void FixedUpdate()
    {
        rb.velocity += Physics.gravity * gravityModifier * Time.fixedDeltaTime;

        moveInput = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"));

        // Method for setting facing direction
        Rotate(moveInput);

        // Vector showing where is the Player facing
        Vector3 movement = new Vector3(currentRotation.x, 0.0f, currentRotation.y);

        // Velocity before new velocity is created
        Vector3 past = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (mechanicMovement)
        {
            if (isJumping && time >= 0.5f)
            {
                rb.velocity = new Vector3(0f, rb.velocity.y, 0f) + past;
            }
            else if (!isJumping && (time <= 0.55f || Math.Abs(time) < Mathf.Epsilon))
            {
                rb.velocity = new Vector3(
                    moveInput.x * speed,
                    rb.velocity.y,
                    moveInput.y * speed);
            }
        }

        if (mechanicRotation)
        {
            // Rotating the Player
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(movement),
                0.15f);
        }
    }

    private void Rotate(Vector2 vector)
    {
        
        if (vector == Vector2.one)
        {
            currentRotation = Vector2.one;
        }
        else if (vector == Vector2.right)
        {
            currentRotation = Vector2.right;
        }
        else if (vector == new Vector2(1f, -1f))
        {
            currentRotation = new Vector2(1f, -1f);
        }
        else if (vector == Vector2.up)
        {
            currentRotation = Vector2.up;
        }
        else if (vector == Vector2.down)
        {
            currentRotation = Vector2.down;
        }
        else if (vector == new Vector2(-1f, 1f))
        {
            currentRotation = new Vector2(-1f, 1f);
        }
        else if (vector == Vector2.left)
        {
            currentRotation = Vector2.left;
        }
        else if (vector == new Vector2(-1f, -1f))
        {
            currentRotation = new Vector2(-1f, -1f);
        }
    }

    public void Kill()
    {
        StartCoroutine(Die());
    }

    IEnumerator Die()
    {
        isDead = true;
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
