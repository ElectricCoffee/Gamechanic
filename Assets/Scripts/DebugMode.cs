using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugMode : MonoBehaviour
{
    [SerializeField] Button movementOption;
    [SerializeField] Image[] xMarkImages;
    [SerializeField] Sprite imageSprite;
    
    void Start()
    {
        movementOption.GetComponent<Button>();
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
        Debug.Log(movementOption.gameObject.name + " was pressed.");
        ChangeImage(0);
    }

    public void ChangeImage(int imageIndex)
    {
        xMarkImages[imageIndex].sprite = imageSprite;
        xMarkImages[imageIndex].color = new Color(1, 1, 1, 1);
    }
}
