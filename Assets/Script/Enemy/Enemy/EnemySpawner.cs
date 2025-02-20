using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [Header("-----------------------Dialog-End Map4-----------------------------")]
    [SerializeField] private GameObject winDialogPanel; // Reference to the Panel
    [SerializeField] private TextMeshProUGUI winDialogText; // Reference to the TextMeshPro component
    [SerializeField] private string winMessage; // The message to display
    
    
    public GameObject[] enemyPrefabs;  // Prefab ของศัตรู
    public float spawnInterval = 2f;   // ระยะห่างในการเกิดศัตรูแต่ละครั้ง
    public int maxEnemies = 5;         // จำนวนศัตรูสูงสุดที่สามารถอยู่ในฉากได้ในแต่ละครั้ง
    public int maxSpawnCount = 10;     // จำนวนการเกิดศัตรูสูงสุด

    private float spawnTimer = 0f;
    private List<GameObject> currentEnemies = new List<GameObject>(); // รายชื่อศัตรูที่อยู่ในฉาก
    private int spawnCount = 0;
    
    private bool gameWon = false;      // ใช้เพื่อตรวจสอบว่าชนะแล้วหรือไม่
    
    [SerializeField] private GameObject nextPanel; // ตัวแปรสำหรับ Obj Square (1)
    [SerializeField] private GameObject arrowObject; // ตัวแปรสำหรับ Obj ลูกศร

    // ขอบเขตการสร้างศัตรู
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;

    void Start()
    {
        // Make sure the dialog is hidden at the start
        winDialogPanel.SetActive(false);
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
            nextPanel.SetActive(true); // เปิด Obj Square (1)
            arrowObject.SetActive(true);
            gameWon = true;  // ตั้งค่าเพื่อหยุดแสดงข้อความ "Win" ซ้ำ
            ShowWinDialog(); // เรียกฟังก์ชันแสดงข้อความชนะ
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
            
            
            // ถ้าไม่มีศัตรูในฉากแล้ว ให้เปิด Obj Square (1)
            if (currentEnemies.Count == 0 && gameWon)
            {
                // เปิด Obj Square (1)
                if (nextPanel != null) // ตรวจสอบว่า nextPanel ถูกตั้งค่าไว้หรือไม่
                {
                    nextPanel.SetActive(true); // เปิด Obj Square (1)
                    arrowObject.SetActive(true);
                    Debug.Log("Next Panel (Obj Square (1)) is now active.");
                    
                    ShowWinDialog(); // เรียกฟังก์ชันเพื่อแสดง Dialog
                }
            }
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
    
    private void ShowWinDialog()
    {
        // แสดง UI ที่แสดงข้อความชนะ
        if (winDialogPanel != null) // เปลี่ยนจาก winDialog เป็น winDialogPanel
        {
            winDialogPanel.SetActive(true); // เปิด UI ชนะ
            winDialogText.text = winMessage; // กำหนดข้อความที่จะแสดงใน TextMeshPro
            Debug.Log("แสดงข้อความชนะ"); // ข้อความในคอนโซล
        }
    }

}
