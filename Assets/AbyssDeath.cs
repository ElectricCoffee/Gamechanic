using UnityEngine;

public class AbyssDeath : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().Kill();
        }
    }
}
