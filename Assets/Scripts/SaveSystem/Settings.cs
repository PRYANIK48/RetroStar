using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Localization.Settings;
using UnityEngine.InputSystem;
using TMPro;

public class Settings : MonoBehaviour
{
    [Header("Audio")]
    public AudioMixer audioMixer;
    public Slider musicSlider, sfxSlider;

    [Header("Resolution")]
    public Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;
    Resolution[] resolutions;

    [Header("Language")]
    public Dropdown languageDropdown;


    private void Start()
    {
        LoadAudioSettings();
        InitResolutions();
        LoadResolutionSettings();
        InitLanguageDropdown();
    }

    #region Audio
    public void OnMusicVolumeChange(float value)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void OnSFXVolumeChange(float value)
    {
        audioMixer.SetFloat("SFX", Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    void LoadAudioSettings()
    {
        float music = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        float sfx = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        musicSlider.value = music;
        sfxSlider.value = sfx;
        OnMusicVolumeChange(music);
        OnSFXVolumeChange(sfx);
    }
    #endregion

    #region Resolution
    void InitResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        int current = 0;
        var options = new System.Collections.Generic.List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                current = i;
        }

        resolutionDropdown.AddOptions(options);
    }

    public void OnResolutionChange(int index)
    {
        Resolution res = resolutions[index];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", index);
    }

    public void OnToggleFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }

    void LoadResolutionSettings()
    {
        int resIndex = PlayerPrefs.GetInt("ResolutionIndex", resolutions.Length - 1);
        resolutionDropdown.value = resIndex;
        resolutionDropdown.RefreshShownValue();
        OnResolutionChange(resIndex);

        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        fullscreenToggle.isOn = isFullscreen;
        Screen.fullScreen = isFullscreen;
    }

    #endregion

    #region Language
    public void OnLanguageChange(int index)
    {
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
        PlayerPrefs.SetInt("LanguageIndex", index);
    }

    void InitLanguageDropdown()
    {
        int savedLang = PlayerPrefs.GetInt("LanguageIndex", 0);
        languageDropdown.value = savedLang;
        languageDropdown.onValueChanged.AddListener(OnLanguageChange);
        OnLanguageChange(savedLang);
    }

    #endregion

}
