using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAntiHoming : MonoBehaviour
{
    public GameObject playerObj;
    public float coolTime = 2.0f;//攻撃のクールタイム
    public float TPcoolTime = 1.5f;
    private float time = 0.0f;//時間計測用
    private float ATtime = 0.0f;

    private bool moveOn = false;//行動可能フラグ

    EnemyAttack enemyAttack;//攻撃用スクリプト
    enemyAttackTypeChange enemyattacktypechange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //アクティブエリア内に入ったら
        if (collision.gameObject.tag == "ActiveArea")
        {
            moveOn = true;//行動可能フラグをオン
            Debug.Log("行動可能");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.Find("Player");

        enemyAttack = GetComponent<EnemyAttack>();
        enemyattacktypechange = GetComponent<enemyAttackTypeChange>();


    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (moveOn)
        {
            time += 0.02f;//1秒で1増える
            ATtime += 0.02f;

            //timeがクールタイムを超えたら
            if (time >= TPcoolTime)
            {
                teleport();

                time = 0.0f;

            }
            if(ATtime >= coolTime)
            {
                //攻撃処理コルーチン呼び出し
                StartCoroutine(AttackCt());

                ATtime = 0.0f;
            }
        }
    }

    public void teleport()
    {
        //プレイヤーの１マス前に移動
        transform.position = playerObj.transform.position + transform.up;
    }

    private IEnumerator AttackCt()
    {
        //待機
        yield return new WaitForSeconds(1.0f);

        //攻撃処理
        enemyAttack.Attack();

        enemyattacktypechange.Boss03AttackChange();
    }
}
