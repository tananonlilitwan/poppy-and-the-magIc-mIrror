using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBulletWhenaplayer : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab ของกระสุน
    public Transform firePoint;     // ตำแหน่งที่กระสุนจะถูกยิงออกมา
    [SerializeField] float fireRate;     // อัตราการยิง (ทุก ๆ กี่วินาทีกระสุนจะถูกยิง)
    [SerializeField] float raycastDistance; // ระยะทางของ Raycast
    [SerializeField] float bulletSpeed; // ความเร็วของกระสุน
    
    private float nextFireTime = 0f; // เวลาในการยิงกระสุนครั้งถัดไป

    private bool playerDetected = false; // ตัวแปรเพื่อตรวจสอบว่าพบ Player หรือไม่

    private AudioManager audioManager; // เสียงในเกม

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // เสียงในเกม
    }
    
    void Update()
    {
        // Raycast เพื่อตรวจสอบว่าพบ Player หรือไม่
        RaycastCheck();

        // ถ้าพบ Player และถึงเวลายิงกระสุน
        if (playerDetected && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate; // กำหนดเวลาในการยิงกระสุนครั้งถัดไป
        }
    }

    void Shoot()
    {
        // สร้างกระสุนจาก Prefab
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // ยิงกระสุนในทิศทางไปข้างหน้า (firePoint.right)
        rb.velocity = firePoint.right * bulletSpeed;
        
        audioManager.PlaySFX(audioManager.shoot); // เสียง SFX เมื่อยิง
    }

    void RaycastCheck()
    {
        // ส่ง Ray ออกไปทางด้านขวาจาก firePoint
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right, raycastDistance);

        // ถ้า Ray ชนกับวัตถุที่มี Tag เป็น "Player"
        if (hitInfo.collider != null && hitInfo.collider.CompareTag("Player"))
        {
            // ตรวจจับ Player
            playerDetected = true;
            Debug.Log("Player detected.");
        }
        else
        {
            // ไม่พบ Player
            playerDetected = false;
            Debug.Log("Player not detected.");
        }
    }

    void OnDrawGizmos()
    {
        // วาด Gizmo เพื่อแสดง Raycast ใน Editor เป็นสีเขียวเมื่อค้นหา Player
        Gizmos.color = Color.green;
        Gizmos.DrawRay(firePoint.position, firePoint.right * raycastDistance);
    }
}
