using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{
    public AudioMixer mixer;

    Slider sfxSlider, musicSlider;
    TMPro.TMP_Dropdown resolutionDropdown;
    Toggle fullscreenToggle;

    void Awake()
    {
        sfxSlider = GameObject.Find("SFXSlider").GetComponent<Slider>();
        musicSlider = GameObject.Find("MusicSlider").GetComponent<Slider>();
        resolutionDropdown = GetComponentInChildren<TMPro.TMP_Dropdown>();
        fullscreenToggle = GetComponentInChildren<Toggle>();
    }

    void OnEnable()
    {
        if (PlayerPrefs.HasKey("SFXVolume"))
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        if (PlayerPrefs.HasKey("MusicVolume"))
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");

        fullscreenToggle.isOn = Screen.fullScreen;

        foreach (var res in Screen.resolutions)
        {
            if (res.width == Screen.currentResolution.width && res.height == Screen.currentResolution.height)
                resolutionDropdown.value = resolutionDropdown.options.FindIndex(x => x.text == res.width + "x" + res.height);
            resolutionDropdown.options.Add(new TMPro.TMP_Dropdown.OptionData(res.width + "x" + res.height + " @ " + res.refreshRate + "Hz"));
        }

    }

    public void ClosePanel()
    {
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetInt("Resolution", resolutionDropdown.value);
        PlayerPrefs.SetInt("Fullscreen", fullscreenToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();

        MainMenuState.Instance.SetState(MainMenuState.MenuState.Main);
    }

    public void SetResolution(int index) => Screen.SetResolution(Screen.resolutions[index].width, Screen.resolutions[index].height, Screen.fullScreen);

    public void SetFullscreen(bool fullscreen) => Screen.fullScreen = fullscreen;

    public void SetSFXVolume(float volume) => mixer.SetFloat("SFX", volume);

    public void SetMusicVolume(float volume) => mixer.SetFloat("Music", volume);
}
