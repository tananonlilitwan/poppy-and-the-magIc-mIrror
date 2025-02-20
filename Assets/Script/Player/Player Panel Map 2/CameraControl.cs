using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("-----------------------1------------------------------")]
    [Header("Settings for Player 1")]
    public Transform player; // ตำแหน่งของ Player
    public float smoothSpeed; // ความเร็วในการตาม Player แบบสมูท
    public Vector3 offset; // การย้ายตำแหน่งกล้องเล็กน้อยเมื่อโฟกัสที่ Player
    public float zoomSpeed; // ความเร็วในการซูม
    public float mapZoomSize; // ขนาดซูมเมื่อฉายภาพ Map โดยรวม
    public float playerZoomSize; // ขนาดซูมเมื่อโฟกัสที่ Player
    public Vector3 startPosition1; // ตำแหน่งเริ่มต้นของกล้องใน Map 1
    public GameObject map5Panel; // เพิ่มการประกาศตัวแปร Panel Map 2
    [Header("-----------------------2------------------------------")]
    [Header("Settings for Player 2")]
    public Transform player2;
    public float smoothSpeed2;
    public Vector3 offset2;
    public float zoomSpeed2;
    public float mapZoomSize2;
    public float playerZoomSize2;
    public Vector3 startPosition2; // ตำแหน่งเริ่มต้นของกล้องใน Map 2
    public GameObject map6Panel;
    
    [Header("-----------------------3------------------------------")]
    [Header("Settings for Player 3")]
    public Transform player3;
    public float smoothSpeed3;
    public Vector3 offset3;
    public float zoomSpeed3;
    public float mapZoomSize3;
    public float playerZoomSize3;
    public Vector3 startPosition3; // ตำแหน่งเริ่มต้นของกล้องใน Map 2
    public GameObject map7Panel;
    
    [Header("-----------------------4------------------------------")]
    [Header("Settings for Player 4")]
    public Transform player4;
    public float smoothSpeed4;
    public Vector3 offset4;
    public float zoomSpeed4;
    public float mapZoomSize4;
    public float playerZoomSize4;
    public Vector3 startPosition4; // ตำแหน่งเริ่มต้นของกล้องใน Map 2
    public GameObject map8Panel;
    
    [Header("-----------------------5------------------------------")]
    [Header("Settings for Player 5")]
    public Transform player5;
    public float smoothSpeed5;
    public Vector3 offset5;
    public float zoomSpeed5;
    public float mapZoomSize5;
    public float playerZoomSize5;
    public Vector3 startPosition5; // ตำแหน่งเริ่มต้นของกล้องใน Map 2
    public GameObject map9Panel;
    
    [Header("-----------------------6------------------------------")]
    [Header("Settings for Player 6")]
    public Transform player6;
    public float smoothSpeed6;
    public Vector3 offset6;
    public float zoomSpeed6;
    public float mapZoomSize6;
    public float playerZoomSize6;
    public Vector3 startPosition6; // ตำแหน่งเริ่มต้นของกล้องใน Map 2
    public GameObject map10Panel;
    
    public float aspectRatio = 1.78f; // Aspect Ratio ของกล้องแนวยาว (เช่น 16:9)
    
    private Camera cam;
    private bool isZoomingToPlayer = false; // ตรวจสอบว่ากำลังซูมเข้าหา Player หรือไม่
    
    [Header("-------------Reset -------------------------------------")]
    // ตั้งค่าซูมและตำแหน่งเริ่มต้นสำหรับ map11Panel
    public GameObject map11Panel;
    public float defaultZoomSize = 5f; // ขนาดซูมเริ่มต้น
    public Vector3 defaultStartPosition = new Vector3(0, 0, -10); // ตำแหน่งเริ่มต้นของกล้อง
    
    [SerializeField] GameObject map1Panel; // อ้างอิงถึง Panel Map 1
    [SerializeField] GameObject map2Panel; // อ้างอิงถึง Panel Map 2
    [SerializeField] GameObject map3Panel; // อ้างอิงถึง Panel Map 3
    [SerializeField] GameObject map4Panel; // อ้างอิงถึง Panel Map 4
    
     // เพิ่มการประกาศตัวแปร Panel Map 2

    void Start()
    {
        cam = GetComponent<Camera>();

        // เริ่มต้นด้วยการโฟกัสที่ Map รวม
        cam.orthographicSize = mapZoomSize;
        transform.position = new Vector3(0, 0, -10); // ปรับตำแหน่งกล้องให้อยู่ตรงกลาง Map
        
        // ตั้งค่า Orthographic Size ให้สัมพันธ์กับ mapZoomSize
        Camera.main.orthographicSize = mapZoomSize / aspectRatio;
        Camera.main.orthographicSize = mapZoomSize2 / aspectRatio;
        Camera.main.orthographicSize = mapZoomSize3 / aspectRatio;
        Camera.main.orthographicSize = mapZoomSize4 / aspectRatio;
        Camera.main.orthographicSize = mapZoomSize5 / aspectRatio;
        Camera.main.orthographicSize = mapZoomSize6 / aspectRatio;
        //transform.position = map5Panel.activeSelf ? startPosition1 : startPosition2;
        // ตั้งตำแหน่งเริ่มต้นของกล้องตามพาเนลที่เปิดอยู่
        if (map5Panel.activeSelf)
        {
            transform.position = startPosition1;
        }
        else if (map6Panel.activeSelf)
        {
            transform.position = startPosition2;
        }
        else if (map7Panel.activeSelf)
        {
            transform.position = startPosition3;
        }
        else if (map8Panel.activeSelf)
        {
            transform.position = startPosition4;
        }
        else if (map9Panel.activeSelf)
        {
            transform.position = startPosition5;
        }
        else if (map10Panel.activeSelf)
        {
            transform.position = startPosition6;
        }
        
        
        /*cam.orthographicSize = mapZoomSize; // หรือค่าเริ่มต้นอื่นที่คุณต้องการ
        transform.position = defaultStartPosition; // ตั้งค่าตำแหน่งเริ่มต้นของกล้อง*/
        
        // เริ่มต้นด้วยการโฟกัสที่ Map รวม
        //cam.orthographicSize = mapZoomSize;
        //transform.position = defaultStartPosition; // ปรับตำแหน่งกล้องให้อยู่ตรงกลาง Map

    }

    void Update()
    {
        Debug.Log("Update called");
    
        if (map1Panel.activeSelf || map2Panel.activeSelf || map3Panel.activeSelf || map4Panel.activeSelf)
        {
            ResetCamera();
            return; 
        }
        
        if (map5Panel.activeSelf && !isZoomingToPlayer)
        {
            StartCoroutine(ZoomToPlayer(player, playerZoomSize, zoomSpeed));
        }
        else if (map6Panel.activeSelf && !isZoomingToPlayer)
        {
            StartCoroutine(ZoomToPlayer(player2, playerZoomSize2, zoomSpeed2));
        }
        else if (map7Panel.activeSelf && !isZoomingToPlayer)
        {
            StartCoroutine(ZoomToPlayer(player3, playerZoomSize3, zoomSpeed3));
        }
        else if (map8Panel.activeSelf && !isZoomingToPlayer)
        {
            StartCoroutine(ZoomToPlayer(player4, playerZoomSize4, zoomSpeed4));
        }
        else if (map9Panel.activeSelf && !isZoomingToPlayer)
        {
            StartCoroutine(ZoomToPlayer(player5, playerZoomSize5, zoomSpeed5));
        }
        else if (map10Panel.activeSelf && !isZoomingToPlayer)
        {
            StartCoroutine(ZoomToPlayer(player6, playerZoomSize6, zoomSpeed6));
        }
        
        
        // ตรวจสอบว่า map11Panel ถูกเปิดหรือไม่
        if (map11Panel.activeSelf)
        {
            cam.orthographicSize = defaultZoomSize; // รีเซ็ตซูม
            transform.position = defaultStartPosition; // รีเซ็ตตำแหน่งกล้อง
            return; // ไม่ทำอะไรเพิ่มเติม
        }
    }

    /*// ฟังก์ชันในการซูมจาก Map เข้าหา Player แบบ Smooth
    IEnumerator ZoomToPlayer()
    {
        isZoomingToPlayer = true;
        float targetZoom = playerZoomSize;

        // ซูมเข้าหา Player
        while (Mathf.Abs(cam.orthographicSize - targetZoom) > 0.01f)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, zoomSpeed * Time.deltaTime);
            yield return null;
        }

        cam.orthographicSize = targetZoom; // ปรับซูมให้ตรงกับ targetZoom พอดี
        isZoomingToPlayer = false;
    }

    void LateUpdate()
    {
        // หากซูมเข้าหา Player แล้วทำให้กล้องตาม Player แบบสมูท
        if (!isZoomingToPlayer && map5Panel.activeSelf)
        {
            Vector3 desiredPosition = player.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, -10f);
        }
    }*/
    
    // ฟังก์ชันสำหรับการซูมออกและติดตามผู้เล่น
    IEnumerator ZoomOutAndFollowPlayer()
    {
        // ซูมออกให้เห็นแผนที่รวมก่อน
        cam.orthographicSize = mapZoomSize; // เริ่มต้นที่ขนาดซูมรวม
        float targetZoom = defaultZoomSize; // ขนาดซูมที่ต้องการเมื่อซูมเข้าหาผู้เล่น

        // ซูมออกอย่างราบรื่น
        while (cam.orthographicSize > targetZoom)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, zoomSpeed * Time.deltaTime);
            yield return null;
        }

        cam.orthographicSize = targetZoom; // ปรับซูมให้ตรงกับ targetZoom พอดี

        // เริ่มต้นซูมเข้าหาผู้เล่น
        if (map5Panel.activeSelf)
            yield return StartCoroutine(ZoomToPlayer(player, playerZoomSize, zoomSpeed));
        else if (map6Panel.activeSelf)
            yield return StartCoroutine(ZoomToPlayer(player2, playerZoomSize2, zoomSpeed2));
        else if (map7Panel.activeSelf)
            yield return StartCoroutine(ZoomToPlayer(player3, playerZoomSize3, zoomSpeed3));
        else if (map8Panel.activeSelf)
            yield return StartCoroutine(ZoomToPlayer(player4, playerZoomSize4, zoomSpeed4));
        else if (map9Panel.activeSelf)
            yield return StartCoroutine(ZoomToPlayer(player5, playerZoomSize5, zoomSpeed5));
        else if (map10Panel.activeSelf)
            yield return StartCoroutine(ZoomToPlayer(player6, playerZoomSize6, zoomSpeed6));
    }
    
    IEnumerator ZoomToPlayer(Transform player, float targetZoom, float zoomSpeed)
    {
        isZoomingToPlayer = true;
        

        while (Mathf.Abs(cam.orthographicSize - targetZoom) > 0.01f)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, zoomSpeed * Time.deltaTime);
            yield return null;
        }
        

        cam.orthographicSize = targetZoom;
        isZoomingToPlayer = false;
    }
    

    
    void LateUpdate()
    {
        if (!isZoomingToPlayer)
        {
            if (map5Panel.activeSelf)
            {
                FollowPlayer(player, offset, smoothSpeed);
            }
            else if (map6Panel.activeSelf)
            {
                FollowPlayer(player2, offset2, smoothSpeed2);
            }
            else if (map7Panel.activeSelf)
            {
                FollowPlayer(player3, offset3, smoothSpeed3);
            }
            else if (map8Panel.activeSelf)
            {
                FollowPlayer(player4, offset4, smoothSpeed4);
            }
            else if (map9Panel.activeSelf)
            {
                FollowPlayer(player5, offset5, smoothSpeed5);
            }
            else if (map10Panel.activeSelf)
            {
                FollowPlayer(player6, offset6, smoothSpeed6);
            }
            
        }
    }

    void FollowPlayer(Transform player, Vector3 offset, float smoothSpeed)
    {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, -10f);
    }
    
    
    void ResetCamera()
    {
        Debug.Log("Resetting camera to default settings");
        Debug.Log("Before reset, orthographic size: " + cam.orthographicSize);
    
        cam.orthographicSize = defaultZoomSize; 
        Debug.Log("After reset, orthographic size: " + cam.orthographicSize);
    
        transform.position = defaultStartPosition; 
        isZoomingToPlayer = false; 
    }
    
    
    /*public Transform player; // ตำแหน่งของ Player
    public float smoothSpeed = 0.125f; // ความเร็วในการตาม Player แบบสมูท
    public Vector3 offset; // การย้ายตำแหน่งกล้องเล็กน้อยเมื่อโฟกัสที่ Player
    public float zoomSpeed = 2.0f; // ความเร็วในการซูม
    public float mapZoomSize = 15.0f; // ขนาดซูมเมื่อฉายภาพ Map โดยรวม
    public float playerZoomSize = 5.0f; // ขนาดซูมเมื่อโฟกัสที่ Player

    public GameObject map2Panel; // เพิ่มการประกาศตัวแปร Panel Map 2

    private Camera cam;
    private bool isZoomingToPlayer = false; // ตรวจสอบว่ากำลังซูมเข้าหา Player หรือไม่

    void Start()
    {
        cam = GetComponent<Camera>();

        // เริ่มต้นด้วยการโฟกัสที่ Map รวม
        cam.orthographicSize = mapZoomSize;
        transform.position = new Vector3(0, 0, -10); // ปรับตำแหน่งกล้องให้อยู่ตรงกลาง Map
    }

    void Update()
    {
        // เมื่อเปิด Panel Map 2 จะทำให้เริ่มซูมเข้าหา Player
        if (map2Panel.activeSelf && !isZoomingToPlayer)
        {
            StartCoroutine(ZoomToPlayer());
        }
    }

    // ฟังก์ชันในการซูมจาก Map เข้าหา Player แบบ Smooth
    IEnumerator ZoomToPlayer()
    {
        isZoomingToPlayer = true;
        float targetZoom = playerZoomSize;

        // ซูมเข้าหา Player
        while (Mathf.Abs(cam.orthographicSize - targetZoom) > 0.01f)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, zoomSpeed * Time.deltaTime);
            yield return null;
        }

        cam.orthographicSize = targetZoom; // ปรับซูมให้ตรงกับ targetZoom พอดี
        isZoomingToPlayer = false;
    }

    void LateUpdate()
    {
        // หากซูมเข้าหา Player แล้วทำให้กล้องตาม Player แบบสมูท
        if (!isZoomingToPlayer && map2Panel.activeSelf)
        {
            Vector3 desiredPosition = player.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, -10f);
        }
    }*/
}
