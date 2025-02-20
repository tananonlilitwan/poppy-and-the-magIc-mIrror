using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sensorOfMonster : MonoBehaviour
{
    [SerializeField] GameObject monsterObject; // Monster ที่จะเปิดเมื่อ Player เข้ามาใกล้

    // เมื่อ Player ชนกับ Collider ของ Monster
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // เปิด Monster เมื่อ Player ชนกับ Monster
            monsterObject.SetActive(true);
            Debug.Log("Player collided with the sensor. Monster is activated.");
           
        }
    }
}