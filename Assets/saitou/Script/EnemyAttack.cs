using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float clonepos = -1.0f;//クローン生成位置調整用
    public float coolTime = 2.0f; //攻撃のクールタイム
    public int attackType = 0;    //どの攻撃ダメージを参照するか
    private float time = 0.0f;    //時間計測用

    private bool moveOn = false;//行動可能フラグ

    public GameObject AttackEffect;//クローンするプレハブ
    private GameObject cloneObj;   //クローンしたオブジェクト

    EnemyManager enemyManager;//スクリプト

    private void Start()
    {
        //最初の攻撃タイミングを乱数で少し変える
        time = Random.RandomRange(0.0f, coolTime / 2);

        //スクリプト取得
        enemyManager = gameObject.GetComponent<EnemyManager>();
    }

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
            time += 0.02f;//1秒で1増える

            //timeがクールタイムを超えたら
            if (time >= coolTime)
            {
                //攻撃処理
                //これの前方(下方向)に攻撃エフェクトのクローン生成
                cloneObj = Instantiate(AttackEffect, transform.position + (transform.up * clonepos), Quaternion.identity);

                if (enemyManager != null)
                {
                    //攻撃エフェクトのダメージ数値を変更する
                    cloneObj.GetComponent<EffectData>().damage = enemyManager.status.GetAttackDamage(attackType);
                }
                time = 0.0f;
            }
        }
    }
}
