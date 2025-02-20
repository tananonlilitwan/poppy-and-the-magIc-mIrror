using UnityEngine;
using UnityEngine.UI;
using TMPro;

using System.Collections;
public class Switch : MonoBehaviour
{
    /*[SerializeField] GameObject puzzlePanel; // อ้างอิงถึง Panel Canvas puzzle ที่จะเปิด
    [SerializeField] TextMeshProUGUI displayText; // อ้างอิงถึง Text ที่จะแสดงข้อความ
    private bool isPressed = false; // สถานะของ switch 
    public Button backButton; // ปุ่ม Back

    void OnCollisionEnter2D(Collision2D collision)
    {
        // ตรวจสอบว่าผู้เล่นเหยียบสวิตช์หรือไม่
        if (collision.gameObject.CompareTag("Player") && !isPressed)
        {
            isPressed = true; // ตั้งค่าสถานะว่าสวิตช์ถูกกด
            PressSwitch(); // เรียกฟังก์ชันการทำงานของสวิตช์
            OpenPuzzlePanel(); // เปิด Panel Canvas puzzle
        }
    }

    void PressSwitch()
    {
        // สวิตช์ถูกกด ทำงานตามที่ต้องการ
        Vector3 newPosition = transform.position;
        newPosition.y -= 0.1f; // ปรับตำแหน่ง Y ลงเมื่อสวิตช์ถูกกด
        transform.position = newPosition;
    }

    void OpenPuzzlePanel()
    {
        // หยุดเกมชั่วคราว
        Time.timeScale = 0; 
        // เปิด Panel Canvas puzzle
        if (puzzlePanel != null)
        {
            puzzlePanel.SetActive(true);
            backButton.gameObject.SetActive(false); // ซ่อนปุ่ม Back
        }
    }
    
    // ฟังก์ชันสำหรับปิด Panel puzzle
    public void OnBackButtonPressed()
    {
        puzzlePanel.SetActive(false); // ปิด Panel Canvas puzzle
        Time.timeScale = 1; // ยกเลิกการหยุดเกมชั่วคราว
        backButton.gameObject.SetActive(true); // แสดงปุ่ม Back (ถ้าต้องการ)
    }
    
    // ฟังก์ชันสำหรับตรวจสอบการแสดงผลของปุ่ม Back
    void UpdateBackButtonVisibility()
    {
        // ถ้า displayText.text เป็น "I Love You" ให้ซ่อนปุ่ม Back
        if (displayText.text == "I Love You")
        {
            backButton.gameObject.SetActive(false);
        }
        else
        {
            backButton.gameObject.SetActive(true);
        }
    }

    // เรียกฟังก์ชันนี้ในกรณีที่ displayText.text เปลี่ยนแปลงหลังจากเปิด Panel แล้ว
    void Update()
    {
        UpdateBackButtonVisibility(); // ตรวจสอบในทุกเฟรม
    }*/
    
