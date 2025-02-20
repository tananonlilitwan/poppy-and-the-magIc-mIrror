using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverController : MonoBehaviour
{
    /*public float moveDistance = 100f; // ระยะที่คันโยกจะโยก
    private bool isPlayerNear = false; // ตรวจสอบว่าผู้เล่นอยู่ใกล้หรือไม่
    private Vector3 originalPosition; // ตำแหน่งเริ่มต้นของคันโยก
    
    private float rotationAmount = -45f; // จำนวนองศาที่จะหมุน (ปรับตามต้องการ)
    
    [SerializeField] GameObject Dor; // อ้างอิงถึง GameObject Dor ที่จะเลื่อนลง

    void Start()
    {
        originalPosition = transform.position; // บันทึกตำแหน่งเริ่มต้น
    }

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E)) // กด E เพื่อโยกคันโยก
        {
            // โยกคันโยกไปทาง -X
            //transform.position += Vector3.left * moveDistance;
            //Debug.Log("คันโยกโยกไปทาง -X");
            //Debug.Log("ตำแหน่งใหม่: " + transform.position); // แสดงตำแหน่งใหม่
            // หมุนคันโยก
            transform.Rotate(0, 0, rotationAmount);
            Debug.Log("คันโยกถูกโยกไป");
            
            // เลื่อนวัตถุ Dor ลงตามแกน Y หรือ Z (ขึ้นอยู่กับ 2D หรือ 3D)
            MoveDor(); // เรียกฟังก์ชันสำหรับเลื่อน Dor

        }
    }

    void MoveDor()
    {
        // ในกรณีที่เป็นเกม 2D ใช้แกน Y สำหรับเลื่อน Dor ลง
        // ถ้าเป็นเกม 3D ใช้แกน Z
        Dor.transform.position = new Vector3(Dor.transform.position.x, Dor.transform.position.y - moveDistance, Dor.transform.position.z);
        Debug.Log("Dor เลื่อนลงไปแล้ว");
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // ตรวจสอบว่าชนกับผู้เล่น
        {
            isPlayerNear = true; // ตั้งค่าสถานะผู้เล่นใกล้
            Debug.Log("ผู้เล่นอยู่ใกล้คันโยก");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // ตรวจสอบว่าผู้เล่นออกจากการชน
        {
            isPlayerNear = false; // ตั้งค่าสถานะผู้เล่นไม่ใกล้
            Debug.Log("ผู้เล่นออกจากการชนคันโยก");
        }
    }*/
    
    
    /*[SerializeField] float moveDistance; // ระยะที่ Dor จะเลื่อนลง
    [SerializeField] float moveSpeed; // ความเร็วในการเลื่อน Dor
    private bool isPlayerNear = false; // ตรวจสอบว่าผู้เล่นอยู่ใกล้หรือไม่
    private bool isLeverPulled = false; // ตรวจสอบว่าคันโยกถูกโยกแล้วหรือไม่

    private Vector3 originalPosition; // ตำแหน่งเริ่มต้นของคันโยก
    private Vector3 dorFinalPosition; // ตำแหน่งสุดท้ายที่ Dor จะเลื่อนไป

    private float rotationAmount = -45f; // จำนวนองศาที่จะหมุนคันโยก

    [SerializeField] GameObject Dor; // อ้างอิงถึง GameObject Dor ที่จะเลื่อนลง

    void Start()
    {
        originalPosition = transform.position; // บันทึกตำแหน่งเริ่มต้นของคันโยก
        dorFinalPosition = new Vector3(Dor.transform.position.x, Dor.transform.position.y - moveDistance, Dor.transform.position.z); // ตำแหน่งปลายทางของ Dor
    }

    void Update()
    {
        // ตรวจสอบว่าผู้เล่นอยู่ใกล้และกดปุ่ม E หนึ่งครั้ง
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && !isLeverPulled)
        {
            // หมุนคันโยกโดยใช้ rotationAmount
            transform.Rotate(0, 0, rotationAmount);
            
            // เริ่มเลื่อน Dor ลง
            isLeverPulled = true; // คันโยกถูกโยกแล้ว
            
        }

        // ถ้าคันโยกถูกโยกแล้ว ให้เลื่อน Dor ลงจนถึงตำแหน่งปลายทาง
        if (isLeverPulled)
        {
            MoveDor(); // เรียกฟังก์ชันสำหรับเลื่อน Dor
        }
    }

    void MoveDor()
    {
        // เลื่อน Dor ลงไปทีละน้อยจนกว่าจะถึงตำแหน่งปลายทาง
        Dor.transform.position = Vector3.MoveTowards(Dor.transform.position, dorFinalPosition, moveSpeed * Time.deltaTime);
        
        // ตรวจสอบว่า Dor ถึงตำแหน่งปลายทางแล้วหรือยัง
        if (Dor.transform.position == dorFinalPosition)
        {
            isLeverPulled = false; // หยุดการเลื่อน Dor เมื่อถึงปลายทาง
            Debug.Log("Dor เลื่อนลงจนสุดแล้ว");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // ตรวจสอบว่าชนกับผู้เล่น
        {
            isPlayerNear = true; // ตั้งค่าสถานะผู้เล่นใกล้
            Debug.Log("ผู้เล่นอยู่ใกล้คันโยก");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // ตรวจสอบว่าผู้เล่นออกจากการชน
        {
            isPlayerNear = false; // ตั้งค่าสถานะผู้เล่นไม่ใกล้
            Debug.Log("ผู้เล่นออกจากการชนคันโยก");
        }
    }*/

    
    /*[SerializeField] float moveSpeed; // ความเร็วในการเลื่อน Dor
    private bool isPlayerNear = false; // ตรวจสอบว่าผู้เล่นอยู่ใกล้หรือไม่
    private bool isLeverPulled = false; // ตรวจสอบว่าคันโยกถูกโยกแล้วหรือไม่
    private bool hasRotated = false; // ตรวจสอบว่าคันโยกหมุนไปแล้วหรือยัง
    private Vector3 originalPosition; // ตำแหน่งเริ่มต้นของคัน
    private Vector3 dorFinalPosition; // ตำแหน่งสุดท้ายที่ Dor จะเลื่อนไป
    private float rotationAmount = -45f; // จำนวนองศาที่จะหมุนคันโยก
    [SerializeField] GameObject Dor; // อ้างอิงถึง GameObject Dor ที่จะเลื่อนลง
    [SerializeField] float moveDistance; // ระยะที่ Dor จะเลื่อนลง
    
    void Start()
    {
        originalPosition = transform.position; // บันทึกตำแหน่งเริ่มต้นของคันโยก
        dorFinalPosition = new Vector3(Dor.transform.position.x, Dor.transform.position.y - moveDistance, Dor.transform.position.z); // ตำแหน่งปลายทางของ Dor
    }
    
    void Update()
    {
        // ตรวจสอบว่าผู้เล่นอยู่ใกล้และกดปุ่ม E หนึ่งครั้ง
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && !isLeverPulled && !hasRotated)
        {
            // หมุนคันโยกโดยใช้ rotationAmount
            transform.Rotate(0, 0, rotationAmount);
            // เริ่มเลื่อน Dor ลง
            isLeverPulled = true; // คันโยกถูกโยกแล้ว
            hasRotated = true; // คันโยกหมุนแล้ว
        }
        
        // ถ้าคันโยกถูกโยกแล้ว ให้เลื่อน Dor ลงจนถึงตำแหน่งปลายทาง
        if (isLeverPulled)
        {
            MoveDor(); // เรียกฟังก์ชันสำหรับเลื่อน Dor
        }
    }
    
    void MoveDor()
    {
        // เลื่อน Dor ลงไปทีละน้อยจนกว่าจะถึงตำแหน่งปลายทาง
        Dor.transform.position = Vector3.MoveTowards(Dor.transform.position, dorFinalPosition, moveSpeed * Time.deltaTime);
        // ตรวจสอบว่า Dor ถึงตำแหน่งปลายทางแล้วหรือยัง
        if (Dor.transform.position == dorFinalPosition)
        {
            isLeverPulled = false; // หยุดการเลื่อน Dor เมื่อถึงปลายทาง
            Debug.Log("Dor เลื่อนลงจนสุดแล้ว");
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // ตรวจสอบว่าชนกับผู้เล่น
        {
            isPlayerNear = true; // ตั้งค่าสถานะผู้เล่นใกล้
            Debug.Log("ผู้เล่นอยู่ใกล้คันโยก");
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // ตรวจสอบว่าผู้เล่นออกจากการชน
        {
            isPlayerNear = false; // ตั้งค่าสถานะผู้เล่นไม่ใกล้
            Debug.Log("ผู้เล่นออกจากการชนคันโยก");
        }
    }*/
    
    [SerializeField] float moveSpeed; // ความเร็วในการเลื่อน Dor
    private bool isPlayerNear = false; // ตรวจสอบว่าผู้เล่นอยู่ใกล้หรือไม่
    private bool isLeverPulled = false; // ตรวจสอบว่าคันโยกถูกโยกแล้วหรือไม่
    private bool hasRotated = false; // ตรวจสอบว่าคันโยกหมุนไปแล้วหรือยัง
    private bool isDorAtFinalPosition = false; // ตรวจสอบว่า Dor เลื่อนถึงปลายทางแล้วหรือยัง

    private Vector3 originalPosition; // ตำแหน่งเริ่มต้นของคันโยก
    private Vector3 dorFinalPosition; // ตำแหน่งสุดท้ายที่ Dor จะเลื่อนไป

    private float rotationAmount = -45f; // จำนวนองศาที่จะหมุนคันโยก

    [SerializeField] GameObject Dor; // อ้างอิงถึง GameObject Dor ที่จะเลื่อนลง
    [SerializeField] float moveDistance; // ระยะที่ Dor จะเลื่อนลง

    /*private AudioManager audioManager; // เสียงในเกม
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // เสียงในเกม
       
    }*/
    private AudioManager audioManager; // เสียงในเกม
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); // เสียงในเกม
       
    }
    void Start()
    {
        originalPosition = transform.position; // บันทึกตำแหน่งเริ่มต้นของคันโยก
        dorFinalPosition = new Vector3(Dor.transform.position.x, Dor.transform.position.y - moveDistance, Dor.transform.position.z); // ตำแหน่งปลายทางของ Dor
    }

    void Update()
    {
        // ตรวจสอบว่าผู้เล่นอยู่ใกล้และกดปุ่ม E หนึ่งครั้ง
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && !isLeverPulled && !hasRotated)
        {
            audioManager.PlaySFX(audioManager.click);
            // หมุนคันโยกโดยใช้ rotationAmount
            transform.Rotate(0, 0, rotationAmount);
           // audioManager.PlaySFX(audioManager.Lever); // เสียงSFX Get Hp

            // เริ่มเลื่อน Dor ลง
            isLeverPulled = true; // คันโยกถูกโยกแล้ว
            hasRotated = true; // คันโยกหมุนแล้ว
        }
        
        /*if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && !isLeverPulled && !hasRotated)
        {
            Debug.Log("กดปุ่ม E เพื่อโยกคันโยก");
            transform.Rotate(0, 0, rotationAmount);
            isLeverPulled = true; 
            hasRotated = true; 
        }*/

        // ถ้าคันโยกถูกโยกแล้ว และประตูยังไม่ถึงตำแหน่งสุดท้าย ให้เลื่อน Dor ลง
        if (isLeverPulled && !isDorAtFinalPosition)
        {
            MoveDor(); // เรียกฟังก์ชันสำหรับเลื่อน Dor
        }
    }

    void MoveDor()
    {
        // เลื่อน Dor ลงไปทีละน้อยจนกว่าจะถึงตำแหน่งปลายทาง
        Dor.transform.position = Vector3.MoveTowards(Dor.transform.position, dorFinalPosition, moveSpeed * Time.deltaTime);

        // ตรวจสอบว่า Dor ถึงตำแหน่งปลายทางแล้วหรือยัง
        if (Dor.transform.position == dorFinalPosition)
        {
            isLeverPulled = false; // หยุดการเลื่อน Dor เมื่อถึงปลายทาง
            isDorAtFinalPosition = true; // ตั้งค่าว่า Dor ถึงปลายทางแล้ว
            Debug.Log("Dor เลื่อนลงจนสุดแล้ว");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // ตรวจสอบว่าชนกับผู้เล่น
        {
            isPlayerNear = true; // ตั้งค่าสถานะผู้เล่นใกล้
            Debug.Log("ผู้เล่นอยู่ใกล้คันโยก");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // ตรวจสอบว่าผู้เล่นออกจากการชน
        {
            isPlayerNear = false; // ตั้งค่าสถานะผู้เล่นไม่ใกล้
            Debug.Log("ผู้เล่นออกจากการชนคันโยก");
        }
    }
    
    /*void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // ตรวจสอบว่าชนกับผู้เล่น
        {
            isPlayerNear = true; // ตั้งค่าสถานะผู้เล่นใกล้
            Debug.Log("ผู้เล่นอยู่ใกล้คันโยก");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // ตรวจสอบว่าผู้เล่นออกจากการชน
        {
            isPlayerNear = false; // ตั้งค่าสถานะผู้เล่นไม่ใกล้
            Debug.Log("ผู้เล่นออกจากการชนคันโยก");
        }
    }*/

    
}
