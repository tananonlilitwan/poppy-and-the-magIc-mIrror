using System.Collections;
using UnityEngine;
using UnityEngine.AI; // สำหรับระบบนำทางด้วย NavMesh

public class CatAi : MonoBehaviour
{
    #region <CatAi // Vertion 1>
    /*public Transform player; // ตำแหน่งของ Player
    public LayerMask boxLayer; // Layer ของ Box
    public float followDuration = 10f; // เวลาในการนำทาง
    public float cooldownDuration = 10f; // คูลดาวห์
    public float detectRange = 10f; // ระยะที่ Cat ตรวจจับ Box
    public float jumpForce = 5f; // ความแรงในการกระโดด
    public ParticleSystem glowEffect; // เอฟเฟคแสงกระพริบ

    private bool isFollowing = false;
    private bool isCooldown = false;
    private GameObject targetBox; // วัตถุ Box ที่ Cat จะนำทางไป
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        glowEffect.Stop(); // ปิดเอฟเฟคแสงก่อน
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isCooldown)
        {
            SummonCat();
        }

        if (isFollowing)
        {
            MoveToBox();
        }
    }

    void SummonCat()
    {
        // เกิด Cat ขึ้นตรงหน้าผู้เล่น
        transform.position = player.position + Vector3.right * 2; // วาง Cat ด้านขวาผู้เล่น
        isFollowing = true;
        glowEffect.Play(); // เริ่มเอฟเฟคแสง
        FindNearestBox();
        StartCoroutine(FollowDuration());
    }

    void FindNearestBox()
    {
        // หาวัตถุที่มี Tag "Box" ใกล้ที่สุด
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectRange, boxLayer);
        float minDistance = Mathf.Infinity;
        GameObject closestBox = null;

        foreach (Collider2D collider in hitColliders)
        {
            float distance = Vector2.Distance(transform.position, collider.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestBox = collider.gameObject;
            }
        }

        if (closestBox != null)
        {
            targetBox = closestBox;
        }
    }

    void MoveToBox()
    {
        if (targetBox != null)
        {
            Vector2 direction = (targetBox.transform.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * 3f, rb.velocity.y); // เดินไปหากล่อง

            // กระโดดถ้ามีสิ่งกีดขวาง
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f);
            if (hit.collider != null && hit.collider.CompareTag("Trap")) // หลบ Trap
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce); // กระโดดหลบ
            }
        }
    }

    IEnumerator FollowDuration()
    {
        yield return new WaitForSeconds(followDuration); // นำทาง 10 วินาที
        isFollowing = false;
        glowEffect.Stop(); // ปิดเอฟเฟคแสง
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(cooldownDuration); // รอคูลดาวห์ 10 วินาที
        isCooldown = false;
    }

    void OnDrawGizmosSelected()
    {
        // แสดงระยะตรวจจับ Box
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }*/
    #endregion
    
    #region <CatAi // Vertion 2>
    /*public Transform player; // ตำแหน่งของ Player
    public LayerMask boxLayer; // Layer ของ Box
    public float followDuration = 10f; // เวลาในการนำทาง
    public float cooldownDuration = 10f; // คูลดาวน์
    public float detectRange = 10f; // ระยะที่ Cat ตรวจจับ Box
    public float jumpForce = 5f; // ความแรงในการกระโดด
    public ParticleSystem glowEffect; // เอฟเฟคแสงกระพริบ

    private bool isFollowing = false;
    private bool isCooldown = false;
    private GameObject targetBox; // วัตถุ Box ที่ Cat จะนำทางไป
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        glowEffect.Stop(); // ปิดเอฟเฟคแสงก่อน
        FindPlayer();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isCooldown)
        {
            SummonCat();
        }

        if (isFollowing)
        {
            MoveToBox();
        }
    }

    void FindPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure your player GameObject has the tag 'Player'.");
        }
    }

    void SummonCat()
    {
        transform.position = player.position + Vector3.right; // วาง Cat ด้านขวาผู้เล่น
        isFollowing = true; // เริ่มติดตาม
        glowEffect.Play(); // เริ่มเอฟเฟคแสง
        FindNearestBox(); // หาวัตถุ Box ใกล้ที่สุด
        StartCoroutine(FollowDuration());
    }

    void FindNearestBox()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectRange, boxLayer);
        float minDistance = Mathf.Infinity;
        GameObject closestBox = null;

        foreach (Collider2D collider in hitColliders)
        {
            float distance = Vector2.Distance(transform.position, collider.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestBox = collider.gameObject;
            }
        }

        if (closestBox != null)
        {
            targetBox = closestBox;
        }
    }

    void MoveToBox()
    {
        if (targetBox != null)
        {
            Vector2 direction = (targetBox.transform.position - transform.position).normalized;
            rb.velocity = new Vector2(direction.x * 3f, rb.velocity.y); // เดินไปหากล่อง

            // กระโดดถ้ามีสิ่งกีดขวาง
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f);
            if (hit.collider != null && hit.collider.CompareTag("Trap")) // หลบ Trap
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce); // กระโดดหลบ
            }

            if (Vector2.Distance(transform.position, targetBox.transform.position) < 1f)
            {
                isFollowing = false; // หยุดติดตามเมื่อถึงกล่อง
            }
        }
    }

    IEnumerator FollowDuration()
    {
        yield return new WaitForSeconds(followDuration); // นำทาง 10 วินาที
        isFollowing = false;
        glowEffect.Stop(); // ปิดเอฟเฟคแสง
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        isCooldown = true; // เริ่มสถานะคูลดาวน์
        yield return new WaitForSeconds(cooldownDuration); // รอคูลดาวน์ 10 วินาที
        isCooldown = false; // สิ้นสุดสถานะคูลดาวน์
    }

    void OnDrawGizmosSelected()
    {
        // แสดงระยะตรวจจับ Box
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }*/
    #endregion
    
