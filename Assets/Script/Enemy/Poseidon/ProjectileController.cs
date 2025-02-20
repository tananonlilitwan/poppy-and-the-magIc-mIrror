using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    /*public float moveSpeed = 5f; // ความเร็วของกระสุน
    private Vector3 direction; // ทิศทางของกระสุน
    private float lifeTime = 5f; // อายุของกระสุน

    void Start()
    {
        // ทำลายกระสุนหลังจากเวลาที่กำหนด
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // เคลื่อนที่กระสุนไปในทิศทางที่ตั้งไว้
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    // ฟังก์ชันสำหรับตั้งค่าทิศทาง
    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection; // ตั้งค่าทิศทางของกระสุน
        transform.up = direction; // ทำให้กระสุนหันไปทางทิศทางนั้น
    }*/
    
    /*public float moveSpeed = 10f; // ความเร็วของกระสุน
    private Vector3 direction; // ทิศทางของกระสุน
    private float lifeTime = 5f; // อายุของกระสุน

    void Start()
    {
        // ทำลายกระสุนหลังจากเวลาที่กำหนด
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // เคลื่อนที่กระสุนไปในทิศทางที่ตั้งไว้
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    // ฟังก์ชันสำหรับตั้งค่าทิศทาง
    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection; // ตั้งค่าทิศทางของกระสุน
        // ไม่ต้องทำการเปลี่ยนการหมุนของ Sprite Renderer ที่นี่
        // transform.up = direction; // ลบการตั้งค่านี้ออก
    }*/
    
    public float moveSpeed = 10f; // ความเร็วของกระสุน
    private Vector3 direction; // ทิศทางของกระสุน
    private float lifeTime = 5f; // อายุของกระสุน

    void Start()
    {
        // ทำลายกระสุนหลังจากเวลาที่กำหนด
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // เคลื่อนที่กระสุนไปในทิศทางที่ตั้งไว้
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    // ฟังก์ชันสำหรับตั้งค่าทิศทาง
    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized; // ตั้งค่าทิศทางของกระสุน

        // ตั้งค่า rotation ของกระสุนให้ตรงกับทิศทางที่ยิง
        // เปลี่ยนทิศทางให้เป็นมุม
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // หมุนกระสุนตามมุมที่คำนวณ
    }
    
    
    
}