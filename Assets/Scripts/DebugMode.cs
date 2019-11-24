using System;
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
    [SerializeField] Text kbAvailable;
    [SerializeField] Text kbUsed;

    public int mMovementKB = 5;
    public int mJump = 5;
    public int mRotation = 5;
    public int mHealth = 5;
    public int mCombat = 5;
    public int mDialogue = 5;
    public int mUnlockables = 10;
    public int mTimeFlow = 10;
    public int mInteractables = 10;

    int optionIndex = 0;
    int[] counters;
    List<bool> isActive;
    public int maxKB = 20;
    public int currentKB = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
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

        capacityText[0].text = mMovementKB.ToString();
        capacityText[1].text = mJump.ToString();
        capacityText[2].text = mRotation.ToString();
        capacityText[3].text = mHealth.ToString();
        capacityText[4].text = mCombat.ToString();
        capacityText[5].text = mDialogue.ToString();
        capacityText[6].text = mUnlockables.ToString();
        capacityText[7].text = mTimeFlow.ToString();
        capacityText[8].text = mInteractables.ToString();

        foreach (Button button in mechanicsOptions)
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
            RemoveKB(mMovementKB);
            ChangeXMark(0);
            counters[0]++;
        }
        else if(AddKB(mMovementKB))
        {
            player.mechanicMovement = true;
            ChangeXMark(0);
            counters[0]++;
        }

    }

    public void JumpingOptionPress()
    {
        if (player.mechanicJump)
        {
            player.mechanicJump = false;
            isActive[1] = false;
            RemoveKB(mJump);
            ChangeXMark(1);
            counters[1]++;
        }
        else if (AddKB(mJump))
        {
            player.mechanicJump = true;
            ChangeXMark(1);
            counters[1]++;
        }

    }

    public void RotationOptionPress()
    {
        if (player.mechanicRotation)
        {
            player.mechanicRotation = false;
            isActive[2] = false;
            RemoveKB(mRotation);
            ChangeXMark(2);
            counters[2]++;
        }
        else if (AddKB(mRotation))
        {
            player.mechanicRotation = true;
            ChangeXMark(2);
            counters[2]++;
        }

    }

    public void HealthOptionPress()
    {
        if (player.mechanicHealth)
        {
            player.mechanicHealth = false;
            isActive[3] = false;
            RemoveKB(mHealth);
            ChangeXMark(3);
            counters[3]++;
        }
        else if (AddKB(mHealth))
        {
            player.mechanicHealth = true;
            ChangeXMark(3);
            counters[3]++;
        }

    }

    public void CombatOptionPress()
    {
        if (player.mechanicCombat)
        {
            player.mechanicCombat = false;
            isActive[4] = false;
            RemoveKB(mCombat);
            ChangeXMark(4);
            counters[4]++;
        }
        else if (AddKB(mCombat))
        {
            player.mechanicCombat = true;
            ChangeXMark(4);
            counters[4]++;
        }

    }

    public void DialogueOptionPress()
    {
        if (player.mechanicDialogue)
        {
            player.mechanicDialogue = false;
            isActive[5] = false;
            RemoveKB(mDialogue);
            ChangeXMark(5);
            counters[5]++;
        }
        else if (AddKB(mDialogue))
        {
            player.mechanicDialogue = true;
            ChangeXMark(5);
            counters[5]++;
        }

    }

    public void UnlockablesOptionPress()
    {
        if (player.mechanicUnlockables)
        {
            player.mechanicUnlockables = false;
            isActive[6] = false;
            RemoveKB(mUnlockables);
            ChangeXMark(6);
            counters[6]++;
        }
        else if (AddKB(mUnlockables))
        {
            player.mechanicUnlockables = true;
            ChangeXMark(6);
            counters[6]++;
        }

    }

    public void TimeflowOptionPress()
    {
        if (player.mechanicTimeflow)
        {
            player.mechanicTimeflow = false;
            isActive[7] = false;
            RemoveKB(mTimeFlow);
            ChangeXMark(7);
            counters[7]++;
        }
        else if (AddKB(mTimeFlow))
        {
            player.mechanicTimeflow = true;
            ChangeXMark(7);
            counters[7]++;
        }

    }

    public void InteractablesOptionPress()
    {
        if (player.mechanicInteractables)
        {
            player.mechanicInteractables = false;
            isActive[8] = false;
            RemoveKB(mInteractables);
            ChangeXMark(8);
            counters[8]++;
        }
        else if (AddKB(mInteractables))
        {
            player.mechanicInteractables = true;
            ChangeXMark(8);
            counters[8]++;
        }

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
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            optionIndex -= 1;
            if (optionIndex - 1 < 0)
            {
                optionIndex = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
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
                AddKB(Int32.Parse(capacityText[i].text));
                counters[i]++;
            }
        }
    }

    bool AddKB(int mechanic)
    {
        bool result = false;
        if((currentKB + mechanic) < maxKB)
        {
            currentKB += mechanic;
            result = true;
        }
        kbAvailable.text = maxKB.ToString();
        kbUsed.text = currentKB.ToString();
        return result;
    }

    void RemoveKB(int mechanic)
    {
        currentKB -= mechanic;
        kbAvailable.text = maxKB.ToString();
        kbUsed.text = currentKB.ToString();
    }
}