using UnityEngine;
using TMPro; 
using System.Collections;

public class MagicFishNPC : MonoBehaviour
{
   // public GameObject Nextpanel;
    [Header("---------- Dialog -------------")]
    public GameObject bgDialog; // อ้างอิง BG Dialog
    public TextMeshProUGUI textDialog1; // อ้างอิง Text Dialog 1
    public TextMeshProUGUI textDialog2; // อ้างอิง Text Dialog 2
    public TextMeshProUGUI textDialog3; // อ้างอิง Text Dialog 3
    public TextMeshProUGUI textDialog4; // อ้างอิง Text Dialog 4
    public TextMeshProUGUI textDialog5; // อ้างอิง Text Dialog 5
    public float dialogDisplayTime = 3f; // เวลาในการแสดง Dialog 2

    [SerializeField] private string dialog1Text; // ประโยค Dialog 1 ที่สามารถพิมพ์จาก Unity ได้
    [SerializeField] private string dialog2Text; // ประโยค Dialog 2 ที่สามารถพิมพ์จาก Unity ได้
    [SerializeField] private string dialog3Text; // ประโยค Dialog 3 ที่สามารถพิมพ์จาก Unity ได้
    [SerializeField] private string dialog4Text; // ประโยค Dialog 3 ที่สามารถพิมพ์จาก Unity ได้
    [SerializeField] private string dialog5Text; // ประโยค Dialog 3 ที่สามารถพิมพ์จาก Unity ได้

    private bool isPlayerNearby = false;
    private bool isDialogActive = false;

    [Header("----------brickSpawner -------------")]
    public Transform playerTargetPosition; // จุดที่ Player จะย้ายไป
    public GameObject brickSpawner; // อ้างอิงถึง Brick Spawner
    private Collider2D playerCollider; // ใช้สำหรับเก็บ Collider ของผู้เล่น
    public float moveSpeed = 5f; // ความเร็วในการเคลื่อนที่ของ Player

    [SerializeField] float spawnerBrickActiveTime; // เวลาในการเปิด Brick Spawner 
    
    private bool isBrickSpawnerActive = false; // ตัวแปรเพื่อติดตามสถานะ Brick Spawner
    
