using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] GameObject pauseScreen;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    
    public void SetHealth(int health)
    {
        slider.value = health;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
    }
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }
    } 
}
