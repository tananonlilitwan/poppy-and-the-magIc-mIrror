using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float lifeTime; // เวลาในการทำลายตัวเองหลังจากยิงออกไป
    
    void Start()
    {
        // ทำลายกระสุนหลังจากผ่านไป lifeTime วินาที
        Destroy(gameObject, lifeTime);
    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        // ตรวจสอบว่าชนกับวัตถุที่มี tag เป็น "Untagged"
        if (collision.gameObject.CompareTag("Untagged"))
        {
            // ทำลายกระสุนเมื่อชนกับวัตถุที่มี tag "Untagged"
            Destroy(gameObject);
        }
    }*/
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        // ตรวจสอบว่าชนกับวัตถุที่มี tag เป็น "Untagged"
        if (collision.gameObject.CompareTag("Untagged"))
        {
            // ทำลายกระสุนเมื่อชนกับวัตถุที่มี tag "Untagged"
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // ตรวจสอบว่าชนกับวัตถุที่มี tag เป็น "Untagged"
        if (collision.gameObject.CompareTag("Player"))
        {
            // ทำลายกระสุนเมื่อชนกับวัตถุที่มี tag "Untagged"
            Destroy(gameObject);
        }
    }
}
