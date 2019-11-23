using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody myRigidbody;
    private RaycastHit hit;

    public float speed;
    public float gravityModifier;
    public float jumpForce;
    public float timeSpeed = 1;

    public bool mechanicJump;
    public bool mechanicRotation;
    public bool mechanicMovement;
    public bool mechanicHealth;

    private bool canJump;
    private bool isJumping;
    private bool isDead;

    private float moveInputVertical;
    private float moveInputHorizontal;
    private Vector2 currentRotation;
    private float health = 1f; // shouldn't this be a uint?
    private float t; // what the hell is 't'?

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = timeSpeed; // Set time speed

        // Kill player if health is 0 or mechanic is off
        if (Math.Abs(health) < Mathf.Epsilon || mechanicHealth == false)
        {
            Kill();
        }

        if (mechanicJump == true)
        {
            var keyPressed = Input.GetKeyDown(KeyCode.Space);
            // Jumping when there is a hole in front
            if (canJump == true && keyPressed && isJumping == false)
            {
                t = 0;
                isJumping = true;
                myRigidbody.velocity = new Vector3 (
                    currentRotation.x * jumpForce,
                    jumpForce / 2,
                    currentRotation.y * jumpForce);
            }
        }

        isJumping &= (canJump != false || t < 0.5f);

        if (isJumping == true) // Timer for jumping only for 0.5 seconds
        {
            if (t < 0.5f)
            {
                t += Time.deltaTime;
            }
        }
    }

    void FixedUpdate()
    {
        // Raycast that checks if there is a hole in front of the Player
        canJump = !Physics.Raycast(
            myRigidbody.position + (transform.forward * 4),
            Vector3.down,
            out hit,
            13.5f);


        myRigidbody.velocity += Physics.gravity * gravityModifier * Time.fixedDeltaTime;

        moveInputHorizontal = Input.GetAxisRaw("Horizontal");
        moveInputVertical = Input.GetAxisRaw("Vertical");

        // Method for setting facing direction
        Rotate(new Vector2(moveInputHorizontal, moveInputVertical));

        // Vector showing where is the Player facing
        Vector3 movement = new Vector3(currentRotation.x, 0.0f, currentRotation.y);

        // Velocity before new velocity is created
        Vector3 past = new Vector3(myRigidbody.velocity.x, 0, myRigidbody.velocity.z);

        if (mechanicMovement)
        {
            if (isJumping && t >= 0.5f)
            {
                myRigidbody.velocity = new Vector3(0, myRigidbody.velocity.y, 0) + past;
            }
            else if (!isJumping && (t <= 0.55f || Math.Abs(t) < 0.001f))
            {
                myRigidbody.velocity = new Vector3(
                    moveInputHorizontal * speed,
                    myRigidbody.velocity.y,
                    moveInputVertical * speed);
            }
        }

        if (mechanicRotation)
        {
            // Rotating the Player
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(movement),
                0.15F);
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
