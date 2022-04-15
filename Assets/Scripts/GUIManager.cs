using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] GameObject pauseScreen;
    [SerializeField] GameObject mapScreen;
    [SerializeField] GameObject settingsScreen;
    [SerializeField] GameObject deathScreen;
    [SerializeField] Text moneyNumber;
    [SerializeField] GameObject[] HUD;

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

    public void Pause()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }
    
    public void Map()
    {
        mapScreen.SetActive(true);
    }
    
    public void Resume()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void Update()
    {
        if (!deathScreen.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.Tab))
            mapScreen.SetActive(true);
        else if(Input.GetKeyUp(KeyCode.Tab))
            mapScreen.SetActive(false);
        
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
