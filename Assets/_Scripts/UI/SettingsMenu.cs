using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SettingsMenu :MonoBehaviour
{
    enum VolumeType { MasterVolume, MusicVolume, SoundVolume }
    //private UIDocument document;
    private VisualElement container;
    private Slider MasterVolumeSlider;
    private Slider MusicVolumeSlider;
    private Slider SoundVolumeSlider;
    private AudioMixer audioMixer;
    AudioSource audioSource;
    const string MasterVolumestr = "MasterVolume";
    const string MusicVolumestr = "MusicVolume";
    const string SoundVolumestr = "SoundVolume";
    public void Initialize(VisualElement container, AudioMixer audioMixer, AudioSource audioSource)
    {


    //document = GetComponent<UIDocument>();
        this.audioMixer= audioMixer;
        this.audioSource = audioSource;
        MasterVolumeSlider = container.Q("MasterVolume") as Slider;
        MusicVolumeSlider = container.Q("MusicVolume") as Slider;
        SoundVolumeSlider = container.Q("SoundVolume") as Slider;
        MasterVolumeSlider.RegisterValueChangedCallback(evt => VolumeChange(evt.newValue, VolumeType.MasterVolume.ToString()));
        MusicVolumeSlider.RegisterValueChangedCallback(evt => VolumeChange(evt.newValue, VolumeType.MusicVolume));
        SoundVolumeSlider.RegisterValueChangedCallback(evt => VolumeChange(evt.newValue, VolumeType.SoundVolume));
        SetupInitialValues();
    }
    private void SetupInitialValues()
    {
        MasterVolumeSlider.value = PlayerPrefs.GetFloat(MasterVolumestr, -10f);
        MusicVolumeSlider.value = PlayerPrefs.GetFloat(MusicVolumestr, -10f);
        SoundVolumeSlider.value = PlayerPrefs.GetFloat(SoundVolumestr, -10f);

        audioMixer.SetFloat(MasterVolumestr, MasterVolumeSlider.value);
        audioMixer.SetFloat(MusicVolumestr, MasterVolumeSlider.value);
        audioMixer.SetFloat(SoundVolumestr, MasterVolumeSlider.value);

        Debug.Log(PlayerPrefs.GetFloat(SoundVolumestr, -10f) + " " + PlayerPrefs.GetFloat(MusicVolumestr, -10f) + " " + PlayerPrefs.GetFloat(SoundVolumestr, -10f));
    }

    private void VolumeChange(float value, VolumeType type)
    {
        Debug.Log("new value" + value + " " + type);
        audioMixer.SetFloat(type.ToString(), value);
        PlayerPrefs.SetFloat(type.ToString(), value);
    } private void VolumeChange(float value, string type)
    {
        Debug.Log("new value" + value + " " + type);
        audioMixer.SetFloat(type, value);
        PlayerPrefs.SetFloat(type, value);
    }
    private void PlaySound(VolumeType type)
    {
        switch (type)
        {
            case VolumeType.MusicVolume:
                break;
        }
        if (!audioSource.isPlaying)
        {
            //audioSource.clip = MeowSound;
            Debug.Log("audioSource plays " + type.ToString());
            audioSource.Play();
        }
    }
}
