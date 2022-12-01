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
    TMP_Dropdown resolutionDropdown;
    Toggle fullscreenToggle;

    void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);

        settingsPanel = MainMenuState.Instance.settingsView;
        if (!settingsPanel) throw new System.Exception("Settings panel not found");

        var sliders = settingsPanel.GetComponentsInChildren<Slider>();
        sfxSlider = sliders[0];
        musicSlider = sliders[1];
        resolutionDropdown = settingsPanel.GetComponentInChildren<TMP_Dropdown>();
        fullscreenToggle = settingsPanel.GetComponentInChildren<Toggle>();


    }

    void Start() => LoadSettings();

    void LoadSettings()
    {
        if (PlayerPrefs.HasKey("SFXVolume"))
            sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        if (PlayerPrefs.HasKey("MusicVolume"))
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        if (PlayerPrefs.HasKey("Fullscreen"))
            fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen") == 1;

        SetSFXVolume(sfxSlider.value);
        SetMusicVolume(musicSlider.value);

        resolutionDropdown.ClearOptions();
        int currentResolutionIndex = 0;
        foreach (var res in Screen.resolutions)
        {
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(res.width + "x" + res.height + " @ " + res.refreshRate + "Hz"));
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

        MainMenuState.Instance.SetState(MainMenuState.MenuState.Main);
    }

    public void SetResolution(int index)
    {
        Resolution res = Screen.resolutions[index];
        if (res.width == Screen.currentResolution.width && res.height == Screen.currentResolution.height) return;
        Screen.SetResolution(res.width, res.height, Screen.fullScreenMode, res.refreshRate);
    }

    public void SetFullscreen(bool fullscreen) => Screen.fullScreen = fullscreen;

    public void SetSFXVolume(float volume) => mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);

    public void SetMusicVolume(float volume) => mixer.SetFloat("Music", Mathf.Log10(volume) * 20);
}
