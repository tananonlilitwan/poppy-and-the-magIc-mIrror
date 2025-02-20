using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlPlayer : MonoBehaviour
{
    [Header("-----------------------Control Player------------------------------")]
    [SerializeField] float moveSpeed;     // ความเร็วในการเคลื่อนที่ของ Player
    [SerializeField] float acceleration; // ความเร่งในการเคลื่อนที่
    [SerializeField] float deceleration; // ความช้าลงเมื่อไม่มี input
    private Rigidbody2D rb;          // อ้างอิงถึง Rigidbody2D ของ Player
    private Vector2 movement;        // ทิศทางการเคลื่อนไหวของ Player
    private Vector2 currentVelocity; // ความเร็วปัจจุบันของ Player
    
    [Header("-----------------------Sprite Renderer------------------------------")]
    [SerializeField] SpriteRenderer spriteRenderer; // อ้างอิงถึง SpriteRenderer
    
    [Header("-----------------------Map Boundaries------------------------------")]
    [SerializeField] float minX;  // ขอบเขตด้านซ้ายของแผนที่
    [SerializeField] float maxX;  // ขอบเขตด้านขวาของแผนที่
    [SerializeField] float minY;  // ขอบเขตด้านล่างของแผนที่
    [SerializeField] float maxY;  // ขอบเขตด้านบนของแผนที่
    
    [Header("-----------------------Next panel---------------------------------------")]
    [SerializeField] GameObject map1Panel; // อ้างอิงถึง Panel Map 1
    [SerializeField] GameObject map2Panel; // อ้างอิงถึง Panel Map 2
    
    [Header("-----------------------New Player ---------------------------------------")]
    [SerializeField] Vector2 startingPositionMap2; // ตำแหน่งเริ่มต้นใน Map 2
    [SerializeField] GameObject newPlayerPrefab; // Prefab ของ Player ตัวใหม่
    
    [Header("---------------------- Cut Scene ------------------------------")]
    //[SerializeField] private CutsceneFathesDiary cutsceneController; // อ้างอิงไปยังคัทซีน
    public Image cutsceneImage; // รูปที่จะแสดงใน Cutscene
    public Image dialogBG; // พื้นหลังไดอะล็อก
    public TMP_Text dialogText1; // ข้อความไดอะล็อก 1 (TextMeshPro)
    public TMP_Text dialogText2; // ข้อความไดอะล็อก 2 (TextMeshPro)
    public float fadeDuration = 1f; // ระยะเวลาในการค่อยๆ แสดงผล
    public float dialogDisplayTime = 3f; // เวลาการแสดงข้อความแต่ละข้อความ
    private bool isCutsceneActive = false;
    public ControlPlayer playerController; // อ้างอิงไปยัง PlayerController

    private AudioManager audioManager; // เสียงในเกม
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // เสียงในเกม
       
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  // เข้าถึง Rigidbody2D ของ Player
        map2Panel.SetActive(false);        // ทำให้ Panel Map 2 ปิดในตอนเริ่มต้น
        newPlayerPrefab.SetActive(false);  // ซ่อน Player ตัวใหม่ไว้ก่อน
        PlayBackgroundMusic(audioManager.map1Music); // เล่นเพลง Map 1
        
        // ซ่อน UI ทั้งหมดในตอนเริ่มต้น
        SetUIVisibility(false);
        
    }

    // Update is called once per frame
    void Update()
    {
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
        if (other.CompareTag("Next panel"))
        {
            isCutsceneActive = true;
            playerController.enabled = false; // ปิดการควบคุม Player
            StartCoroutine(StartCutsceneAndSwitchMap()); // เรียกใช้ Cutscene และรอให้จบก่อนเปลี่ยนแผนที่
            
            StartCoroutine(StartCutsceneAndSwitchMap());
            
            /*
            // ปิด Panel Map 1
            map1Panel.SetActive(false);

            // เปิด Panel Map 2
            map2Panel.SetActive(true);
            
            // ตั้งค่าตำแหน่ง Player ให้เริ่มเต้นในตำแหน่งที่กำหนดใน Panel Map 2
            rb.position = startingPositionMap2;
            
            // ตั้งค่าตำแหน่ง Player ใหม่ให้เริ่มต้นในตำแหน่งที่กำหนดใน Map 2
            newPlayerPrefab.transform.position = startingPositionMap2;

            // เปิดใช้งาน Player ที่ซ่อนอยู่
            newPlayerPrefab.SetActive(true);
            
            // ลบ Player ตัวเดิมออก
            Destroy(gameObject);*/
        }
        
        
        if (other.CompareTag("Untagged"))
        {
            // หยุดการเคลื่อนที่หรือทำการย้อนตำแหน่ง Player เพื่อไม่ให้ทะลุผ่าน
            Debug.Log("ชนกับวัตถุที่มี tag 'Untagged'");
            movement = Vector2.zero; // ทำให้การเคลื่อนที่เป็น 0 เมื่อชน
        }
    }
    
    
    private IEnumerator StartCutsceneAndSwitchMap()
    {
        // เรียกใช้ PlayCutscene และรอให้คัทซีนแสดงจนจบ
        yield return PlayCutscene();

        // เปลี่ยนแผนที่เมื่อคัทซีนแสดงจบ
        map1Panel.SetActive(false);
        map2Panel.SetActive(true);
        
        PlayBackgroundMusic(audioManager.map2Music); // เล่นเพลง Map 2
        
        newPlayerPrefab.transform.position = startingPositionMap2;
        newPlayerPrefab.SetActive(true);

        // ลบ Player ตัวเดิมออก
        Destroy(gameObject);
    }
    

    private IEnumerator PlayCutscene()
    {
        // ค่อยๆ แสดงภาพ Cutscene
        yield return FadeIn(cutsceneImage);

        // แสดงพื้นหลังไดอะล็อกและข้อความแรก
        dialogBG.gameObject.SetActive(true);
        yield return FadeIn(dialogText1);
        yield return new WaitForSeconds(dialogDisplayTime); // รอให้ข้อความแรกแสดงครบเวลา

        // แสดงข้อความที่สองแทนข้อความแรก
        yield return FadeOut(dialogText1);
        yield return FadeIn(dialogText2);
        yield return new WaitForSeconds(dialogDisplayTime); // รอให้ข้อความที่สองแสดงครบเวลา

        // ซ่อนพื้นหลังไดอะล็อกและแสดง Note กับปุ่มปิด Cutscene
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
        map1Panel.SetActive(false);
        map2Panel.SetActive(true);
        newPlayerPrefab.transform.position = startingPositionMap2; // ตั้งค่าตำแหน่ง Player ใน Map 2
        newPlayerPrefab.SetActive(true); // เปิดใช้งาน Player ตัวใหม่

        // ลบ Player ตัวเดิมออก
        Destroy(gameObject);
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