    /*[SerializeField] GameObject puzzlePanel; // อ้างอิงถึง Panel Canvas puzzle ที่จะเปิด
    [SerializeField] TextMeshProUGUI displayText; // อ้างอิงถึง Text ที่จะแสดงข้อความ
    private bool isPressed = false; // สถานะของ switch 
    public Button backButton; // ปุ่ม Back

    void OnCollisionEnter2D(Collision2D collision)
    {
        // ตรวจสอบว่าผู้เล่นเหยียบสวิตช์หรือไม่
        if (collision.gameObject.CompareTag("Player") && !isPressed)
        {
            isPressed = true; // ตั้งค่าสถานะว่าสวิตช์ถูกกด
            PressSwitch(); // เรียกฟังก์ชันการทำงานของสวิตช์
            OpenPuzzlePanel(); // เปิด Panel Canvas puzzle
        }
    }

    void PressSwitch()
    {
        // สวิตช์ถูกกด ทำงานตามที่ต้องการ
        Vector3 newPosition = transform.position;
        newPosition.y -= 0.1f; // ปรับตำแหน่ง Y ลงเมื่อสวิตช์ถูกกด
        transform.position = newPosition;
    }

    void OpenPuzzlePanel()
    {
        // หยุดเกมชั่วคราว
        Time.timeScale = 0;
        // เปิด Panel Canvas puzzle
        if (puzzlePanel != null)
        {
            puzzlePanel.SetActive(true);
            backButton.gameObject.SetActive(false); // ซ่อนปุ่ม Back
        }
    }

    // ฟังก์ชันสำหรับปิด Panel puzzle
    public void OnBackButtonPressed()
    {
        puzzlePanel.SetActive(false); // ปิด Panel Canvas puzzle
        Time.timeScale = 1; // ยกเลิกการหยุดเกมชั่วคราว
        backButton.gameObject.SetActive(true); // แสดงปุ่ม Back (ถ้าต้องการ)
        isPressed = false; // รีเซ็ตสถานะของสวิตช์ ให้สามารถเหยียบได้อีกครั้ง
    }

    // ฟังก์ชันสำหรับตรวจสอบการแสดงผลของปุ่ม Back
    void UpdateBackButtonVisibility()
    {
        // ถ้า displayText.text เป็น "I Love You" ให้ซ่อนปุ่ม Back
        if (displayText.text == "I Love You")
        {
            backButton.gameObject.SetActive(false);
        }
        else
        {
            backButton.gameObject.SetActive(true);
        }
    }

    // เรียกฟังก์ชันนี้ในกรณีที่ displayText.text เปลี่ยนแปลงหลังจากเปิด Panel แล้ว
    void Update()
    {
        UpdateBackButtonVisibility(); // ตรวจสอบในทุกเฟรม
    }*/
    
    
    
