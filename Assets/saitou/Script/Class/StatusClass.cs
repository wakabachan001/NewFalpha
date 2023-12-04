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


//ステータス計算クラス
public class StatusCalc
{
    private float addMaxHP;       //追加最大体力
    private float increaseAttack; //増加ダメージ(*)
    private float increaseBlock;  //防御（割合）＊

    private float addCriticalDamage;    //会心ダメージ
    private int addCriticalChance;    //会心率
    

    //コンストラクタ
    public StatusCalc()
    {
        //初期化
        addMaxHP = 1f;
        increaseAttack = 1f;
        increaseBlock = 1f;

        addCriticalDamage = 0f;
        addCriticalChance = 0;
    }
    //ダメージ計算関数 引数が１つならクリティカルは起きない
    public float DamageCalc(float damage, int criC = -100, float criD = 1f)
    {
        //1~100のランダム
        int dice = Random.RandomRange(1, 101);

        //ランダムの値が、クリティカル率以下なら
        if( dice <= criC + addCriticalChance)
        {
            //クリティカルダメージを加える
            return damage * increaseAttack * (criD + addCriticalDamage);
        }
        else
        {
            //通常のダメージ
            return damage * increaseAttack;
        }     
    }
    //体力計算関数
    public float HPCalc(float hp, float damage, float barrier = 1.0f)
    {
        return hp - (damage* increaseBlock * barrier);
    }
    //体力数値回復関数
    public float HealHP(float maxhp, float hp, float heal)
    {
        //回復して最大体力を超えるなら
        if (hp + heal >= MaxHPCalc(maxhp))
        {
            //現在の体力を最大体力と同じにする
            return MaxHPCalc(maxhp);
        }
        else
        {
            //現在の体力を回復する
            return hp + heal;
        }
    }
    //体力割合回復関数
    public float HealHPper(float maxhp, float hp, float per)
    {
        float maxHP = MaxHPCalc(maxhp);

        float heal = maxHP * per;

        //回復して最大体力を超えるなら
        if (hp + heal >= maxHP)
        {
            //現在の体力を最大体力と同じにする
            return maxHP;
        }
        else
        {
            //現在の体力を回復する
            return hp + heal;
        }
    }
    //最大体力計算
    public float MaxHPCalc(float maxhp)
    {
        return maxhp *= addMaxHP;
    }

    //プロパティ
    public float AddMaxHP
    {
        get { return addMaxHP; }
        set { addMaxHP = value; }
    }
    public float IncreaseAttack
    {
        get { return increaseAttack; }
        set { increaseAttack = value; }
    }
    public float IncreaseBlock
    {
        get { return increaseBlock; }
        set { increaseBlock = value; }
    }
    public float AddCriticalDamage
    {
        get { return addCriticalDamage; }
        set { addCriticalDamage = value; }
    }
    public int AddCriticalChance
    {
        get { return addCriticalChance; }
        set { addCriticalChance = value; }
    }
}