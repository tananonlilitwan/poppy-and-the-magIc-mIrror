using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class dialogPoseidon : MonoBehaviour
{
    public GameObject arrowObject;
    [Header("---------- Dialog -------------")]
    public Image cutsceneImage1; // รูปที่จะแสดงใน Cutscene
    public Image cutsceneImage2; // รูปที่จะแสดงใน Cutscene
    public GameObject Poseidon;
    
    public GameObject bgDialog; // อ้างอิง BG Dialog

    public TextMeshProUGUI textDialog1; // อ้างอิง Text Dialog 1
    public TextMeshProUGUI textDialog2; // อ้างอิง Text Dialog 2
    public TextMeshProUGUI textDialog3; // อ้างอิง Text Dialog 3
    public TextMeshProUGUI textDialog4; // อ้างอิง Text Dialog 4
    public TextMeshProUGUI textDialog5; // อ้างอิง Text Dialog 5
    public float dialogDisplayTime = 3f; // เวลาในการแสดง Dialog 2
    public float fadeDuration = 1f; // ระยะเวลาในการค่อยๆ แสดงผล
    [SerializeField] private string dialog1Text; // ประโยค Dialog 1 ที่สามารถพิมพ์จาก Unity ได้
    [SerializeField] private string dialog2Text; // ประโยค Dialog 2 ที่สามารถพิมพ์จาก Unity ได้
    [SerializeField] private string dialog3Text; // ประโยค Dialog 3 ที่สามารถพิมพ์จาก Unity ได้
    [SerializeField] private string dialog4Text; // ประโยค Dialog 3 ที่สามารถพิมพ์จาก Unity ได้
    [SerializeField] private string dialog5Text; // ประโยค Dialog 3 ที่สามารถพิมพ์จาก Unity ได้

    private bool isPlayerNearby = false;
    private bool isDialogActive = false;
    private Collider2D playerCollider; // ใช้สำหรับเก็บ Collider ของผู้เล่น
    
    private bool isDialogShowing = false; // ตัวแปรใหม่เพื่อตรวจสอบสถานะการแสดงไดอะล็อก

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
       
        
        yield return StartCoroutine(FadeIn(cutsceneImage1)); // เฟดขึ้นรูปภาพที่ 1

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
        yield return StartCoroutine(FadeOut(cutsceneImage1)); // เฟดขึ้นรูปภาพที่ 1
        yield return StartCoroutine(FadeIn(cutsceneImage2)); // เฟดขึ้นรูปภาพที่ 1
        // แสดง Dialog 4
        textDialog3.gameObject.SetActive(false); 
        textDialog4.gameObject.SetActive(true); 
        textDialog4.text = dialog4Text;
        Debug.Log("Dialog 4 is shown with text: " + dialog4Text);
        yield return new WaitForSeconds(dialogDisplayTime); // รอ 3 วินาที

        // แสดง 5
        textDialog4.gameObject.SetActive(false); 
        textDialog5.gameObject.SetActive(true);
        textDialog5.text = dialog5Text;
        yield return StartCoroutine(FadeOut(cutsceneImage2)); 
        
        // ปิด Dialog ทั้งหมด
        bgDialog.SetActive(false); 
        textDialog5.gameObject.SetActive(false); 
        isDialogActive = false;
        isDialogShowing = false; // เปลี่ยนสถานะกลับเป็นไม่แสดง
        Debug.Log("All dialogs are closed");
         
        // ลบ GameObject ที่มีสคริปต์นี้ออก
        Destroy(gameObject); // ลบ GameObject ที่มีสคริปต์ dialogPoseidon
        Debug.Log("dialogPoseidon GameObject destroyed"); // เพิ่มการตรวจสอบการทำลาย
        Poseidon.SetActive(false);
        arrowObject.SetActive(true);
        
    }
     /*private IEnumerator CloseDialog5AfterTime()
    {
        yield return new WaitForSeconds(3f); // รอ 3 วินาที
        bgDialog.SetActive(false); // ปิด BG ของ Dialog
        textDialog5.gameObject.SetActive(false); // ปิด Text Dialog 5
        Debug.Log("Dialog 5 is closed");
        
        // คืนค่าการใช้มานา
        PlayerShooting playerShooting = playerCollider.GetComponent<PlayerShooting>();
        if (playerShooting != null)
        {
            playerShooting.ResetSkill2Usage(); // รีเซ็ตการใช้สกิล 2 เป็นปกติ
        }
    }*/
    
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
        
        /*// ลบ GameObject ที่มีสคริปต์นี้ออก
        Destroy(gameObject); // ลบ GameObject ที่มีสคริปต์ dialogPoseidon
        Debug.Log("dialogPoseidon GameObject destroyed"); // เพิ่มการตรวจสอบการทำลาย
        Poseidon.SetActive(false);*/
    }
    

    private IEnumerator FadeIn(Graphic graphic)
    {
        graphic.gameObject.SetActive(true);
        float elapsedTime = 0f;
        CanvasGroup canvasGroup = graphic.GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = graphic.gameObject.AddComponent<CanvasGroup>();

        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOut(Graphic graphic)
    {
        float elapsedTime = 0f;
        CanvasGroup canvasGroup = graphic.GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = graphic.gameObject.AddComponent<CanvasGroup>();

        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;
        graphic.gameObject.SetActive(false);
    }
}