    /*[SerializeField] GameObject puzzlePanel; // อ้างอิงถึง Panel Canvas puzzle ที่จะเปิด
    [SerializeField] TextMeshProUGUI displayText; // อ้างอิงถึง Text ที่จะแสดงข้อความ
    private bool isPressed = false; // สถานะของ switch 
    public Button backButton; // ปุ่ม Back
    private Vector3 initialPosition; // ตำแหน่งเริ่มต้นของ Switch
    private bool playerOnSwitch = false; // ตรวจสอบว่า Player อยู่บน Switch หรือไม่

    void Start()
    {
        initialPosition = transform.position; // เก็บตำแหน่งเริ่มต้นของ Switch
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // ตรวจสอบว่าผู้เล่นเหยียบสวิตช์หรือไม่
        if (collision.gameObject.CompareTag("Player") && !isPressed)
        {
            isPressed = true; // ตั้งค่าสถานะว่าสวิตช์ถูกกด
            playerOnSwitch = true; // ตั้งค่าว่าผู้เล่นอยู่บน Switch
            PressSwitch(); // เรียกฟังก์ชันการทำงานของสวิตช์
            OpenPuzzlePanel(); // เปิด Panel Canvas puzzle
        }
    }
    
    /*void OnCollisionExit2D(Collision2D collision)
    {
        // ตรวจสอบว่าผู้เล่นออกจากสวิตช์หรือไม่
        if (collision.gameObject.CompareTag("Player") && isPressed)
        {
            playerOnSwitch = false; // ตั้งค่าว่าผู้เล่นออกจาก Switch แล้ว
        }
    }#1#

    void OnCollisionExit2D(Collision2D collision)
    {
        // ตรวจสอบว่าผู้เล่นออกจากสวิตช์หรือไม่
        if (collision.gameObject.CompareTag("Player"))
        {
            isPressed = false; // รีเซ็ตสถานะของ Switch เพื่อให้สามารถเหยียบได้อีกครั้ง
            playerOnSwitch = false; // ตั้งค่าว่าผู้เล่นออกจาก Switch แล้ว
        }
    }

    void PressSwitch()
    {
        // สวิตช์ถูกกด ทำงานตามที่ต้องการ
        Vector3 newPosition = transform.position;
        newPosition.y -= 0.1f; // ปรับตำแหน่ง Y ลงเมื่อสวิตช์ถูกกด
        transform.position = newPosition;
    }

    void OpenPuzzlePanel()
    {
        // หยุดเกมชั่วคราว
        Time.timeScale = 0; 
        // เปิด Panel Canvas puzzle
        if (puzzlePanel != null)
        {
            puzzlePanel.SetActive(true);
            backButton.gameObject.SetActive(false); // ซ่อนปุ่ม Back
        }
    }

    // ฟังก์ชันสำหรับปิด Panel puzzle และรีเซ็ต Switch
    public void OnBackButtonPressed()
    {
        puzzlePanel.SetActive(false); // ปิด Panel Canvas puzzle
        Time.timeScale = 1; // ยกเลิกการหยุดเกมชั่วคราว
        
        // รีเซ็ต Switch เมื่อผู้เล่นออกจาก Switch
        if (!playerOnSwitch)
        {
            ResetSwitch(); // เรียกฟังก์ชันรีเซ็ต Switch
        }
    }

    // ฟังก์ชันสำหรับรีเซ็ต Switch
    void ResetSwitch()
    {
        isPressed = false; // รีเซ็ตสถานะของ Switch
        transform.position = initialPosition; // รีเซ็ตตำแหน่งของ Switch กลับไปยังตำแหน่งเริ่มต้น
    }

    // ฟังก์ชันสำหรับตรวจสอบการแสดงผลของปุ่ม Back
    void UpdateBackButtonVisibility()
    {
        // ถ้า displayText.text เป็น "I Love You" ให้ซ่อนปุ่ม Back
        if (displayText.text == "I Love You")
        {
            backButton.gameObject.SetActive(false);
        }
        else
        {
            backButton.gameObject.SetActive(true);
        }
    }

    // เรียกฟังก์ชันนี้ในกรณีที่ displayText.text เปลี่ยนแปลงหลังจากเปิด Panel แล้ว
    void Update()
    {
        UpdateBackButtonVisibility(); // ตรวจสอบในทุกเฟรม
    }*/
    
