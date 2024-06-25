using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OptionsButton()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitButton()
    {
        Application.Quit(); //Работает только после компиляции (по крайней мере так было сказано в гайдах)
    }
}
