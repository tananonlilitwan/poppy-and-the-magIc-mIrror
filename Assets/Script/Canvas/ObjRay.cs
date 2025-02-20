using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjRay : MonoBehaviour
{
    private int weightOnSwitch = 0; // นับจำนวนวัตถุที่มีน้ำหนักบนสวิตช์
    private bool isPressed = false; // สถานะของ switch
    private Coroutine timerCoroutine; // ใช้เก็บ Coroutine
    private float timer; // ตัวแปรนับเวลา
    private Vector3 originalPosition; // ตำแหน่งเริ่มต้นของสวิตช์

    // อ้างอิงไปที่ TimeLineStarGame
    [SerializeField] public TimeLineStarGame timeLineStarGame;
    [SerializeField] private GameObject panelTime;  // อ้างอิงไปที่ Panel 'Time'
    private void Awake()
    {
        // บันทึกตำแหน่งเริ่มต้นของสวิตช์
        originalPosition = transform.position;

        // ตรวจสอบว่ามีการเชื่อม TimeLineStarGame หรือไม่
        if (timeLineStarGame == null)
        {
            timeLineStarGame = GameObject.FindObjectOfType<TimeLineStarGame>();
        }
        
        // ตรวจสอบว่ามีการเชื่อม Panel Time หรือไม่
        if (panelTime != null)
        {
            panelTime.SetActive(false); // ปิด Panel Time ตอนเริ่มเกม
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // ตรวจสอบว่าชนกับผู้เล่น
        {
            weightOnSwitch++;
            Debug.Log("Player stepped on the switch, weightOnSwitch: " + weightOnSwitch);
            if (weightOnSwitch == 1 && !isPressed)
            {
                isPressed = true;
                PressSwitch();

                // เริ่มนับเวลาใน TimeLineStarGame
                timeLineStarGame.StartTimer();

                // เริ่มนับเวลาใน ObjRay
                if (timerCoroutine == null) // ตรวจสอบว่า Coroutine ยังไม่เริ่มทำงาน
                {
                    timerCoroutine = StartCoroutine(StartTimer());
                }
                
                // เปิด Panel Time
                if (panelTime != null)
                {
                    panelTime.SetActive(true);  // เปิด Panel Time เมื่อสวิตช์ถูกกด
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            weightOnSwitch--;
            Debug.Log("Player left the switch, weightOnSwitch: " + weightOnSwitch);
            if (weightOnSwitch <= 0)
            {
                isPressed = false;
                // หยุดนับเวลาเมื่อผู้เล่นออกจากสวิตช์
                if (timerCoroutine != null)
                {
                    StopCoroutine(timerCoroutine);
                    timerCoroutine = null;
                }
            }
        }
    }

    private void Update()
    {
        // เมื่อผู้เล่นไม่อยู่บนสวิตช์ ให้สวิตช์เด้งกลับ
        if (!isPressed && weightOnSwitch <= 0)
        {
            ReleaseSwitch();
        }
    }

    private IEnumerator StartTimer()
    {
        timer = 0f; // รีเซ็ตตัวนับเวลา
        while (isPressed)
        {
            timer += Time.deltaTime; // เพิ่มเวลา
            Debug.Log("Time on switch: " + timer);
            yield return null; // รอเฟรมถัดไป
        }
    }

    private void PressSwitch()
    {
        // สวิตช์ถูกกด ทำงานตามที่ต้องการ
        Vector3 newPosition = transform.position;
        newPosition.y -= 0.1f; // ปรับตำแหน่ง Y ลงเมื่อสวิตช์ถูกกด
        transform.position = newPosition;
    }

    private void ReleaseSwitch()
    {
        // สวิตช์ถูกปล่อยคืนสภาพเดิม
        if (transform.position.y < originalPosition.y) // ตรวจสอบว่าตำแหน่งปัจจุบันต่ำกว่าตำแหน่งเริ่มต้น
        {
            Vector3 newPosition = transform.position;
            newPosition.y += 0.1f; // ปรับตำแหน่ง Y กลับเมื่อสวิตช์ถูกปล่อย
            transform.position = newPosition;
        }
    }
}
