using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform lookAt;
    public float boundX = 1f;
    public float boundZ = 1f;

    private Vector3 desiredPossition;

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
