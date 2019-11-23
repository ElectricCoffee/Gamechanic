using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugMode : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI kBAvailable, kBUsed;
    [SerializeField] Button movementOption;
    
    void Start()
    {
        movementOption.GetComponent<Button>();
    }
    
    public void MovementOptionPress()
    {
        Debug.Log(movementOption.gameObject.name + " was pressed.");
    }
}
