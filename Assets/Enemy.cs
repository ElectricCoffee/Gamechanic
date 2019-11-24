using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private EnemyMovementAI enemyMovementAI;
    // Start is called before the first frame update
    void Start()
    {
        enemyMovementAI = gameObject.GetComponentInParent<EnemyMovementAI>();
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Weapon")
        {
            enemyMovementAI.Kill();
        }
    }
}

