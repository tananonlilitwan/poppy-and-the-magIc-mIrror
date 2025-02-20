using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Import นี้สำหรับการใช้งาน Slider

public class AudioManager : MonoBehaviour
{
    
    [Header("----------------- Audio Source --------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----------------- Audio Clip --------")]
    public AudioClip background;
    //public AudioClip death;
    public AudioClip shoot;
    public AudioClip Win;
    public AudioClip GameOver;
    public AudioClip CutScene;
    public AudioClip paper;
    public AudioClip click;
    
    public AudioClip map1Music;
    public AudioClip map2Music;
    public AudioClip map3Music; // เพิ่มตัวแปรนี้
    public AudioClip map4Music; // เพิ่มตัวแปรนี้
    public AudioClip map5Music; // เพิ่มตัวแปรนี้
    public AudioClip map6Music; // เพิ่มตัวแปรนี้
    public AudioClip map7Music; // เพิ่มตัวแปรนี้
    public AudioClip map8Music; // เพิ่มตัวแปรนี้
    public AudioClip map9Music; // เพิ่มตัวแปรนี้
    public AudioClip map10Music; // เพิ่มตัวแปรนี้
    public AudioClip map11Music; // เพิ่มตัวแปรนี้
    
   
    
    private void Start()
    { 
        
        //musicSource.clip = background;
        //musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
        
    }
    
    public void PlayBackgroundMusic(AudioClip clip)
    {
        if (musicSource != null && clip != null)
        {
            musicSource.clip = clip;  // ตั้งค่า AudioClip ที่จะเล่น
            musicSource.Play();
        }
    }

    public void StopBackgroundMusic()
    {
        if (musicSource != null && musicSource.isPlaying)
        {
            musicSource.Stop();
        }
    }

    
}