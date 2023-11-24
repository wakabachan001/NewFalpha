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
    private float addAttack;      //追加ダメージ(+)
    private float increaseAttack; //増加ダメージ(*)
    private float addBlock;         //防御+
    private float increaseBlock;    //防御（割合）＊

    private float addCriticalDamage;    //会心ダメージ
    private int addCriticalChance;    //会心率
    private int addMoney;               //所持金追加

    //コンストラクタ
    public StatusCalc()
    {
        //初期化
        addAttack = 0f;//要らないかも
        increaseAttack = 1f;
        addBlock = 0f;//要らないかも
        increaseBlock = 1f;

        addCriticalDamage = 0f;
        addCriticalChance = 0;
        addMoney = 0;
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
            return (damage + addAttack) * increaseAttack * (criD + addCriticalDamage);
        }
        else
        {
            //通常のダメージ
            return (damage + addAttack) * increaseAttack;
        }     
    }
    //体力計算関数
    public float HPCalc(float hp, float damage, float barrier = 1.0f)
    {
        return hp - ((damage - addBlock)* increaseBlock * barrier);
    }
    //体力回復関数
    public float HealHP(float maxhp, float hp, float heal)
    {
        //回復して最大体力を超えるなら
        if (hp + heal >= maxhp)
        {
            //現在の体力を最大体力と同じにする
            return maxhp;
        }
        else
        {
            //現在の体力を回復する
            return hp + heal;
        }
    }
    //取得金額計算関数
    public int MoneyCalc(int money)
    {
        return money + addMoney;
    }

    //プロパティ
    public float AddAttack
    {
        get { return addAttack; }
        set { addAttack = value; }
    }
    public float IncreaseAttack
    {
        get { return increaseAttack; }
        set { increaseAttack = value; }
    }
    public float AddBlock
    {
        get { return addBlock; }
        set { addBlock = value; }
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
    public int AddMoney
    {
        get { return addMoney; }
        set { addMoney = value; }
    }
}