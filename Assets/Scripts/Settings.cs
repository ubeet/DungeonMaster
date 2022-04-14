using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] Slider slider;
    [SerializeField] GameObject settingsScreen;
    [SerializeField] GameObject previousScreen;
    [SerializeField] Dropdown dropdown;
    [SerializeField] Toggle toggle;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volume", volume);
    }
    
    public void SetHUDScale(int hudScale)
    {
        PlayerPrefs.SetInt("hudScale", hudScale);
    }
    
    public void FullScreen(bool isFull)
    {
        Screen.fullScreen = isFull;
        PlayerPrefs.SetInt("fullscreen", Convert.ToInt32(isFull));
    }

    public void Save()
    {
        PlayerPrefs.Save();
        settingsScreen.SetActive(false);
        previousScreen.SetActive(true);
    }
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            settingsScreen.SetActive(false);
            previousScreen.SetActive(true);
        }
    }

    public void Start()
    {
        Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("fullscreen"));
        audioMixer.SetFloat("volume",PlayerPrefs.GetFloat("volume"));
        toggle.GetComponent<Toggle>().isOn = Convert.ToBoolean(PlayerPrefs.GetInt("fullscreen"));
        slider.value = PlayerPrefs.GetFloat("volume");
        dropdown.value = PlayerPrefs.GetInt("hudScale");
    }
}
