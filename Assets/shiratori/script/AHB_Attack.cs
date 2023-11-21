using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AHB_Attack : MonoBehaviour
{
    public float clonepos = -1.0f;//クローン生成位置調整用
    public float coolTime = 2.0f;//攻撃のクールタイム
    private float time = 0.0f;//時間計測用

    private bool moveOn = false;//行動可能フラグ

    public GameObject AttackEffect;//クローンするオブジェクト

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("当たり");
        //アクティブエリア内に入ったら
        if (collision.gameObject.tag == "ActiveArea")
        {
            moveOn = true;//行動可能フラグをオン
            Debug.Log("行動可能");
        }
    }

    private void FixedUpdate()
    {
        if (true)
        {
            time += 0.02f;//1秒で1増える

            //timeがクールタイムを超えたら
            if (time >= coolTime)
            {
                Debug.Log("攻撃");
                //攻撃処理
                // Bossの X+1,Y-2 の位置に攻撃
                Vector2 position1 = new Vector2(transform.position.x + 1.0f, transform.position.y - 2.0f);
                Instantiate(AttackEffect, position1, Quaternion.identity);

                // Bossの X-1,Y-2 の位置に攻撃
                Vector2 position2 = new Vector2(transform.position.x - 1.0f, transform.position.y - 2.0f);
                Instantiate(AttackEffect, position2, Quaternion.identity);

                time = 0.0f;
            }
        }
    }
}