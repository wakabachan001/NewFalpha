using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEnemy : MonoBehaviour
{
    GameObject playerObj;

    private Vector2 EnemyPos;

    public float clonepos = -1.0f;//クローン生成位置調整用
    public float coolTime = 2.0f;//攻撃のクールタイム
    private float time = 0.0f;//時間計測用

    private bool moveOn = false;//行動可能フラグ

    EnemyAttack enemyAttack;//攻撃用スクリプト


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
        EnemyPos = transform.position;

        enemyAttack = GetComponent<EnemyAttack>();

        //最初の攻撃タイミングを乱数で少し変える
        time = Random.RandomRange(0.0f, coolTime / 2);
    }

    public void teleport()
    {
        //プレイヤーの１マス前に移動
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
                teleport();


                //攻撃処理コルーチン呼び出し
                StartCoroutine(AttackCt());


                //逃走処理コルーチン呼び出し
                StartCoroutine(Escape());
                time = 0.0f;

            }
        }
    }

    private IEnumerator AttackCt()
    {
        //待機
        yield return new WaitForSeconds(0.4f);

        //攻撃処理
        enemyAttack.Attack();
    }

    private IEnumerator Escape()
    {
        //待機
        yield return new WaitForSeconds(0.6f);

        //元の位置に逃げる
        transform.position = EnemyPos;
    }
}
