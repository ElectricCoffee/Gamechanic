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
    [SerializeField] PlayerMovement player;

    int optionIndex = 0;
    int[] counters;
    List<bool> isActive;

    void Start()
    {
        player.GetComponent<PlayerMovement>();
        counters = new int[mechanicsOptions.Length];
        isActive = new List<bool>{
            player.mechanicMovement,
            player.mechanicJump,
            player.mechanicRotation,
            player.mechanicHealth,
            player.mechanicCombat,
            player.mechanicDialogue,
            player.mechanicUnlockables,
            player.mechanicTimeflow,
            player.mechanicInteractables
        };

        foreach(Button button in mechanicsOptions)
        {
            button.GetComponent<Button>();
        }

        CheckForActiveMechanic();
    }

    private void Update()
    {
        ChangeTextColor();
    }

    public void MovementOptionPress()
    {
        if (player.mechanicMovement)
        {
            player.mechanicMovement = false;
            isActive[0] = false;
        }
        else
        {
            player.mechanicMovement = true;
        }
        ChangeXMark(0);
        counters[0]++;
    }

    public void JumpingOptionPress()
    {
        if (player.mechanicJump)
        {
            player.mechanicJump = false;
            isActive[1] = false;
        }
        else
        {
            player.mechanicJump = true;
        }
        ChangeXMark(1);
        counters[1]++;
    }

    public void RotationOptionPress()
    {
        if (player.mechanicRotation)
        {
            player.mechanicRotation = false;
            isActive[2] = false;
        }
        else
        {
            player.mechanicRotation = true;
        }
        ChangeXMark(2);
        counters[2]++;
    }

    public void HealthOptionPress()
    {
        if (player.mechanicHealth)
        {
            player.mechanicHealth = false;
            isActive[3] = false;
        }
        else
        {
            player.mechanicHealth = true;
        }
        ChangeXMark(3);
        counters[3]++;
    }

    public void CombatOptionPress()
    {
        if (player.mechanicCombat)
        {
            player.mechanicCombat = false;
            isActive[4] = false;
        }
        else
        {
            player.mechanicCombat = true;
        }
        ChangeXMark(4);
        counters[4]++;
    }

    public void DialogueOptionPress()
    {
        if (player.mechanicDialogue)
        {
            player.mechanicDialogue = false;
            isActive[5] = false;
        }
        else
        {
            player.mechanicDialogue = true;
        }
        ChangeXMark(5);
        counters[5]++;
    }

    public void UnlockablesOptionPress()
    {
        if (player.mechanicUnlockables)
        {
            player.mechanicUnlockables = false;
            isActive[6] = false;
        }
        else
        {
            player.mechanicUnlockables = true;
        }
        ChangeXMark(6);
        counters[6]++;
    }

    public void TimeflowOptionPress()
    {
        if (player.mechanicTimeflow)
        {
            player.mechanicTimeflow = false;
            isActive[7] = false;
        }
        else
        {
            player.mechanicTimeflow = true;
        }
        ChangeXMark(7);
        counters[7]++;
    }

    public void InteractablesOptionPress()
    {
        if (player.mechanicInteractables)
        {
            player.mechanicInteractables = false;
            isActive[8] = false;
        }
        else
        {
            player.mechanicInteractables = true;
        }
        ChangeXMark(8);
        counters[8]++;
    }

    public void ChangeXMark(int index)
    {
        if (counters[index] % 2 == 0)
        {
            xMark[index].text = "X";
        }
        else
        {
            xMark[index].text = "";
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

    private void CheckForActiveMechanic()
    {
        for(int i = 0; i < isActive.Count; i++)
        {
            if (isActive[i])
            {
                xMark[i].text = "X";
                counters[i]++;
            }
        }
    }
}