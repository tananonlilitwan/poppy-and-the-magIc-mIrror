using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Calculator : MonoBehaviour
{
    public TMP_Text displayText; // ช่องข้อความสำหรับแสดงผลลัพธ์ (ใช้ TextMeshPro)
    public Button nextButton; // ปุ่ม Next
    
    private float total = 0; // ตัวแปรสำหรับเก็บผลรวม
    private float currentNumber = 0; // ตัวเลขที่กำลังกรอกอยู่
    private string lastOperation = ""; // การดำเนินการล่าสุด
    
    
    void Start()
    {
        displayText.text = "Please fix the puzzle."; // แสดงข้อความเริ่มต้นในช่องข้อความ
        nextButton.gameObject.SetActive(false); // ซ่อนปุ่ม Next ในตอนเริ่มต้น
    }
    
    // ฟังก์ชันสำหรับกดตัวเลข
    public void OnNumberButtonPressed(string number)
    {
        // เช็คว่าข้อความใน displayText เท่ากับ "Please fix the puzzle." หรือไม่
        if (displayText.text == "Please fix the puzzle.")
        {
            displayText.text = ""; // ลบข้อความออก
        }
        displayText.text += number; // เพิ่มตัวเลขที่กดลงในช่องข้อความ
    }

    // ฟังก์ชันสำหรับปุ่มบวก
    public void OnAddButtonPressed()
    {
        if (float.TryParse(displayText.text, out currentNumber))
        {
            total += currentNumber; // บวกตัวเลขกับผลรวม
            displayText.text = ""; // เคลียร์ช่องข้อความสำหรับกรอกตัวเลขถัดไป
            lastOperation = "+"; // บันทึกการดำเนินการล่าสุด
        }
    }

    // ฟังก์ชันสำหรับปุ่มลบ
    public void OnSubtractButtonPressed()
    {
        if (float.TryParse(displayText.text, out currentNumber))
        {
            total -= currentNumber; // ลบตัวเลขจากผลรวม
            displayText.text = ""; // เคลียร์ช่องข้อความสำหรับกรอกตัวเลขถัดไป
            lastOperation = "-"; // บันทึกการดำเนินการล่าสุด
        }
    }

    // ฟังก์ชันสำหรับปุ่มแสดงผลลัพธ์
    public void OnEqualsButtonPressed()
    {
        if (displayText.text == "3" && lastOperation == "+")
        {
            // แสดงผลลัพธ์เป็น "I Love You" เมื่อกด 1 + 3
            displayText.text = "Congratulations, you won."; 
            // ปิดปุ่ม Next
            nextButton.gameObject.SetActive(true);
            // รีเซ็ตตัวแปรเพื่อใช้ในการคำนวณใหม่
            total = 0; 
            currentNumber = 0; 
            lastOperation = ""; 
        }
        else
        {
            if (lastOperation == "+")
            {
                if (float.TryParse(displayText.text, out currentNumber))
                {
                    total += currentNumber; // บวกตัวเลขสุดท้าย
                    displayText.text = total.ToString(); // แสดงผลรวมในช่องข้อความ
                    total = 0; // รีเซ็ตผลรวม
                }
            }
            else if (lastOperation == "-")
            {
                if (float.TryParse(displayText.text, out currentNumber))
                {
                    total -= currentNumber; // ลบตัวเลขสุดท้าย
                    displayText.text = total.ToString(); // แสดงผลรวมในช่องข้อความ
                    total = 0; // รีเซ็ตผลรวม
                }
            }
        }
    }

    // ฟังก์ชันสำหรับลบตัวเลขล่าสุด
    public void OnDeleteButtonPressed()
    {
        if (displayText.text.Length > 0)
        {
            displayText.text = displayText.text.Substring(0, displayText.text.Length - 1); // ลบตัวอักษรสุดท้าย
        }
    }

    // ฟังก์ชันสำหรับเคลียร์
    public void OnClearButtonPressed()
    {
        displayText.text = ""; // เคลียร์ช่องข้อความ
        total = 0; // รีเซ็ตผลรวม
        currentNumber = 0; // รีเซ็ตตัวเลขที่กรอกอยู่
        lastOperation = ""; // เคลียร์การดำเนินการล่าสุด
    }
    
    
    
}
