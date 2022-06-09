using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject winScreen;
    [SerializeField] Text moneyNumber;
    [SerializeField] GameObject[] HUD;
    [SerializeField] Slider slider;

    private static bool _win;
    
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
    }
    
    public void SetHealth(int health)
    {
        slider.value = health;
    }
    
    public void SetMoney(int money)
    {
        moneyNumber.text = money.ToString();
    }

    public static void SetWin()
    {
        _win = true;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }
    
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneTransition.SwitchScene(0);
    }
    
    public void Restart()
    {
        Time.timeScale = 1;
        SceneTransition.SwitchScene(1);
    }
    
    private void Win()
    {
        _win = false;
        Time.timeScale = 0;
        winScreen.SetActive(true);
    }
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !deathScreen.activeInHierarchy && !winScreen.activeInHierarchy)
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }

        switch (PlayerPrefs.GetInt("hudScale"))
        {
            case 0:
                foreach (var element in HUD)
                    element.transform.localScale = new Vector3(0.75f,0.75f,1);
                break;
            case 1:
                foreach (var element in HUD)
                    element.transform.localScale = new Vector3(1,1,1);
                break;
            case 2:
                foreach (var element in HUD)
                    element.transform.localScale = new Vector3(1.25f,1.25f,1);
                break;
        }
        
        if (_win) Win();
    }
}
