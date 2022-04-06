using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static System.DateTime;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Text versionField;
    
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    
    public void Settings()
    {
        SceneManager.LoadScene(2);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
