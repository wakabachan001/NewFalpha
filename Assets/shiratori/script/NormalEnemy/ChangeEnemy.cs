using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEnemy : MonoBehaviour
{
    public Transform PlayerTransform;
    public Transform Enemy04Transform;
    GameObject playerObj;

    public float coolTime = 2.0f;//攻撃のクールタイム
    private float time = 0.0f;//時間計測用

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

    private void Start()
    {
        playerObj = GameObject.Find("Player");
        Vector2 EnemyPos = transform.position;
    }

    public void Swap()
    {
        //Vector2 PlayerPos = new Vector2(playerObj.transform.position.x, playerObj.transform.position.y);
        //PlayerTransform.position = Enemy04Transform.position;
        transform.position = playerObj.transform.position + transform.up;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (moveOn)
        {
            time += 0.02f;//1秒で1増える

            //timeがクールタイムを超えたら
            if (time >= coolTime)
            {
                Swap();
                time = 0.0f;


            }
        }
    }
}
