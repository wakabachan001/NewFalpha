using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss03 : EnemyAttack
{
    public GameObject playerObj;
    public float TPcoolTime = 1.5f;
    private float ATtime = 0.0f;

    private bool moveOn = false;//行動可能フラグ

    //public GameObject[] AttackEffect = new GameObject[2];//クローンするプレハブ
    private GameObject objCanvas; //キャンバスのオブジェクト

    enemyAttackTypeChange enemyattacktypechange;//スクリプト
    BossHPBarACTIVE active_boss_hpbar;//スクリプト

    private void Start()
    {
        //最初の攻撃タイミングを乱数で少し変える
        time = Random.RandomRange(0.0f, coolTime / 2);

        objCanvas = GameObject.Find("Canvas");
        playerObj = GameObject.Find("Player");

        //スクリプト取得
        enemyattacktypechange = gameObject.GetComponent<enemyAttackTypeChange>();
        active_boss_hpbar = objCanvas.GetComponent<BossHPBarACTIVE>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //アクティブエリア内に入ったら
        if (collision.gameObject.tag == "ActiveArea")
        {
            moveOn = true;//行動可能フラグをオン

            if (GameObject.FindWithTag("Boss"))
            {   //BossHPBarを表示させる
                active_boss_hpbar.ActiveBossHPBar();
                Debug.Log("BossHP");
            }
            Debug.Log("行動可能");
        }
    }


    private void FixedUpdate()
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
            if (ATtime >= coolTime)
            {
                //攻撃処理コルーチン呼び出し
                StartCoroutine(AttackCt());

                ATtime = 0.0f;
            }
        }
    }

    private  void teleport()
    {
        //プレイヤーの１マス前に移動
        transform.position = playerObj.transform.position + transform.up;
    }

    private IEnumerator AttackCt()
    {
        //待機
        yield return new WaitForSeconds(1.0f);

        //攻撃処理
        Attack();

        enemyattacktypechange.Boss03AttackChange();
    }
}
