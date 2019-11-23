using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugMode : MonoBehaviour
{
    [SerializeField] Button[] mechanicsOptions;
    [SerializeField] Text[] xMark;
    [SerializeField] Sprite imageSprite;

    int[] counters;

    void Start()
    {
        counters = new int[mechanicsOptions.Length];

        foreach(Button button in mechanicsOptions)
        {
            button.GetComponent<Button>();
        }
    }
    
    public void MovementOptionPress()
    {
        ChangeImage(0);
        counters[0]++;
    }

    public void JumpingOptionPress()
    {
        ChangeImage(1);
        counters[1]++;
    }

    public void RotationOptionPress()
    {
        ChangeImage(2);
        counters[2]++;
    }

    public void HealthOptionPress()
    {
        ChangeImage(3);
        counters[3]++;
    }

    public void CombatOptionPress()
    {
        ChangeImage(4);
        counters[4]++;
    }

    public void DialogueOptionPress()
    {
        ChangeImage(5);
        counters[5]++;
    }

    public void ChangeImage(int imageIndex)
    {
        if(counters[imageIndex] % 2 == 0)
        {
            xMark[imageIndex].text = "X";
        }
        else
        {
            xMark[imageIndex].text = "";
        }
    }
}