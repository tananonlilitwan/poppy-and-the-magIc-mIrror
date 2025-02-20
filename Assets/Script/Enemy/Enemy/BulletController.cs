using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed = 5f;

    void Update()
    {
        transform.Translate(Vector2.left * bulletSpeed * Time.deltaTime);

        // ทำลายกระสุนเมื่อออกนอกขอบเขต
        if (transform.position.x < -60)
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
    }
}