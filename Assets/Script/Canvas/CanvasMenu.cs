using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // ต้องนำเข้า UI namespace เพื่อใช้ Button

public class CanvasMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;         // Panel 'CanvasMenu'
    [SerializeField] private GameObject canvasPanel;       // Panel 'Canvas Ui in game'
    [SerializeField] private GameObject howToPlayPanel;    // Panel 'HowToPlay'
    [SerializeField] private GameObject canvasCutScene;    // Panel 'CanvasCutScene'

    [SerializeField] private GameObject aboutGameSystem;   // Panel 'About Game System'
    
    //private CutScene cutScene; // Reference to the CutScene component
    
    private AudioManager audioManager; // เสียงในเกม

    private void Start()
    {
        // หยุดเกมเมื่อเริ่มต้น (จนกว่าจะกดปุ่ม Play)
        Time.timeScale = 0;
        
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // เสียงในเกม

        // แสดง Panel 'menu' เมื่อเริ่มต้น
        if (menuPanel != null)
        {
            menuPanel.SetActive(true);
        }

        // ซ่อน Panel 'Canvas' เมื่อเริ่มต้น
        if (canvasPanel != null)
        {
            canvasPanel.SetActive(false);
        }

        // ซ่อน Panel 'HowToPlay' เมื่อเริ่มต้น
        if (howToPlayPanel != null)
        {
            howToPlayPanel.SetActive(false);
        }
        
        // หยุดระบบของเกม (ถ้า About Game System ถูกตั้งค่าไว้)
        if (aboutGameSystem != null)
        {
            aboutGameSystem.SetActive(false);
        }
        
    }

    // ฟังก์ชันนี้จะถูกเรียกเมื่อกดปุ่ม Play
    public void Play()
    {
        // ปิด Panel 'HowToPlay' ถ้ามันถูกเปิดอยู่
        if (howToPlayPanel != null && howToPlayPanel.activeSelf)
        {
            howToPlayPanel.SetActive(false);  // ปิด Panel 'HowToPlay'
            Debug.Log("Closed HowToPlay Panel");
            
            if (aboutGameSystem.activeSelf) // ถ้า Panel ถูกเปิดอยู่
            {
                audioManager.StopBackgroundMusic(); // หยุดเสียงเกมโดยใช้ฟังก์ชันของ AudioManager
            }
        }

        // เปิด Panel 'Canvas'
        if (canvasPanel != null)
        {
            canvasPanel.SetActive(true);
            audioManager.PlaySFX(audioManager.gameStart); // เสียงSFX Get Hp
        }

        // เปิดระบบในเกมที่อยู่ใต้ 'About Game System'
        if (aboutGameSystem != null)
        {
            aboutGameSystem.SetActive(true);

            // ถ้า Panel 'About Game System' ถูกเปิด ให้เล่นเสียง BG
            if (aboutGameSystem.activeSelf)
            {
                audioManager.PlayBackgroundMusic(); // เล่นเสียง BG
                Debug.Log("Background music started");
            }
        }

        // ตั้งค่า Time.timeScale ให้เป็น 1 เพื่อให้เกมทำงานตามปกติ
        Time.timeScale = 1;
        
    }

    // ฟังก์ชันสำหรับการแสดง Panel 'HowToPlay'
    public void HowToPlay()
    {
        // ตรวจสอบว่า Panel 'menuPanel' ถูกเปิดอยู่ก่อนปิดมัน
        if (menuPanel != null && menuPanel.activeSelf)
        {
            menuPanel.SetActive(false);  // ปิด Panel 'CanvasMenu'
            Debug.Log("Closed CanvasMenu Panel");
        }

        // ตรวจสอบว่า Panel 'HowToPlay' ถูกปิดอยู่ก่อนเปิดมัน
        if (howToPlayPanel != null && !howToPlayPanel.activeSelf)
        {
            howToPlayPanel.SetActive(true);  // เปิด Panel "How To Play"
            Debug.Log("Opened HowToPlay Panel");
            
            if (aboutGameSystem.activeSelf) // ถ้า Panel ถูกเปิดอยู่
            {
                audioManager.StopBackgroundMusic(); // หยุดเสียงเกมโดยใช้ฟังก์ชันของ AudioManager
            }
        }
        
        if (aboutGameSystem != null && aboutGameSystem.activeSelf)
        {
            audioManager.StopBackgroundMusic(); // หยุดเสียง BG
            Debug.Log("Background music stopped");
        }
        
        // บังคับให้ Canvas อัปเดตการแสดงผล
        //Canvas.ForceUpdateCanvases();
    }

    public void NextCanvasMenu()
    {
        // ปิด Panel 'CanvasCutScene'
        if (canvasCutScene != null && canvasCutScene.activeSelf)
        {
            canvasCutScene.SetActive(false);
            Debug.Log("Closed CanvasCutScene Panel");
            
        }
        if (aboutGameSystem != null && aboutGameSystem.activeSelf)
        {
            audioManager.StopBackgroundMusic(); // หยุดเสียง BG
            Debug.Log("Background music stopped");
        }

        // เปิด Panel 'CanvasMenu'
        if (menuPanel != null)
        {
            menuPanel.SetActive(true);
            Debug.Log("Opened CanvasMenu Panel");
            
            if (aboutGameSystem.activeSelf) // ถ้า Panel ถูกเปิดอยู่
            {
                audioManager.StopBackgroundMusic(); // หยุดเสียงเกมโดยใช้ฟังก์ชันของ AudioManager
            }
        }
        
    }


    
    
    public void Quit()
    {
        Application.Quit();
        Debug.Log("quit");
    }
    
}