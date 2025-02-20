using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ต้องนำเข้า UI namespace เพื่อใช้ Button

public class CutScene : MonoBehaviour
{
    public RectTransform imageRectTransform; // อ้างอิง RectTransform ของรูปภาพ
    public Vector3 startPosition; // ตำแหน่งเริ่มต้นนอกฉาก
    public Vector3 targetPosition; // ตำแหน่งเป้าหมายที่รูปภาพจะเลื่อนเข้ามาในฉาก
    public RectTransform image2RectTransform; // อ้างอิง RectTransform ของ Image2
    public Button hiddenButton; // อ้างอิง Button ที่ซ่อนอยู่
    [SerializeField] private float baseSmoothTime; // เวลาความสมูทพื้นฐาน
    [SerializeField] private float delayBeforeShowingImage2 = 3f; // เวลาที่รอเพื่อแสดง Image2
    [SerializeField] private float delayBeforeShowingButton = 10f; // เวลาที่รอเพื่อแสดงปุ่มหลังจาก Image2 แสดง

    // dialog 
    public RectTransform bgDialog; // อ้างอิง Background ของ Dialog
    public RectTransform dialog1; // อ้างอิง Dialog 1
    public RectTransform dialog2; // อ้างอิง Dialog 2
    public RectTransform dialog3; // อ้างอิง Dialog 3
    
    [Header("----------------Dialog---------------------------")]    
    [SerializeField] private float dialog1Duration = 6f; // ระยะเวลาที่จะแสดง Dialog 1
    [SerializeField] private float dialog2Duration = 2f; // ระยะเวลาที่จะแสดง Dialog 2
    [SerializeField] private float dialog3Duration = 5f; // ระยะเวลาที่จะแสดง Dialog 3
    
    //