    /*[SerializeField] GameObject puzzlePanel; // อ้างอิงถึง Panel Canvas puzzle ที่จะเปิด
    [SerializeField] TextMeshProUGUI displayText; // อ้างอิงถึง Text ที่จะแสดงข้อความ
    public Button backButton; // ปุ่ม Back
    private Vector3 initialPosition; // ตำแหน่งเริ่มต้นของ Switch
    private bool isPressed = false; // สถานะของ switch 
    private bool playerOnSwitch = false; // ตรวจสอบว่า Player อยู่บน Switch หรือไม่

    void Start()
    {
        initialPosition = transform.position; // เก็บตำแหน่งเริ่มต้นของ Switch
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // ตรวจสอบว่าผู้เล่นเหยียบสวิตช์หรือไม่
        if (collision.gameObject.CompareTag("Player") && !isPressed)
        {
            isPressed = true; // ตั้งค่าสถานะว่าสวิตช์ถูกกด
            playerOnSwitch = true; // ตั้งค่าว่าผู้เล่นอยู่บน Switch
            PressSwitch(); // เรียกฟังก์ชันการทำงานของสวิตช์
            OpenPuzzlePanel(); // เปิด Panel Canvas puzzle
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // ตรวจสอบว่าผู้เล่นออกจากสวิตช์หรือไม่
        if (collision.gameObject.CompareTag("Player") && isPressed)
        {
            playerOnSwitch = false; // ตั้งค่าว่าผู้เล่นออกจาก Switch แล้ว
            ReleaseSwitch(); // ทำให้ปุ่มเด้งกลับ
            isPressed = false; // รีเซ็ตสถานะของ Switch เพื่อให้สามารถเหยียบได้อีกครั้ง
        }
    }

    void PressSwitch()
    {
        // สวิตช์ถูกกด ทำงานตามที่ต้องการ (หยุบลง)
        Vector3 newPosition = transform.position;
        newPosition.y -= 0.1f; // ปรับตำแหน่ง Y ลงเมื่อสวิตช์ถูกกด
        transform.position = newPosition;
    }

    void ReleaseSwitch()
    {
        // ปุ่มเด้งกลับขึ้นเมื่อผู้เล่นออกจาก Switch
        transform.position = initialPosition; // รีเซ็ตตำแหน่งของ Switch กลับไปยังตำแหน่งเริ่มต้น
    }

    void OpenPuzzlePanel()
    {
        // หยุดเกมชั่วคราว
        Time.timeScale = 0;
        // เปิด Panel Canvas puzzle
        if (puzzlePanel != null)
        {
            puzzlePanel.SetActive(true);
            backButton.gameObject.SetActive(false); // ซ่อนปุ่ม Back
        }
    }

    // ฟังก์ชันสำหรับปิด Panel puzzle และรีเซ็ต Switch
    public void OnBackButtonPressed()
    {
        puzzlePanel.SetActive(false); // ปิด Panel Canvas puzzle
        Time.timeScale = 1; // ยกเลิกการหยุดเกมชั่วคราว

        // รีเซ็ต Switch เมื่อผู้เล่นออกจาก Switch
        if (!playerOnSwitch)
        {
            ReleaseSwitch(); // ทำให้ปุ่มเด้งกลับ
        }
    }

    // ฟังก์ชันสำหรับตรวจสอบการแสดงผลของปุ่ม Back
    void UpdateBackButtonVisibility()
    {
        // ถ้า displayText.text เป็น "I Love You" ให้ซ่อนปุ่ม Back
        if (displayText.text == "I Love You")
        {
            backButton.gameObject.SetActive(false);
        }
        else
        {
            backButton.gameObject.SetActive(true);
        }
    }

    // เรียกฟังก์ชันนี้ในกรณีที่ displayText.text เปลี่ยนแปลงหลังจากเปิด Panel แล้ว
    void Update()
    {
        UpdateBackButtonVisibility(); // ตรวจสอบในทุกเฟรม
    }*/
    
    [SerializeField] GameObject puzzlePanel; // อ้างอิงถึง Panel Canvas puzzle ที่จะเปิด
    [SerializeField] TextMeshProUGUI displayText; // อ้างอิงถึง Text ที่จะแสดงข้อความ
    public Button backButton; // ปุ่ม Back
    private Vector3 initialPosition; // ตำแหน่งเริ่มต้นของ Switch
    private bool isPressed = false; // สถานะของ switch 
    private bool playerOnSwitch = false; // ตรวจสอบว่า Player อยู่บน Switch หรือไม่

    void Start()
    {
        initialPosition = transform.position; // เก็บตำแหน่งเริ่มต้นของ Switch
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // ตรวจสอบว่าผู้เล่นเหยียบสวิตช์หรือไม่
        if (collision.gameObject.CompareTag("Player") && !isPressed)
        {
            isPressed = true; // ตั้งค่าสถานะว่าสวิตช์ถูกกด
            playerOnSwitch = true; // ตั้งค่าว่าผู้เล่นอยู่บน Switch
            PressSwitch(); // เรียกฟังก์ชันการทำงานของสวิตช์
            OpenPuzzlePanel(); // เปิด Panel Canvas puzzle
        }
    }

