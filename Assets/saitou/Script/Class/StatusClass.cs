using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ステータス管理クラス
public class StatusData
{
    protected float maxHP;      //最大HP   
    protected float currentHP;  //現在のHP
    protected int money;        //所持金
    protected List<float> attackDamage = new List<float>();//攻撃のダメージ

    //コンストラクタ
    public StatusData(float hp, int mon, float attack1, float attack2 = 10)
    {
        //引数からステータスを初期化
        maxHP = hp;
        currentHP = maxHP;
        money = mon;
        attackDamage.Insert(0, attack1);
        attackDamage.Insert(1, attack2);
    }

    public float MaxHP
    {
        get { return maxHP; }
        set { maxHP = value; }
    }
    public float CurrentHP
    {
        get { return currentHP; }
        set { currentHP = value; }
    }
    public int Money
    {
        get { return money; }
        set { money = value; }
    }
    public void SetAttackDamage(int n, float damage)
    {
        attackDamage.Insert(n, damage);
    }
    public float GetAttackDamage(int n)
    {
        return attackDamage[n];
    }

    //会心ダメージ　会心率　を追加予定
}

//プレイヤーステータス管理クラス
public class PlayerStatusData : StatusData
{
    float barrier;          //バリア値(被ダメージを減少する割合)
    int criticalChance;   //クリティカル率
    float criticalDamage;   //クリティカルダメージ
    
    //コンストラクタ
    public PlayerStatusData(float hp,  int mon, float attack, float shot, float barri, int criC, float criD) : base(hp, mon, attack, shot)
    {
        //引数からステータスを初期化
        barrier = barri;
        criticalChance = criC;
        criticalDamage = criD;
    }

    //プロパティ
    public float Barrier
    {
        get { return barrier; }
        set { barrier = value; }
    }
    public int CriChance
    {
        get { return criticalChance; }
        set { criticalChance = value; }
    }
    public float CriDamage
    {
        get { return criticalDamage; }
        set { criticalDamage = value; }
    }
}
