using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shitakara : MonoBehaviour
{
    GameObject playerObj;
    public GameObject enemy; // 敵のゲームオブジェクト
    public GameObject attackEffect; // 攻撃エフェクト

    Vector3 playerCenterPosition;


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
        playerObj = GameObject.Find("player");
        
    }

    private void FixedUpdate()
    {
        if (moveOn)
        {
            time += 0.02f;//1秒で1増える

            //timeがクールタイムを超えたら
            if (time >= coolTime)
            {
                Attack();
                time = 0.0f;
            }
        }
    }

    void Attack()
    {
        if (enemy != null)
        {
            // プレイヤーの中心位置を取得
            playerCenterPosition = playerObj.transform.position;
           //Invoke("sisyou", 0.5f);
            // 攻撃エフェクトを生成
            if (attackEffect != null)
            {
                Instantiate(attackEffect, playerCenterPosition, Quaternion.identity);
                // ここに攻撃エフェクトの発生音などを追加する

                // 攻撃範囲内の敵にダメージを与える処理などをここに追加する
            }
        }
    }
   
}
