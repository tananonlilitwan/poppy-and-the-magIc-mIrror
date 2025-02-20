using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables; // ต้องนำเข้า Playable namespace เพื่อใช้ Timeline
using UnityEngine.UI;

public class CutSceneController : MonoBehaviour
{
    public PlayableDirector playableDirector; // อ้างอิงถึง PlayableDirector สำหรับ Timeline
    public RectTransform image2RectTransform; // อ้างอิง RectTransform ของ Image2
    public Button hiddenButton; // อ้างอิง Button ที่ซ่อนอยู่

    // dialog 
    public RectTransform bgDialog; // อ้างอิง Background ของ Dialog
    public RectTransform dialog1; // อ้างอิง Dialog 1
    public RectTransform dialog2; // อ้างอิง Dialog 2
    public RectTransform dialog3; // อ้างอิง Dialog 3
    [SerializeField] private float dialogDuration = 4f; // ระยะเวลาที่จะแสดง Dialog แต่ละตัว

    void OnEnable()
    {
        Debug.Log("OnEnable called, initializing cutscene");

        // ปิด Image2 เริ่มต้น
        image2RectTransform.gameObject.SetActive(false);
        hiddenButton.gameObject.SetActive(false); // ปิดปุ่มที่ซ่อนอยู่เริ่มต้น

        bgDialog.gameObject.SetActive(false); // ปิด BG dialog
        dialog1.gameObject.SetActive(false); // ปิด dialog1
        dialog2.gameObject.SetActive(false); // ปิด dialog2
        dialog3.gameObject.SetActive(false); // ปิด dialog3

        // ตรวจสอบว่า PlayableDirector มี Timeline ที่จะเล่น
        if (playableDirector.playableAsset != null)
        {
            playableDirector.Play();
            Debug.Log("PlayableDirector started playing the Timeline");
        }
        else
        {
            Debug.LogError("PlayableDirector does not have a playableAsset assigned.");
        }
        
        if (playableDirector.playableAsset != null)
        {
            playableDirector.Play();
            Debug.Log("PlayableDirector started playing the Timeline");
        }
        else
        {
            Debug.LogError("PlayableDirector does not have a playableAsset assigned.");
        }

        // เพิ่ม Debug.Log ใน Cutscene Clip
        foreach (var track in playableDirector.playableAsset.outputs)
        {
            Debug.Log($"Track: {track.sourceObject}");
        }
        
    
        // เรียก Coroutine เพื่อรอเวลา 2 วินาทีก่อนเริ่มการแสดง Dialog
        StartCoroutine(StartCutSceneAfterDelay(2f));
        
        
    }


    private IEnumerator StartCutSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // รอเวลาที่กำหนด
        StartCoroutine(ShowDialogsSequentially()); // เรียก Coroutine เพื่อแสดง Dialog
    }

    // Coroutine เพื่อแสดง Dialog แต่ละอันตามลำดับ
    private IEnumerator ShowDialogsSequentially()
    {
        Debug.Log("Showing dialogs");
        // รอเวลา 5 วินาทีก่อนเริ่มแสดง Dialog
        yield return new WaitForSeconds(3f); // รอ 3 วินาที

        // แสดง BG dialog และ dialog 1
        bgDialog.gameObject.SetActive(true);
        dialog1.gameObject.SetActive(true);
        Debug.Log("Showing dialog1");
        yield return new WaitForSeconds(dialogDuration); // รอเวลา dialogDuration วินาที

        // ปิด dialog 1 และแสดง dialog 2
        dialog1.gameObject.SetActive(false);
        dialog2.gameObject.SetActive(true);
        Debug.Log("Showing dialog2");
        yield return new WaitForSeconds(dialogDuration); // รอเวลา dialogDuration วินาที

        // ปิด dialog 2 และแสดง dialog 3
        dialog2.gameObject.SetActive(false);
        dialog3.gameObject.SetActive(true);
        Debug.Log("Showing dialog3");
        yield return new WaitForSeconds(dialogDuration); // รอเวลา dialogDuration วินาที

        // ปิด dialog 3
        bgDialog.gameObject.SetActive(false);
        dialog3.gameObject.SetActive(false);
        Debug.Log("Dialogs finished");

        // เปิด image2RectTransform หลังจาก Dialog ทั้งหมดแสดงเสร็จ
        image2RectTransform.gameObject.SetActive(true); // เปิด Image2
        Debug.Log("Showing image2RectTransform");

        // รอเวลา 3 วินาทีก่อนแสดงปุ่ม
        yield return new WaitForSeconds(3f);
        hiddenButton.gameObject.SetActive(true); // แสดงปุ่มที่ซ่อนอยู่
        Debug.Log("Showing hidden button");
    }

}
