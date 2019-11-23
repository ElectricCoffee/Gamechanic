using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody myRigidbody;
    private RaycastHit hit;

    public float speed;
    public float gravityModifier;

    public bool canJump;

    private float moveInputVertical;
    private float moveInputHorizontal;
    private float horizontalRotation;
    private float verticalRotation;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Debug.DrawRay(new Vector3(myRigidbody.position.x, myRigidbody.position.y, myRigidbody.position.z) + transform.forward, Vector3.down * 45f, Color.green);
        canJump = Physics.Raycast(new Vector3(myRigidbody.position.x, myRigidbody.position.y, myRigidbody.position.z + 26), Vector3.down, out hit , 45f);
        

        myRigidbody.velocity += Physics.gravity * gravityModifier * Time.deltaTime;

        moveInputHorizontal = Input.GetAxisRaw("Horizontal");
        moveInputVertical = Input.GetAxisRaw("Vertical");

        Rotate(new Vector2(moveInputHorizontal, moveInputVertical));

        Vector3 movement = new Vector3(horizontalRotation, 0.0f, verticalRotation);

        print(movement);

        myRigidbody.velocity = new Vector3(moveInputHorizontal * speed, myRigidbody.velocity.y, moveInputVertical * speed);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);

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
