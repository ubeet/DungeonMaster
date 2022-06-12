using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [Header("Attributes")]
    
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private GameObject previousScreen;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Dropdown dropdown;
    [SerializeField] private Slider slider;
    [SerializeField] private Toggle toggle;
    
    private void Start()
    {
        Screen.fullScreen = Convert.ToBoolean(PlayerPrefs.GetInt("fullscreen"));
        audioMixer.SetFloat("volume",PlayerPrefs.GetFloat("volume"));
        toggle.GetComponent<Toggle>().isOn = Convert.ToBoolean(PlayerPrefs.GetInt("fullscreen"));
        slider.value = PlayerPrefs.GetFloat("volume");
        dropdown.value = PlayerPrefs.GetInt("hudScale");
    }
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

    private void Save()
    {
        PlayerPrefs.Save();
        settingsScreen.SetActive(false);
        previousScreen.SetActive(true);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && settingsScreen.activeInHierarchy)
        {
            settingsScreen.SetActive(false);
            previousScreen.SetActive(true);
        }
    }

    
}
