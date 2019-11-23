using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private EnemyMovementAI enemyMovement;


    // Start is called before the first frame update
    void Start()
    {
        playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
    }

    private void OnTriggerEnter(BoxCollider other)
    {
        if (other.gameObject.layer.Equals(LayerMask.NameToLayer("Enemy")))
        {
            other.gameObject.GetComponent<EnemyMovementAI>().Kill();
        }
    }
}
