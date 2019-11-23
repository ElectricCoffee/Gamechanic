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

    private bool canJump;
    private bool isJumping;

    private float moveInputVertical;
    private float moveInputHorizontal;
    private float horizontalRotation;
    private float verticalRotation;
    private float t;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canJump == true && Input.GetKeyDown(KeyCode.Space)) // Jumping when there is a hole in front
        {
            t = 0;
            isJumping = true;
            myRigidbody.velocity = new Vector3(horizontalRotation * jumpForce, jumpForce / 2, verticalRotation * jumpForce);
        }
        if (canJump == true && t >= 0.1f) // Stoping jump
        {
            isJumping = false;
        }

        if (isJumping == true) // Timer for jumping only for 0.1 seconds
        {
            if (t < 0.1f)
            {
                t += Time.deltaTime;
            }
        }
    }

    void FixedUpdate()
    {
        canJump = !Physics.Raycast(new Vector3(myRigidbody.position.x, myRigidbody.position.y, myRigidbody.position.z) + (transform.forward * 4), Vector3.down, out hit , 13.5f); // Raycast that checks if there is a hole in front of the Player
        

        myRigidbody.velocity += Physics.gravity * gravityModifier * Time.deltaTime;

        moveInputHorizontal = Input.GetAxisRaw("Horizontal");
        moveInputVertical = Input.GetAxisRaw("Vertical");

        Rotate(new Vector2(moveInputHorizontal, moveInputVertical)); // Method for setting facing direction

        Vector3 movement = new Vector3(horizontalRotation, 0.0f, verticalRotation); // Vector showing where is the Player facing

        Vector3 past = new Vector3(myRigidbody.velocity.x, 0, myRigidbody.velocity.z); // Velocity before new velocity is created

        if(isJumping == true && t >= 0.1f)
        {
            myRigidbody.velocity = new Vector3(moveInputHorizontal * speed, myRigidbody.velocity.y, moveInputVertical * speed) + past; // Movement if it jumps
        }
        else if(isJumping == false && (t <= 0.15f || Math.Abs(t) < 0.001f))
        {
            myRigidbody.velocity = new Vector3(moveInputHorizontal * speed, myRigidbody.velocity.y, moveInputVertical * speed); // Movement if it doesn't jump
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F); // Rotating Player
    }

    private void Rotate(Vector2 vector)
    {
        if (vector == new Vector2(1,1))
        {
            horizontalRotation = 1;
            verticalRotation = 1;
        }
        else if (vector == new Vector2(1, 0))
        {
            horizontalRotation = 1;
            verticalRotation = 0;
        }
        else if (vector == new Vector2(1, -1))
        {
            horizontalRotation = 1;
            verticalRotation = -1;
        }
        else if (vector == new Vector2(0, 1))
        {
            horizontalRotation = 0;
            verticalRotation = 1;
        }
        else if (vector == new Vector2(0, -1))
        {
            horizontalRotation = 0;
            verticalRotation = -1;
        }
        else if (vector == new Vector2(-1, 1))
        {
            horizontalRotation = -1;
            verticalRotation = 1;
        }
        else if (vector == new Vector2(-1, 0))
        {
            horizontalRotation = -1;
            verticalRotation = 0;
        }
        else if (vector == new Vector2(-1, -1))
        {
            horizontalRotation = -1;
            verticalRotation = -1;
        }
    }
}