    [SerializeField] GameObject nextPanel; // ตัวแปรสำหรับ Obj Square (1)

    
    private AudioManager audioManager; // เสียงในเกม
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // เสียงในเกม
       
    }
    void Update()
    {
        /*// เมื่อ Dialog1 แสดงอยู่แล้ว ถ้าผู้เล่นกด E ให้แสดง Dialog2
        if (isDialogActive && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Player pressed E for Dialog 2"); 
            ShowDialog2();
        }*/
        
        // ให้ผู้เล่นกด E ครั้งเดียวแล้วแสดง Dialog ทั้งหมดโดยอัตโนมัติ
        if (isDialogActive && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Player pressed E to show dialogs");
            StartCoroutine(ShowAllDialogs()); // เริ่มแสดง Dialog ทั้งหมดโดยอัตโนมัติ
            
            audioManager.PlaySFX(audioManager.click);
        }
    }

    private void ShowDialog1()
    {
        /*bgDialog.SetActive(true); // เปิด BG ของ Dialog
        textDialog1.gameObject.SetActive(true); // แสดง Text Dialog 1

        // ตั้งค่าข้อความให้กับ TextMeshPro
        textDialog1.text = dialog1Text; // ใช้ข้อความจากตัวแปร dialog1Text

        isDialogActive = true; // กำหนดว่า Dialog กำลังแสดงอยู่

        Debug.Log("Dialog 1 is shown with text: " + dialog1Text);*/
        
        bgDialog.SetActive(true); // เปิด BG ของ Dialog
        textDialog1.gameObject.SetActive(true); // แสดง Text Dialog 1

        // ตั้งค่าข้อความให้กับ TextMeshPro
        textDialog1.text = dialog1Text; // ใช้ข้อความจากตัวแปร dialog1Text
        isDialogActive = true; // กำหนดว่า Dialog กำลังแสดงอยู่

        Debug.Log("Dialog 1 is shown with text: " + dialog1Text);
    }

    /*private void ShowDialog2()
    {
        textDialog1.gameObject.SetActive(false); // ปิด Text Dialog 1
        textDialog2.gameObject.SetActive(true); // แสดง Text Dialog 2
        textDialog2.text = dialog2Text; // ใช้ข้อความจากตัวแปร dialog2Text ในการแสดงผล
        StartCoroutine(CloseDialogAfterTime()); // เริ่มรอเวลา 3 วินาทีเพื่อลบ Dialog 2

        Debug.Log("Dialog 2 is shown with text: " + dialog2Text);
    }*/
    
    
    private IEnumerator ShowAllDialogs()
    {
        // ปิดการควบคุมผู้เล่นระหว่างที่ Dialog แสดง
        ControlPlayer1 playerController = playerCollider.GetComponent<ControlPlayer1>();
        if (playerController != null)
        {
            playerController.isPlayerControllable = false;
        }
        
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

        // แสดง Dialog 4
        textDialog3.gameObject.SetActive(false); 
        textDialog4.gameObject.SetActive(true); 
        textDialog4.text = dialog4Text;
        Debug.Log("Dialog 4 is shown with text: " + dialog4Text);
        yield return new WaitForSeconds(dialogDisplayTime); // รอ 3 วินาที

        // ปิด Dialog ทั้งหมด
        bgDialog.SetActive(false); 
        textDialog4.gameObject.SetActive(false); 
        isDialogActive = false;
        Debug.Log("All dialogs are closed");
        
        // เปิดการควบคุมผู้เล่นอีกครั้ง
        if (playerController != null)
        {
            playerController.isPlayerControllable = true;
        }

        // ย้าย Player ไปยังตำแหน่งที่กำหนด
        if (playerTargetPosition != null && playerCollider != null)
        {
            MovePlayerToTarget(playerCollider.transform, playerTargetPosition.position); // ย้าย Player แบบสมูท
        }

        // เปิด Brick Spawner
        if (brickSpawner != null)
        {
            brickSpawner.SetActive(true);
            StartCoroutine(CloseBrickSpawnerAfterTime()); // เริ่ม Coroutine ปิด Brick Spawner หลังจากเวลา
        }
        // อนุญาตให้ผู้เล่นยิงได้
        AllowPlayerToShoot();
        
        AllowSkill2ForFree();  // เรียกฟังก์ชัน AllowSkill2ForFree หลังจากจบการสนทนา
 
    }

    /*private IEnumerator CloseDialogAfterTime()
    {
        yield return new WaitForSeconds(dialogDisplayTime); // รอเป็นเวลา 3 วินาที (ตั้งไว้ในตัวแปร dialogDisplayTime)
        bgDialog.SetActive(false); // ปิด BG ของ Dialog
        textDialog2.gameObject.SetActive(false); // ปิด Text Dialog 2
        isDialogActive = false; // รีเซ็ตสถานะว่าการแสดงผลจบแล้ว
        Debug.Log("Dialog 2 is closed after " + dialogDisplayTime + " seconds");
        
        

        // ย้าย Player ไปยังตำแหน่งที่กำหนด
        if (playerTargetPosition != null && playerCollider != null)
        {
            MovePlayerToTarget(playerCollider.transform, playerTargetPosition.position); // ย้าย Player แบบสมูท
        }

        // เปิด Brick Spawner
        if (brickSpawner != null)
        {
            brickSpawner.SetActive(true);
            StartCoroutine(CloseBrickSpawnerAfterTime()); // เริ่ม Coroutine ปิด Brick Spawner หลังจากเวลา
        }
    }*/
    
    private void ShowDialog5()
    {
        bgDialog.SetActive(true); // เปิด BG ของ Dialog
        textDialog5.gameObject.SetActive(true); // แสดง Text Dialog 5
        textDialog5.text = dialog5Text; // ตั้งค่าข้อความให้กับ Dialog 5
        Debug.Log("Dialog 5 is shown with text: " + dialog5Text);

        // รอ 3 วินาทีก่อนที่จะปิด Dialog 5
        StartCoroutine(CloseDialog5AfterTime());
    }
    
    private IEnumerator CloseDialog5AfterTime()
    {
        yield return new WaitForSeconds(3f); // รอ 3 วินาที
        bgDialog.SetActive(false); // ปิด BG ของ Dialog
        textDialog5.gameObject.SetActive(false); // ปิด Text Dialog 5
        Debug.Log("Dialog 5 is closed");
        
       // Nextpanel.SetActive(true);
        
        // คืนค่าการใช้มานา
        PlayerShooting playerShooting = playerCollider.GetComponent<PlayerShooting>();
        if (playerShooting != null)
        {
            playerShooting.ResetSkill2Usage(); // รีเซ็ตการใช้สกิล 2 เป็นปกติ
        }
    }
    
    private IEnumerator CloseBrickSpawnerAfterTime()
    {
        yield return new WaitForSeconds(spawnerBrickActiveTime); // รอเวลาตามที่กำหนด
        if (brickSpawner != null)
        {
            brickSpawner.SetActive(false); // ปิด Brick Spawner
            isBrickSpawnerActive = false; // อัปเดตสถานะ Brick Spawner
            ShowDialog5(); // แสดง Dialog 5
            
            // เปิด Obj Square (1)
            if (nextPanel != null) // ตรวจสอบว่า nextPanel ถูกตั้งค่าไว้หรือไม่
            {
                nextPanel.SetActive(true); // เปิด Obj Square (1)
                Debug.Log("Next Panel (Obj Square (1)) is now active.");
            }
            
            // คืนค่าการใช้มานา
            PlayerShooting playerShooting = playerCollider.GetComponent<PlayerShooting>();
            if (playerShooting != null)
            {
                playerShooting.ResetSkill2Usage(); // รีเซ็ตการใช้สกิล 2 เป็นปกติ
            }
        }
    }

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
            isPlayerNearby = true;
            playerCollider = other; // เก็บค่า Collider ของผู้เล่น

            // แสดง Dialog1 เมื่อเข้าใกล้ Player
            if (!isDialogActive)
            {
                ShowDialog1();
            }
            
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        /*if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Debug.Log("Player has left the area"); // ตรวจสอบว่า Player ออกจากบริเวณแล้ว
            if (isDialogActive)
            {
                bgDialog.SetActive(false);
                textDialog1.gameObject.SetActive(false); // ปิด Text Dialog 1
                textDialog2.gameObject.SetActive(false); // ปิด Text Dialog 2
                isDialogActive = false;
            }
        }*/
        
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;

            // ปิด Dialog หาก Player ออกจากพื้นที่
            if (isDialogActive)
            {
                bgDialog.SetActive(false);
                textDialog1.gameObject.SetActive(false);
                textDialog2.gameObject.SetActive(false);
                isDialogActive = false;
            }
        }
    }


    private void MovePlayerToTarget(Transform playerTransform, Vector3 targetPosition)
    {
        StartCoroutine(MoveToTarget(playerTransform, targetPosition));
    }

    private IEnumerator MoveToTarget(Transform playerTransform, Vector3 targetPosition)
    {
        float step = moveSpeed * Time.deltaTime; // คำนวณความก้าวหน้าต่อเฟรม
        while (Vector3.Distance(playerTransform.position, targetPosition) > 0.01f)
        {
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, targetPosition, step);
            yield return null;
        }
        playerTransform.position = targetPosition;
    }
    
    private void AllowPlayerToShoot()
    {
        PlayerShooting playerShooting = playerCollider.GetComponent<PlayerShooting>();
        if (playerShooting != null)
        {
            playerShooting.canShoot = true; // อนุญาตให้ยิงได้
            Debug.Log("Player can now shoot.");
        }
    }
    
    public void AllowSkill2ForFree()
    {
        PlayerShooting playerShooting = playerCollider.GetComponent<PlayerShooting>();
        if (playerShooting != null)
        {
            playerShooting.isSkill2Free = true; // เปิดให้ใช้สกิล 2 ได้ฟรี
            Debug.Log("Player can now use skill 2 for free.");
        }
    }
    
    
    public bool IsBrickSpawnerActive()
    {
        return isBrickSpawnerActive; // ส่งกลับค่าจริงถ้า Brick Spawner เปิดอยู่
    }
    
    
}
