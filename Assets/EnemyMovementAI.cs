using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementAI : MonoBehaviour
{
    // IDEA:
    // Have two enemy behaviours:
    // 1) the enemy walks around in a random direction for 1-2 seconds, then stop
    // 2) if the player is within a certain radius, the enemy will sprint towards the player

    [SerializeField]
    private float walkingSpeed = 5f;
    [SerializeField]
    private float runningSpeed = 20f;

    [SerializeField]
    private float stopDelay = 2f;

    public float detectionRadius = 5f;

    [SerializeField]
    private List<GameObject> targets;

    private List<GameObject> visibleObjects;

    private Rigidbody rb;

    private Vector3 currentTarget;

    void Start()
    {
        visibleObjects = new List<GameObject>();
        rb = gameObject.GetComponent<Rigidbody>();

        currentTarget = transform.position;
    }

    void FixedUpdate()
    {
        foreach (var target in targets)
        {
            if (visibleObjects.Contains(target))
            {
                Chase(target);
            }
            else
            {
                Walk();
            }
        }
    }

    private bool EqualWithoutY(Vector3 target)
    {
        // epsilon too small using 0.001 instead
        return Mathf.Abs(transform.position.x - target.x) <= 0.01f
            && Mathf.Abs(transform.position.z - target.z) <= 0.01f;
    }

    private void Walk()
    {
        if (EqualWithoutY(currentTarget))
        {
            Debug.Log("Equal without Y");
            var randDistance = Random.Range(walkingSpeed, walkingSpeed * 3);
            currentTarget = new Vector3(
                x: Random.Range(-1f, 1f) * randDistance,
                y: transform.position.y,
                z: Random.Range(-1f, 1f) * randDistance);
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            currentTarget,
            walkingSpeed * Time.deltaTime);

    }

    private void Chase(GameObject target)
    {
        currentTarget = target.transform.position;
        currentTarget.y = transform.position.y;

        transform.position = Vector3.MoveTowards(
            transform.position,
            currentTarget,
            runningSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        visibleObjects.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        visibleObjects.Remove(other.gameObject);
    }
}
