using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class BossManager : MonoBehaviour
{
    [SerializeField] private float maxHP = 100.0f;       //最大体力
    [SerializeField] private int money = 100;            //落とすお金
    [SerializeField] private float attackDamage1 = 10.0f;//攻撃1のダメージ
    [SerializeField] private float attackDamage2 = 10.0f;//攻撃2のダメージ

    private float currentHP;    //現在のHP
    private float takeDamage;   //被ダメージ

    private StatusData enemyStatus = new StatusData();    //敵ステータスクラス
    private StatusCalc statusCalc = new StatusCalc();     //ダメージ計算クラス

    PlayerStatusManager playerStatusManager;//PlayerStatusManagerスクリプト
    GameObject obj;//DataInfo用

    void Start()
    {
        Debug.Log("ボス初期化");
        //ステータスをクラスで管理
        enemyStatus.MaxHP = maxHP;
        enemyStatus.Money = money;
        enemyStatus.SetAttackDamage(0, attackDamage1);
        enemyStatus.SetAttackDamage(1, attackDamage2);

        Debug.Log("ボス初期化完了");

        currentHP = maxHP;
    }

    //他collider接触時
    void OnTriggerEnter2D(Collider2D other)
    {
        //DataInfoのPlayerStatusManagerを取得
        obj = GameObject.Find("DataInfo");
        playerStatusManager = obj.GetComponent<PlayerStatusManager>();

        Debug.Log("OnTriggerEnter2D: " + other.gameObject.name);

        //剣との接触
        if (other.gameObject.tag == "Sword")
        {
            Debug.Log("剣のダメージ");

            //プレイヤーの近距離攻撃ダメージを調べる
            takeDamage = playerStatusManager.AttackDamageCalc();

            //HP計算
            currentHP = statusCalc.HPCalc(currentHP, takeDamage);
        }
        //手裏剣との接触
        if (other.gameObject.tag == "Syuriken")
        {
            Debug.Log("手裏剣のダメージ");

            //プレイヤーの遠距離攻撃ダメージを調べる
            takeDamage = playerStatusManager.AttackDamageCalc();

            //HP計算
            currentHP = statusCalc.HPCalc(currentHP, takeDamage);
        }
        //倒れるか調べる
        EnemyDead();
    }

    //倒れるか調べる関数
    void EnemyDead()
    {
        //敵HPが0以下なら、このオブジェクトを消す
        if (currentHP <= 0.0f)
        {
            //CreateMapスクリプトを探す
            CreateMap createMap;
            GameObject obj = GameObject.Find("Main Camera");
            createMap = obj.GetComponent<CreateMap>();

            //ボスが倒されたとき関数
            createMap.BossDead();

            //gameObject.SetActive(true);
            Destroy(gameObject);
            Debug.Log("ボスが倒れた");

                ////　ボスHPが０になると次のステージに行くためのマスが表示される
                //if (BossHP <= 0)
                //{
                //    Instantiate(NextStageTiles, new Vector2(5, 20), Quaternion.identity);
                //}
        }
    }
}
