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
    private float addDamage;      //追加ダメージ(+)
    private float increaseDamage; //増加ダメージ(*)

    //コンストラクタ
    public StatusCalc()
    {
        //初期化
        addDamage = 0.0f;
        increaseDamage = 1.0f;
    }
    //ダメージ計算関数
    public float DamageCalc(float damage)
    {
        return (damage + addDamage) * increaseDamage;
    }
    //体力計算関数
    public float HPCalc(float HP, float damage, float barrier = 1.0f)
    {
        return HP - (damage * barrier);
    }
    //AddDamageプロパティ
    public float AddDamage
    {
        get { return addDamage; }
        set { addDamage = value; }
    }
    //IncreaseDamageプロパティ
    public float IncreaseDamage
    {
        get { return increaseDamage; }
        set { increaseDamage = value; }
    }
}