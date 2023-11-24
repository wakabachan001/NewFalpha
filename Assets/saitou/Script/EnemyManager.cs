using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private float maxHP = 100.0f;       //最大体力
    [SerializeField] private int money = 100;            //落とすお金
    [SerializeField] private float attackDamage1 = 10.0f;//攻撃1のダメージ
    [SerializeField] private float attackDamage2 = 10.0f;//攻撃2のダメージ

    private float takeDamage;   //被ダメージ

    private StatusData status;    //敵ステータスクラス
    private StatusCalc statusCalc = new StatusCalc();     //ダメージ計算クラス

    PlayerStatusManager playerStatusManager;//PlayerStatusManagerスクリプト

    void Start()
    {
        Debug.Log("敵初期化");

        //DataInfoのPlayerStatusManagerを取得
        playerStatusManager = LoadManagerScene.GetPlayerStatusManager();

        //ステータス初期化
        status = new StatusData(maxHP, money, attackDamage1, attackDamage2);

        Debug.Log("敵初期化完了");
    }

    //他collider接触時
    void OnTriggerEnter2D(Collider2D other)
    {
        //剣との接触
        if(other.gameObject.tag == "Sword")
        {
            Debug.Log("剣のダメージ");

            //プレイヤーの近距離攻撃ダメージを調べる
            takeDamage = playerStatusManager.AttackDamageCalc();

            //HP計算
            status.CurrentHP = statusCalc.HPCalc(status.CurrentHP, takeDamage);
        }
        //手裏剣との接触
        if (other.gameObject.tag == "Syuriken")
        {
            Debug.Log("手裏剣のダメージ");

            //プレイヤーの遠距離攻撃ダメージを調べる
            takeDamage = playerStatusManager.AttackDamageCalc();

            //HP計算
            status.CurrentHP = statusCalc.HPCalc(status.CurrentHP, takeDamage);
        }
        if (other.gameObject.tag == "DeleteArea")
        {
            Destroy(gameObject);
        }
        //倒れるか調べる
        EnemyDead();
    }

    //倒れるか調べる関数
    void EnemyDead()
    {
        //敵HPが0以下なら、このオブジェクトを消す
        if (status.CurrentHP <= 0.0f)
        {
            //プレイヤーの所持金を増やす
            playerStatusManager.GettingMoney(status.Money);

            Destroy(gameObject);
            Debug.Log("敵が倒れた");
        }
    }
}