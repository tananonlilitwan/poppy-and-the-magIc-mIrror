using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ต้องการใช้งาน UI
using UnityEngine.SceneManagement; // สำหรับจัดการ Scene


public class menu : MonoBehaviour
{
    [SerializeField] GameObject menuCanvas; // ถึง Canvas เมนู
    [SerializeField] GameObject CanvasHPandManaPlayer; // Canvas HP and Mana Player

    // เพิ่ม GameObject หรือ Script ที่ต้องการเปิดใช้งาน
    [SerializeField] GameObject gameSystem; // Game system ที่ต้องการเปิดใช้งาน
    
    void Start()
    {
        // หยุดเกมเมื่อเริ่มต้น
        Time.timeScale = 0f; // หยุดเวลาเกม
        // ปิด Game system
        if (gameSystem != null)
        {
            gameSystem.SetActive(false); // ปิด Game system เริ่มต้น
        }
    }

    public void PlayGame()
    {
        // ปิด Canvas เมนู
        menuCanvas.SetActive(false);
        CanvasHPandManaPlayer.SetActive(true);
        
        // เริ่มเกม
        Time.timeScale = 1f; // เปิดเวลาเกม
        
        // เปิด Game system
        if (gameSystem != null)
        {
            gameSystem.SetActive(true); // เปิด Game system
        }
    }
    
    public void ResetGame()
    {
        // ตั้งค่า Time.timeScale เป็น 1 ก่อนโหลด Scene ใหม่
        Time.timeScale = 1f; // เปิดเวลาเกม
        // โหลด Scene ปัจจุบันใหม่เพื่อรีเซ็ตเกม
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void QuitGame()
    {
        // ออกจากเกม
#if UNITY_EDITOR
        // ถ้าใน Unity Editor
        UnityEditor.EditorApplication.isPlaying = false; // หยุดการเล่นใน Unity Editor
#else
            // ถ้าใน Build
            Application.Quit(); // ออกจากเกม
#endif
    }
}