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

    public float detectionRadius = 7f;

    [SerializeField]
    private List<GameObject> targets;

    [SerializeField]
    private float timeDelay = 2;

    private List<GameObject> visibleObjects;

    private Rigidbody rb;

    private Vector3 currentTarget;

    private float elapsedTime = 0f;

    private bool waiting = false;

    private BoxCollider boxCollider;

    void Start()
    {
        visibleObjects = new List<GameObject>();
        rb = gameObject.GetComponent<Rigidbody>();
        boxCollider = gameObject.GetComponent<BoxCollider>();

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
            else if (!waiting)
            {
                waiting = Walk();
            }
            else if (waiting && TimeElapsed(elapsedTime))
            {
                waiting = false;
                elapsedTime = 0f;
            }
        }

        elapsedTime += Time.fixedDeltaTime;
    }

    /// <summary>
    /// Checks if two vectors are equal within a given margin of error while also ignoring the y direction
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private bool EqualWithoutY(Vector3 target, float marginOfError = 0.01f)
    {
        // epsilon too small using 0.001 instead
        return Mathf.Abs(transform.position.x - target.x) <= marginOfError
            && Mathf.Abs(transform.position.z - target.z) <= marginOfError;
    }

    /// <summary>
    /// Returns a target vector where the y position is equal to the enemy's current y position.
    /// This is done to effectively ignore vertical information and only focus on the hoirzontal informatio (x, z)
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    private Vector3 GetWithoutY(Vector3 target)
    {
        target.y = transform.position.y;
        return target;
    }

    /// <summary>
    /// Checks if the given time meets the elapsed time requirement
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private bool TimeElapsed(float time)
    {
        return time >= timeDelay;
    }


    /// <summary>
    /// Walks the enemy character in a random direction.
    /// </summary>
    /// <returns>Returns true if it needs to wait afterwards or not</returns>
    private bool Walk()
    {
        bool isWaiting = false;
        if (EqualWithoutY(currentTarget) || TimeElapsed(elapsedTime))
        {
            var randDistance = Random.Range(walkingSpeed, walkingSpeed * 3);
            currentTarget = new Vector3(
                x: Random.Range(-1f, 1f) * randDistance,
                y: transform.position.y,
                z: Random.Range(-1f, 1f) * randDistance);

            isWaiting = true;
            elapsedTime = 0f;
        }

        RotateAndMoveTowards(currentTarget, walkingSpeed);

        return isWaiting;
    }

    /// <summary>
    /// Chases the target object
    /// </summary>
    /// <param name="target"></param>
    private void Chase(GameObject target)
    {
        currentTarget = GetWithoutY(target.transform.position);

        RotateAndMoveTowards(currentTarget, runningSpeed);
    }

    /// <summary>
    /// Combines movement and rotation towards a single target
    /// </summary>
    /// <param name="target"></param>
    /// <param name="speed"></param>
    private void RotateAndMoveTowards(Vector3 target, float speed)
    {
        var targetDir = target - transform.position;
        var newDir = Vector3.RotateTowards(
            transform.forward,
            targetDir,
            speed * 2f * Time.deltaTime,
            0f);

        Debug.DrawRay(transform.position, newDir, Color.red);

        transform.rotation = Quaternion.LookRotation(newDir);

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime);
    }

    /// <summary>
    /// Adds an object to visibleObjects when it enters the trigger
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        visibleObjects.Add(other.gameObject);

        
    }

    /// <summary>
    /// Removes an object from visibleObjects when it leaves the trigger
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        visibleObjects.Remove(other.gameObject);
    }
    public void Kill()
    {
        StartCoroutine(Die());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Weapon")
        {
            Kill();
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
