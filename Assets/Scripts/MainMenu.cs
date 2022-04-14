using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject warningScreen;
    
    private void Start()
    {
        
    }

    public void NewGame()
    {
        SaveSystem.DeleteData();
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void ContinueGame()
    {
        if (File.Exists(SaveSystem.PlayerPath) && File.Exists(SaveSystem.WorldPath))      
        {
            SceneManager.LoadScene(1);
            Time.timeScale = 1;
        }
        else
        {
            warningScreen.SetActive(true);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
