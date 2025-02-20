using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ต้องใช้เพื่อเข้าถึง UI
using TMPro;

public class ItemNoteStory : MonoBehaviour
{

    public GameObject bgDialog;
    public TextMeshProUGUI textDialog;
    [SerializeField] private string dialogText;
    
    public Button closeButton; // ปุ่มสำหรับปิด SPR
    private bool isNearItem = false; // ตรวจสอบว่าติดกับ Item หรือไม่
    public Image imageDisplay; // Image component สำหรับแสดงภาพ

    private AudioManager audioManager; // เสียงในเกม
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // เสียงในเกม
       
    }
    void Start()
    {
        // ซ่อน Image เริ่มต้น
        imageDisplay.gameObject.SetActive(false); 
        
        bgDialog.SetActive(false); // ซ่อน Dialog เริ่มต้น

        // ตั้งค่าให้ปุ่มปิดเรียกใช้ฟังก์ชัน CloseSprite เมื่อถูกคลิก
        closeButton.onClick.AddListener(CloseSprite);
    }

    void Update()
    {
        // ตรวจสอบว่าผู้เล่นกดปุ่ม T และอยู่ใกล้ Item หรือไม่
        if (isNearItem && Input.GetKeyDown(KeyCode.T))
        {
            ToggleImage();
            CloseDialog();
        }
    }
    
    void ToggleImage()
    {
        // เปลี่ยนสถานะการแสดงของ Image
        imageDisplay.gameObject.SetActive(!imageDisplay.gameObject.activeSelf);
        audioManager.PlaySFX(audioManager.paper);

    }

    void CloseSprite()
    {
        // ปิด Image
        imageDisplay.gameObject.SetActive(false);
    }

    void ShowDialog()
    {
        bgDialog.SetActive(true); // แสดง Dialog
        textDialog.text = dialogText; // ตั้งค่าข้อความ
        Debug.Log("Dialog shown with text: " + dialogText); // ตรวจสอบข้อความ
    }

    void CloseDialog()
    {
        bgDialog.SetActive(false); // ปิด Dialog
        audioManager.PlaySFX(audioManager.paper);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ตรวจสอบว่าชนกับ obj Item หรือไม่
        if (collision.CompareTag("Player")) // ตรวจสอบ Tag ของ obj Item
        {
            isNearItem = true; // ตั้งค่าว่าติดกับ Item
            ShowDialog(); // แสดง Dialog เมื่อชนกับ Player
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // ตรวจสอบว่าออกจากการชนกับ obj Item หรือไม่
        if (collision.CompareTag("Player"))
        {
            isNearItem = false; // ตั้งค่าว่าไม่ติดกับ Item
            CloseSprite(); // ปิด Image ถ้าออกจาก Item
            CloseDialog(); // ปิด Dialog เมื่อ Player ออกจากวัตถุ
        }
        
        /*// ตรวจสอบว่าออกจากการชนกับ obj Item หรือไม่
        if (collision.CompareTag("Player"))
        {
            isNearItem = false; // ตั้งค่าว่าไม่ติดกับ Item
            CloseSprite(); // ปิด Image ถ้าออกจาก Item
        
            // ปิด Dialog แต่ถ้า Dialog เปิดอยู่เท่านั้น
            if (bgDialog.activeSelf) 
            {
                CloseDialog(); // ปิด Dialog เมื่อ Player ออกจากวัตถุ
            }
        }*/
    }
    
}