    private Vector3 velocity = Vector3.zero; // ตัวแปรเก็บค่า velocity สำหรับการคำนวณ SmoothDamp
    private bool isMoving = false; // เช็คว่ารูปภาพกำลังเลื่อนอยู่หรือไม่
    
    
    private bool isDialogPlaying = false; // ตัวแปรเก็บสถานะการแสดง Dialog
    
    
    private AudioManager audioManager; // เสียงในเกม
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // เสียงในเกม
        audioManager.PlaySFX(audioManager.CutScene); // เสียงSFX Get Hp
    }

    // เพิ่มฟังก์ชันเพื่อเริ่ม CutScene โดยไม่ต้องรอ
    public void StartCutSceneImmediately()
    {
        StartCoroutine(StartCutSceneAfterDelay(0f)); // เริ่ม cut scene ทันที
    }
    
    void OnEnable()
    {
        //Debug.Log("OnEnable called, initializing cutscene");
        // ตั้งค่าตำแหน่งเริ่มต้นของรูปภาพให้อยู่ที่ตำแหน่ง startPosition
        imageRectTransform.localPosition = startPosition;
        image2RectTransform.localPosition = startPosition; // ตั้งค่า Image2 ให้อยู่ที่ตำแหน่งเริ่มต้นเดียวกัน
        image2RectTransform.gameObject.SetActive(false); // ปิด Image2 เริ่มต้น
        hiddenButton.gameObject.SetActive(false); // ปิดปุ่มที่ซ่อนอยู่เริ่มต้น

        bgDialog.gameObject.SetActive(false); // ปิด BG dialog
        dialog1.gameObject.SetActive(false); // ปิด dialog1
        dialog2.gameObject.SetActive(false); // ปิด dialog2
        dialog3.gameObject.SetActive(false); // ปิด dialog3
        
        //Debug.Log("OnEnable called, initializing cutscene");
        // เรียก Coroutine เพื่อรอเวลา 2 วินาทีก่อนเริ่มการเลื่อนภาพและการแสดง Dialog
        StartCoroutine(StartCutSceneAfterDelay(2f));
        // เริ่ม Cut Scene ทันที
        //StartCutSceneImmediately(); // เรียกใช้ที่นี่
        
    }

    private IEnumerator StartCutSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // รอเวลา 2 วินาที
        StartSlideIn(); // เริ่มการเลื่อนภาพเมื่อเริ่มเกม
        StartCoroutine(ShowDialogsSequentially()); // เรียก Coroutine เพื่อแสดง Dialog ตั้งแต่เริ่มเกม
    }

    /*void Update()
    {
        // ถ้ากำลังเลื่อนรูปภาพอยู่
        if (isMoving)
        {
            Debug.Log("Image is moving. Current position: " + imageRectTransform.localPosition);
            // เลื่อนรูปภาพไปยังตำแหน่งเป้าหมายแบบ Smooth โดยใช้ SmoothDamp
            imageRectTransform.localPosition = Vector3.SmoothDamp(
                imageRectTransform.localPosition, 
                targetPosition, 
                ref velocity, 
                baseSmoothTime // ใช้ baseSmoothTime สำหรับการคำนวณสมูท
            );
            Debug.Log("New position after SmoothDamp: " + imageRectTransform.localPosition);

            // ตรวจสอบว่ารูปภาพถึงตำแหน่งเป้าหมายแล้วหรือยัง
            if (Vector3.Distance(imageRectTransform.localPosition, targetPosition) < 0.1f)
            {
                Debug.Log("Image reached the target position");
                isMoving = false; // หยุดการเคลื่อนที่เมื่อถึงตำแหน่งสุดท้าย
                StartCoroutine(ShowImage2AfterDelay(delayBeforeShowingImage2)); // เรียกใช้ Coroutine เพื่อแสดง Image2 หลังจากเวลาที่กำหนดใน Inspector
            }
        }
    }*/
    
    void Update()
    {
        // ถ้ากำลังเลื่อนรูปภาพอยู่
        if (isMoving)
        {
            //Debug.Log("Image is moving. Current position: " + imageRectTransform.localPosition);
            // เลื่อนรูปภาพไปยังตำแหน่งเป้าหมายแบบธรรมดา โดยใช้ MoveTowards
            imageRectTransform.localPosition = Vector3.MoveTowards(
                imageRectTransform.localPosition, 
                targetPosition, 
                Time.deltaTime * 200f // ปรับความเร็วการเลื่อนตาม Time.deltaTime
            );
            //Debug.Log("New position after MoveTowards: " + imageRectTransform.localPosition);

            // ตรวจสอบว่ารูปภาพถึงตำแหน่งเป้าหมายแล้วหรือยัง
            if (Vector3.Distance(imageRectTransform.localPosition, targetPosition) < 0.01f)
            {
                //Debug.Log("Image reached the target position");
                isMoving = false; // หยุดการเคลื่อนที่เมื่อถึงตำแหน่งสุดท้าย
                imageRectTransform.localPosition = targetPosition; // บังคับให้ตำแหน่งเป็น targetPosition ทันที
                StartCoroutine(ShowImage2AfterDelay(delayBeforeShowingImage2));
            }
        }
    }


    // ฟังก์ชันสำหรับเริ่มต้นการเลื่อนภาพ
    public void StartSlideIn()
    {
        //Debug.Log("StartSlideIn called, moving image");
        isMoving = true;
    }

    // Coroutine เพื่อแสดง Dialog แต่ละอันตามลำดับ
    private IEnumerator ShowDialogsSequentially()
    {
        if (isDialogPlaying) yield break; // ถ้ามี Dialog กำลังเล่น ให้หยุดฟังก์ชันทันที
        isDialogPlaying = true; // ตั้งค่าว่า Dialog กำลังเล่นอยู่
        
        //Debug.Log("Showing dialogs");
        // รอเวลา 5 วินาทีก่อนเริ่มแสดง Dialog
        yield return new WaitForSeconds(3f); // รอ 3 วินาที
        
        // แสดง BG dialog และ dialog 1
        bgDialog.gameObject.SetActive(true);
        dialog1.gameObject.SetActive(true);
        //Debug.Log("Showing dialog1");
        audioManager.PlaySFX(audioManager.dialog11); // เล่นเสียง dialog1
        yield return new WaitForSeconds(dialog1Duration); // รอเวลาของ dialog1

        // ปิด dialog 1 และแสดง dialog 2
        dialog1.gameObject.SetActive(false);
        dialog2.gameObject.SetActive(true);
        //Debug.Log("Showing dialog2");
        audioManager.PlaySFX(audioManager.dialog22); // เล่นเสียง dialog2
        yield return new WaitForSeconds(dialog2Duration); // รอเวลาของ dialog2

        // ปิด dialog 2 และแสดง dialog 3
        dialog2.gameObject.SetActive(false);
        dialog3.gameObject.SetActive(true);
        //Debug.Log("Showing dialog3");
        audioManager.PlaySFX(audioManager.dialog33); // เล่นเสียง dialog3
        yield return new WaitForSeconds(dialog3Duration); // รอเวลาของ dialog3

        // ปิด dialog 3
        bgDialog.gameObject.SetActive(false);
        dialog3.gameObject.SetActive(false);
       // Debug.Log("Dialogs finished");
        
        isDialogPlaying = false; // ตั้งค่า Dialog เล่นเสร็จแล้ว
    }

    // Coroutine เพื่อแสดง Image2 หลังจากที่เลื่อน Image1 เสร็จ
    private IEnumerator ShowImage2AfterDelay(float delay)
    {
        //Debug.Log("Waiting to show Image2");
        yield return new WaitForSeconds(delay);
        //Debug.Log("Showing Image2");
        //yield return new WaitForSeconds(delay); // รอเป็นเวลา delay
        imageRectTransform.gameObject.SetActive(false); // ปิด Image1
        image2RectTransform.gameObject.SetActive(true); // เปิด Image2
        image2RectTransform.localPosition = startPosition; // ตั้งค่าตำแหน่งเริ่มต้นของ Image2
        StartSlideInForImage2(); // เริ่มการเลื่อน Image2

        // รอเป็นเวลา delayBeforeShowingButton แล้วแสดงปุ่ม
        yield return new WaitForSeconds(delayBeforeShowingButton);
        hiddenButton.gameObject.SetActive(true); // แสดงปุ่มที่ซ่อนอยู่
    }

    // ฟังก์ชันเริ่มต้นการเลื่อน Image2
    private void StartSlideInForImage2()
    {
        // สามารถเพิ่มโค้ดในการเลื่อน Image2 เข้าไปที่นี่ ถ้าต้องการให้มันเลื่อนเข้ามาเหมือน Image1
        image2RectTransform.localPosition = targetPosition; // ตั้งค่าตำแหน่งเป้าหมายของ Image2
    }
    


}
