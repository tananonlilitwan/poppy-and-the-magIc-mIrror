using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab ของกระสุน
    public Transform firePoint;     // ตำแหน่งที่กระสุนจะถูกยิงออกมา
    [SerializeField] float fireRate;     // อัตราการยิง (ทุก ๆ กี่วินาทีกระสุนจะถูกยิง)
    [SerializeField] float bulletSpeed;  // ความเร็วของกระสุน
    [SerializeField] float detectionRange; // ระยะค้นหา
    [SerializeField] float chaseDistance;  // ระยะห่างที่ไม่ไล่
    //[SerializeField] float patrolSpeed = 0.5f; // ความเร็วในการเดินปกติ
    [SerializeField] float chaseSpeed = 0.3f;  // ความเร็วในการไล่ตาม

    
    public Transform posA, posB; // ตำแหน่งเดินวน
    [SerializeField] float speed = 0.5f; // ความเร็วในการเดิน
    Vector2 targetPos;
    Transform player;
    private bool isChasing = false;
    
    public float raycastDistance = 2f; // ระยะทางของ Raycast

    private AudioManager audioManager; // เสียงในเกม
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // เสียงในเกม
       
    }
    
    void Start()
    {
        targetPos = posB.position;
        player = GameObject.FindGameObjectWithTag("Player").transform; // สมมติว่า Player มีแท็กว่า "Player"
        StartCoroutine(ShootRoutine()); // เริ่ม Coroutine สำหรับการยิง
        
        // Raycast เพื่อตรวจสอบวัตถุด้านหน้า
        RaycastCheck();
    }

    void Update()
    {
        /*float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            isChasing = true;
            ChasePlayer();
        }
        else if (isChasing && distanceToPlayer > chaseDistance)
        {
            isChasing = false;
            ReturnToPatrol();
        }
        else if (isChasing)
        {
            // ไม่เรียก Shoot() ที่นี่อีกต่อไป
        }
        else
        {
            Patrol();
        }*/
        
        // ตรวจสอบว่า player ยังไม่ถูกทำลาย
        if (player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer < detectionRange)
            {
                isChasing = true;
                ChasePlayer();
            }
            else if (isChasing && distanceToPlayer > chaseDistance)
            {
                isChasing = false;
                ReturnToPatrol();
            }
            else if (isChasing)
            {
                // ไม่เรียก Shoot() ที่นี่อีกต่อไป
            }
            else
            {
                Patrol();
            }
        }
        else
        {
            // หยุดการทำงานของ Robot เมื่อตรวจไม่เจอ Player
            isChasing = false;
            ReturnToPatrol();
        }
    }


    void Patrol()
    {
        if (Vector2.Distance(transform.position, posA.position) < 0.1f)
        {
            targetPos = posB.position;
        }
        else if (Vector2.Distance(transform.position, posB.position) < 0.1f)
        {
            targetPos = posA.position;
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }


    void ChasePlayer()
    {
        // คำนวณทิศทางไปยัง Player
        Vector2 direction = (player.position - transform.position).normalized;

        // ยึด Y ไว้ที่ตำแหน่งเดิม และอัปเดตเฉพาะ X
        Vector2 newPosition = new Vector2(player.position.x, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, newPosition, chaseSpeed * Time.deltaTime);
    }


    IEnumerator ShootRoutine()
    {
        while (true)
        {
            if (isChasing)
            {
                Shoot();
                audioManager.PlaySFX(audioManager.shoot); // เสียงSFX Get Hp
            }
            yield return new WaitForSeconds(fireRate);
        }
    }


    /*void Shoot()
    {
        // สร้างกระสุนจาก Prefab
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
    
        // คำนวณทิศทางไปยัง Player
        Vector2 direction = (player.position - firePoint.position).normalized;
    
        // ยิงกระสุนในทิศทางไปยัง Player
        rb.velocity = direction * bulletSpeed;
    }*/
    void Shoot()
    {
        // สร้างกระสุนจาก Prefab
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // ยิงกระสุนไปในทิศทางของแกน x หรือ -x ตามทิศทางของ firePoint
        Vector2 direction = firePoint.right; // firePoint.right คือตำแหน่งแกน x ขวา (1,0)

        // ยิงกระสุนไปในทิศทางของ firePoint
        rb.velocity = direction * bulletSpeed;
    }
    
    

    void ReturnToPatrol()
    {
        targetPos = transform.position; // เปลี่ยนเป้าหมายกลับไปที่ตำแหน่งปัจจุบัน
        Patrol(); // เริ่มเดินวนอีกครั้ง
    }
    
    void OnDrawGizmos()
    {
        // วาด Gizmo เพื่อแสดง Raycast ใน Editor
        Gizmos.color = Color.red;
        Gizmos.DrawRay(firePoint.position, firePoint.right * raycastDistance);
    }
    void RaycastCheck()
    {
        // ส่ง Ray ออกไปทางด้านขวาจาก firePoint
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right, raycastDistance);
    }
}
