using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ตรวจสอบการชนกับวัตถุอื่น ๆ (เช่น ศัตรู)
        if (collision.CompareTag("Enemy"))
        {
            // ทำให้ศัตรูเสียชีวิตหรือทำอะไรบางอย่าง
            Destroy(collision.gameObject);
            Destroy(gameObject); // ทำลายกระสุน
        }
        if (collision.CompareTag("Brick"))
        {
            // ทำให้ศัตรูเสียชีวิตหรือทำอะไรบางอย่าง
            Destroy(collision.gameObject);
            Destroy(gameObject); // ทำลายกระสุน
        }
    }
    
    void Update()
    {
       // transform.Translate(Vector2.right * Time.deltaTime);
       
       // เคลื่อนที่กระสุนไปตามทิศทางด้วยความเร็วที่กำหนด
       transform.Translate(Vector2.right * Time.deltaTime);
       
        // ทำลายกระสุนเมื่อออกนอกขอบเขต
        if (transform.position.x > 60)
        {
            Destroy(gameObject);
        }
        if (transform.position.x < -60 )
        {
            Destroy(gameObject);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject); // ทำลายกระสุนเมื่อออกนอกจอ
    }
    
    /*public float bulletSpeed = 5f;

    void Update()
    {
        transform.Translate(Vector2.left * bulletSpeed * Time.deltaTime);

        // ทำลายกระสุนเมื่อออกนอกขอบเขต
        if (transform.position.x < -10)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // ลดเลือดของ Player
            // Debug.Log("Player hit by bullet!");
            Destroy(gameObject);
        }
    }*/
}
