using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] Text[] menuOptions;
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
            if (optionIndex + 1 >= menuOptions.Length)
            {
                optionIndex = menuOptions.Length - 1;
            }
        }

        menuOptions[optionIndex].color = Color.blue;

        if (optionIndex > 0)
        {
            menuOptions[optionIndex - 1].color = Color.white;
        }
        if (optionIndex < menuOptions.Length - 1)
        {
            menuOptions[optionIndex + 1].color = Color.white;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
        SceneManager.UnloadSceneAsync("StartMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}