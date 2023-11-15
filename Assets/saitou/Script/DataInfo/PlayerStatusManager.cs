using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤーのステータス管理クラス
public class PlayerStatusManager : MonoBehaviour
{
    [SerializeField] private float maxHP;       //最大体力
    private float currentHP;                //現在の体力
    [SerializeField] private float maxBarrier;  //最大バリア
    private float currentBarrier;           //現在のバリア
    [SerializeField] private float attckDamage; //近距離攻撃ダメージ
    [SerializeField] private float shotDamage;  //遠距離攻撃ダメージ
    [SerializeField] private int initialMoney;  //初期所持金

    public PlayerStatusData playerStatus = new PlayerStatusData(); //プレイヤーのステータス格納用
    public StatusCalc statusCalc = new StatusCalc();               //ステータス計算用

    public HPBar hpbar;              //HPBarスクリプト

    public float MaxHP
    {
        get { return maxHP; }
    }
    public float MaxBarrier
    {
        get { return maxBarrier; }
    }
    public float CurrentBarrier
    {
        get { return currentBarrier; }
        set { currentBarrier = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //ステータスの初期化
        playerStatus.MaxHP = maxHP;
        playerStatus.MaxBarrier = maxBarrier;
        playerStatus.SetAttackDamage(0, attckDamage);//attackDamage[0]初期化
        playerStatus.SetAttackDamage(1, shotDamage); //attackDamage[1]初期化
        playerStatus.Money = initialMoney;

        currentHP = maxHP;
        currentBarrier = maxBarrier;

        Debug.Log("maxHP" + playerStatus.MaxHP);
    }

    //お金獲得関数
    public void GettingMoney(int money)
    {
        playerStatus.Money += money;
    }

    //ダメージを受ける関数
    public bool TakeDamage(float damage)
    {
        Debug.Log("ダメージを食らった");

        //HP計算関数を呼んで、現在体力を更新
        currentHP = statusCalc.HPCalc(currentHP, damage, currentBarrier);

        //HPBarの呼び出し
        hpbar.UpdateHP(currentHP);

        //HPが０以下だったらfalseを返す
        if (currentHP <= 0)
            return false;
        else
            return true;
    }

    //近距離攻撃のダメージ計算関数
    public float AttackDamageCalc()
    {
        //AttackDamage[0]を引数として、ダメージ計算関数を呼ぶ
        return statusCalc.DamageCalc(playerStatus.GetAttackDamage(0));
    }

    //遠距離攻撃のダメージ計算関数
    public float ShotDamageCalc()
    {
        //AttackDamage[1]を引数として、ダメージ計算関数を呼ぶ
        return statusCalc.DamageCalc(playerStatus.GetAttackDamage(1));
    }
}

