using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    //ทำให้ แสดงปุ่มที่ถูกซ่อนเมื่อ Canvas ต่างๆที่มีคลิปนี้อยู่  ทำให้ แสดงปุ่มขึ้นมา 
    
    [Header("Button to Show/Hide")]
    [SerializeField] private GameObject button; // อ้างอิงถึงปุ่มที่ต้องการแสดง

    // Start is called before the first frame update
    void Start()
    {
        // ตรวจสอบว่า button มีการตั้งค่าให้แสดง
        if (button != null)
        {
            button.SetActive(false); // เริ่มต้นด้วยการซ่อนปุ่ม
        }
    }

    // ฟังก์ชันสำหรับแสดงปุ่ม
    public void ShowButton()
    {
        if (button != null)
        {
            button.SetActive(true); // แสดงปุ่ม
        }
    }

    // ฟังก์ชันสำหรับซ่อนปุ่ม
    public void HideButton()
    {
        if (button != null)
        {
            button.SetActive(false); // ซ่อนปุ่ม
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ตัวอย่าง: เรียกใช้ ShowButton() เมื่อปุ่ม "P" ถูกกด
        if (Input.GetKeyDown(KeyCode.P))
        {
            ShowButton(); // แสดงปุ่ม
        }

        // ตัวอย่าง: เรียกใช้ HideButton() เมื่อปุ่ม "H" ถูกกด
        if (Input.GetKeyDown(KeyCode.H))
        {
            HideButton(); // ซ่อนปุ่ม
        }
    }
}