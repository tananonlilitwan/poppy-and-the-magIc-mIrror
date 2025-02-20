using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CanvasPanel : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GameObject currentPausePanel; // Pause Panel อันเดิม
    //[SerializeField] private GameObject heavenPausePanel;  // Pause Panel อันใหม่ 
    
    [SerializeField] private GameObject menuPanel;         // Panel 'CanvasMenu'
    [SerializeField] private GameObject canvasPanel;       // Panel 'Canvas Ui in game'
    
    [SerializeField] private GameObject endCreditPanel;    // Panel 'EndCredit'
    
    [SerializeField] private GameObject winPanel;          // Panel 'Win' ที่จะเปิดเมื่อกด Next
    [SerializeField] private GameObject canvasPuzzlePanel;  // Panel 'Canvas puzzle' ที่จะปิดเมื่อกด Next
    
    [SerializeField] private GameObject aboutGameSystem;   // Panel 'About Game System'
    
    [Header("-------------------------Reset Game Objects in About Game System Panel---------------------------------------")]
    public GameObject aboutGameSystemPanel; // อ้างอิงถึง Panel ที่ต้องการรีเซ็ต

    [SerializeField] private Vector3[] initialPositions; // ตำแหน่งเริ่มต้นของวัตถุทั้งหมด
    [SerializeField] private Transform[] gameObjectsInPanel; // อ้างอิงถึงวัตถุทั้งหมดใน Panel
    
    [SerializeField] private Rigidbody2D playerRigidbody; // ถ้า Player ใช้ Rigidbody2D
    [SerializeField] private Transform playerTransform; // ถ้าต้องการอ้างอิง Transform ของ Player

    void Start()
    {
        // ดึงวัตถุทั้งหมดที่เป็นลูกของ aboutGameSystemPanel (ไม่รวม aboutGameSystemPanel เอง)
        gameObjectsInPanel = aboutGameSystemPanel.GetComponentsInChildren<Transform>(true); // (true) ให้ดึงวัตถุทั้งหมด รวมถึงที่ถูกปิดการทำงานอยู่
    
        // กรองเพื่อเอาวัตถุทั้งหมดใน panel ที่ไม่ใช่ตัว panel เอง
        gameObjectsInPanel = gameObjectsInPanel.Where(t => t.gameObject != aboutGameSystemPanel).ToArray();

        // เก็บตำแหน่งเริ่มต้นของวัตถุทั้งหมด
        initialPositions = new Vector3[gameObjectsInPanel.Length];
        for (int i = 0; i < gameObjectsInPanel.Length; i++)
        {
            initialPositions[i] = gameObjectsInPanel[i].position;
        }
    }

    public void ResetAboutGameSystem()
    {
        // รีเซ็ตตำแหน่งของ Player
        playerTransform.position = initialPositions[0]; // ตั้งตำแหน่งใหม่ให้ Player (ตัวอย่างเชื่อว่า Player อยู่ที่ index 0 ใน initialPositions)

        // รีเซ็ตค่า Rigidbody ถ้า Player มี Rigidbody
        if (playerRigidbody != null)
        {
            playerRigidbody.velocity = Vector2.zero; // ตั้งค่าความเร็วให้เป็น 0
            playerRigidbody.angularVelocity = 0f;   // ตั้งค่าการหมุนให้เป็น 0
        }

        // รีเซ็ตตำแหน่งของวัตถุอื่นๆ ใน Panel
        for (int i = 1; i < gameObjectsInPanel.Length; i++) // เริ่มที่ 1 เพราะ index 0 คือ Player
        {
            gameObjectsInPanel[i].position = initialPositions[i];
        }
    }

    
    
    
    private AudioManager audioManager; // เสียงในเกม
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // เสียงในเกม
       
    }
    
    //[SerializeField] private GameObject heavenPanel;

    /*void Update()
    {
        if (heavenPanel.activeSelf)
        {
            // รีเซ็ตเกมโดยการโหลด Scene ปัจจุบันอีกครั้ง
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }*/
    
    public void Pause()
    {
        // ปิด Panel 'About Game System' หรือหากคุณมี reference ของ Panel นี้ให้ปิดมัน
        /*GameObject aboutGamePanel = GameObject.Find("About Game System"); // เปลี่ยนชื่อให้ตรงกับ Panel ของคุณ
        if (aboutGamePanel != null)
        {
            aboutGamePanel.SetActive(false); // ปิด Panel About Game System
        }*/
        
        if (aboutGameSystem != null && aboutGameSystem.activeSelf)
        {
            audioManager.StopBackgroundMusic(); // หยุดเสียง BG
            Debug.Log("Background music stopped");
        }
        
        pauseMenu.SetActive(true);
        audioManager.PlaySFX(audioManager.End_Win_Player_Q_Enamy);
        Time.timeScale = 0;
    }
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Help()
    {
        SceneManager.LoadScene("UIhelp 0");
    }

    public void Quit()
    {
        //SceneManager.LoadScene("menu");
        
        /*// ปิด Panel 'Heaven'
        if (heavenPausePanel != null)
        {
            heavenPausePanel.SetActive(false);
        }*/

        /*// เปิด Panel 'menu'
        if (menuPanel != null)
        {
            menuPanel.SetActive(true);
        }
        */
        
        
        // ปิด Panel 'EndCredit' หากเปิดอยู่
        if (endCreditPanel != null)
        {
            endCreditPanel.SetActive(false);
        }

        // เปิด Panel 'CanvasMenu'
        if (menuPanel != null)
        {
            menuPanel.SetActive(true);
        }
        
        if (aboutGameSystem != null && aboutGameSystem.activeSelf)
        {
            audioManager.StopBackgroundMusic(); // หยุดเสียง BG
            Debug.Log("Background music stopped");
        }
        
        // รีเซ็ตเกม โดยโหลด Scene 'menu'
        SceneManager.LoadScene("MapScene");

        // ตั้งค่า Time.timeScale ให้เป็น 1 เพื่อให้เกมทำงานตามปกติ
        Time.timeScale = 1;
        
    }

    public void Back()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void EndCredit()
    {
        // ปิด Panel 'Heaven'
        /*if (heavenPausePanel != null)
        {
            heavenPausePanel.SetActive(false);
        }*/

        // เปิด Panel 'EndCredit'
        if (endCreditPanel != null)
        {
            endCreditPanel.SetActive(true);
        }
        
        // หยุดเวลาเพื่อป้องกันการกระทำอื่นในเกม
        Time.timeScale = 0;
    }

    
    public void Next1()
    {
        SceneManager.LoadSceneAsync(0);
    }
    
    
    public void Next3()
    {
        SceneManager.LoadSceneAsync(1);
        Time.timeScale = 1;
    }
    
    
    // ฟังก์ชันนี้จะถูกเรียกเมื่อปุ่มถูกกด
    public void OnNextButtonClick()
    {
        // ปิด Pause Panel อันเดิม
        if (currentPausePanel != null)
        {
            currentPausePanel.SetActive(false);
        }
        
        // ปิด Panel 'Canvas puzzle'
        if (canvasPuzzlePanel != null)
        {
            canvasPuzzlePanel.SetActive(false);
        }
        // เปิด Panel 'Win'
        if (winPanel != null) // ตรวจสอบว่า Panel 'Win' มีการเชื่อมโยงอยู่
        {
            winPanel.SetActive(true);
        }
        // ปิด Panel 'About Game System' หรือหากคุณมี reference ของ Panel นี้ให้ปิดมัน
        GameObject aboutGamePanel = GameObject.Find("About Game System"); // เปลี่ยนชื่อให้ตรงกับ Panel ของคุณ
        if (aboutGamePanel != null)
        {
            aboutGamePanel.SetActive(false); // ปิด Panel About Game System
        }

        /*// เปิด Pause Panel อันใหม่ (Heaven)
        if (heavenPausePanel != null)
        {
            heavenPausePanel.SetActive(true);
            //audioManager.PlaySFX(audioManager.End_Win_Player_Q_Enamy); // เสียง SFX Win
        }*/

        // หยุดเวลา
        Time.timeScale = 0;
    }
}