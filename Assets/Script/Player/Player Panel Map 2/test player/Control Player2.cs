using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlayer2 : MonoBehaviour
{
    [Header("-----------------------Control Player------------------------------")]
    [SerializeField] float moveSpeed;     // ความเร็วในการเคลื่อนที่ของ Player
    [SerializeField] float acceleration; // ความเร่งในการเคลื่อนที่
    [SerializeField] float deceleration; // ความช้าลงเมื่อไม่มี input
    
    private Rigidbody2D rb;          // อ้างอิงถึง Rigidbody2D ของ Player
    private Vector2 movement;        // ทิศทางการเคลื่อนไหวของ Player
    private Vector2 currentVelocity; // ความเร็วปัจจุบันของ Player
    
    [SerializeField] float rotationSpeed; // ความเร็วในการหมุน
    private float currentRotationZ;  // มุม Z ปัจจุบัน
    private bool isLookingUp = false; // สถานะการหมุนขึ้น
    private bool isLookingRight = false; // สถานะการหมุนไปทางขวา
    private bool isLookingLeft = false;  // สถานะการหมุนไปทางซ้าย
    
    
    [Header("-----------------------Map Boundaries------------------------------")]
    [SerializeField] float minX;  // ขอบเขตด้านซ้ายของแผนที่
    [SerializeField] float maxX;  // ขอบเขตด้านขวาของแผนที่
    [SerializeField] float minY;  // ขอบเขตด้านล่างของแผนที่
    [SerializeField] float maxY;  // ขอบเขตด้านบนของแผนที่
    
    [Header("-----------------------Next panel---------------------------------------")]
    [SerializeField] GameObject map2Panel; // อ้างอิงถึง Panel Map 2
    [SerializeField] GameObject map3Panel; // อ้างอิงถึง Panel Map 3
    
    [SerializeField] Vector2 startingPositionMap3; // ตำแหน่งเริ่มต้นใน Map 3
    [SerializeField] GameObject enemySpawner; // อ้างอิงถึง Enemy Spawner
    
    [Header("-----------------------Magic Fish------------------------------")]
    [SerializeField] GameObject magicFish; // อ้างอิงถึง Magic Fish
    
    [Header("-----------------------HP Player------------------------------")]
    //UI Hp 
    [SerializeField] int lives; // เลือดของ Player
    public GameObject[] livesImage; // บล็อคสีแสดงจำนวนเลือด
    [SerializeField] GameObject hpCanvas; // อ้างอิงถึง Canvas HP Player

    public bool isPlayerControllable = true;
    
    
    [Header("-----------------------Player Sprites------------------------------")]
    public Sprite[] walkingSprites; // อาเรย์ของสปริงที่ใช้เมื่อเดิน
    public Sprite idleSprite; // สปริงตั้งต้นเมื่อหยุดเดิน

    public SpriteRenderer spriteRenderer; // อ้างอิงถึง SpriteRenderer
    private bool isWalking = false;
    private int currentSpriteIndex = 0;
    [SerializeField] private float spriteChangeInterval; // ระยะเวลาในการเปลี่ยน SPR
    
    

    // Start is called before the first frame updateต
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // เข้าถึง Rigidbody2D ของ Player
        map2Panel.SetActive(false);        // ทำให้ Panel Map 2 ปิดในตอนเริ่มต้น
        magicFish.SetActive(false);        // ทำให้ obj Magic Fish ปิดในตอนเริ่มต้น
        enemySpawner.SetActive(false);     // ทำให้ Enemy Spawner ปิดในตอนเริ่มต้น
        hpCanvas.SetActive(true);          // เปิด Canvas HP Player เมื่อ Player ถูกสร้างขึ้น
        
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on this GameObject!");
        }

        if (idleSprite == null)
        {
            Debug.LogError("Idle sprite is not assigned in the Inspector!");
        }

        spriteRenderer.sprite = idleSprite; // ตั้งค่าตั้งต้นให้เป็นสปริงเมื่อหยุด
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerControllable)
            return;
        
        // รับค่าการเคลื่อนไหวจากปุ่ม WASD หรือปุ่มลูกศร
        //movement.x = Input.GetAxis("Horizontal");  // รับค่าการเคลื่อนไหวแกน X (A/D)
        //movement.y = Input.GetAxis("Vertical");    // รับค่าการเคลื่อนไหวแกน Y (W/S)
        
        
        float horizontalInput = Input.GetAxis("Horizontal"); // รับค่าการเคลื่อนที่ในแนวนอน
        float verticalInput = Input.GetAxis("Vertical"); // รับค่าการเคลื่อนที่ในแนวตั้ง
        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        if (movement.magnitude > 0)
        {
            isWalking = true;
            transform.Translate(movement.normalized * moveSpeed * Time.deltaTime);

            // เริ่ม Coroutine เพื่อเปลี่ยน SPR
            if (!IsInvoking("UpdateSprite"))
            {
                InvokeRepeating("UpdateSprite", 0f, spriteChangeInterval);
            }
        }
        else
        {
            isWalking = false;
            spriteRenderer.sprite = idleSprite; // เปลี่ยนกลับเป็นสปริงตั้งต้น
            CancelInvoke("UpdateSprite"); // หยุดการเปลี่ยน SPR
        }
        
        
        
        // เช็คสถานะของ Panel Map 2 ในทุกเฟรม
        if (!map2Panel.activeSelf && !map3Panel.activeSelf) // เปิด Panel Map 2 เฉพาะเมื่อทั้งสอง Panel ปิดอยู่
        {
            // บังคับให้เปิด Panel Map 2
            map2Panel.SetActive(true);
           // Debug.Log("เปิด Panel Map 2");
        }
        else if (map2Panel.activeSelf) // ถ้า Panel Map 2 เปิดอยู่
        {
            magicFish.SetActive(true); // เปิด obj Magic Fish
        }
        else // ถ้า Panel Map 2 ปิด
        {
            magicFish.SetActive(false); // ปิด obj Magic Fish
        }
        
        
    }
    
    
    void UpdateSprite()
    {
        if (isWalking)
        {
            if (walkingSprites.Length == 0)
            {
                Debug.LogError("Walking sprites array is empty!");
                return; // ออกถ้าอาเรย์ว่าง
            }

            currentSpriteIndex = (currentSpriteIndex + 1) % walkingSprites.Length; // เปลี่ยนสปริง
            spriteRenderer.sprite = walkingSprites[currentSpriteIndex];
        }
    }

    
    
    
    void FixedUpdate()
    {
        // คำนวณตำแหน่งใหม่
        Vector2 newPosition = rb.position + movement * moveSpeed * Time.fixedDeltaTime;

        // จำกัดการเคลื่อนที่ของ Player ให้อยู่ในขอบเขตแผนที่
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        // ทำให้การเคลื่อนที่สมูทด้วย Lerp
        rb.MovePosition(Vector2.Lerp(rb.position, newPosition, Time.fixedDeltaTime * acceleration));
    }
    
    // ฟังก์ชันที่ใช้ตรวจสอบการชนกับวัตถุที่มี tag "Next panel"
    void OnTriggerEnter2D(Collider2D other)
    {
        // ตรวจสอบว่า Player ชนกับวัตถุที่มี tag "Next panel"
        if (other.CompareTag("Next panel"))
        {
            Debug.Log("Player ชนกับ Next panel");  // Debug เพื่อตรวจสอบการชน

            // ปิด Panel Map 2
            if (map2Panel.activeSelf) 
            {
                map2Panel.SetActive(false);
                magicFish.SetActive(false); // ปิด obj Magic Fish เมื่อ Panel Map 2 ถูกปิด
                Debug.Log("ปิด Panel Map 2");
            }

            // เปิด Panel Map 3
            if (!map3Panel.activeSelf) 
            {
                map3Panel.SetActive(true);
                Debug.Log("เปิด Panel Map 3");

                // เปิด Enemy Spawner เมื่อ Panel Map 3 ถูกเปิด
                enemySpawner.SetActive(true);
            }

            // ตั้งค่าตำแหน่ง Player ให้เริ่มต้นในตำแหน่งที่กำหนดใน Map 3
            rb.position = startingPositionMap3;
        }

        // เมื่อ Player ชนกับวัตถุที่ไม่มี tag
        if (other.CompareTag("Untagged"))
        {
            Debug.Log("ชนกับวัตถุที่มี tag 'Untagged'");
            movement = Vector2.zero; // หยุดการเคลื่อนที่
        }
        
        
        // ตรวจสอบว่า Player ชนกับ Bullet
        if (other.gameObject.CompareTag("BulletEnemy"))
        {
            //Debug.Log("Player ชนกับ Bullet!");
            TakeDamage(); // เรียกฟังก์ชันลดเลือด
        }
        
        // ตรวจสอบว่า Player ชนกับ Brick
        if (other.gameObject.CompareTag("Brick"))
        {
            //Debug.Log("Player ชนกับ Brick!");
            TakeDamage(); // เรียกฟังก์ชันลดเลือด
        }
        
    }
    
    
    // ฟังก์ชันสำหรับลดเลือด
    void TakeDamage()
    {
        if (lives > 0)
        {
            lives--; // ลดค่าเลือด
            UpdateLivesUI(); // อัปเดต UI แสดงผลเลือด

            if (lives <= 0)
            {
                Destroy(gameObject); // ลบ Player ออกจากเกมเมื่อเลือดหมด
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

    
    
    
}
