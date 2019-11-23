using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugMode : MonoBehaviour
{
    [SerializeField] Button[] mechanicsOptions;
    [SerializeField] Image[] xMarkImages;
    [SerializeField] Sprite imageSprite;

    int[] counters;

    void Start()
    {
        counters = new int[mechanicsOptions.Length];

        foreach(Button button in mechanicsOptions)
        {
            button.GetComponent<Button>();
        }

        foreach(Image img in xMarkImages)
        {
            img.GetComponent<Image>();
            Color newColor = img.color;
            newColor.a = 0f;
            img.color = newColor;
        }
    }
    
    public void MovementOptionPress()
    {
        Debug.Log(mechanicsOptions[0].gameObject.name + " was pressed.");
        ChangeImage(0);
        counters[0]++;
    }

    public void JumpingOptionPress()
    {
        Debug.Log(mechanicsOptions[1].gameObject.name + " was pressed.");
        ChangeImage(1);
        counters[1]++;
    }

    public void RotationOptionPress()
    {
        Debug.Log(mechanicsOptions[2].gameObject.name + " was pressed.");
        ChangeImage(2);
        counters[2]++;
    }

    public void HealthOptionPress()
    {
        Debug.Log(mechanicsOptions[3].gameObject.name + " was pressed.");
        ChangeImage(3);
        counters[3]++;
    }

    public void CombatOptionPress()
    {
        Debug.Log(mechanicsOptions[4].gameObject.name + " was pressed.");
        ChangeImage(4);
        counters[4]++;
    }

    public void DialogueOptionPress()
    {
        Debug.Log(mechanicsOptions[5].gameObject.name + " was pressed.");
        ChangeImage(5);
        counters[5]++;
    }

    public void ChangeImage(int imageIndex)
    {
        if(counters[imageIndex] % 2 == 0)
        {
            xMarkImages[imageIndex].sprite = imageSprite;
            xMarkImages[imageIndex].color = new Color(1, 1, 1, 1);
        }
        else
        {
            xMarkImages[imageIndex].sprite = null;
            xMarkImages[imageIndex].color = new Color(1, 1, 1, 0);
        }
    }
}