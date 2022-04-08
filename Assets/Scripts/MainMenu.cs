using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static System.DateTime;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text versionField;
    
    public void NewGame()
    {
        SaveSystem.DeleteData();
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }
    
    public void ContinueGame()
    {
        if (File.Exists(SaveSystem.path) && File.Exists(SaveSystem.worldPath))      
        {
            SceneManager.LoadScene(1);
            Time.timeScale = 1;
        }
    }

    public void Settings()
    {
        //loading settings screen
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
