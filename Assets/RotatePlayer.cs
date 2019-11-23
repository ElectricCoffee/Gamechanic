using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePlayer : MonoBehaviour
{
    private float moveInputVertical;
    private float moveInputHorizontal;
    private float horizontalRotation;
    private float verticalRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInputHorizontal = Input.GetAxisRaw("Horizontal");
        moveInputVertical = Input.GetAxisRaw("Vertical");

        Rotate(new Vector2(moveInputHorizontal, moveInputVertical));

        Vector3 movement = new Vector3(horizontalRotation, 0.0f, verticalRotation);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
    }

    void FixedUpdate()
    {
        

    }

    private void Rotate(Vector2 vector)
    {
        if (vector == new Vector2(1, 1))
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
