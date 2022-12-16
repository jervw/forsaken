using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This script manages settings for the game.
/// </summary>
public class Settings : MonoBehaviour
{
    private static Settings instance;

    [SerializeField] private GameObject playPanel, settingsPanel;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Sound menuMusic;
    [SerializeField] private LevelData[] levels;

    private LevelData _selectedLevel;
    private Slider _sfxSlider, _musicSlider;
    private TMP_Dropdown _resolutionDropdown;
    private Toggle _fullscreenToggle;

    private void Start()
    {
        if (!instance)
        {
            instance = this;
            _selectedLevel = levels[0];
            SetupUserInterface();
            AudioManager.Instance.Play(menuMusic.clip.name);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// This method gets references to UI elements and sets their values. 
    /// </summary>
    private void SetupUserInterface()
    {
        Cursor.visible = true;

        // Setup for play panel UI elements
        if (playPanel)
        {
            var levelSelect = playPanel.GetComponentInChildren<TMP_Dropdown>();
            levelSelect.ClearOptions();

            var levelNames = levels.Select(level => level.levelName).ToList();
            levelSelect.AddOptions(levelNames);

            var preview = GameObject.FindGameObjectWithTag("LevelPreview").GetComponent<Image>();

            // get selected level scene name
            levelSelect.onValueChanged.AddListener(delegate
            {
                _selectedLevel = levels[levelSelect.value];
                preview.sprite = _selectedLevel.levelSprite;
            });

            playPanel.SetActive(false);
        }

        // Setup for settings UI elements
        if (settingsPanel)
        {
            // caution, hardcoded indexes used for sliders
            var sliders = settingsPanel.GetComponentsInChildren<Slider>();
            _sfxSlider = sliders[0];
            _musicSlider = sliders[1];

            _resolutionDropdown = settingsPanel.GetComponentInChildren<TMP_Dropdown>();
            _fullscreenToggle = settingsPanel.GetComponentInChildren<Toggle>();

            LoadSettings();
        }
        else
        {
            Debug.LogError("Settings panel not found");
        }
    }

    /// <summary>
    /// Load settings from PlayerPrefs, additionally sets the values of the UI elements
    /// </summary>
    private void LoadSettings()
    {
        // Check for existing PlayerPrefs
        if (PlayerPrefs.HasKey("SFXVolume"))
            _sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        if (PlayerPrefs.HasKey("MusicVolume"))
            _musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        if (PlayerPrefs.HasKey("Fullscreen"))
            _fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen") == 1;

        SetEffectVolume(_sfxSlider.value);
        SetMusicVolume(_musicSlider.value);

        _resolutionDropdown.ClearOptions();
        var currentResolutionIndex = 0;
        foreach (var res in Screen.resolutions)
        {
            _resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(res.width + "x" + res.height));
            if (res.width == Screen.currentResolution.width && res.height == Screen.currentResolution.height)
            {
                currentResolutionIndex = _resolutionDropdown.options.Count - 1;
            }
        }

        _resolutionDropdown.value = currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }

    /// <summary>
    /// Save settings to PlayerPrefs.
    /// </summary>
    private void SaveSettings()
    {
        PlayerPrefs.SetFloat("SFXVolume", _sfxSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", _musicSlider.value);
        PlayerPrefs.SetInt("Resolution", _resolutionDropdown.value);
        PlayerPrefs.SetInt("Fullscreen", _fullscreenToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void OpenPlayPanel()
    {
        playPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void OpenSettingsPanel()
    {
        settingsPanel.SetActive(true);
        playPanel.SetActive(false);
        LoadSettings();
    }

    public void CloseActivePanel()
    {
        settingsPanel.SetActive(false);
        playPanel.SetActive(false);
        SaveSettings();
    }

    public void ExitGame()
    {
        Debug.Log("Exiting game");
        Application.Quit();
    }

    /// <summary>
    ///  Set resolution according to the dropdown value.
    /// </summary>
    /// <param name="index"></param>
    public void SetResolution(int index)
    {
        if (index < 0 || index >= Screen.resolutions.Length)
        {
            Debug.LogError("Invalid resolution index");
            return;
        }

        var res = Screen.resolutions[index];
        if (res.width != Screen.width || res.height != Screen.height)
        {
            Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        }
    }

    /// <summary>
    /// Set fullscreen mode.
    /// </summary>
    /// <param name="fullscreen"></param>
    public void SetFullscreen(bool fullscreen) => Screen.fullScreen = fullscreen;

    /// <summary>
    /// Set the volume of the SFX mixer group logarithmically.
    /// </summary>
    /// <param name="volume"></param>
    public void SetEffectVolume(float volume) => mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);

    /// <summary>
    /// Set the volume of the music mixer group logarithmically.
    /// </summary>
    /// <param name="volume"></param>
    public void SetMusicVolume(float volume) => mixer.SetFloat("Music", Mathf.Log10(volume) * 20);
}