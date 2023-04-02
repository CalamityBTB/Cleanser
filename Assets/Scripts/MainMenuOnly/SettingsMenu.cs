using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider VolumeSlider;
    public AudioMixer audioMixer;
    public float value;

    public void Start()
    {
        audioMixer.GetFloat("Volume", out value);
        VolumeSlider.value = value;
    }
    public void SetVolume()
    {

        audioMixer.SetFloat("Volume", VolumeSlider.value);
    }

    public void SetQuality(int QualityIndex)
    {
        QualitySettings.SetQualityLevel(QualityIndex);
    }
    
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
