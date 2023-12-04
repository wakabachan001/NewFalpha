using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーのステータス管理クラス
public class PlayerStatusManager : MonoBehaviour
{
    //ステータスの初期化用
    [SerializeField] private float maxHP;       //最大体力      
    [SerializeField] private float attackDamage;//近距離攻撃ダメージ
    [SerializeField] private float shotDamage;  //遠距離攻撃ダメージ
    [SerializeField] private int criChance;     //クリティカル率
    [SerializeField] private float criDamage;   //クリティカルダメージ
    [SerializeField] private int initialMoney;  //初期所持金
    [SerializeField] private float[] barrier = new float[5];  //バリアの倍率 最初は要素0番
    public float addAttackDamage;   //追加近距離攻撃ダメージ
    public float addShotDamage;     //追加遠距離攻撃ダメージ
    public float plusShotDamage;
    public float addMoney;         //所持金追加
    public bool onSelfHarm = false;   //アイテム用フラグ
    public bool onHealthTreat = false;

    public bool onResetHpBr = false;    //体力、バリアリセットしたか
    public PlayerStatusData status;     //プレイヤーのステータス格納用
    public StatusCalc statusCalc = new StatusCalc();  //ステータス計算用

    // Start is called before the first frame update
    void Start()
    {
        //ステータスの初期化
        status = new PlayerStatusData(maxHP, initialMoney, attackDamage, shotDamage, barrier[0], criChance, criDamage);

        addAttackDamage = 1;
        addShotDamage = 1;
        addMoney = 1;

        Debug.Log("maxHP" + status.MaxHP);
    }

    //お金獲得関数
    public void GettingMoney(int money)
    {
        Debug.Log("お金取得");
        status.Money += (int)(money * addMoney);
    }
    //お金を使ったとき関数
    public void UseMoney(int money)
    {
        status.Money -= money;
    }

    //ダメージを受ける関数
    public bool TakeDamage(float damage)
    {
        Debug.Log("ダメージを食らった");

        //HP計算関数を呼んで、現在体力を更新
        status.CurrentHP = statusCalc.HPCalc(status.CurrentHP, damage, status.Barrier);

        //HPが０以下だったらfalseを返す
        //直接シーンを変更してもいい
        if (status.CurrentHP <= 0)
            return false;
        else
            return true;
    }

    //近距離攻撃のダメージ計算関数
    public float AttackDamageCalc()
    {
        //AttackDamage[0]を引数として、ダメージ計算関数を呼ぶ
        return statusCalc.DamageCalc(status.GetAttackDamage(0), status.CriChance, status.CriDamage)
                * addAttackDamage;
    }

    //遠距離攻撃のダメージ計算関数
    public float ShotDamageCalc()
    {
        //SelfHarmを持っているかつ体力が１より多いなら
        if(onSelfHarm == true && status.CurrentHP >1f)
        {
            //最大体力の5%を受ける
            TakeDamage(MaxHP() * 0.05f);
        }

        //AttackDamage[1]を引数として、ダメージ計算関数を呼ぶ
        return (statusCalc.DamageCalc(status.GetAttackDamage(1), status.CriChance, status.CriDamage)
                + plusShotDamage )
                * addShotDamage;
    }

    //バリアの倍率更新関数
    public void ChangeBarrier(int n)
    {
        //受け取った引数番目の要素に更新
        status.Barrier = barrier[n];
    }
    //体力、バリアリセット関数
    public void ResetHpBr()
    {
        status.CurrentHP = statusCalc.MaxHPCalc(status.MaxHP);
        status.Barrier = barrier[0];

        onResetHpBr = true;//バリアゲージの初期化用
    }
    //現在の体力の割合を求める関数
    public float HPper()
    {
        return status.CurrentHP / statusCalc.MaxHPCalc(status.MaxHP);
    }
    //HealthTreat用関数
    public void HT()
    {
        status.CurrentHP = statusCalc.HealHPper(status.MaxHP, status.CurrentHP, 0.03f);
    }
    //最大体力関数
    public float MaxHP()
    {
        return statusCalc.MaxHPCalc(status.MaxHP);
    }
    //現在の体力を調整する関数
    public void RoadHP()
    {
        //現在の体力が最大体力を超えていたら
        if(status.CurrentHP > statusCalc.MaxHPCalc(status.MaxHP))
        {
            //現在の体力を最大体力に更新
            status.CurrentHP = statusCalc.MaxHPCalc(status.MaxHP);
        }
    }
}

