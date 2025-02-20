using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBrick : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public float spawnInterval = 2f;

    private float spawnTimer = 0f;
    private int currentPattern = 0;

    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnEnemyPattern();
            spawnTimer = 0f;
        }
    }

    void SpawnEnemyPattern()
    {
        // เลือก pattern ของศัตรูตาม currentPattern
        switch (currentPattern)
        {
            case 0:
                // pattern ที่ 1: ศัตรูวิ่งเป็นเส้นตรง
                SpawnStraightEnemies();
                break;
            case 1:
                // pattern ที่ 2: ศัตรูวิ่งเป็นซิกแซก
                SpawnZigZagEnemies();
                break;
            case 2:
                // pattern ที่ 3: ศัตรูวิ่งแบบกลุ่ม
                SpawnGroupEnemies();
                break;
        }

        // เปลี่ยนไป pattern ถัดไป
        currentPattern = (currentPattern + 1) % 3;
    }

    void SpawnStraightEnemies()
    {
        Instantiate(enemyPrefabs[0], new Vector2(10, Random.Range(-4f, 4f)), Quaternion.identity);
    }

    void SpawnZigZagEnemies()
    {
        Instantiate(enemyPrefabs[1], new Vector2(10, Random.Range(-4f, 4f)), Quaternion.identity);
    }

    void SpawnGroupEnemies()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(enemyPrefabs[2], new Vector2(10, -2 + i), Quaternion.identity);
        }
    }
}
