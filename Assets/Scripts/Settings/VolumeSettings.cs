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

    // mixer exposed names
    private const string masterName = "MasterVolume";
    private const string musicName = "MusicVolume";
    private const string fxName = "EffectsVolume";
    private const string dialogName = "DialogsVolume";

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
    }

    private void LoadSaveValues()
    {
        float masterVolume = PlayerPrefs.GetFloat(masterName, 0.5f);
        float musicVolume = PlayerPrefs.GetFloat(musicName, 0.5f);
        float sfxVolume = PlayerPrefs.GetFloat(fxName, 0.5f);
        float dialogVolume = PlayerPrefs.GetFloat(dialogName, 0.5f);

        masterSlider.value = masterVolume;
        musicSlider.value = musicVolume;
        effectSlider.value = sfxVolume;
        dialogSlider.value = dialogVolume;
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
    }

    private void SetVolume(string volumeType, float volume)
    {
        mixer.SetFloat(volumeType, Mathf.Log10(volume) * 20f);
        PlayerPrefs.SetFloat(volumeType, volume);
    }
    #endregion
}