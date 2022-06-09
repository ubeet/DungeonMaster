using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject warningScreen;

    public void NewGame()
    {
        SaveSystem.DeleteData();
        SceneTransition.SwitchScene(1);
        Time.timeScale = 1;
    }

    public void ContinueGame()
    {
        if (File.Exists(SaveSystem.PlayerPath) && File.Exists(SaveSystem.WorldPath))      
        {
            SceneTransition.SwitchScene(1);
            Time.timeScale = 1;
        }
        else
        {
            Debug.LogError(SaveSystem.WorldPath);
            warningScreen.SetActive(true);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
