using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class dialogWarden : MonoBehaviour
{
    [Header("---------- Dialog -------------")]
    public GameObject bgDialog; // อ้างอิง BG Dialog

    public TextMeshProUGUI textDialog1; // อ้างอิง Text Dialog 1
    public TextMeshProUGUI textDialog2; // อ้างอิง Text Dialog 2
    public TextMeshProUGUI textDialog3; // อ้างอิง Text Dialog 3
    public TextMeshProUGUI textDialog4; // อ้างอิง Text Dialog 4
    public TextMeshProUGUI textDialog5; // อ้างอิง Text Dialog 5
    public float dialogDisplayTime = 3f; // เวลาในการแสดง Dialog 2

    [SerializeField] private string dialog1Text; // ประโยค Dialog 1 ที่สามารถพิมพ์จาก Unity ได้
    [SerializeField] private string dialog2Text; // ประโยค Dialog 2 ที่สามารถพิมพ์จาก Unity ได้
    [SerializeField] private string dialog3Text; // ประโยค Dialog 3 ที่สามารถพิมพ์จาก Unity ได้
    [SerializeField] private string dialog4Text; // ประโยค Dialog 3 ที่สามารถพิมพ์จาก Unity ได้
    [SerializeField] private string dialog5Text; // ประโยค Dialog 3 ที่สามารถพิมพ์จาก Unity ได้

    private bool isPlayerNearby = false;
    private bool isDialogActive = false;
    private Collider2D playerCollider; // ใช้สำหรับเก็บ Collider ของผู้เล่น
    
    private bool isDialogShowing = false; // ตัวแปรใหม่เพื่อตรวจสอบสถานะการแสดงไดอะล็อก
    
    [SerializeField] GameObject Door;
    
    //private Rigidbody2D playerRigidbody;
    
    private AudioManager audioManager; // เสียงในเกม
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // เสียงในเกม
       
    }
    
    void Update()
    {
        // ให้ผู้เล่นกด E ครั้งเดียวแล้วแสดง Dialog ทั้งหมดโดยอัตโนมัติ
        if (isDialogActive && Input.GetKeyDown(KeyCode.E) && !isDialogShowing)
        {
            Debug.Log("Player pressed E to show dialogs");
            StartCoroutine(ShowAllDialogs()); // เริ่มแสดง Dialog ทั้งหมดโดยอัตโนมัติ
            
            audioManager.PlaySFX(audioManager.click);
        }
    }
    private void ShowDialog1()
    {
        
        bgDialog.SetActive(true); // เปิด BG ของ Dialog
        textDialog1.gameObject.SetActive(true); // แสดง Text Dialog 1

        // ตั้งค่าข้อความให้กับ TextMeshPro
        textDialog1.text = dialog1Text; // ใช้ข้อความจากตัวแปร dialog1Text
        isDialogActive = true; // กำหนดว่า Dialog กำลังแสดงอยู่

        Debug.Log("Dialog 1 is shown with text: " + dialog1Text);
    }
    private IEnumerator ShowAllDialogs()
    {
        isDialogShowing = true; // กำหนดสถานะว่าเริ่มแสดงไดอะล็อก
        // ปิดการควบคุมผู้เล่นระหว่างที่ Dialog แสดง
        ControlPlayer1 playerController = playerCollider.GetComponent<ControlPlayer1>();
       
        
        /*if (playerController != null)
        {
            //playerController.isPlayerControllable = false;
            playerRigidbody.bodyType = RigidbodyType2D.Kinematic; // เปลี่ยนเป็น Kinematic เพื่อปิดการควบคุม
            //playerRigidbody.gravityScale = 0; // ปิดแรงโน้มถ่วง
        }*/
        
        // แสดง Dialog 2
        textDialog1.gameObject.SetActive(false); 
        textDialog2.gameObject.SetActive(true); 
        textDialog2.text = dialog2Text;
        Debug.Log("Dialog 2 is shown with text: " + dialog2Text);
        yield return new WaitForSeconds(dialogDisplayTime); // รอ 3 วินาที
    
        // แสดง Dialog 3
        textDialog2.gameObject.SetActive(false); 
        textDialog3.gameObject.SetActive(true); 
        textDialog3.text = dialog3Text;
        Debug.Log("Dialog 3 is shown with text: " + dialog3Text);
        yield return new WaitForSeconds(dialogDisplayTime); // รอ 3 วินาที

        // แสดง Dialog 4
        textDialog3.gameObject.SetActive(false); 
        textDialog4.gameObject.SetActive(true); 
        textDialog4.text = dialog4Text;
        Debug.Log("Dialog 4 is shown with text: " + dialog4Text);
        yield return new WaitForSeconds(dialogDisplayTime); // รอ 3 วินาที

        // ปิด Dialog ทั้งหมด
        bgDialog.SetActive(false); 
        textDialog4.gameObject.SetActive(false); 
        isDialogActive = false;
        isDialogShowing = false; // เปลี่ยนสถานะกลับเป็นไม่แสดง
        Debug.Log("All dialogs are closed");
        Door.SetActive(false); 
        
        /*// เปิดการควบคุมผู้เล่นอีกครั้ง
        if (playerController != null)
        {
            //playerController.isPlayerControllable = true;
            playerRigidbody.bodyType = RigidbodyType2D.Dynamic; // กลับมาเป็น Dynamic เพื่อเปิดการควบคุมอีกครั้ง
            //playerRigidbody.gravityScale = 1; // คืนค่า Gravity Scale กลับมาเป็นปกติ
        }*/
        
    }
    private void ShowDialog5()
    {
        bgDialog.SetActive(true); // เปิด BG ของ Dialog
        textDialog5.gameObject.SetActive(true); // แสดง Text Dialog 5
        textDialog5.text = dialog5Text; // ตั้งค่าข้อความให้กับ Dialog 5
        Debug.Log("Dialog 5 is shown with text: " + dialog5Text);

        // รอ 3 วินาทีก่อนที่จะปิด Dialog 5
        StartCoroutine(CloseDialog5AfterTime());
    }
    
    private IEnumerator CloseDialog5AfterTime()
    {
        yield return new WaitForSeconds(3f); // รอ 3 วินาที
        bgDialog.SetActive(false); // ปิด BG ของ Dialog
        textDialog5.gameObject.SetActive(false); // ปิด Text Dialog 5
        Debug.Log("Dialog 5 is closed");
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            playerCollider = other; // เก็บค่า Collider ของผู้เล่น
            Debug.Log("Player is nearby"); // ตรวจสอบว่า Player เข้ามาใกล้
        }*/
        
        if (other.CompareTag("Player"))
        {
            if (other.CompareTag("Player"))
            {
                isPlayerNearby = true;
                playerCollider = other; // เก็บค่า Collider ของผู้เล่น

                // แสดง Dialog1 เมื่อเข้าใกล้ Player
                if (!isDialogActive && !isDialogShowing)
                {
                    ShowDialog1();
                }
            }
            
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false; // กำหนดว่า Player ไม่อยู่ใกล้แล้ว
            Debug.Log("Player is no longer nearby"); // ตรวจสอบว่า Player ออกไปแล้ว

            // ปิด Dialog ทั้งหมด ถ้ายังไม่แสดง
            if (!isDialogShowing)
            {
                CloseAllDialogs();
            }
        }
    }
    private void CloseAllDialogs()
    {
        // ปิด BG ของ Dialog และ Text ทั้งหมด
        bgDialog.SetActive(false);
        textDialog1.gameObject.SetActive(false);
        textDialog2.gameObject.SetActive(false);
        textDialog3.gameObject.SetActive(false);
        textDialog4.gameObject.SetActive(false);
        textDialog5.gameObject.SetActive(false);
    
        isDialogActive = false; // เปลี่ยนสถานะ Dialog เป็นไม่แสดง
        Debug.Log("All dialogs are closed because the player is no longer nearby");
    }
}

