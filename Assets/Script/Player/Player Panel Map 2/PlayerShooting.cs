using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting Sound")]
    public AudioClip shootSound; // เสียงเมื่อยิง
    private AudioSource audioSource; // ตัวจัดการเสียง
    
    [Header("-----------------------Player Skill------------------------------")]
    public GameObject bulletPrefab; // Prefab ของกระสุน
    [SerializeField] float bulletSpeed; // ความเร็วของกระสุน
    
    public bool canShoot = false; // ตัวแปรเพื่อเก็บสถานะการยิง
    // ตัวแปร isSkill2Free
    public bool isSkill2Free = false; // สถานะการใช้สกิล 2 โดยไม่ต้องใช้มานา
    
    [Header("Bullet Sprites")]
    public Sprite skill1Sprite; // Sprite ของสกิล 1
    public Sprite skill2Sprite; // Sprite ของสกิล 2
    
    [Header("-----------------------Player Mana------------------------------")]
    [SerializeField] private int maxMana; // ค่ามานาสูงสุด = 100
    [SerializeField] private int currentMana;   // ค่ามานาปัจจุบัน
    [SerializeField] private int manaCostSkill2; // ค่ามานาที่ใช้สำหรับสกิล 2 = 20
    [SerializeField] private int manaRegenAmount; // ค่ามานาที่จะเพิ่มเมื่อเติมมานาในแต่ละช่วง = 10
    [SerializeField] private float manaRegenInterval; // เวลาระหว่างการเติมมานาแต่ละครั้ง = 1f

    [SerializeField] private Image[] manaBarSegments; // Array ของแถบมานาแต่ละส่วนใน UI
    [SerializeField] private TextMeshProUGUI manaWarningText; // ข้อความเตือนเมื่อมานาไม่พอ (UI)
    private int manaSegmentValue; // ค่ามานาต่อแถบใน UI
    
    private bool isInManaStation = false; // ตัวแปรเพื่อเช็คว่าอยู่ในจุดเติมมานาหรือไม่
    private float manaRegenTimer = 0f; // ตัวจับเวลาสำหรับการเติมมานา
    
    
    private AudioManager audioManager; // เสียงในเกม
    private void Awake() // หรือ Start()
    {
        audioSource = GetComponent<AudioSource>(); // เพิ่มบรรทัดนี้เพื่อรับ AudioSource ของวัตถุ
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        currentMana = maxMana; // กำหนดให้มานาปัจจุบันเต็ม
        manaSegmentValue = maxMana / manaBarSegments.Length; // คำนวณมานาที่แต่ละแถบแสดง
        manaWarningText.gameObject.SetActive(false); // ซ่อนข้อความเตือนในตอนเริ่มต้น
        UpdateManaUI(); // อัปเดตแถบมานาใน UI
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot) // KeyCode ที่ใช้ในการยิง
        {
            if (Input.GetMouseButtonDown(0)) // กด 1 สำหรับยิงเป็นเส้นตรง
            {
                ShootStraight();
                audioManager.PlaySFX(audioManager.shoot); // เสียง SFX  ยิงกระสุน
            }

            if (Input.GetMouseButtonDown(1)) // กด 2 สำหรับยิงเป็นสามแฉก
            {
                // ถ้า isSkill2Free เป็นจริง ให้ใช้สกิล 2 ได้โดยไม่ต้องใช้มานา
                if (isSkill2Free || currentMana >= manaCostSkill2)
                {
                    ShootTriple(); // ยิงสกิล 2
                    audioManager.PlaySFX(audioManager.shoot); // เสียง SFX  ยิงกระสุน
                    if (!isSkill2Free)
                    {
                        //ResetSkill2Usage();
                        UseManaForSkill2(); // ลดมานาเมื่อใช้สกิล หาก isSkill2Free เป็น false
                    }
                    manaWarningText.gameObject.SetActive(false); // ซ่อนข้อความเตือนเมื่อมานาเพียงพอ
                }
                else
                {
                    Debug.Log("มานาไม่พอสำหรับสกิล 2");
                    manaWarningText.gameObject.SetActive(true); // แสดงข้อความเตือนมานาไม่พอ
                }
                
                /*if (currentMana >= manaCostSkill2) // ตรวจสอบว่ามานาพอสำหรับสกิล 2 หรือไม่
                {
                    ShootTriple(); // ยิงสกิล 2
                    UseManaForSkill2(); // ลดมานาเมื่อใช้สกิล
                    manaWarningText.gameObject.SetActive(false); // ซ่อนข้อความเตือนเมื่อมานาเพียงพอ
                }
                else
                {
                    Debug.Log("มานาไม่พอสำหรับสกิล 2");
                    manaWarningText.gameObject.SetActive(true); // แสดงข้อความเตือนมานาไม่พอ
                }*/
            }
        }
        // เติมมานาอย่างต่อเนื่องเมื่ออยู่ในจุดเติมมานา
        if (isInManaStation)
        {
            manaRegenTimer += Time.deltaTime;
            if (manaRegenTimer >= manaRegenInterval)
            {
                RestoreMana();
                manaRegenTimer = 0f; // รีเซ็ตตัวจับเวลา
            }
        }
    }
    
    void ShootStraight()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        
        // ตรวจสอบการหันหน้าของ Player แล้วกำหนดทิศทางกระสุน
        Vector2 shootingDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;
        
        bullet.GetComponent<Rigidbody2D>().velocity = transform.right * bulletSpeed; // เปลี่ยนทิศทางให้กระสุนไปในแนว X
        bullet.GetComponent<SpriteRenderer>().sprite = skill1Sprite; // เปลี่ยน Sprite ของกระสุนเป็นของสกิล 1
        
        // เล่นเสียงเมื่อยิง
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
        else
        {
            Debug.LogError("AudioSource or shootSound is not assigned!");
        }
        
    }


    void ShootTriple()
    {
        Vector2 shootingDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;; 
        
        // ยิงกระสุนตรงกลาง
        GameObject middleBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        middleBullet.GetComponent<Rigidbody2D>().velocity = transform.right * bulletSpeed; // กระสุนตรงกลาง
        middleBullet.GetComponent<SpriteRenderer>().sprite = skill2Sprite; // เปลี่ยน Sprite เป็นของสกิล 2
        
        middleBullet.transform.localScale = new Vector3(shootingDirection.x, 1f, 1f);

        // ยิงกระสุนทางซ้าย
        GameObject leftBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        leftBullet.GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(0, 0, 15) * transform.right * bulletSpeed; // หมุน 15 องศาไปทางซ้าย
        leftBullet.GetComponent<SpriteRenderer>().sprite = skill2Sprite; // เปลี่ยน Sprite เป็นของสกิล 2

        leftBullet.transform.localScale = new Vector3(shootingDirection.x, 1f, 1f);

        // ยิงกระสุนทางขวา
        GameObject rightBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        rightBullet.GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(0, 0, -15) * transform.right * bulletSpeed; // หมุน 15 องศาไปทางขวา
        rightBullet.GetComponent<SpriteRenderer>().sprite = skill2Sprite; // เปลี่ยน Sprite เป็นของสกิล 2
        
        rightBullet.transform.localScale = new Vector3(shootingDirection.x, 1f, 1f);
        
        // เล่นเสียงเมื่อยิง
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
        else
        {
            Debug.LogError("AudioSource or shootSound is not assigned!");
        }
    }
    
    
    void UseManaForSkill2()
    {
        currentMana -= manaCostSkill2; // หักมานาจากการใช้สกิล 2
        UpdateManaUI(); // อัปเดต UI แสดงมานาที่ลดลง
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ManaStation")) // ตรวจสอบว่าชนกับวัตถุที่มี Tag เป็น "ManaStation"
        {
            isInManaStation = true; // เริ่มเติมมานา
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("ManaStation")) // เมื่อออกจากจุดเติมมานา
        {
            isInManaStation = false; // หยุดการเติมมานา
            manaRegenTimer = 0f; // รีเซ็ตตัวจับเวลา
        }
    }

    void RestoreMana()
    {
        currentMana = Mathf.Min(currentMana + manaRegenAmount, maxMana); // เพิ่มมานาโดยไม่ให้เกิน maxMana
        UpdateManaUI(); // อัปเดต UI แสดงมานาที่เพิ่มขึ้น
        Debug.Log("มานาได้รับการเติม");
    }

    void UpdateManaUI()
    {
        int activeSegments = currentMana / manaSegmentValue; // คำนวณว่าควรแสดงแถบมานากี่อัน
        for (int i = 0; i < manaBarSegments.Length; i++)
        {
            if (i < activeSegments)
            {
                manaBarSegments[i].enabled = true; // แสดงแถบมานา
            }
            else
            {
                manaBarSegments[i].enabled = false; // ซ่อนแถบมานา
            }
        }
    }
    
    public void ResetSkill2Usage()
    {
        isSkill2Free = false; // คืนค่าการใช้สกิล 2 เป็นปกติ
        Debug.Log("Skill 2 usage reset to normal.");
    }

}
