using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AUDIOVolumeSettings : MonoBehaviour
{
    [SerializeField] private Slider muicSlider;
    //[SerializeField] private Slider SFXlider;
    //[SerializeField] private AudioMixer audioMixer; // เชื่อมต่อ AudioMixer

    private void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
        
        /*if (!PlayerPrefs.HasKey("SFXlider"))
        {
            PlayerPrefs.SetFloat("SFXlider", 1);
            Load();
        }
        
        {
            Load();
        }*/
        
        
        
    }

    public void ChangeVolume()
    {
        AudioListener.volume = muicSlider.value;

        //AudioListener.volume = SFXlider.value;
        
        /*// เปลี่ยน Volume สำหรับ Music
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(muicSlider.value) * 20);
        // เปลี่ยน Volume สำหรับ SFX
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(SFXlider.value) * 20);*/
        
        Save();
        
    }

    private void Load()
    {
        muicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        //SFXlider.value = PlayerPrefs.GetFloat("SFXlider");
        
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", muicSlider.value);
        //PlayerPrefs.SetFloat("SFXlider", SFXlider.value);
    }
}
