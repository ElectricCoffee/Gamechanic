using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] Text[] loseScreenOptions;
    int optionIndex = 0;

    void Update()
    {
        MenuTextColors();
    }

    private void MenuTextColors()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            optionIndex -= 1;
            if (optionIndex - 1 < 0)
            {
                optionIndex = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            optionIndex += 1;
            if (optionIndex + 1 >= loseScreenOptions.Length)
            {
                optionIndex = loseScreenOptions.Length - 1;
            }
        }

        loseScreenOptions[optionIndex].color = Color.blue;

        if (optionIndex > 0)
        {
            loseScreenOptions[optionIndex - 1].color = Color.white;
        }
        if (optionIndex < loseScreenOptions.Length - 1)
        {
            loseScreenOptions[optionIndex + 1].color = Color.white;
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Level1");
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
