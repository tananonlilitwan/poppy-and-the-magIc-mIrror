using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerBrick : MonoBehaviour
{
    [SerializeField] float speed;
    public bool isZigZag = false;
    private float zigzagTimer = 0f;

    void Update()
    {
        if (isZigZag)
        {
            ZigZagMove();
        }
        else
        {
            StraightMove();
        }
    }

    void StraightMove()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < -10)
        {
            Destroy(gameObject);
        }
    }

    void ZigZagMove()
    {
        zigzagTimer += Time.deltaTime;
        float zigzag = Mathf.Sin(zigzagTimer * speed) * 2f;
        transform.Translate(new Vector2(-1f, zigzag) * speed * Time.deltaTime);

        if (transform.position.x < -10)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ตรวจสอบการชนกับวัตถุอื่น ๆ (เช่น ศัตรู)
        if (collision.CompareTag("Player"))
        {
            
            Destroy(gameObject); // ทำลายBrick
        }
    }
}
