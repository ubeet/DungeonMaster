using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private Text moneyNumber;
    [SerializeField] private GameObject[] HUD;
    [SerializeField] private Slider slider;

    public bool _win { get; set; } = false;
    
    internal void SetMaxHealth(int health)
    {
        slider.maxValue = health;
    }
    
    internal void SetHealth(int health)
    {
        slider.value = health;
    }
    
    internal void SetMoney(int money)
    {
        moneyNumber.text = money.ToString();
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
        SceneTransition.SwitchScene(0);
        Time.timeScale = 1;
    }
    
    public void Restart()
    {
        SceneTransition.SwitchScene(1);
        Time.timeScale = 1;
    }
    
    internal void Win()
    {
        _win = true;
        winScreen.SetActive(true);
        Time.timeScale = 0;
        
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !deathScreen.activeInHierarchy && !winScreen.activeInHierarchy)
        {
            Pause();
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
        
    }
}