    public Transform player; // ตำแหน่งของ Player
    public LayerMask boxLayer; // Layer ของ Box
    public float followDuration = 10f; // เวลาในการนำทาง
    public float cooldownDuration = 10f; // คูลดาวน์
    public float detectRange = 10f; // ระยะที่ Cat ตรวจจับ Box
    public float jumpForce = 5f; // ความแรงในการกระโดด
    public ParticleSystem glowEffect; // เอฟเฟคแสงกระพริบ

    private bool isFollowing = false;
    private bool isCooldown = false;
    private GameObject targetBox; // วัตถุ Box ที่ Cat จะนำทางไป
    private Rigidbody2D rb;
    
    private bool isGrounded = false; // ตัวแปรเพื่อตรวจสอบว่าตกพื้นไหม

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        glowEffect.Stop(); // ปิดเอฟเฟคแสงก่อน
        FindPlayer();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isCooldown)
        {
            SummonCat();
        }

        if (isFollowing)
        {
            MoveToBox();
        }
        if (isFollowing)
        {
            CheckIfGrounded(); // ตรวจสอบสถานะว่าตกพื้นหรือไม่
            MoveToBox();
        }
    }

    void FindPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player not found! Make sure your player GameObject has the tag 'Player'.");
        }
    }

    void SummonCat()
    {
        transform.position = player.position + Vector3.right; // วาง Cat ด้านขวาผู้เล่น
        isFollowing = true; // เริ่มติดตาม
        glowEffect.Play(); // เริ่มเอฟเฟคแสง
        FindNearestBox(); // หาวัตถุ Box ใกล้ที่สุด
        StartCoroutine(FollowDuration());
    }

    void FindNearestBox()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectRange, boxLayer);
        float minDistance = Mathf.Infinity;
        GameObject closestBox = null;

        foreach (Collider2D collider in hitColliders)
        {
            float distance = Vector2.Distance(transform.position, collider.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestBox = collider.gameObject;
            }
        }

        if (closestBox != null)
        {
            targetBox = closestBox;
        }
    }

    void MoveToBox()
    {
        if (targetBox != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // ถ้าระยะห่างจาก Cat ไปยัง Player เกินระยะค้นหา ให้ Cat วิ่งไปหา Box
            if (distanceToPlayer > detectRange)
            {
                Vector2 direction = (targetBox.transform.position - transform.position).normalized;
                rb.velocity = new Vector2(direction.x * 3f, rb.velocity.y); // เดินไปหากล่อง
            }
            else
            {
                Vector2 direction = (targetBox.transform.position - transform.position).normalized;
                rb.velocity = new Vector2(direction.x * 3f, rb.velocity.y); // เดินไปหากล่อง

                /*// ตรวจสอบสิ่งกีดขวาง
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f);
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Trap") || hit.collider.CompareTag("groung") || hit.collider.CompareTag("ArrowSign"))
                    {
                        // กระโดดเมื่อเจอสิ่งกีดขวาง
                        if (rb.velocity.y == 0) // ถ้าไม่กระโดดอยู่
                        {
                            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // กระโดดหลบ
                        }
                    }
                }*/
                // ตรวจสอบสิ่งกีดขวาง
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f);
                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Trap") || hit.collider.CompareTag("groung") || hit.collider.CompareTag("ArrowSign"))
                    {
                        // กระโดดเมื่อเจอสิ่งกีดขวาง
                        if (isGrounded) // เปลี่ยนเงื่อนไขให้ตรวจสอบตัวแปร isGrounded
                        {
                            rb.velocity = new Vector2(rb.velocity.x, jumpForce); // กระโดดหลบ
                        }
                    }
                }
            }

            // หยุดการติดตามเมื่อถึงกล่อง
            if (Vector2.Distance(transform.position, targetBox.transform.position) < 1f)
            {
                isFollowing = false; // หยุดติดตามเมื่อถึงกล่อง
            }
            
        }
        
    }

    void CheckIfGrounded()
    {
        // ใช้ Physics2D.Raycast เพื่อตรวจสอบว่าตกพื้นหรือไม่
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        isGrounded = hit.collider != null; // ถ้ามี Collider ใต้ Cat แสดงว่าตกพื้น
    }
    
    IEnumerator FollowDuration()
    {
        yield return new WaitForSeconds(followDuration); // นำทาง 10 วินาที
        isFollowing = false;
        glowEffect.Stop(); // ปิดเอฟเฟคแสง
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        isCooldown = true; // เริ่มสถานะคูลดาวน์
        yield return new WaitForSeconds(cooldownDuration); // รอคูลดาวน์ 10 วินาที
        isCooldown = false; // สิ้นสุดสถานะคูลดาวน์
    }

    void OnDrawGizmosSelected()
    {
        // แสดงระยะตรวจจับ Box
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}