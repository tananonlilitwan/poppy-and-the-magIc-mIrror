using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Press3Levers : MonoBehaviour
{
    [SerializeField] private GameObject[] levers; // อาร์เรย์ของคันโยก
    private int leversPressedCount = 0; // นับจำนวนคันโยกที่ถูกโยก
    
    [SerializeField] private GameObject Dor; // อ้างอิงถึง GameObject Dor ที่จะเลื่อนลง
    [SerializeField] private float moveSpeed = 1f; // ความเร็วในการเลื่อน Dor
    [SerializeField] private float moveDistance = 3f; // ระยะที่ Dor จะเลื่อนลง
    private bool isDorAtFinalPosition = false; // ตรวจสอบว่า Dor เลื่อนถึงปลายทางแล้วหรือยัง
    private Vector3 dorFinalPosition; // ตำแหน่งสุดท้ายที่ Dor จะเลื่อนไป
    
    private bool isPlayerNear = false; // ตรวจสอบว่าผู้เล่นอยู่ใกล้หรือไม่

    private void Start()
    {
        dorFinalPosition = new Vector3(Dor.transform.position.x, Dor.transform.position.y - moveDistance, Dor.transform.position.z); // ตำแหน่งปลายทางของ Dor
    }

    public void LeverPressed()
    {
        leversPressedCount++; // เพิ่มจำนวนคันโยกที่ถูกโยก
        Debug.Log("คันโยกถูกโยกแล้ว " + leversPressedCount + " อัน");

        // ตรวจสอบว่ากดคันโยกครบ 3 อันแล้วหรือยัง
        if (leversPressedCount >= 4)
        {
            Debug.Log("คันโยกครบแล้ว! กำลังเลื่อน Dor ลง");
        }
    }

    void MoveDor()
    {
        // ตรวจสอบว่า Dor ยังไม่ถึงตำแหน่งปลายทาง
        if (!isDorAtFinalPosition)
        {
            // เลื่อน Dor ลงไปทีละน้อยจนกว่าจะถึงตำแหน่งปลายทาง
            Dor.transform.position = Vector3.MoveTowards(Dor.transform.position, dorFinalPosition, moveSpeed * Time.deltaTime);

            // ตรวจสอบว่า Dor ถึงตำแหน่งปลายทางแล้วหรือยัง
            if (Dor.transform.position == dorFinalPosition)
            {
                isDorAtFinalPosition = true; // ตั้งค่าว่า Dor ถึงปลายทางแล้ว
                Debug.Log("Dor เลื่อนลงจนสุดแล้ว");
            }
        }
    }

    void Update()
    {
        // ตรวจสอบว่า คันโยกถูกโยกครบ 3 อันแล้ว และ Dor ยังไม่ถึงปลายทาง
        if (leversPressedCount >= 4 && !isDorAtFinalPosition)
        {
            MoveDor(); // เรียกการเลื่อน Dor
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // ตรวจสอบว่าชนกับผู้เล่น
        {
            isPlayerNear = true; // ตั้งค่าสถานะผู้เล่นใกล้
            Debug.Log("ผู้เล่นอยู่ใกล้");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // ตรวจสอบว่าผู้เล่นออกจากการชน
        {
            isPlayerNear = false; // ตั้งค่าสถานะผู้เล่นไม่ใกล้
            Debug.Log("ผู้เล่นออก");
        }
    }
}
