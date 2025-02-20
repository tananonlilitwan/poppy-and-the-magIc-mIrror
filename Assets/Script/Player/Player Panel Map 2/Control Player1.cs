using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ControlPlayer1 : MonoBehaviour
{
    private bool isMap2Closed = false; // เก็บสถานะว่า Map 2 ถูกปิดแล้วหรือยัง
    
    private Vector2 targetPosition; // Declare targetPosition

    public LayerMask obstacleLayer;
    public float balanceForce; // แรงในการรักษาสมดุล
    
    [Header("-----------------------Control Player------------------------------")]
    [SerializeField] float moveSpeed;     // ความเร็วในการเคลื่อนที่ของ Player
    [SerializeField] float acceleration; // ความเร่งในการเคลื่อนที่
    [SerializeField] float deceleration; // ความช้าลงเมื่อไม่มี input
    
    [SerializeField] float swimAmplitude = 0.2f; // ความสูงของการเคลื่อนไหวขึ้นและลง
    [SerializeField] float swimFrequency = 2f; // ความถี่ในการเคลื่อนไหว
    
    
    private Rigidbody2D rb;          // อ้างอิงถึง Rigidbody2D ของ Player
    private Vector2 movement;        // ทิศทางการเคลื่อนไหวของ Player
    private Vector2 currentVelocity; // ความเร็วปัจจุบันของ Player
    
    private float lookUpAngle = 16f; // โยกขึ้น
    private float lookDownAngle = -16f; // โยกลง

    private float lookUp = -74.353f;  // เปลี่ยนเป็น -74.353f
    private float lookDown = -106.351f; // เปลี่ยนเป็น -106.351f

    [SerializeField] float rotationSpeed; // ความเร็วในการหมุน
    private float currentRotationZ = 0f; // มุม Z ปัจจุบัน
    private float originalRotationZ; // มุม Z ต้นฉบับ
    private bool isLookingUp = false; // สถานะการหมุนขึ้น
    private bool isLookingRight = false; // สถานะการหมุนไปทางขวา
    private bool isLookingLeft = false;  // สถานะการหมุนไปทางซ้าย
    
    [Header("-----------------------Sprite Renderer------------------------------")]
    //[SerializeField] SpriteRenderer spriteRendererrr; // อ้างอิงถึง SpriteRenderer
    
    
    [Header("-----------------------Map Boundaries------------------------------")]
    [SerializeField] float minX;  // ขอบเขตด้านซ้ายของแผนที่
    [SerializeField] float maxX;  // ขอบเขตด้านขวาของแผนที่
    [SerializeField] float minY;  // ขอบเขตด้านล่างของแผนที่
    [SerializeField] float maxY;  // ขอบเขตด้านบนของแผนที่
    
    [Header("-----------------------Next panel---------------------------------------")]
    [SerializeField] GameObject map2Panel; // อ้างอิงถึง Panel Map 2
    [SerializeField] GameObject map3Panel; // อ้างอิงถึง Panel Map 3
    [SerializeField] GameObject map4Panel; // อ้างอิงถึง Panel Map 4
    [SerializeField] GameObject map6Panel; // อ้างอิงถึง Panel Map 6
    [SerializeField] GameObject map7Panel; // อ้างอิงถึง Panel Map 7
    [SerializeField] GameObject map8Panel; // อ้างอิงถึง Panel Map 8
    [SerializeField] GameObject map9Panel; // อ้างอิงถึง Panel Map 9
    [SerializeField] GameObject map10Panel; // อ้างอิงถึง Panel Map 10    
    [SerializeField] GameObject map11Panel; // อ้างอิงถึง Panel Map 11
    
    [SerializeField] Vector2 startingPositionMap3; // ตำแหน่งเริ่มต้นใน Map 3
    [SerializeField] Vector2 startingPositionMap4; // ตำแหน่งเริ่มต้นใน Map 4
    [SerializeField] Vector2 startingPositionMap6; // ตำแหน่งเริ่มต้นใน Map 6
    [SerializeField] Vector2 startingPositionMap7; // ตำแหน่งเริ่มต้นใน Map 7
    [SerializeField] Vector2 startingPositionMap8; // ตำแหน่งเริ่มต้นใน Map 8
    [SerializeField] Vector2 startingPositionMap9; // ตำแหน่งเริ่มต้นใน Map 9
    [SerializeField] Vector2 startingPositionMap10; // ตำแหน่งเริ่มต้นใน Map 10
    [SerializeField] Vector2 startingPositionMap11; // ตำแหน่งเริ่มต้นใน Map 11
    
    [SerializeField] GameObject enemySpawner; // อ้างอิงถึง Enemy Spawner
    
    [Header(" --------------------Map 5 Settings-----------------------------------")]
    [SerializeField] GameObject map5Panel; // อ้างอิงถึง Panel Map 5
    [SerializeField] Vector2 startingPositionMap5; // ตำแหน่งเริ่มต้นใน Map 5

    [Header("-----------------------Map 5 Boundaries------------------------------")]
    [SerializeField] float minX5;  // ขอบเขตด้านซ้ายของแผนที่
    [SerializeField] float maxX5;  // ขอบเขตด้านขวาของแผนที่
    [SerializeField] float minY5;  // ขอบเขตด้านล่างของแผนที่
    [SerializeField] float maxY5;  // ขอบเขตด้านบนของแผนที่

    private GameObject player; // อ้างอิงถึงตัวละคร (Player)
    private bool isMap5Active = false; // สถานะการเปิดของ Map 5
    
    
    
    [Header("-----------------------Magic Fish------------------------------")]
    [SerializeField] GameObject magicFish; // อ้างอิงถึง Magic Fish
    [SerializeField] GameObject Warden; // อ้างอิงถึง Warden
   
    
    
    [Header("-----------------------Player Sprites------------------------------")]
    public Sprite[] walkingSprites; // อาเรย์ของสปริงที่ใช้เมื่อเดิน
    public Sprite idleSprite; // สปริงตั้งต้นเมื่อหยุดเดิน

    public SpriteRenderer spriteRenderer; // อ้างอิงถึง SpriteRenderer
    private bool isWalking = false;
    private int currentSpriteIndex = 0;
    [SerializeField] private float spriteChangeInterval; // ระยะเวลาในการเปลี่ยน SPR
    
    [Header("-----------------------Dialog Map 3------------------------------")]
    public GameObject bgDialog; // อ้างอิง BG Dialog
    public TextMeshProUGUI textDialog1; // อ้างอิง Text Dialog 1
    public TextMeshProUGUI textDialog2; // อ้างอิง Text Dialog 2
    public TextMeshProUGUI textDialog3; // อ้างอิง Text Dialog 3
    public TextMeshProUGUI textDialog4; // อ้างอิง Text Dialog 4
    public TextMeshProUGUI textDialog5; // อ้างอิง Text Dialog 5
    public float dialogDisplayTime = 3f; // เวลาในการแสดง Dialog

    [SerializeField] private string dialog1Text; // ประโยค Dialog 1
    [SerializeField] private string dialog2Text; // ประโยค Dialog 2
    [SerializeField] private string dialog3Text; // ประโยค Dialog 3
    [SerializeField] private string dialog4Text; // ประโยค Dialog 4
    [SerializeField] private string dialog5Text; // ประโยค Dialog 5
    
    [Header("-----------------------Dialog Map 4------------------------------")]
    public GameObject bgDialog4; // อ้างอิง BG Dialog
    public TextMeshProUGUI textDialog41; // อ้างอิง Text Dialog 1
    public TextMeshProUGUI textDialog42; // อ้างอิง Text Dialog 2
    public TextMeshProUGUI textDialog43; // อ้างอิง Text Dialog 3
    public TextMeshProUGUI textDialog44; // อ้างอิง Text Dialog 4
    public TextMeshProUGUI textDialog45; // อ้างอิง Text Dialog 5
    public float dialogDisplayTimes = 3f; // เวลาในการแสดง Dialog

    [SerializeField] private string dialog41Text; // ประโยค Dialog 1
    [SerializeField] private string dialog42Text; // ประโยค Dialog 2
    [SerializeField] private string dialog43Text; // ประโยค Dialog 3
    [SerializeField] private string dialog44Text; // ประโยค Dialog 4
    [SerializeField] private string dialog45Text; // ประโยค Dialog 5
    
    [Header("-----------------------Mana Station------------------------------")]
    [SerializeField] GameObject manaStation; // อ้างอิงถึง Mana Station
    [SerializeField] GameObject ObjectMana; // เปิด UI มานา
    
    [Header("-----------------------Player HP------------------------------")]
    [SerializeField] private int maxLives = 5; // จำนวนเลือดสูงสุด (บล็อค)
    [SerializeField] private int currentLives; // เลือดปัจจุบัน (บล็อค)
    public GameObject[] livesImage; // บล็อคสีแสดงจำนวนเลือด
    [SerializeField] private GameObject hpCanvas; // อ้างอิงถึง Canvas HP Player
    private bool isHealing = false; // ตรวจสอบว่า Player อยู่ในจุดเติมเลือดหรือไม่
    private Coroutine healingCoroutine; // จัดการกับ Coroutine ของการเติมเลือด
    
    public bool isPlayerControllable = true;
    
    [Header("-----------------------HP Station------------------------------")]
    [SerializeField] GameObject HpStation; // อ้างอิงถึง HP Station
    [SerializeField] GameObject HpStationMap5; // อ้างอิงถึง HP Station
    
    [Header("-----------------------UI lose ------------------------------")]
    [SerializeField] private GameObject losePanel;  // Panel สำหรับแสดง UI lose
    
    [SerializeField] private GameObject poseidonObject; // Obj Poseidon ที่จะเปิด
    [SerializeField] private GameObject BossObject; // Obj Poseidon ที่จะเปิด
    [SerializeField] private GameObject BossObject2; // Obj Poseidon ที่จะเปิด
    
    [Header("---------------------- Cut Scene Map 10 ------------------------------")]
    /*public Image cutsceneImage; // รูปที่จะแสดงใน Cutscene
    public Image dialogBG; // พื้นหลังไดอะล็อก
    public TMP_Text dialogText1; // ข้อความไดอะล็อก 1
    public TMP_Text dialogText2; // ข้อความไดอะล็อก 2
    public float fadeDuration = 1f; // ระยะเวลาในการค่อยๆ แสดงผล
    public float dialogDisplayTimesss = 3f; // เวลาการแสดงข้อความแต่ละข้อความ
    private bool isCutsceneActive = false;*/
    public Image cutsceneImage; // รูปที่จะแสดงใน Cutscene
    public Image dialogBG; // พื้นหลังไดอะล็อก
    public TMP_Text dialogText1; // ข้อความไดอะล็อก 1 (TextMeshPro)
    public TMP_Text dialogText2; // ข้อความไดอะล็อก 2 (TextMeshPro)
    public float fadeDuration = 1f; // ระยะเวลาในการค่อยๆ แสดงผล
    public float dialogDisplayTimesss = 3f; // เวลาการแสดงข้อความแต่ละข้อความ
    private bool isCutsceneActive = false;
    public ControlPlayer1 playerController; // อ้างอิงไปยัง PlayerController
    
    [SerializeField] GameObject Poseidondialog;
    
    
    private AudioManager audioManager; // เสียงในเกม
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // เสียงในเกม
       
    }
    // Start is called before the first frame updateต
    void Start()
    {
        //map6Panel.SetActive(true);
        
        audioManager = FindObjectOfType<AudioManager>(); // ค้นหา AudioManager ในซีน
        
        rb = GetComponent<Rigidbody2D>();  // เข้าถึง Rigidbody2D ของ Player
        map2Panel.SetActive(true);        // ทำให้ Panel Map 2 ปิดในตอนเริ่มต้น
        magicFish.SetActive(true);        // ทำให้ obj Magic Fish ปิดในตอนเริ่มต้น
        enemySpawner.SetActive(false);     // ทำให้ Enemy Spawner ปิดในตอนเริ่มต้น
        hpCanvas.SetActive(true);          // เปิด Canvas HP Player เมื่อ Player ถูกสร้างขึ้น
        originalRotationZ = transform.eulerAngles.z; // บันทึกมุมเริ่มต้น
        Warden.SetActive(false);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = idleSprite; // ตั้งค่าตั้งต้นให้เป็นสปริงเมื่อหยุด
        
        bgDialog.SetActive(false); // ซ่อน BG Dialog ในตอนเริ่มต้น
        
        bgDialog4.SetActive(false);
        poseidonObject.SetActive(false);
        BossObject.SetActive(false);
        BossObject2.SetActive(false);
        currentLives = maxLives; // กำหนดเลือดเริ่มต้น
        UpdateHealthUI();
        Poseidondialog.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlayerControllable)
            return;
        
        // รับค่าการเคลื่อนไหวจากปุ่ม WASD หรือปุ่มลูกศร
        movement.x = Input.GetAxis("Horizontal");  // รับค่าการเคลื่อนไหวแกน X (A/D)
        movement.y = Input.GetAxis("Vertical");    // รับค่าการเคลื่อนไหวแกน Y (W/S)
        
        // Flip สไปรท์ตามทิศทางการเคลื่อนไหวในแกน X
        if (movement.x > 0)
        {
            spriteRenderer.flipX = false;  // ถ้าเคลื่อนที่ไปขวา ไม่สลับสไปรท์
        }
        else if (movement.x < 0)
        {
            spriteRenderer.flipX = true;   // ถ้าเคลื่อนที่ไปซ้าย สลับสไปรท์
        }
        
        
        // Animation
        if (movement.magnitude > 0)
        {
            // ถ้า player กำลังเคลื่อนไหว ให้เปลี่ยนสถานะ isWalking เป็น true
            if (!isWalking)
            {
                isWalking = true;
                
                // เริ่ม Coroutine เพื่อเปลี่ยน SPR
                if (!IsInvoking("UpdateSprite"))
                {
                    InvokeRepeating("UpdateSprite", 0f, spriteChangeInterval);
                }
            }
        }
        else
        {
            // ถ้า player หยุดเคลื่อนไหว
            if (isWalking)
            {
                isWalking = false;
                
                // เปลี่ยนกลับเป็นสปริงตั้งต้น
                spriteRenderer.sprite = idleSprite;
                
                // หยุดการเปลี่ยน SPR
                CancelInvoke("UpdateSprite");
            }
        }
        
        //---------------------------
        
        targetPosition = rb.position + movement * moveSpeed * Time.deltaTime;
        
        // ตรวจสอบว่ามี Obstacle อยู่ข้างหน้า
        if (!IsObstacleInWay(targetPosition))
        {
            rb.MovePosition(targetPosition);
        }
        
        //----------------------------------------------
        
        // ตรวจสอบสถานะของ map5Panel ถึง map11Panel
        if (map5Panel.activeSelf || map6Panel.activeSelf || map7Panel.activeSelf || 
            map8Panel.activeSelf || map9Panel.activeSelf || map10Panel.activeSelf || 
            map11Panel.activeSelf)
        {
            // ถ้าอย่างน้อยหนึ่งใน Panel เปิดอยู่ เปลี่ยน Rigidbody2D เป็น Dynamic
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {
            // ถ้าไม่มี Panel ใด ๆ เปิดอยู่ เปลี่ยน Rigidbody2D กลับเป็น Kinematic
            rb.bodyType = RigidbodyType2D.Kinematic;
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
        if (!isPlayerControllable)
            return;
        
        // คำนวณตำแหน่งใหม่
        Vector2 newPosition = rb.position + movement * moveSpeed * Time.fixedDeltaTime;
        
        // ถ้า Player ไม่ได้อยู่ใน map5Panel ถึง map11Panel ให้ใช้การจำกัดขอบเขตตามปกติ
        if (!(map5Panel.activeSelf || map6Panel.activeSelf || map7Panel.activeSelf ||
              map8Panel.activeSelf || map9Panel.activeSelf || map10Panel.activeSelf))
        {
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        }
        
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
            
            // ปิด Panel Map 2 ถ้าเปิดอยู่  // ปิด Panel Map 2 ถ้าเปิดอยู่ และยังไม่เคยปิดมาก่อน
            if (map2Panel.activeSelf && !isMap2Closed)//if (map2Panel.activeSelf) 
            {
                map2Panel.SetActive(false);
                magicFish.SetActive(false); // ปิด obj Magic Fish เมื่อ Panel Map 2 ถูกปิด
                isMap2Closed = true; // ตั้งค่าเป็น true เพื่อบันทึกสถานะว่าปิด Map 2 แล้ว
                Debug.Log("ปิด Panel Map 2");

                // เปิด Panel Map 3
                if (!map3Panel.activeSelf) 
                {
                    map3Panel.SetActive(true);
                    Debug.Log("เปิด Panel Map 3");
                    
                    PlayBackgroundMusic(audioManager.map3Music); // เล่นเพลง Map 3
                    
                    // เปิด Mana Station เมื่อ Map 3 เปิด
                    manaStation.SetActive(true); // เปิด obj Mana Station
                    ObjectMana.SetActive(true); // เปิด UI มานา

                    // ตั้งค่าตำแหน่ง Player ให้เริ่มต้นในตำแหน่งที่กำหนดใน Map 3
                    rb.position = startingPositionMap3;

                    // เรียกใช้ฟังก์ชันแสดงไดอะลอค
                    StartCoroutine(ShowDialogsAndEnableSpawner());

                    // return เพื่อหยุดไม่ให้ดำเนินการต่อไปเปิด Panel Map 4 ทันที
                    return; // หยุดการทำงานที่นี้เพื่อป้องกันการข้ามไปเปิด Map ต่อไป
                }
            }

            // ปิด Panel Map 3 ถ้าเปิดอยู่ และเปิด Panel Map 4
            if (map3Panel.activeSelf) 
            {
                map3Panel.SetActive(false);
                Debug.Log("ปิด Panel Map 3");

                PlayBackgroundMusic(audioManager.map4Music); // เล่นเพลง Map 4
                
                // เปิด Panel Map 4
                map4Panel.SetActive(true);
                Debug.Log("เปิด Panel Map 4");
                if (HpStation != null)
                {
                    HpStation.SetActive(true); // เปิด HP Station
                }
                
                // เรียกใช้ฟังก์ชันแสดงไดอะลอคของ Map 4
                StartCoroutine(ShowDialogMap4());

                // ตั้งค่าตำแหน่ง Player ให้เริ่มต้นในตำแหน่งที่กำหนดใน Map 4
                rb.position = startingPositionMap4;
            }
            
            // ปิด Panel Map 4 และเปิด Panel Map 5
            else if (map4Panel.activeSelf) 
            {
                map4Panel.SetActive(false);
                Debug.Log("ปิด Panel Map 4");
                
                PlayBackgroundMusic(audioManager.map5Music); // เล่นเพลง Map 4
                
                map5Panel.SetActive(true);
                Debug.Log("เปิด Panel Map 5");
                //rb.position = startingPositionMap5;
                
                HpStation.SetActive(false); // เปิด HP Station
                manaStation.SetActive(false); // เปิด obj Mana Station
                
                // เปลี่ยน Rigidbody2D เป็น Dynamic
                rb.bodyType = RigidbodyType2D.Dynamic; // เปลี่ยนประเภทเป็น Dynamic

                // เริ่มต้นตำแหน่ง Player ใน Map 5 ถ้าจำเป็น
                rb.position = startingPositionMap5; // กำหนดตำแหน่ง Player ใน Map 5
                
                return;
            }
            
            // ปิด Panel Map 5 และเปิด Panel Map 6
            else if (map5Panel.activeSelf) 
            {
                map5Panel.SetActive(false);
                Debug.Log("ปิด Panel Map 5");

                PlayBackgroundMusic(audioManager.map6Music); // เล่นเพลง Map 4
                
                map6Panel.SetActive(true);
                Debug.Log("เปิด Panel Map 6");
                
                // เปลี่ยน Rigidbody2D เป็น Dynamic
                rb.bodyType = RigidbodyType2D.Dynamic; // เปลี่ยนประเภทเป็น Dynamic
                
                rb.position = startingPositionMap6;

                return;
            }

            // ปิด Panel Map 6 และเปิด Panel Map 7
            else if (map6Panel.activeSelf) 
            {
                map6Panel.SetActive(false);
                Debug.Log("ปิด Panel Map 6");

                PlayBackgroundMusic(audioManager.map7Music); // เล่นเพลง Map 4
                
                map7Panel.SetActive(true);
                Debug.Log("เปิด Panel Map 7");
                BossObject.SetActive(true); // Boss 1
                
                // เปลี่ยน Rigidbody2D เป็น Dynamic
                rb.bodyType = RigidbodyType2D.Dynamic; // เปลี่ยนประเภทเป็น Dynamic
                
                rb.position = startingPositionMap7;

                return;
            }
            
            else if (map7Panel.activeSelf) 
            {
                map7Panel.SetActive(false);
                Debug.Log("ปิด Panel Map 7");
                
                PlayBackgroundMusic(audioManager.map8Music); // เล่นเพลง Map 4
                
                map8Panel.SetActive(true);
                Debug.Log("เปิด Panel Map 8");
                
                // เปลี่ยน Rigidbody2D เป็น Dynamic
                rb.bodyType = RigidbodyType2D.Dynamic; // เปลี่ยนประเภทเป็น Dynamic
                
                rb.position = startingPositionMap8;
                
                return;
            }
            
            else if (map8Panel.activeSelf) 
            {
                map8Panel.SetActive(false);
                Debug.Log("ปิด Panel Map 8");
                
                PlayBackgroundMusic(audioManager.map9Music); // เล่นเพลง Map 4
                
                map9Panel.SetActive(true);
                Debug.Log("เปิด Panel Map 9");
                BossObject2.SetActive(true); // Boss 2
                // เปลี่ยน Rigidbody2D เป็น Dynamic
                rb.bodyType = RigidbodyType2D.Dynamic; // เปลี่ยนประเภทเป็น Dynamic
                
                rb.position = startingPositionMap9;
                
                return;
            }
            
            else if (map9Panel.activeSelf) 
            {
                map9Panel.SetActive(false);
                Debug.Log("ปิด Panel Map 9");

                /*isCutsceneActive = true; // เริ่ม Cutscene
                StartCoroutine(PlayCutscene());
                */

                playerController.enabled = false; // ปิดการควบคุม Player
                StartCoroutine(StartCutsceneAndSwitchMap()); // เรียกใช้ Cutscene และรอให้จบก่อนเปลี่ยนแผนที่
                
                /*map10Panel.SetActive(true);
                Debug.Log("เปิด Panel Map 10");
                
                // เปลี่ยน Rigidbody2D เป็น Dynamic
                rb.bodyType = RigidbodyType2D.Dynamic; // เปลี่ยนประเภทเป็น Dynamic
                
                rb.position = startingPositionMap10;*/
                
                return;
            }
            
            else if (map10Panel.activeSelf) 
            {
                map10Panel.SetActive(false);
                Debug.Log("ปิด Panel Map 10");
                
                map11Panel.SetActive(true);
                Debug.Log("เปิด Panel Map 11");
                
                PlayBackgroundMusic(audioManager.map11Music); // เล่นเพลง Map 4
                
                Debug.Log("Map 11 เปิดแล้ว, กำลังเปิด Obj Poseidon");
                poseidonObject.SetActive(true);
                
                rb.position = startingPositionMap11;
                
                return;
            }
            
            if (other.CompareTag("Untagged"))
            {
                // หยุดการเคลื่อนที่หรือทำการย้อนตำแหน่ง Player เพื่อไม่ให้ทะลุผ่าน
                Debug.Log("ชนกับวัตถุที่มี tag 'Untagged'");
                movement = Vector2.zero; // ทำให้การเคลื่อนที่เป็น 0 เมื่อชน
            }
        }
        
        // ตรวจสอบว่า Player ชนกับ Brick
        if (other.gameObject.CompareTag("Brick"))
        {
            //Debug.Log("Player ชนกับ Brick!");
            TakeDamage(1); // เรียกฟังก์ชันลดเลือด
            Destroy(other.gameObject); // ทำลายกระสุน
        }
        if (other.CompareTag("BulletEnemy"))
        {
            TakeDamage(1); // ลดเลือดลง 1 บล็อคเมื่อโดน BulletEnemy
            Destroy(other.gameObject); // ทำลายกระสุน
        }
        else if (other.CompareTag("HPStation") && !isHealing)
        {
            // เริ่มการเติมเลือดเมื่อยืนที่จุด HPStation
            isHealing = true;
            healingCoroutine = StartCoroutine(HealOverTime(1, 1f)); // เติม HP 1 จุดทุก 1 วินาที
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HealthPickup") && isHealing)
        {
            // หยุดการเติมเลือดเมื่อออกจากจุด HealthPickup
            isHealing = false;
            if (healingCoroutine != null)
            {
                StopCoroutine(healingCoroutine);
            }
        }
    }
    
    private IEnumerator ShowDialogsAndEnableSpawner()
    {
        audioManager.PlaySFX(audioManager. CutScene); // เสียง Player ตาย
        Debug.Log("เริ่มแสดงไดอะลอค"); // เพิ่ม log เพื่อดูว่าเริ่มทำงานหรือไม่
        isPlayerControllable = false; // ห้ามควบคุม Player ระหว่างการแสดงไดอะลอค
        bgDialog.SetActive(true); // แสดง BG Dialog
        
        // ตั้งค่าตำแหน่งผู้เล่นก่อนเริ่มไดอะลอค
        rb.position = startingPositionMap3;

        // แสดงไดอะลอคแต่ละประโยค
        textDialog1.text = dialog1Text;
        textDialog1.gameObject.SetActive(true); // เปิด Text Dialog 1
        Debug.Log("แสดงข้อความ: " + dialog1Text);
        yield return new WaitForSeconds(dialogDisplayTime);
        textDialog1.gameObject.SetActive(false); // ปิด Text Dialog 1

        textDialog2.text = dialog2Text;
        textDialog2.gameObject.SetActive(true); // เปิด Text Dialog 2
        Debug.Log("แสดงข้อความ: " + dialog2Text);
        yield return new WaitForSeconds(dialogDisplayTime);
        textDialog2.gameObject.SetActive(false); // ปิด Text Dialog 2

        textDialog3.text = dialog3Text;
        textDialog3.gameObject.SetActive(true); // เปิด Text Dialog 3
        Debug.Log("แสดงข้อความ: " + dialog3Text);
        yield return new WaitForSeconds(dialogDisplayTime);
        textDialog3.gameObject.SetActive(false); // ปิด Text Dialog 3

        textDialog4.text = dialog4Text;
        textDialog4.gameObject.SetActive(true); // เปิด Text Dialog 4
        Debug.Log("แสดงข้อความ: " + dialog4Text);
        yield return new WaitForSeconds(dialogDisplayTime);
        textDialog4.gameObject.SetActive(false); // ปิด Text Dialog 4

        textDialog5.text = dialog5Text;
        textDialog5.gameObject.SetActive(true); // เปิด Text Dialog 5
        Debug.Log("แสดงข้อความ: " + dialog5Text);
        yield return new WaitForSeconds(dialogDisplayTime);
        textDialog5.gameObject.SetActive(false); // ปิด Text Dialog 5

        bgDialog.SetActive(false); // ซ่อน BG Dialog หลังจากแสดงเสร็จ
        Debug.Log("ปิด BG Dialog");
        
        
        isPlayerControllable = true; // เปิดให้ควบคุม Player ได้อีกครั้ง
        // เปิด Enemy Spawner เมื่อไดอะลอคเสร็จ
        yield return new WaitForSeconds(3f); // รอ 3 วินาทีก่อนเปิด Enemy Spawner
        enemySpawner.SetActive(true); // เปิด Enemy Spawner
    }
    
    private IEnumerator ShowDialogMap4()
    {
        Debug.Log("เริ่มแสดงไดอะลอค Map 4");
        isPlayerControllable = false; // ปิดการควบคุม Player ขณะการแสดงไดอะลอค
        bgDialog4.SetActive(true);    // แสดง BG Dialog Map 4
    
        // แสดงไดอะลอคแต่ละประโยค
        textDialog41.text = dialog41Text;
        textDialog41.gameObject.SetActive(true);
        Debug.Log("แสดงข้อความ: " + dialog41Text);
        yield return new WaitForSeconds(dialogDisplayTimes);
        textDialog41.gameObject.SetActive(false);
    
        textDialog42.text = dialog42Text;
        textDialog42.gameObject.SetActive(true);
        Debug.Log("แสดงข้อความ: " + dialog42Text);
        yield return new WaitForSeconds(dialogDisplayTimes);
        textDialog42.gameObject.SetActive(false);
    
        textDialog43.text = dialog43Text;
        textDialog43.gameObject.SetActive(true);
        Debug.Log("แสดงข้อความ: " + dialog43Text);
        yield return new WaitForSeconds(dialogDisplayTimes);
        textDialog43.gameObject.SetActive(false);
    
        textDialog44.text = dialog44Text;
        textDialog44.gameObject.SetActive(true);
        Debug.Log("แสดงข้อความ: " + dialog44Text);
        yield return new WaitForSeconds(dialogDisplayTimes);
        textDialog44.gameObject.SetActive(false);
    
        textDialog45.text = dialog45Text;
        textDialog45.gameObject.SetActive(true);
        Debug.Log("แสดงข้อความ: " + dialog45Text);
        yield return new WaitForSeconds(dialogDisplayTimes);
        textDialog45.gameObject.SetActive(false);
    
        bgDialog4.SetActive(false); // ซ่อน BG Dialog หลังจากแสดงเสร็จ
        Debug.Log("ปิด BG Dialog Map 4");
    
        isPlayerControllable = true; // เปิดการควบคุม Player หลังจากไดอะลอคเสร็จ
    }
    
    public void TakeDamage(int amount)
    {
        currentLives -= amount;
        currentLives = Mathf.Clamp(currentLives, 0, maxLives); // ทำให้เลือดไม่ต่ำกว่า 0
        UpdateHealthUI();

        if (currentLives <= 0)
        {
            Destroy(gameObject); // ลบ Player ออกจากเกมเมื่อเลือดหมด
            Debug.Log("Player Hp < 0");
            LoseGame(); // เรียกฟังก์ชัน LoseGame เมื่อ Player เสียชีวิต
        }
    }

    // Coroutine สำหรับการเติมเลือดทีละ 1 จุดทุกๆ 1 วินาที
    private IEnumerator HealOverTime(int amount, float time)
    {
        while (currentLives < maxLives && isHealing)
        {
            Heal(amount);
            yield return new WaitForSeconds(time); // รอ 1 วินาทีก่อนเพิ่ม HP อีก 1 จุด
        }
        isHealing = false; // หยุดเติมเลือดเมื่อเต็มแล้ว
    }

    public void Heal(int amount)
    {
        currentLives += amount;
        currentLives = Mathf.Clamp(currentLives, 0, maxLives); // ทำให้เลือดไม่เกิน maxLives
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        for (int i = 0; i < livesImage.Length; i++)
        {
            if (i < currentLives)
            {
                livesImage[i].SetActive(true); // แสดงบล็อคเลือด
            }
            else
            {
                livesImage[i].SetActive(false); // ซ่อนบล็อคเลือด
            }
        }
    }
    
    private void LoseGame()
    {
        // ปิดการควบคุม Player
        isPlayerControllable = false;

        // แสดง UI Lose
        if (losePanel != null)
        {
            losePanel.SetActive(true);  // แสดง Panel "lose"
            audioManager.PlaySFX(audioManager.GameOver); // เสียง Player ตาย
        }

        // หยุดการทำงานอื่นๆ ของ Player หรือเกม
        Debug.Log("Player แพ้แล้ว!");

        // อาจเพิ่มการรีเซ็ตเกมหรือกลับไปหน้าเมนูหลักที่นี่
    }
    
    
    private bool IsObstacleInWay(Vector2 targetPosition)
    {
        // ใช้ Physics2D เพื่อเช็คว่ามี Collider ใน Layer ที่กำหนดหรือไม่
        Collider2D obstacle = Physics2D.OverlapCircle(targetPosition, 0.1f, obstacleLayer);
        return obstacle != null;
    }
    
    void Balance()
    {
        // คำนวณความแตกต่างระหว่างมุมปัจจุบันกับ 0
        float angleDifference = Mathf.DeltaAngle(rb.rotation, 0f);

        // หากผู้เล่นหมุนมากกว่า 1 องศา ให้ใช้แรงเพื่อกลับไปที่ 0
        if (Mathf.Abs(angleDifference) > 1f)
        {
            // ใช้ AddTorque เพื่อสร้างแรงในการหมุน
            float torque = -angleDifference * balanceForce;
            rb.AddTorque(torque);
        }
    }

    
    //[("---------------------- Cut Scene Map 10 ------------------------------")]
    
    private IEnumerator StartCutsceneAndSwitchMap()
    {
        // เรียกใช้ PlayCutscene และรอให้คัทซีนแสดงจนจบ
        yield return PlayCutscene();

        // เปลี่ยนแผนที่เมื่อคัทซีนแสดงจบ
        map9Panel.SetActive(false);
        map10Panel.SetActive(true);
        
         
    }
    private void Show(Graphic graphic)
    {
        graphic.gameObject.SetActive(true);
    }

    private void Hide(Graphic graphic)
    {
        graphic.gameObject.SetActive(false);
    }
    

    private IEnumerator PlayCutscene()
    {
        // ค่อยๆ แสดงภาพ Cutscene
        //yield return FadeIn(cutsceneImage);
        Show(cutsceneImage);

        // แสดงพื้นหลังไดอะล็อกและข้อความแรก
        dialogBG.gameObject.SetActive(true);
        yield return FadeIn(dialogText1);
        yield return new WaitForSeconds(dialogDisplayTimesss); // รอให้ข้อความแรกแสดงครบเวลา

        // แสดงข้อความที่สองแทนข้อความแรก
        yield return FadeOut(dialogText1);
        yield return FadeIn(dialogText2);
        yield return new WaitForSeconds(dialogDisplayTimesss); // รอให้ข้อความที่สองแสดงครบเวลา

        
        yield return FadeOut(dialogText2);
        dialogBG.gameObject.SetActive(false);
        
        // ปิด Cutscene และเปิด Map 2
        CloseCutscene();
    }

    private IEnumerator FadeIn(Graphic graphic)
    {
        graphic.gameObject.SetActive(true);
        float elapsedTime = 0f;
        CanvasGroup canvasGroup = graphic.GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = graphic.gameObject.AddComponent<CanvasGroup>();

        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOut(Graphic graphic)
    {
        float elapsedTime = 0f;
        CanvasGroup canvasGroup = graphic.GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = graphic.gameObject.AddComponent<CanvasGroup>();

        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;
        graphic.gameObject.SetActive(false);
    }

    private void CloseCutscene()
    {
        // ปิด UI ทั้งหมดและรีเซ็ตสถานะ
        SetUIVisibility(false);
        playerController.enabled = true; // เปิดการควบคุม Player
        isCutsceneActive = false;

        // เปลี่ยนแผนที่เมื่อคัทซีนแสดงจบ
        map9Panel.SetActive(false);
        
        map10Panel.SetActive(true);
        PlayBackgroundMusic(audioManager.map10Music); // เล่นเพลง Map 4
        
        Warden.SetActive(true);
        Poseidondialog.SetActive(true);
        // เปลี่ยน Rigidbody2D เป็น Dynamic
        rb.bodyType = RigidbodyType2D.Dynamic; // เปลี่ยนประเภทเป็น Dynamic
        rb.position = startingPositionMap10;
        
    }

    private void SetUIVisibility(bool isVisible)
    {
        cutsceneImage.gameObject.SetActive(isVisible);
        dialogBG.gameObject.SetActive(isVisible);
        dialogText1.gameObject.SetActive(isVisible);
        dialogText2.gameObject.SetActive(isVisible);
    }
    
    
    // ฟังก์ชันสำหรับเล่นเสียงพื้นหลัง
    void PlayBackgroundMusic(AudioClip musicClip)
    {
        if (audioManager != null)
        {
            audioManager.StopBackgroundMusic(); // หยุดเพลงพื้นหลังก่อน
            audioManager.PlayBackgroundMusic(musicClip); // เล่นเพลงใหม่
        }
    }
}
