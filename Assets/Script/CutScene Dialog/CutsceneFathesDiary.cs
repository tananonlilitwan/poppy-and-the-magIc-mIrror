using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutsceneFathesDiary : MonoBehaviour
{
    public Image cutsceneImage; // รูปที่จะแสดงใน Cutscene
    public Image dialogBG; // พื้นหลังไดอะล็อก
    public TMP_Text dialogText1; // ข้อความไดอะล็อก 1 (TextMeshPro)
    public TMP_Text dialogText2; // ข้อความไดอะล็อก 2 (TextMeshPro)
    public Image noteImage; // รูปภาพ Note
    public Button closeButton; // ปุ่มสำหรับปิด Cutscene ทั้งหมด
    public float fadeDuration = 1f; // ระยะเวลาในการค่อยๆ แสดงผล
    public float dialogDisplayTime = 3f; // เวลาการแสดงข้อความแต่ละข้อความ

    private bool isCutsceneActive = false;
    public ControlPlayer playerController; // อ้างอิงไปยัง PlayerController

    void Start()
    {
        /*// เริ่มต้นซ่อน Image, Text และ Button ทั้งหมด
        cutsceneImage.color = new Color(1, 1, 1, 0); // Set alpha to 0 (invisible)
        dialogBG.gameObject.SetActive(false);
        dialogText1.color = new Color(1, 1, 1, 0);
        dialogText2.color = new Color(1, 1, 1, 0);
        noteImage.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);

        // ตั้งค่าให้ปุ่มปิดเรียกใช้ฟังก์ชัน CloseCutscene เมื่อถูกคลิก
        closeButton.onClick.AddListener(CloseCutscene);*/
        
        // ซ่อน UI ทั้งหมดในตอนเริ่มต้น
        SetUIVisibility(false);
        closeButton.onClick.AddListener(CloseCutscene);
        
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        // ตรวจสอบว่าชนกับผู้เล่น
        if (collision.CompareTag("Player") && !isCutsceneActive)
        {
            isCutsceneActive = true;
            StartCoroutine(PlayCutscene());
        }
    }

    private IEnumerator PlayCutscene()
    {
        // ค่อยๆ แสดงภาพ Cutscene
        yield return StartCoroutine(FadeInImage(cutsceneImage));

        // แสดงพื้นหลังไดอะล็อกและข้อความแรก
        dialogBG.gameObject.SetActive(true);
        yield return StartCoroutine(FadeInText(dialogText1));
        yield return new WaitForSeconds(dialogDisplayTime); // แสดงข้อความแรก 3 วินาที

        // ปิดข้อความแรกและแสดงข้อความที่สอง
        yield return StartCoroutine(FadeOutText(dialogText1));
        yield return StartCoroutine(FadeInText(dialogText2));
        yield return new WaitForSeconds(dialogDisplayTime); // แสดงข้อความที่สอง 3 วินาที

        // ปิดข้อความที่สองและพื้นหลังไดอะล็อก
        yield return StartCoroutine(FadeOutText(dialogText2));
        dialogBG.gameObject.SetActive(false);

        // แสดงภาพ Note และปุ่มปิด Cutscene
        noteImage.gameObject.SetActive(true);
        closeButton.gameObject.SetActive(true);
    }

    private IEnumerator FadeInImage(Image image)
    {
        float elapsedTime = 0;
        Color color = image.color;

        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            image.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 1;
        image.color = color;
    }

    private IEnumerator FadeInText(TMP_Text text)
    {
        float elapsedTime = 0;
        Color color = text.color;

        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            text.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 1;
        text.color = color;
    }

    private IEnumerator FadeOutText(TMP_Text text)
    {
        float elapsedTime = 0;
        Color color = text.color;

        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            text.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 0;
        text.color = color;
    }

    private void CloseCutscene()
    {
        // ปิด Cutscene ทั้งหมด
        cutsceneImage.color = new Color(1, 1, 1, 0);
        dialogText1.color = new Color(1, 1, 1, 0);
        dialogText2.color = new Color(1, 1, 1, 0);
        dialogBG.gameObject.SetActive(false);
        noteImage.gameObject.SetActive(false);
        closeButton.gameObject.SetActive(false);
        isCutsceneActive = false;
    }*/
    
    private AudioManager audioManager; // เสียงในเกม
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // เสียงในเกม
       
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // เริ่ม Cutscene เมื่อ Player ชนกับวัตถุที่กำหนด
        if (collision.CompareTag("Player") && !isCutsceneActive)
        {
            isCutsceneActive = true;
            playerController.enabled = false; // ปิดการควบคุม Player
            StartCoroutine(PlayCutscene());
        }
    }

    private IEnumerator PlayCutscene()
    {
        // ค่อยๆ แสดงภาพ Cutscene
        yield return FadeIn(cutsceneImage);

        // แสดงพื้นหลังไดอะล็อกและข้อความแรก
        dialogBG.gameObject.SetActive(true);
        yield return FadeIn(dialogText1);
        yield return new WaitForSeconds(dialogDisplayTime); // รอให้ข้อความแรกแสดงครบเวลา

        // แสดงข้อความที่สองแทนข้อความแรก
        yield return FadeOut(dialogText1);
        yield return FadeIn(dialogText2);
        yield return new WaitForSeconds(dialogDisplayTime); // รอให้ข้อความที่สองแสดงครบเวลา

        // ซ่อนพื้นหลังไดอะล็อกและแสดง Note กับปุ่มปิด Cutscene
        yield return FadeOut(dialogText2);
        dialogBG.gameObject.SetActive(false);
        noteImage.gameObject.SetActive(true);
        
        // เล่นเสียง paper SFX เมื่อแสดง noteImage
        audioManager.PlaySFX(audioManager.paper);
        
        closeButton.gameObject.SetActive(true);
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

    private void CloseCutscene()
    {
        // ปิด UI ทั้งหมดและรีเซ็ตสถานะ
        SetUIVisibility(false);
        playerController.enabled = true; // เปิดการควบคุม Player
        isCutsceneActive = false;
    }

    private void SetUIVisibility(bool isVisible)
    {
        cutsceneImage.gameObject.SetActive(isVisible);
        dialogBG.gameObject.SetActive(isVisible);
        dialogText1.gameObject.SetActive(isVisible);
        dialogText2.gameObject.SetActive(isVisible);
        noteImage.gameObject.SetActive(isVisible);
        closeButton.gameObject.SetActive(isVisible);
    }
}
