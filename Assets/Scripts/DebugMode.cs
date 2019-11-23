using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugMode : MonoBehaviour
{
    [SerializeField] Button[] mechanicsOptions;
    [SerializeField] Text[] xMark;
    [SerializeField] Text[] selectableMechanics;
    [SerializeField] Text[] capacityText;
    [SerializeField] Sprite imageSprite;

    int optionIndex = 0;

    int[] counters;

    void Start()
    {
        counters = new int[mechanicsOptions.Length];

        foreach(Button button in mechanicsOptions)
        {
            button.GetComponent<Button>();
        }
    }

    private void Update()
    {
        ChangeTextColor();
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

    private void ChangeTextColor()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)){
            optionIndex -= 1;
            if (optionIndex - 1 < 0)
            {
                optionIndex = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            optionIndex += 1;
            if (optionIndex + 1 >= mechanicsOptions.Length)
            {
                optionIndex = mechanicsOptions.Length - 1;
            }
        }

        xMark[optionIndex].color = Color.blue;
        selectableMechanics[optionIndex].color = Color.blue;
        capacityText[optionIndex].color = Color.blue;

        if (optionIndex > 0)
        {
            selectableMechanics[optionIndex - 1].color = Color.white;
            xMark[optionIndex - 1].color = Color.white;
            capacityText[optionIndex - 1].color = Color.white;
        }
        if(optionIndex < mechanicsOptions.Length - 1)
        {
            xMark[optionIndex + 1].color = Color.white;
            selectableMechanics[optionIndex + 1].color = Color.white;
            capacityText[optionIndex + 1].color = Color.white;
        }
    }
}