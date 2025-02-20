using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;


public class Player_controller : MonoBehaviour
{
    [SerializeField] float speed; 
    int jump;
    float x, sx;
    bool ks;
    Animator am;
    Rigidbody2D rb;
    //----------------------------------------
    public Transform carryPoint; // ตำแหน่งที่ใช้ยก Obj Box
    private GameObject carriedObj = null;
    private bool isCarrying = false;
    // เพิ่ม LayerMask สำหรับ Obj Box
    public LayerMask boxLayerMask;
    
    private GameObject pulledBox = null;
    private bool isPulling = false; // สถานะการดึง
    
    [SerializeField] float pushSpeed; // ความเร็วในการผลัก Big Box
    private float normalPushSpeed; // ความเร็วในการผลักกล่องทั่วไป
    [SerializeField] private GameObject pausePanel;
    //-----------------------------------------
    //UI Hp 
    [SerializeField] int lives = 3; // เลือดของ Player
    public GameObject[] livesImage; // บล็อคสีแสดงจำนวนเลือด
   
    //-----------------------------------------
    public float lookUpAngle = 16f; // มุมที่เงยขึ้น
    public float lookDownAngle = -16f; // มุมที่ก้มลง
    private float currentLookAngle = 0f; // มุมปัจจุบัน
    //-----------------------------------------
    // ตัวแปรสำหรับการเรียกแมว
    public GameObject cat;  // อ้างอิงไปยัง GameObject ของแมว
    private CatAi catAi;    // อ้างอิงไปยังสคริปต์ของ CatAi
    //-----------------------------------------
    
