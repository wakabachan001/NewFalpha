using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
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

    //private void OnCollisionExit2D(Collision2D collision)
    //{
    //    if (collision.gameObject.name == "ActiveArea")
    //    {
    //        moveOn = false;
    //    }
    //}

    private void FixedUpdate()
    {
        if (moveOn)
        {
            time += 0.02f;//1秒で1増える

            //timeがクールタイムを超えたら
            if (time >= coolTime)
            {
                //攻撃処理
                //これの前方(下方向)に攻撃エフェクトのクローン生成
                Instantiate(AttackEffect, transform.position + (transform.up * clonepos), Quaternion.identity);
                time = 0.0f;
            }
        }
    }
}
