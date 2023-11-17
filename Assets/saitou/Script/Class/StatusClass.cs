using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusClass : MonoBehaviour
{
}

//ステータス管理クラス
public class StatusData
{
    //コンストラクタ
    public StatusData()
    {

    }
    protected float maxHP;  //最大HP   
    protected int money;    //所持金
    protected List<float> attackDamage = new List<float>();//攻撃のダメージ


    public float MaxHP
    {
        get { return maxHP; }
        set { maxHP = value; }
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
    //コンストラクタ
    public PlayerStatusData()
    {

    }
    private float maxBarrier;//最大バリア値

    public float MaxBarrier
    {
        get { return maxBarrier; }
        set { maxBarrier = value; }
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
    private float addCriticalChance;    //会心率
    private int addMoney;               //所持金追加

    //コンストラクタ
    public StatusCalc()
    {
        //初期化
        addAttack = 0f;
        increaseAttack = 1f;
        addBlock = 0f;
        increaseBlock = 1f;

        addCriticalDamage = 0f;
        addCriticalChance = 0f;
        addMoney = 0;
    }
    //ダメージ計算関数
    public float DamageCalc(float damage)
    {
        //1~100のランダム
        int dice = Random.RandomRange(1, 101);

        //ランダムの値が、クリティカル率以下なら
        if( dice <= 2 + addCriticalChance)
        {
            //クリティカルダメージを加える
            return (damage + addAttack) * increaseAttack * addCriticalDamage;
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
    public float AddCriticalChance
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