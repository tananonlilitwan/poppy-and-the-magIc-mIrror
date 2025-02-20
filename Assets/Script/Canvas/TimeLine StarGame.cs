using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeLineStarGame : MonoBehaviour
{
    /*[SerializeField] TextMeshProUGUI timerText;
    
    //[SerializeField] TextMeshProUGUI TimerText;
    float elapsedTime;
    
    [SerializeField] float remainingTime;
    
    [SerializeField] private GameObject pausePanel;
    
    private AudioManager audioManager; // เสียงในเกม
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // เสียงในเกม
       
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        #region <นับเวลาเดินหน้า>
        /*
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); #1#
       
        
        #endregion
        
        #region <จำเวลานับถอยหลัง>

        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            // gameOver
            PauseGame();
            audioManager.PlaySFX(audioManager.End_Win_Player_Q_Enamy); // เสียง SFX Win
            timerText.color = Color.red;
            
        }
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        
        #endregion
        
        
    }
    private void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        
        if (pausePanel.activeSelf) // ถ้า Panel ถูกเปิดอยู่
        {
            audioManager.StopBackgroundMusic(); // หยุดเสียงเกมโดยใช้ฟังก์ชันของ AudioManager
        }
    }*/
    
    
    ///////////////////////////////
    
    [SerializeField] TextMeshProUGUI timerText;

    //[SerializeField] TextMeshProUGUI TimerText;
    float elapsedTime;

    [SerializeField] public float remainingTime; // เปลี่ยนให้เป็น public เพื่อให้เข้าถึงได้

    [SerializeField] private GameObject pausePanel;
    
    private bool isTimerActive = false; // ตัวแปรสถานะเพื่อตรวจสอบการเริ่มนับเวลา

    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // เสียงในเกม
    }

    void Start()
    {
        // ตั้งค่าเริ่มต้น
        //remainingTime = 60f; // เวลานับถอยหลังเริ่มต้น
    }

    void Update()
    {
        if (isTimerActive)
        {
            #region <จำเวลานับถอยหลัง>
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
            }
            else if (remainingTime <= 0)
            {
                remainingTime = 0;
                PauseGame();
                audioManager.PlaySFX(audioManager.End_Win_Player_Q_Enamy); // เสียง SFX Win
                timerText.color = Color.red;
                isTimerActive = false; // หยุดนับเมื่อหมดเวลา
            }
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            #endregion
        }
    }

    private void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;

        if (pausePanel.activeSelf) // ถ้า Panel ถูกเปิดอยู่
        {
            audioManager.StopBackgroundMusic(); // หยุดเสียงเกม
        }
    }
    
    // เรียกฟังก์ชันนี้เมื่อ Switch ถูกกด
    public void StartTimer()
    {
        isTimerActive = true; // เริ่มนับเวลาเมื่อ switch ถูกกด
    }

    
}