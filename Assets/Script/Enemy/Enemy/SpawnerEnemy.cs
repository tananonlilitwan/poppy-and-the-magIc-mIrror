using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    public GameObject[] enemyPrefabs;  // Prefab ของศัตรู
    [SerializeField] float spawnInterval;    // ระยะห่างในการเกิดศัตรูแต่ละครั้ง
    [SerializeField] int maxEnemies;          // จำนวนศัตรูสูงสุดที่สามารถอยู่ในฉากได้ในแต่ละครั้ง
    [SerializeField] int maxSpawnCount;      // จำนวนการเกิดศัตรูสูงสุด

    private float spawnTimer = 0f;
    private List<GameObject> currentEnemies = new List<GameObject>(); // รายชื่อศัตรูที่อยู่ในฉาก
    private int spawnCount = 0;

    // ขอบเขตการสร้างศัตรู
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;

    void Update()
    {
        // ตรวจสอบเวลาการสร้างศัตรู
        spawnTimer += Time.deltaTime;

        if (spawnCount < maxSpawnCount && currentEnemies.Count < maxEnemies && spawnTimer >= spawnInterval)
        {
            SpawnEnemy();
            spawnTimer = 0f; // รีเซ็ตตัวนับเวลา
        }

        // ตรวจสอบศัตรูที่ถูกทำลาย
        CheckForDestroyedEnemies();
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

    void CheckForDestroyedEnemies()
    {
        // ตรวจสอบว่ามีศัตรูตัวไหนถูกทำลายหรือไม่
        for (int i = currentEnemies.Count - 1; i >= 0; i--)
        {
            if (currentEnemies[i] == null)
            {
                currentEnemies.RemoveAt(i); // ลบศัตรูที่ถูกทำลายออกจากรายการ
                if (currentEnemies.Count < maxEnemies && spawnCount < maxSpawnCount)
                {
                    SpawnEnemy(); // ปล่อยศัตรูใหม่
                }
            }
        }
    }
}

