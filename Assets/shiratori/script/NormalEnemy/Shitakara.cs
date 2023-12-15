using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shitakara : MonoBehaviour
{
    GameObject playerObj;

    Vector3 playerCenterPosition;

    EnemyAttack enemyAttack;//攻撃用スクリプト


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

        enemyAttack = GetComponent<EnemyAttack>();

        //最初の攻撃タイミングを乱数で少し変える
        time = Random.RandomRange(0.0f, coolTime / 2);

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
        // プレイヤーの中心位置を取得
        playerCenterPosition = playerObj.transform.position;
        //Invoke("sisyou", 0.5f);

        StartCoroutine(DelayAttack());//攻撃に遅延
    }

    //攻撃に遅延をかけるコルーチン
    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(0.2f);//遅延

        // 攻撃エフェクトを生成
        enemyAttack.Attack(playerCenterPosition);
    }


}
