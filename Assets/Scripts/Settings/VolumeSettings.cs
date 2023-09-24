using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class VolumeSettings : MonoBehaviour
{
    #region Variables
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer mixer;
    [Header("Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectSlider;
    [SerializeField] private Slider dialogSlider;

    //Subgroups
    [SerializeField] private Slider monstersSlider;
    [SerializeField] private Slider playerSlider;
    [SerializeField] private Slider environmentSlider;

    // mixer exposed names
    private const string masterName = "MasterVolume";
    private const string musicName = "MusicVolume";
    private const string fxName = "EffectsVolume";
    private const string dialogName = "DialogsVolume";

    // Subgroups
    private const string monstersName = "MonstersVolume";
    private const string playerName = "PlayerVolume";
    private const string environmentName = "EnvironmentVolume";

    #endregion

    #region Unity
    private void Start()
    {
        LoadSaveValues();
        AddSliderListeners();
        SetAllVolumes();
    }

    #endregion

    #region Load
    private void SetAllVolumes()
    {
        // Establecer los valores iniciales de volumen llamando a SetVolume() directamente
        SetVolume(masterName, masterSlider.value);
        SetVolume(musicName, musicSlider.value);
        SetVolume(fxName, effectSlider.value);
        SetVolume(dialogName, dialogSlider.value);

        // Subgroups
        SetVolume(monstersName, monstersSlider.value);
        SetVolume(playerName, playerSlider.value);
        SetVolume(environmentName, environmentSlider.value);
    }

    private void LoadSaveValues()
    {
        float masterVolume = PlayerPrefs.GetFloat(masterName, 0.5f);
        float musicVolume = PlayerPrefs.GetFloat(musicName, 0.5f);
        float sfxVolume = PlayerPrefs.GetFloat(fxName, 0.5f);
        float dialogVolume = PlayerPrefs.GetFloat(dialogName, 0.5f);
        // Subgroups
        float monstersVolume = PlayerPrefs.GetFloat(monstersName, 0.5f);
        float playerVolume = PlayerPrefs.GetFloat(playerName, 0.5f);
        float environmentVolume = PlayerPrefs.GetFloat(environmentName, 0.5f);

        masterSlider.value = masterVolume;
        musicSlider.value = musicVolume;
        effectSlider.value = sfxVolume;
        dialogSlider.value = dialogVolume;

        // Subgroups
        monstersSlider.value = monstersVolume;
        playerSlider.value = playerVolume;
        environmentSlider.value = environmentVolume;
    }
    #endregion

    #region Action
    private void AddSliderListeners()
    {
        // Agregar listeners para mantener el volumen actualizado cuando se interactúa con los sliders
        masterSlider.onValueChanged.AddListener(delegate { SetVolume(masterName, masterSlider.value); });
        musicSlider.onValueChanged.AddListener(delegate { SetVolume(musicName, musicSlider.value); });
        effectSlider.onValueChanged.AddListener(delegate { SetVolume(fxName, effectSlider.value); });
        dialogSlider.onValueChanged.AddListener(delegate { SetVolume(dialogName, dialogSlider.value); });

        // Subgroups
        monstersSlider.onValueChanged.AddListener(delegate { SetVolume(monstersName, monstersSlider.value); });
        playerSlider.onValueChanged.AddListener(delegate { SetVolume(playerName, playerSlider.value); });
        environmentSlider.onValueChanged.AddListener(delegate { SetVolume(environmentName, environmentSlider.value); });
    }

    private void SetVolume(string volumeType, float volume)
    {
        mixer.SetFloat(volumeType, Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat(volumeType, volume);
    }
    #endregion
}