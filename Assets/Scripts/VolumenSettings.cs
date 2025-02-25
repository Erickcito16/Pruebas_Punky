using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumenSettings : MonoBehaviour
{

    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        if(PlayerPrefs.HasKey("musicVolumen"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSfxVolume();

        }
           
        
    }

    public void SetMusicVolume()
    {
        float volumen = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volumen) * 20);
        PlayerPrefs.SetFloat("musicVolumen", volumen);
    }

    public void SetSfxVolume()
    {
        float volumen = sfxSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volumen) * 20);
        PlayerPrefs.SetFloat("SFXVolumen", volumen);
    }



    public void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolumen");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolumen");

        SetMusicVolume();
        SetSfxVolume();
    }

}
