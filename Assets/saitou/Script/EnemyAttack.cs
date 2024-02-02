using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float clonepos = -1.0f;//クローン生成位置調整用
    public float coolTime = 2.0f; //攻撃のクールタイム
    public int attackType = 0;    //どの攻撃ダメージを参照するか
    public float time = 0.0f;    //時間計測用

    private bool moveOn = false;//行動可能フラグ

    public GameObject[] AttackEffect = new GameObject[2];//クローンするプレハブ
    private GameObject cloneObj;   //クローンしたオブジェクト
    private GameObject objCanvas; //キャンバスのオブジェクト

    EnemyManager enemyManager;//スクリプト
    enemyAttackTypeChange enemyattacktypechange;//スクリプト
    BossHPBarACTIVE active_boss_hpbar;//スクリプト

    private void Start()
    {
        //最初の攻撃タイミングを乱数で少し変える
        time = Random.RandomRange(0.0f, coolTime / 2);

        objCanvas = GameObject.Find("Canvas");

        //スクリプト取得
        //enemyManager = gameObject.GetComponent<EnemyManager>();
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

            //timeがクールタイムを超えたら
            if (time >= coolTime)
            {
                Attack();//攻撃処理呼び出し

                // Boss01ならこの関数の中に入る
                if( gameObject.name == "Boss01(Clone)")
                    enemyattacktypechange.Boss01AttackChange();

                time = 0.0f;//時間リセット
            }
        }
    }

    //攻撃処理関数
    public void Attack()
    {
        enemyManager = gameObject.GetComponent<EnemyManager>();

        //指定の位置に攻撃エフェクトのクローン生成
        cloneObj = Instantiate(AttackEffect[attackType], transform.position + (transform.up * clonepos), Quaternion.identity);

        if (enemyManager != null)
        {
            //攻撃エフェクトのダメージ数値を変更する
            cloneObj.GetComponent<EffectData>().damage = enemyManager.status.GetAttackDamage(attackType);
        }
    }

    //位置指定オーバーロード
    public void Attack(Vector3 pos)
    {
        enemyManager = gameObject.GetComponent<EnemyManager>();

        //指定の位置に攻撃エフェクトのクローン生成
        cloneObj = Instantiate(AttackEffect[attackType], pos, Quaternion.identity);

        if (enemyManager != null)
        {
            //攻撃エフェクトのダメージ数値を変更する
            cloneObj.GetComponent<EffectData>().damage = enemyManager.status.GetAttackDamage(attackType);
        }
    }

}
