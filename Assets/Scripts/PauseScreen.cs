using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void Settings()
    {
        //loading settings screen
    }
    
    public void Resume()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
        }
    }
}
