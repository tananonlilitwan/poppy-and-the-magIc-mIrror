using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] float detectionRange = 20f; // ระยะค้นหา Player
    [SerializeField] float chaseDistance = 1.5f;  // ระยะห่างที่หยุดไล่ล่า
    [SerializeField] float speed; // ความเร็วในการเดิน
    [SerializeField] GameObject monsterObject; // อ้างอิงถึง GameObject ของ Monster ที่จะเปิด/ปิด
    Transform player;
    
    [SerializeField] private GameObject pausePanel;

    bool isChasing = false;

    // Start is called before the first frame update
    void Start()
    {
        // ค้นหา Player ในฉาก
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player"); 
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        // ซ่อน Monster ตอนเริ่ม
        monsterObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) // ตรวจสอบว่า Player ยังอยู่หรือไม่
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);
            
            // Debug ระยะห่างระหว่าง Monster และ Player
            Debug.Log("Distance to Player: " + distanceToPlayer);

            if (distanceToPlayer <= detectionRange && !isChasing)
            {
                // เปิด Monster และเริ่มไล่ Player
                Debug.Log("Player detected, enabling Monster.");
                monsterObject.SetActive(true);
                isChasing = true;
                Debug.Log("Monster started chasing Player.");
            }

            if (isChasing)
            {
                if (distanceToPlayer > chaseDistance)
                {
                    // ไล่ Player เมื่ออยู่ในระยะ
                    ChasePlayer();
                }
                else
                {
                    // หยุดไล่ล่าเมื่อ Player อยู่ใกล้เกินไป
                    isChasing = false;
                    monsterObject.SetActive(false); // ซ่อน Monster เมื่อหยุดไล่ล่า
                    Debug.Log("Monster stopped chasing Player.");
                }
            }
        }
        else
        {
            // Player ถูกลบออกไปแล้ว หยุดการไล่ล่าและซ่อน Monster
            isChasing = false;
            monsterObject.SetActive(false);
            Debug.Log("Player not found, Monster is hidden.");
        }
    }

    void ChasePlayer()
    {
        // คำนวณทิศทางไปยัง Player ในแกน X เท่านั้น
        float directionX = (player.position.x - transform.position.x) > 0 ? 1 : -1;

        // Debug ทิศทางของ Monster ในแกน X
        Debug.Log("Chasing Player, direction X: " + directionX);

        // เคลื่อนที่ไปในทิศทางของ Player เฉพาะในแกน X
        transform.position = new Vector2(transform.position.x + directionX * speed * Time.deltaTime, transform.position.y);
    
        // Debug ตำแหน่งปัจจุบันของ Monster
        Debug.Log("Monster position: " + transform.position);
    }

    // ฟังก์ชันที่เรียกเมื่อชนกับ Player
    void OnCollisionEnter2D(Collision2D collision)
    {
        // ตรวจสอบว่าชนกับ Player หรือไม่
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Monster collided with Player! Removing Player.");
            Destroy(collision.gameObject); // ลบ Player ออกจากฉาก
            PauseGame();
            // Monster จะยังคงอยู่ในฉาก
        }
    }
    
    private void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
        
        if (pausePanel.activeSelf) // ถ้า Panel ถูกเปิดอยู่
        {
            //audioManager.StopBackgroundMusic(); // หยุดเสียงเกมโดยใช้ฟังก์ชันของ AudioManager
        }
    }
}
