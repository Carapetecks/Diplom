using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject mainCamera;
    public ColorBSC colorBSC;

    public Slider slider;
    public Slider sliderVolume;
    public Slider sliderBrightness;
    public Slider sliderContrast;
    public Slider sliderSaturation;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }
    public void SetBrightness(float brightness)
    {
        colorBSC.brightness = brightness * 1;
    }
    public void SetContrast(float contrast)
    {
        colorBSC.contrast = contrast * 1;
    }
    public void SetSaturation(float saturation)
    {
        colorBSC.saturation = saturation * 1;
    }
    public void DefaultSettings()
    {
        sliderBrightness.value = 1;
        sliderContrast.value = 1;
        sliderSaturation.value = 1;
        sliderVolume.value = 1;       
    }
   
}
