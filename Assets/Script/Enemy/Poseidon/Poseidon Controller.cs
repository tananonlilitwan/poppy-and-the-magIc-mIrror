using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PoseidonController : MonoBehaviour
{
    public GameObject BossSpawner;

    
    public GameObject hpCanvas; // อ้างอิงถึง Canvas ที่แสดง HP ของ Boss
    public GameObject winMenu; // อ้างอิงถึง Canvas Menu ที่จะถูกแสดงเมื่อ HP หมด
    // ขอบเขตการเคลื่อนไหว
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    // ความเร็วการเคลื่อนไหว
    [SerializeField] private float moveSpeed = 5f;
    
    // ความสมูทของการเคลื่อนไหว
    [SerializeField] private float smoothTime = 0.3f; // เวลาที่ใช้ในการเคลื่อนที่

    // กระสุน
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1f;
    private float nextFireTime = 0f;

    // Sprite ที่จะใช้
    public SpriteRenderer spriteRenderer; // อ้างอิงถึง SpriteRenderer
    private bool isAttack = false; 
    private int currentSpriteIndex = 0;
    [SerializeField] private float spriteChangeInterval = 0.1f; // ระยะเวลาในการเปลี่ยน SPR
    [SerializeField] private Sprite[] attackSprites; // เก็บ Sprite สำหรับการโจมตี
    [SerializeField] private Sprite idleSprite; // Sprite สำหรับ Idle
    private float timeSinceLastSpriteChange = 0f;

    // HP ของ Poseidon
    [SerializeField] private int maxHp;
    private int currentHp;
    [SerializeField] private TextMeshProUGUI hpText;

    private Transform player;
    
    // ระยะห่างที่ Poseidon จะเริ่มโจมตี
    [SerializeField] private float attackRange = 10f; // ระยะห่างที่ Poseidon จะเริ่มโจมตี
    [SerializeField] private float retreatDistance = 5f; // ระยะห่างที่จะถอยหนี
    public int damage = 10; // กำหนดความเสียหายของกระสุน
    
    [Header("---------- Dialog -------------")]
    public GameObject bgDialog; // อ้างอิง BG Dialog

    public TextMeshProUGUI textDialog1; // อ้างอิง Text Dialog 1
    public TextMeshProUGUI textDialog2; // อ้างอิง Text Dialog 2
    public TextMeshProUGUI textDialog3; // อ้างอิง Text Dialog 3
    public TextMeshProUGUI textDialog4; // อ้างอิง Text Dialog 4
    public TextMeshProUGUI textDialog5; // อ้างอิง Text Dialog 5
    public float dialogDisplayTime = 3f; // เวลาในการแสดง Dialog 2
    public float fadeDuration = 1f; // ระยะเวลาในการค่อยๆ แสดงผล
    [SerializeField] private string dialog1Text; // ประโยค Dialog 1 ที่สามารถพิมพ์จาก Unity ได้
    [SerializeField] private string dialog2Text; // ประโยค Dialog 2 ที่สามารถพิมพ์จาก Unity ได้
    [SerializeField] private string dialog3Text; // ประโยค Dialog 3 ที่สามารถพิมพ์จาก Unity ได้
    [SerializeField] private string dialog4Text; // ประโยค Dialog 3 ที่สามารถพิมพ์จาก Unity ได้
    [SerializeField] private string dialog5Text; // ประโยค Dialog 3 ที่สามารถพิมพ์จาก Unity ได้
    
   
    private bool isPlayerNearby = false;
    private Collider2D playerCollider; // ใช้สำหรับเก็บ Collider ของผู้เล่น
    private bool isDialogActive = true; // เริ่มต้นเป็น true เพื่อหยุดการโจมตีและเคลื่อนไหวจนกว่าไดอะล็อกจบ
    private bool isDialogShowing = true; // ตัวแปรตรวจสอบสถานะของไดอะล็อก

    private AudioManager audioManager; // เสียงในเกม
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // เสียงในเกม
       
    }
    
    void Start()
    {
        currentHp = maxHp;
        UpdateHpUI();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player").transform; // หา Player
        hpCanvas.SetActive(true); // เปิด Canvas HP เมื่อ Boss ถูกเปิด
        
        StartCoroutine(ShowAllDialogs()); // เริ่มแสดงไดอะล็อกเมื่อเริ่มเกม
    }

    void Update()
    {
        /*MoveAndDodge(); // การเคลื่อนไหว
        HandleAttack();  // จัดการการโจมตี
        UpdateSprite(); // อัปเดต Sprite ตามการโจมตี*/
        
        if (!isDialogActive)
        {
            MoveAndDodge(); // การเคลื่อนไหว
            HandleAttack();  // จัดการการโจมตี
        }
        UpdateSprite(); // อัปเดต Sprite ตามการโจมตี
    }
    
    private void MoveAndDodge()
    {
        Vector3 playerPosition = player.position;
        Vector3 direction = (playerPosition - transform.position).normalized;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position); // คำนวณระยะห่างระหว่าง Poseidon กับ Player

        Vector3 targetPosition;

        if (distanceToPlayer > attackRange) // ถ้าอยู่ไกลกว่า attackRange
        {
            // เดินเข้าหา Player
            targetPosition = transform.position + direction * moveSpeed * Time.deltaTime;
        }
        else if (distanceToPlayer < retreatDistance) // ถ้าอยู่ใกล้กว่า retreatDistance
        {
            // ถอยหนีจาก Player
            targetPosition = transform.position - direction * (moveSpeed * 0.5f) * Time.deltaTime; // สามารถปรับความเร็วในการถอยหนีได้
        }
        else
        {
            // ถ้าอยู่ในระยะที่พอเหมาะ ให้หยุด
            targetPosition = transform.position;
        }

        // ขอบเขตการเคลื่อนไหว
        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

        // ใช้ Lerp เพื่อทำให้การเคลื่อนไหวมีความนุ่มนวล
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }
    
    // ฟังก์ชันจัดการการโจมตี
    private void HandleAttack()
    {
        if (isDialogActive) return; // ไม่ทำการโจมตีเมื่อแสดงไดอะล็อก
        
        // คำนวณระยะห่างระหว่าง Poseidon กับ Player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange) // ถ้าอยู่ในระยะที่กำหนด
        {
            FireProjectile();
            isAttack = true; // กำลังโจมตี
        }
        else
        {
            isAttack = false; // ไม่ได้โจมตี
        }
        
    }
    
    private void FireProjectile()
    {
        if (Time.time > nextFireTime)
        {
            // สร้างกระสุนที่ตำแหน่ง firePoint
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        
            // คำนวณทิศทางไปยัง Player
            Vector3 direction = (player.position - firePoint.position).normalized;

            // ตั้งค่าทิศทางของกระสุน
            ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
            projectileController.SetDirection(direction); // ตั้งค่าทิศทางของกระสุน

            nextFireTime = Time.time + 1f / fireRate; // ตั้งเวลาการยิงครั้งถัดไป
        }
    }


    // ฟังก์ชันสำหรับเปลี่ยน Sprite เมื่อกำลังโจมตี
    private void UpdateSprite()
    {
        if (isAttack && attackSprites.Length > 0)
        {
            timeSinceLastSpriteChange += Time.deltaTime;

            if (timeSinceLastSpriteChange >= spriteChangeInterval)
            {
                // เปลี่ยน Sprite ถัดไปเมื่อถึงเวลา
                currentSpriteIndex = (currentSpriteIndex + 1) % attackSprites.Length;
                spriteRenderer.sprite = attackSprites[currentSpriteIndex];
                timeSinceLastSpriteChange = 0f;
            }
        }
        else
        {
            // กลับไปที่ Idle Sprite ถ้าไม่ได้โจมตี
            spriteRenderer.sprite = idleSprite;
            currentSpriteIndex = 0; // รีเซ็ต sprite index ถ้าต้องการกลับไปที่ idle
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletPlayer")) // ตรวจสอบว่ากระสุนที่ชนกันมีแท็กเป็น BulletPlayer หรือไม่
        {
            // ลดเลือด Boss ลง 1 หน่วย
            TakeDamage(3); // เรียกใช้ฟังก์ชัน TakeDamage เพื่อลดเลือด Boss
            Destroy(collision.gameObject); // ลบกระสุนออกจากเกม
        }
    }
    
    // จัดการ HP
    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        UpdateHpUI();

        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void UpdateHpUI()
    {
        hpText.text = "HP: " + currentHp.ToString();
    }

    private void Die()
    {
        Destroy(gameObject); // ลบ Poseidon ออกจากเกมเมื่อ HP หมด
        hpCanvas.SetActive(false); // เปิด Canvas HP เมื่อ Boss ถูกเปิด
        winMenu.SetActive(true); // แสดง Canvas Menu Win
        audioManager.PlaySFX(audioManager.Win);
        
    }
    
    
    
    private IEnumerator ShowAllDialogs()
    {
        isDialogActive = true; // เริ่มต้นแสดงไดอะล็อก

        // แสดง Dialog 1
        bgDialog.SetActive(true);
        textDialog1.gameObject.SetActive(true);
        textDialog1.text = dialog1Text;
        Debug.Log("Dialog 1 is shown with text: " + dialog1Text);
        yield return StartCoroutine(FadeIn(textDialog1.GetComponent<Graphic>())); // เฟดอิน Dialog 1
        // รอเวลา
        yield return new WaitForSeconds(dialogDisplayTime); // รอ 3 วินาที
        yield return StartCoroutine(FadeOut(textDialog1.GetComponent<Graphic>()));
        
        // ซ่อน Dialog 1 และแสดง Dialog 2
        textDialog1.gameObject.SetActive(false);
        textDialog2.gameObject.SetActive(true); 
        textDialog2.text = dialog2Text;
        yield return StartCoroutine(FadeIn(textDialog2.GetComponent<Graphic>())); // เฟดอิน Dialog 2
        Debug.Log("Dialog 2 is shown with text: " + dialog2Text);
    
        // รอเวลา
        yield return new WaitForSeconds(dialogDisplayTime); // รอ 3 วินาที
        yield return StartCoroutine(FadeOut(textDialog2.GetComponent<Graphic>()));// เฟดเอาท์ Dialog 2
        
        // แสดง Dialog 3
        textDialog2.gameObject.SetActive(false); 
        textDialog3.gameObject.SetActive(true); 
        textDialog3.text = dialog3Text;
        yield return StartCoroutine(FadeIn(textDialog3.GetComponent<Graphic>())); // เฟดอิน Dialog 3
        Debug.Log("Dialog 3 is shown with text: " + dialog3Text);
        yield return new WaitForSeconds(dialogDisplayTime); // รอ 3 วินาที
        // เฟดเอาท์ Dialog 3
        yield return StartCoroutine(FadeOut(textDialog3.GetComponent<Graphic>()));
        
        // แสดง Dialog 4
        textDialog3.gameObject.SetActive(false); 
        textDialog4.gameObject.SetActive(true); 
        textDialog4.text = dialog4Text;
        yield return StartCoroutine(FadeIn(textDialog4.GetComponent<Graphic>())); // เฟดอิน Dialog 4
        Debug.Log("Dialog 4 is shown with text: " + dialog4Text);
        yield return new WaitForSeconds(dialogDisplayTime); // รอ 3 วินาที
        // เฟดเอาท์ Dialog 4
        yield return StartCoroutine(FadeOut(textDialog4.GetComponent<Graphic>()));

        // แสดง 5
        textDialog4.gameObject.SetActive(false); 
        textDialog5.gameObject.SetActive(true);
        textDialog5.text = dialog5Text;
        yield return StartCoroutine(FadeIn(textDialog5.GetComponent<Graphic>())); // เฟดอิน Dialog 5
        // รอเวลาของไดอะล็อกสุดท้าย
        yield return new WaitForSeconds(dialogDisplayTime); // รอเวลาของไดอะล็อกสุดท้าย
        // เฟดเอาท์ Dialog 5
        yield return StartCoroutine(FadeOut(textDialog5.GetComponent<Graphic>()));
        
        
        // ปิด Dialog ทั้งหมด
        bgDialog.SetActive(false); 
        textDialog5.gameObject.SetActive(false); 
        isDialogActive = false;
        isDialogShowing = false; // เปลี่ยนสถานะกลับเป็นไม่แสดง
        
        Debug.Log("All dialogs are closed");
       
        yield return new WaitForSeconds(dialogDisplayTime); // รอเวลาของไดอะล็อกสุดท้าย
        CloseAllDialogs();
        
        isDialogActive = false; // ไดอะล็อกจบแล้ว
        //if (controlPlayer != null) controlPlayer.isPlayerControllable = true; // คืนการควบคุมผู้เล่น
        
        BossSpawner.gameObject.SetActive(true);
        
    }
    private void CloseAllDialogs()
    {
        // ปิด BG ของ Dialog และ Text ทั้งหมด
        bgDialog.SetActive(false);
        textDialog1.gameObject.SetActive(false);
        textDialog2.gameObject.SetActive(false);
        textDialog3.gameObject.SetActive(false);
        textDialog4.gameObject.SetActive(false);
        textDialog5.gameObject.SetActive(false);
    
        isDialogActive = false; // เปลี่ยนสถานะ Dialog เป็นไม่แสดง
        Debug.Log("All dialogs are closed because the player is no longer nearby");
        
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
   
}