    private AudioManager audioManager; // เสียงในเกม
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // เสียงในเกม
       
    }
    
    // Start is called before the first frame update
    void Start()
    {
        jump = 0;
        am = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sx = transform.localScale.x;
        // ล็อคการหมุนในแกน Z แต่ยังคงให้เคลื่อนที่และกระโดดได้
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        
        //------------------------------------------------------------
        normalPushSpeed = speed; // เก็บความเร็วปกติ
        //------------------------------------------------------------
        // หา CatAi component จากแมวที่อ้างอิง
        if (cat != null)
        {
            catAi = cat.GetComponent<CatAi>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        //------------------------------------------------------------
        
        x = Input.GetAxis ("Horizontal");
        am.SetFloat("speed", Abs(x));

        Vector2 direction;
        
        if (Input.GetButtonDown ("Jump") && jump < 2)//if (Input.GetButtonDown ("Jump") && jump < 4)
        {
            jump++;
            //am.SetBool("jump", true);
            rb.velocity = new Vector2 (rb.velocity.x, 5f);
        }
        rb.velocity = new Vector2 (x * speed, rb.velocity.y);
        if (x > 0)
        {
            transform.localScale = new Vector3 (sx, transform.localScale.y, transform.localScale.z);
            //am.SetBool("IsFacingLeft", true);
            //am.SetBool("IsFacingRight", false);
            
            /*// หัน carryPoint ตาม Player
            carryPoint.position = transform.position + new Vector3(0.99f, 0f, 0f); // ห่างจาก Player ไปทางขวา
            // ปิดการหมุนของ carryPoint ให้ไม่หมุนตาม Player
            carryPoint.rotation = Quaternion.identity; // reset rotation
            //carryPoint.position = transform.position + new Vector3();
            carryPoint.rotation = Quaternion.Euler(0, 0, 0); // การหมุนถ้าจำเป็น*/
        }
        if (x < 0)
        {
            transform.localScale = new Vector3 (-sx, transform.localScale.y, transform.localScale.z);
            //am.SetBool("IsFacingRight", true);
            //am.SetBool("IsFacingLeft", false);
            
            /*// หัน carryPoint ตาม Player
            carryPoint.position = transform.position + new Vector3(-0.99f, 0f, 0f); // ห่างจาก Player ไปทางซ้าย
            // ปิดการหมุนของ carryPoint ให้ไม่หมุนตาม Player
            carryPoint.rotation = Quaternion.identity; // reset rotation
            //carryPoint.position = transform.position + new Vector3();
            carryPoint.rotation = Quaternion.Euler(0, 0, 180); // หมุนกลับด้าน*/
        }
        
        
        //-----------------------------------------------------------------------------------------------
        // ตรวจสอบการยก Obj Box เมื่อกดปุ่ม 'E'
        if (Input.GetKeyDown(KeyCode.E) && !isCarrying)
        {
            TryPickup();
        }
        // วาง Obj Box เมื่อกดปุ่ม 'E' อีกครั้ง
        else if (Input.GetKeyDown(KeyCode.E) && isCarrying)
        {
            DropObject();
        }

        // หากกำลังถือ Obj Box ให้อัพเดตตำแหน่งของมันตาม carryPoint
        if (isCarrying && carriedObj != null)
        {
            carriedObj.transform.position = carryPoint.position;
        }
        
        // ตรวจสอบการดึง Box เมื่อกดปุ่ม 'Q'
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!isPulling)
            {
                TryPull(); // ตรวจจับกล่องแล้วเริ่มการดึง
            }
            else
            {
                StopPulling(); // ถ้ากำลังดึงอยู่ให้หยุด
            }
        }
        
        /*if (isPulling && pulledBox != null)
        {
            // ตรวจสอบทิศทางที่ผู้เล่นหันหน้า
            Vector2 targetPosition = new Vector2(transform.position.x + (transform.localScale.x > 0 ? 1 : -1), pulledBox.transform.position.y); //ตรวจสอบตำแหน่ง Box ทุกครั้งที่ดึง
            pulledBox.transform.position = Vector2.MoveTowards(pulledBox.transform.position, targetPosition, Time.deltaTime * speed); // ใช้ความเร็วจากผู้เล่นเพื่อดึงกล่อง
            Debug.Log("Box Position: " + pulledBox.transform.position);
        }*/
        
        if (isPulling && pulledBox != null)
        {
            Vector2 targetPosition = new Vector2(transform.position.x + (transform.localScale.x > 0 ? 1 : -1), pulledBox.transform.position.y); 
            pulledBox.transform.position = Vector2.MoveTowards(pulledBox.transform.position, targetPosition, Time.deltaTime * pushSpeed); // ใช้ความเร็วในการผลัก
            Debug.Log("Box Position: " + pulledBox.transform.position);
        }
        
        
        // ตรวจสอบการเลื่อนสกอร์เม้าส์
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            // ปรับมุมมองตามการเลื่อนสกอร์เม้าส์
            currentLookAngle += scrollInput > 0 ? lookUpAngle : lookDownAngle;
            currentLookAngle = Mathf.Clamp(currentLookAngle, lookDownAngle, lookUpAngle); // จำกัดมุมเพื่อไม่ให้เกิน

            // ปรับตำแหน่งของ Player ตามมุม
            transform.rotation = Quaternion.Euler(0, 0, currentLookAngle);
        }
        
        

        // ตรวจสอบว่าผู้เล่นตกลงไปที่แกน Y ต่ำกว่า -49.3 หรือไม่
        if (transform.position.y < -10f)
        {
            Destroy(gameObject); // ลบผู้เล่น
            PauseGame(); // เรียกใช้ฟังก์ชัน PauseGame
        }

    }
    
    //------------------------------------------------------------------
    void OnCollisionEnter2D(Collision2D coll)
    {
        //am.SetBool("jump", false);
        jump = 0;
        
        //------------------------------------------------------------------
        
        // ตรวจสอบว่า Player ชนกับ Bullet
        if (coll.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Player ชนกับ Bullet!");
            TakeDamage(); // เรียกฟังก์ชันลดเลือด
        }
        
    }
    float Abs(float x)
    {
        return x >= 0f ? x : -x;
    }
    //------------------------------------------------------------------
    
    #region <ชั่วคราว>
    // ฟังก์ชันสำหรับยก Obj Box
    /*void TryPickup()
    {
        RaycastHit2D hit = Physics2D.Raycast(carryPoint.position, carryPoint.right, 1f);
        if (hit.collider != null && hit.collider.CompareTag("Box"))
        {
            carriedObj = hit.collider.gameObject;
            isCarrying = true;
            carriedObj.transform.SetParent(carryPoint); // ทำให้กล่องเป็นลูกของ carryPoint
            Debug.Log("ยกวัตถุขึ้น: " + carriedObj.name);
        }
        else
        {
            Debug.Log("ไม่พบวัตถุที่ชน");
        }
        Debug.DrawRay(carryPoint.position, carryPoint.right * 1f, Color.red, 2f);
        
        //RaycastHit2D hit = Physics2D.Raycast(carryPoint.position, carryPoint.right, 1f);
        if (hit.collider != null && hit.collider.CompareTag("Small Box"))
        {
            carriedObj = hit.collider.gameObject;
            isCarrying = true;
            carriedObj.transform.SetParent(carryPoint); // ทำให้กล่องเป็นลูกของ carryPoint
            Debug.Log("ยกวัตถุขึ้น: " + carriedObj.name);
        }
        else
        {
            Debug.Log("ไม่พบ Small Box ที่ชน");
        }
        Debug.DrawRay(carryPoint.position, carryPoint.right * 1f, Color.red, 2f);
    }

    // ฟังก์ชันสำหรับวาง Obj Box
    void DropObject()
    {
        if (carriedObj != null)
        {
            carriedObj.transform.SetParent(null); // ปลดกล่องออกจาก carryPoint
            carriedObj = null;
            isCarrying = false;
        }
    }*/
    #endregion
    
    // ฟังก์ชันสำหรับยก Obj Box
    void TryPickup()
    {
        RaycastHit2D hit = Physics2D.Raycast(carryPoint.position, carryPoint.right, 1.5f);
        if (hit.collider != null && hit.collider.CompareTag("Box"))
        {
            carriedObj = hit.collider.gameObject;
            isCarrying = true;
            carriedObj.transform.SetParent(carryPoint); // ทำให้กล่องเป็นลูกของ carryPoint
        
            // ปิดการทำงานของ Physics และการชนกัน
            carriedObj.GetComponent<Rigidbody2D>().isKinematic = true;
            carriedObj.GetComponent<Collider2D>().enabled = false;
        
            Debug.Log("ยกวัตถุขึ้น: " + carriedObj.name);
        }
        else
        {
            Debug.Log("ไม่พบวัตถุที่ชน");
        }
        //Debug.DrawRay(carryPoint.position, carryPoint.right * 1.5f, Color.red, 2f); //***
        
        //RaycastHit2D hit = Physics2D.Raycast(carryPoint.position, carryPoint.right, 1f);
        if (hit.collider != null && hit.collider.CompareTag("Small Box"))
        {
            carriedObj = hit.collider.gameObject;
            isCarrying = true;
            carriedObj.transform.SetParent(carryPoint); // ทำให้กล่องเป็นลูกของ carryPoint
            
            // ปิดการทำงานของ Physics และการชนกัน
            carriedObj.GetComponent<Rigidbody2D>().isKinematic = true;
            carriedObj.GetComponent<Collider2D>().enabled = false;
            
            Debug.Log("ยกวัตถุขึ้น: " + carriedObj.name);
        }
        else
        {
            Debug.Log("ไม่พบ Small Box ที่ชน");
        }
        //Debug.DrawRay(carryPoint.position, carryPoint.right * 1.5f, Color.red, 2f); //***
    }

    // ฟังก์ชันสำหรับวาง Obj Box
    void DropObject()
    {
        if (carriedObj != null)
        {
            carriedObj.transform.SetParent(null); // ปลดกล่องออกจาก carryPoint
        
            // เปิดการทำงานของ Physics และการชนกัน
            carriedObj.GetComponent<Rigidbody2D>().isKinematic = false;
            carriedObj.GetComponent<Collider2D>().enabled = true;
        
            carriedObj = null;
            isCarrying = false;
        }
    }
    
    void StopPulling()
    {
        isPulling = false;
        Debug.Log("หยุดดึงกล่อง"); // แสดงข้อความเมื่อหยุดดึงกล่อง
        pulledBox = null; // ล้างข้อมูลกล่องที่กำลังดึง
    }
    
    /*void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Box"))
        {
            pulledBox = collision.gameObject; // เก็บ Box ที่เข้าใกล้
            Debug.Log("Box detected: " + pulledBox.name); // แสดงข้อความเมื่อ Box ถูกตรวจจับ
        }
    }*/

    // ตรวจจับกล่องแล้วเริ่มการดึง
    void TryPull()
    {
        //Vector2 direction = transform.right; // ใช้ transform.right เพื่อให้ Ray หันตามทิศทางที่ตัวละครหัน
        //Vector2 direction = transform.localScale.x > 0 ? Vector2.right : Vector2.left; 
        //Vector2 direction = transform.right;
        
        
        Vector2 direction;
        // ตรวจสอบทิศทางที่ตัวละครหัน
        if (transform.localScale.x > 0) // หันไปทาง +X
        {
            direction = Vector2.right; // ยิงไปทาง +X
        }
        else // หันไปทาง -X
        {
            direction = Vector2.left; // ยิงไปทาง -X
        }
        
        
        //Debug.DrawRay(transform.position, direction, Color.red, 2f); // วาดเรแคสสำหรับการตรวจสอบ //***
        
        Debug.Log("Current LayerMask: " + boxLayerMask.value); 
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, boxLayerMask);
        if (hit.collider != null)
        {
            pulledBox = hit.collider.gameObject; // เก็บ Box ที่ตรวจจับได้
        
            if (hit.collider.CompareTag("Box"))
            {
                StartPulling();
                Debug.Log("พบ Box สำหรับการดึง: " + pulledBox.name);
            }
            else if (hit.collider.CompareTag("Big Box"))
            {
                StartPulling();
                Debug.Log("พบ Big Box สำหรับการผลัก: " + pulledBox.name);
            }
            else
            {
                Debug.Log("ไม่พบ Box หรือ Big Box สำหรับการดึง");
            }
        }
        else
        {
            Debug.Log("ไม่พบวัตถุใด ๆ ที่ถูกเรแคส");
        }
        
        
    }

    void StartPulling()
    {
        if (pulledBox != null)
        {
            isPulling = true;
            pulledBox.layer = LayerMask.NameToLayer("Box"); // เปลี่ยนชื่อ Layer ที่นี่
            Debug.Log("เริ่มดึงกล่อง: " + pulledBox.name);
        }
    }
    
    
    //------------------------------------------------------------------
    // ฟังก์ชันสำหรับลดเลือด
    void TakeDamage()
    {
        if (lives > 0)
        {
            lives--; // ลดค่าเลือด
            UpdateLivesUI(); // อัปเดต UI แสดงผลเลือด
            audioManager.PlaySFX(audioManager.Hp); // เสียงSFX Get Hp

            if (lives <= 0)
            {
                Destroy(gameObject); // ลบ Player ออกจากเกมเมื่อเลือดหมด
                audioManager.PlaySFX(audioManager.End_Over_Enamy_Q_Player); // เสียงSFX Get Hp
                PauseGame();
                Debug.Log("Player Hp < 0");
                
            }
        }
    }

    // ฟังก์ชันอัปเดต UI แสดงผลเลือด
    void UpdateLivesUI()
    {
        // ปิดการแสดงผลของบล็อคสีทีละบล็อค
        for (int i = 0; i < livesImage.Length; i++)
        {
            if (i < lives)
            {
                livesImage[i].SetActive(true); // แสดงบล็อคสีตามจำนวนเลือดที่เหลือ
            }
            else
            {
                livesImage[i].SetActive(false); // ซ่อนบล็อคสีเมื่อเลือดลดลง
            }
        }
    }
    
    private void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        
        if (pausePanel.activeSelf) // ถ้า Panel ถูกเปิดอยู่
        {
            audioManager.StopBackgroundMusic(); // หยุดเสียงเกมโดยใช้ฟังก์ชันของ AudioManager
        }
        
        // ปิด Panel 'About Game System' หรือหากคุณมี reference ของ Panel นี้ให้ปิดมัน
        GameObject aboutGamePanel = GameObject.Find("About Game System"); // เปลี่ยนชื่อให้ตรงกับ Panel ของคุณ
        if (aboutGamePanel != null)
        {
            aboutGamePanel.SetActive(false); // ปิด Panel About Game System
        }
    }
    
    // ฟังก์ชันตรวจจับการชนกับวัตถุอื่น
    void OnTriggerEnter2D(Collider2D collision)
    {
        // ตรวจสอบว่า Player ชนกับ Tag "Trap"
        if (collision.gameObject.CompareTag("Trap"))
        {
            Debug.Log("Player ชนกับ Trap!");
            TakeDamage(); // เรียกฟังก์ชันลดเลือด
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            //Debug.Log("Player ชนกับ Bullet!");
            TakeDamage(); // เรียกฟังก์ชันลดเลือด
        }
    }
    //------------------------------------------------------------------
    
    
}