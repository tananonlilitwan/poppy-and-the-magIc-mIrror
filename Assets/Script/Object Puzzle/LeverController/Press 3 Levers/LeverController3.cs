using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController3 : MonoBehaviour
{
    [SerializeField] private Press3Levers press3Levers; // อ้างอิงถึง Press3Levers

    private bool isPlayerNear = false; // ตรวจสอบว่าผู้เล่นอยู่ใกล้หรือไม่
    private bool isLeverPulled = false; // ตรวจสอบว่าคันโยกถูกโยกแล้วหรือไม่
    private bool hasRotated = false; // ตรวจสอบว่าคันโยกหมุนไปแล้วหรือยัง

    private float rotationAmount = -45f; // จำนวนองศาที่จะหมุนคันโยก

    //private AudioManager audioManager; // เสียงในเกม
    /*private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // เสียงในเกม
       
    }*/
    void Start()
    {
        // ไม่มีการตั้งค่าเพิ่มเติมใน Start
    }

    void Update()
    {
        // ตรวจสอบว่าผู้เล่นอยู่ใกล้และกดปุ่ม E หนึ่งครั้ง
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && !isLeverPulled && !hasRotated)
        {
            // หมุนคันโยกโดยใช้ rotationAmount
            transform.Rotate(0, 0, rotationAmount);
           // audioManager.PlaySFX(audioManager.Lever); // เสียงSFX Get Hp

            // เริ่มเลื่อน Dor ลง
            isLeverPulled = true; // คันโยกถูกโยกแล้ว
            hasRotated = true; // คันโยกหมุนแล้ว
            Debug.Log("คันโยกถูกโยก");

            // เรียกฟังก์ชัน LeverPressed ใน Press3Levers
            press3Levers.LeverPressed(); // เพิ่มการเรียกใช้ที่นี่
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // ตรวจสอบว่าชนกับผู้เล่น
        {
            isPlayerNear = true; // ตั้งค่าสถานะผู้เล่นใกล้
            Debug.Log("ผู้เล่นอยู่ใกล้คันโยก");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // ตรวจสอบว่าผู้เล่นออกจากการชน
        {
            isPlayerNear = false; // ตั้งค่าสถานะผู้เล่นไม่ใกล้
            Debug.Log("ผู้เล่นออกจากการชนคันโยก");
        }
    }
}