using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kamikaze : MonoBehaviour
{
    public float speed = 2.0f;

    private bool moveOn = false;//行動可能フラグ
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //アクティブエリア内に入ったら
        if (collision.gameObject.tag == "ActiveArea")
        {
            moveOn = true;//行動可能フラグをオン
            Debug.Log("行動可能");
        }
    }

    private void FixedUpdate()
    {
        if (moveOn)
        {
            transform.position -= transform.up * (speed * 0.02f);
        }
    }
}
