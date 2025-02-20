using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab ของกระสุนที่ศัตรูจะยิง
    public float bulletSpeed = 5f; // ความเร็วของกระสุน
    public float fireRate = 2f; // อัตราการยิงกระสุน
    private float nextFire = 0f; // เวลาถัดไปที่จะยิงกระสุน

    public float moveSpeed = 2f; // ความเร็วในการเคลื่อนที่ของศัตรู
    public Vector2 startPos; // ตำแหน่งเริ่มต้นของศัตรู
    public PatternType currentPattern; // รูปแบบการเคลื่อนที่ของศัตรู
    public float patternSwitchTime = 10f; // เวลาที่จะเปลี่ยนรูปแบบการเคลื่อนที่
    private float patternTimer; // ตัวนับเวลาสำหรับการเปลี่ยนรูปแบบ

    // ขอบเขตการเคลื่อนที่
   [SerializeField] float minX; // ขอบเขตซ้าย
   [SerializeField] float maxX; // ขอบเขตขวา
   [SerializeField] float minY; // ขอบเขตล่าง
   [SerializeField] float maxY; // ขอบเขตบน

    // Pattern Types
    public enum PatternType {
        Horizontal,
        Circular
    }

    void Start()
    {
        patternTimer = patternSwitchTime;
        startPos = transform.position;
        SetRandomPattern(); // ตั้งค่ารูปแบบเริ่มต้นเมื่อเริ่มเกม
    }

    void Update()
    {
        // อัพเดตรูปแบบการเคลื่อนที่ตาม Pattern
        MoveInPattern();

        // ยิงจรวดตามเวลาที่กำหนด
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    void MoveInPattern()
    {
        switch (currentPattern)
        {
            case PatternType.Horizontal:
                float newX = startPos.x + Mathf.PingPong(Time.time * moveSpeed, maxX - minX) + minX;
                transform.position = new Vector2(newX, transform.position.y);
                break;
            case PatternType.Circular:
                float radius = 2f;
                float angle = Time.time * moveSpeed;
                float newXCircular = startPos.x + Mathf.Cos(angle) * radius;
                float newYCircular = startPos.y + Mathf.Sin(angle) * radius;

                // ตรวจสอบขอบเขต
                newXCircular = Mathf.Clamp(newXCircular, minX, maxX);
                newYCircular = Mathf.Clamp(newYCircular, minY, maxY);
                
                transform.position = new Vector2(newXCircular, newYCircular);
                break;
        }
    }

    public void SetRandomPattern()
    {
        // สุ่มเลือก Pattern จาก PatternType
        int randomPattern = Random.Range(0, 2); // Assuming there are 2 patterns
        currentPattern = (PatternType)randomPattern;
    }

    void Shoot()
    {
        // ยิงกระสุนจากตำแหน่งของศัตรูเอง
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // ให้กระสุนเคลื่อนที่ลงไปในแนวแกน Y
        rb.velocity = Vector2.down * bulletSpeed;
    }
}
