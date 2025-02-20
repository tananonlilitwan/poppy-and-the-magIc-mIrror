using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;  // Prefab ของศัตรู
    public float spawnInterval = 2f;   // ระยะห่างในการเกิดศัตรูแต่ละครั้ง
    public int maxEnemies = 5;         // จำนวนศัตรูสูงสุดที่สามารถอยู่ในฉากได้ในแต่ละครั้ง
    public int maxSpawnCount = 10;     // จำนวนการเกิดศัตรูสูงสุด

    private float spawnTimer = 0f;
    private List<GameObject> currentEnemies = new List<GameObject>(); // รายชื่อศัตรูที่อยู่ในฉาก
    private int spawnCount = 0;
    
    private bool gameWon = false;      // ใช้เพื่อตรวจสอบว่าชนะแล้วหรือไม่
    


    // ขอบเขตการสร้างศัตรู
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;

    void Start()
    {
    }
    
    void Update()
    {
        /*spawnTimer += Time.deltaTime;

        // ตรวจสอบว่ามีจำนวนศัตรูในฉากน้อยกว่าที่กำหนดและยังสามารถสร้างได้
        if (spawnTimer >= spawnInterval && currentEnemies.Count < maxEnemies && spawnCount < maxSpawnCount)
        {
            SpawnEnemy();
            spawnTimer = 0f;
        }

        // ลบศัตรูที่ถูกทำลายออกจากลิสต์
        currentEnemies.RemoveAll(enemy => enemy == null);*/
        
        // ถ้าจำนวนการเกิดถึง maxSpawnCount แล้ว และยังไม่ได้ชนะ
        if (spawnCount >= maxSpawnCount && !gameWon)
        {
            // แสดงข้อความ "Win" ในคอนโซล
            Debug.Log("Win");
            gameWon = true;  // ตั้งค่าเพื่อหยุดแสดงข้อความ "Win" ซ้ำ
        }

        // ถ้าเกมยังไม่จบ ให้ทำการตรวจสอบการเกิดศัตรู
        if (!gameWon)
        {
            spawnTimer += Time.deltaTime;

            // ตรวจสอบว่ามีจำนวนศัตรูในฉากน้อยกว่าที่กำหนดและยังสามารถสร้างได้
            if (spawnTimer >= spawnInterval && currentEnemies.Count < maxEnemies && spawnCount < maxSpawnCount)
            {
                SpawnEnemy();
                spawnTimer = 0f;
            }

            // ลบศัตรูที่ถูกทำลายออกจากลิสต์
            currentEnemies.RemoveAll(enemy => enemy == null);
            
        }
    }

    void SpawnEnemy()
    {
        // สุ่มเลือกศัตรูจาก Prefabs
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyPrefab = enemyPrefabs[randomIndex];

        // สุ่มตำแหน่งการสร้างภายในขอบเขตที่กำหนด
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        // สร้างศัตรูที่ตำแหน่งสุ่ม
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        currentEnemies.Add(newEnemy);

        // ส่งคำสั่งเปลี่ยน Pattern ให้ศัตรู
        EnemyController enemyController = newEnemy.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            enemyController.SetRandomPattern();
        }

        // เพิ่มจำนวนศัตรูที่ถูกสร้างแล้ว
        spawnCount++;
    }
    
}