    /*void OnCollisionExit2D(Collision2D collision)
    {
        // ตรวจสอบว่าผู้เล่นออกจากสวิตช์หรือไม่
        if (collision.gameObject.CompareTag("Player") && isPressed)
        {
            playerOnSwitch = false; // ตั้งค่าว่าผู้เล่นออกจาก Switch แล้ว
            StartCoroutine(ReleaseSwitchWithDelay(2f)); // เรียก Coroutine ให้ดีเลย์ 2 วินาทีก่อนที่ Switch จะเด้งกลับ
            isPressed = false; // รีเซ็ตสถานะของ Switch เพื่อให้สามารถเหยียบได้อีกครั้ง
        }
    }*/
    void OnCollisionExit2D(Collision2D collision)
    {
        // ตรวจสอบว่าผู้เล่นออกจากสวิตช์หรือไม่
        if (collision.gameObject.CompareTag("Player") && isPressed)
        {
            playerOnSwitch = false; // ตั้งค่าว่าผู้เล่นออกจาก Switch แล้ว
            if (gameObject.activeSelf) // ตรวจสอบว่า GameObject เปิดอยู่
            {
                StartCoroutine(ReleaseSwitchWithDelay(2f)); // เรียก Coroutine ให้ดีเลย์ 2 วินาทีก่อนที่ Switch จะเด้งกลับ
            }
            isPressed = false; // รีเซ็ตสถานะของ Switch เพื่อให้สามารถเหยียบได้อีกครั้ง
        }
    }

    void PressSwitch()
    {
        // สวิตช์ถูกกด ทำงานตามที่ต้องการ (หยุบลง)
        Vector3 newPosition = transform.position;
        newPosition.y -= 0.4f; // ปรับตำแหน่ง Y ลงเมื่อสวิตช์ถูกกด
        transform.position = newPosition;
    }

    IEnumerator ReleaseSwitchWithDelay(float delay)
    {
        // รอเป็นเวลาที่กำหนดก่อนที่ปุ่มจะเด้งกลับ
        yield return new WaitForSeconds(delay);
        ReleaseSwitch(); // ปุ่มเด้งกลับหลังจากดีเลย์
    }

    void ReleaseSwitch()
    {
        // ปุ่มเด้งกลับขึ้นเมื่อผู้เล่นออกจาก Switch
        transform.position = initialPosition; // รีเซ็ตตำแหน่งของ Switch กลับไปยังตำแหน่งเริ่มต้น
    }

    void OpenPuzzlePanel()
    {
        // หยุดเกมชั่วคราว
        Time.timeScale = 0;
        // เปิด Panel Canvas puzzle
        if (puzzlePanel != null)
        {
            puzzlePanel.SetActive(true);
            backButton.gameObject.SetActive(false); // ซ่อนปุ่ม Back
        }
    }

    // ฟังก์ชันสำหรับปิด Panel puzzle และรีเซ็ต Switch
    public void OnBackButtonPressed()
    {
        puzzlePanel.SetActive(false); // ปิด Panel Canvas puzzle
        Time.timeScale = 1; // ยกเลิกการหยุดเกมชั่วคราว

        // รีเซ็ต Switch เมื่อผู้เล่นออกจาก Switch
        if (!playerOnSwitch)
        {
            StartCoroutine(ReleaseSwitchWithDelay(2f)); // เรียก Coroutine ให้ดีเลย์ 2 วินาทีก่อนที่ Switch จะเด้งกลับ
        }
    }

    // ฟังก์ชันสำหรับตรวจสอบการแสดงผลของปุ่ม Back
    void UpdateBackButtonVisibility()
    {
        // ถ้า displayText.text เป็น "I Love You" ให้ซ่อนปุ่ม Back
        if (displayText.text == "Congratulations, you won.")
        {
            backButton.gameObject.SetActive(false);
        }
        else
        {
            backButton.gameObject.SetActive(true);
        }
    }

    // เรียกฟังก์ชันนี้ในกรณีที่ displayText.text เปลี่ยนแปลงหลังจากเปิด Panel แล้ว
    void Update()
    {
        UpdateBackButtonVisibility(); // ตรวจสอบในทุกเฟรม
    }
}