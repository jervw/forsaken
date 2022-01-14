using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public static Settings Instance;

    public AudioMixer mixer;

    GameObject settingsPanel;
    Slider sfxSlider, musicSlider;
    TMPro.TMP_Dropdown resolutionDropdown;
    Toggle fullscreenToggle;

    void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        settingsPanel = MainMenuState.Instance.settingsView;
        if (!settingsPanel) throw new System.Exception("Settings panel not found");

        sfxSlider = settingsPanel.GetComponentInChildren<Slider>();
        musicSlider = settingsPanel.GetComponentInChildren<Slider>();
        resolutionDropdown = settingsPanel.GetComponentInChildren<TMPro.TMP_Dropdown>();
        fullscreenToggle = settingsPanel.GetComponentInChildren<Toggle>();

        LoadSettings();
    }

    void LoadSettings()
    {
        if (PlayerPrefs.HasKey("SFXVolume"))
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        if (PlayerPrefs.HasKey("MusicVolume"))
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        if (PlayerPrefs.HasKey("Fullscreen"))
            fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen") == 1;

        mixer.SetFloat("SFX", sfxSlider.value);
        mixer.SetFloat("Music", musicSlider.value);

        resolutionDropdown.ClearOptions();
        int currentResolutionIndex = 0;
        foreach (var res in Screen.resolutions)
        {
            resolutionDropdown.options.Add(new TMPro.TMP_Dropdown.OptionData(res.width + "x" + res.height + " @ " + res.refreshRate + "Hz"));
            if (res.width == Screen.currentResolution.width && res.height == Screen.currentResolution.height && res.refreshRate == Screen.currentResolution.refreshRate)
                currentResolutionIndex = resolutionDropdown.options.Count - 1;
        }
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetInt("Resolution", resolutionDropdown.value);
        PlayerPrefs.SetInt("Fullscreen", fullscreenToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetResolution(int index)
    {
        Resolution res = Screen.resolutions[index];
        if (res.width == Screen.currentResolution.width && res.height == Screen.currentResolution.height) return;
        Screen.SetResolution(res.width, res.height, Screen.fullScreenMode, res.refreshRate);
    }

    public void SetFullscreen(bool fullscreen) => Screen.fullScreen = fullscreen;

    public void SetSFXVolume(float volume) => mixer.SetFloat("SFX", volume);

    public void SetMusicVolume(float volume) => mixer.SetFloat("Music", volume);
}
