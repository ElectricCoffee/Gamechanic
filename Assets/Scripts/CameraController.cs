using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform lookAt;
    public float boundX = 100f;
    public float boundZ = 100f;

    public Collider collider;

    private float maxX;
    private float maxZ;

    private Vector3 desiredPossition;

    // Use this for initialization
    void Start()
    {
        maxX = collider.transform.position.x;
        maxZ = collider.transform.position.z;
    }

    void Update()
    {
        transform.position.x = Mathf.Clamp(transform.position.x, maxX, -maxX);
        transform.position.z = Mathf.Clamp(transform.position.z, maxZ, -maxZ);
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        float dx = lookAt.position.x - transform.position.x;
        float dz = lookAt.position.z - transform.position.z;

        if(dx > boundX || dx < -boundX)
        {
            if(transform.position.x < lookAt.position.x)
            {
                delta.x = dx - boundX;
            }
            else
            {
                delta.x = dx + boundX;
            }
        }

        if (dz > boundZ|| dz < -boundZ)
        {
            if (transform.position.z < lookAt.position.z)
            {
                delta.z = dz - boundZ;
            }
            else
            {
                delta.z = dz + boundZ;
            }
        }

        desiredPossition = transform.position + delta;
        transform.position = Vector3.Lerp(transform.position, desiredPossition, 0.1f);
    }
}
