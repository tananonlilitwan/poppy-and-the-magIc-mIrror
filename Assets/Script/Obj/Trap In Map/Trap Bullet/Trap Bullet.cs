using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapBullet : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab ของกระสุน
    public Transform firePoint;     // ตำแหน่งที่กระสุนจะถูกยิงออกมา
    [SerializeField] float fireRate;     // อัตราการยิง (ทุก ๆ กี่วินาทีกระสุนจะถูกยิง)
    [SerializeField] float raycastDistance; // ระยะทางของ Raycast
    [SerializeField] float bulletSpeed; // ความเร็วของกระสุน
    
    private float nextFireTime = 0f; // เวลาในการยิงกระสุนครั้งถัดไป

    
    private AudioManager audioManager; // เสียงในเกม
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // เสียงในเกม
       
    }
    
    void Update()
    {
        // ถ้าถึงเวลายิงกระสุนแล้ว
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate; // กำหนดเวลาในการยิงกระสุนครั้งถัดไป
            audioManager.PlaySFX(audioManager.shoot); // เสียงSFX Get Hp
        }
        // Raycast เพื่อตรวจสอบวัตถุด้านหน้า
        RaycastCheck();
    }

    void Shoot()
    {
        // สร้างกระสุนจาก Prefab
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // ยิงกระสุนในทิศทางไปข้างหน้า (firePoint.forward หรือ Vector2.right)
        rb.velocity = firePoint.right * bulletSpeed;
    }
    
    void RaycastCheck()
    {
        // ส่ง Ray ออกไปทางด้านขวาจาก firePoint
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right, raycastDistance);

        // ถ้า Ray ชนกับบางสิ่ง
        if (hitInfo.collider != null)
        {
            //Debug.Log("Raycast Hit: " + hitInfo.collider.name);
            // คุณสามารถเพิ่มโค้ดเพิ่มเติมได้ที่นี่ เช่น หยุดการยิงหรือเปลี่ยนทิศทางกระสุน
        }
        else
        {
            Debug.Log("Raycast did not hit anything.");
        }
    }

    void OnDrawGizmos()
    {
        // วาด Gizmo เพื่อแสดง Raycast ใน Editor
        Gizmos.color = Color.red;
        Gizmos.DrawRay(firePoint.position, firePoint.right * raycastDistance);
    }
}
