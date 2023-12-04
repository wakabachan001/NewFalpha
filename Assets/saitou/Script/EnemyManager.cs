using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private float maxHP = 100.0f;       //最大体力
    [SerializeField] private int money = 100;            //落とすお金
    [SerializeField] private float attackDamage1 = 10.0f;//攻撃1のダメージ
    [SerializeField] private float attackDamage2 = 10.0f;//攻撃2のダメージ

    Color mainColor = new Color(1f, 1f, 1f, 1f);     //通常時
    Color damageColor = new Color(1f, 0.6f, 0.6f, 1f); //被ダメージ時

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
            //プレイヤーの近距離攻撃ダメージを調べる
            takeDamage = playerStatusManager.AttackDamageCalc();

            //HP計算
            status.CurrentHP = statusCalc.HPCalc(status.CurrentHP, takeDamage);

            StartCoroutine( DamageEfect());

            Debug.Log("剣のダメージ : " + takeDamage);
        }
        //手裏剣との接触
        if (other.gameObject.tag == "Syuriken")
        {
            //プレイヤーの遠距離攻撃ダメージを調べる
            takeDamage = playerStatusManager.AttackDamageCalc();

            //HP計算
            status.CurrentHP = statusCalc.HPCalc(status.CurrentHP, takeDamage);

            StartCoroutine(DamageEfect());

            Debug.Log("手裏剣のダメージ : " + takeDamage);
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

            if(playerStatusManager.onHealthTreat == true)
            {
                playerStatusManager.HT();
            }

            Destroy(gameObject);
            Debug.Log("敵が倒れた");
        }
    }

    //被ダメージエフェクト
    public IEnumerator DamageEfect()
    {
        //色変更
        gameObject.GetComponent<SpriteRenderer>().color = damageColor;

        //SE　被ダメージ

        yield return new WaitForSeconds(0.1f);

        //色を戻す
        gameObject.GetComponent<SpriteRenderer>().color = mainColor;
    }
}