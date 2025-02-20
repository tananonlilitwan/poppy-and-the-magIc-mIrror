using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossController2 : MonoBehaviour
{
    public GameObject hpCanvas; // อ้างอิงถึง Canvas ที่แสดง HP ของ Boss
    public GameObject enemySpawner; // อ้างอิงถึง Enemy Spawner
    // ขอบเขตการเคลื่อนไหว
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    // ความเร็วการเคลื่อนไหว
    [SerializeField] private float moveSpeed = 5f;

    // กระสุน
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1f;
    private float nextFireTime = 0f;

    // HP ของ Boss
    [SerializeField] private int maxHp;
    private int currentHp;
    [SerializeField] private TextMeshProUGUI hpText;

    private Transform player;

    // ระยะห่างที่ Boss จะเริ่มโจมตี
    [SerializeField] private float attackRange = 10f; // ระยะห่างที่ Boss จะเริ่มโจมตี
    [SerializeField] private float retreatDistance = 5f; // ระยะห่างที่จะถอยหนี
    public int damage = 10; // กำหนดความเสียหายของกระสุน
    
    void Start()
    {
        currentHp = maxHp;
        UpdateHpUI();
        player = GameObject.FindWithTag("Player")?.transform; // หา Player

        if (player == null)
        {
            Debug.LogError("Player not found! Make sure the player has the tag 'Player'.");
        }
        hpCanvas.SetActive(true); // เปิด Canvas HP เมื่อ Boss ถูกเปิด
        StartCoroutine(SpawnEnemies()); // เริ่ม Coroutine สำหรับการเปิด Enemy Spawner
    }

    void Update()
    {
        MoveAndDodge(); // การเคลื่อนไหว
        HandleAttack();  // จัดการการโจมตี
        
    }
    
    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(3f); // หน่วงเวลา 3 วินาที
        enemySpawner.SetActive(true); // เปิดใช้งาน Enemy Spawner
    }

    private void MoveAndDodge()
    {
        Vector3 playerPosition = player.position;
        Vector3 direction = (playerPosition - transform.position).normalized;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position); // คำนวณระยะห่างระหว่าง Boss กับ Player

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
        // คำนวณระยะห่างระหว่าง Boss กับ Player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange) // ถ้าอยู่ในระยะที่กำหนด
        {
            FireProjectile();
        }
    }

    // ฟังก์ชันยิงกระสุน
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BulletPlayer")) // ตรวจสอบว่ากระสุนที่ชนกันมีแท็กเป็น BulletPlayer หรือไม่
        {
            // ลดเลือด Boss
            TakeDamage(damage); // เรียกใช้ฟังก์ชัน TakeDamage เพื่อลดเลือด Boss
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
        StopAllCoroutines(); // หยุด Coroutine ทั้งหมดเมื่อบอสตาย
        enemySpawner.SetActive(false); // ปิด Enemy Spawner เมื่อ Boss ตาย
        Destroy(gameObject); // ลบ Boss ออกจากเกมเมื่อ HP หมด
        hpCanvas.SetActive(false); // ปิด Canvas HP เมื่อ Boss ตาย
    }
}
