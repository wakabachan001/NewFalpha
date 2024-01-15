using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アイテムデータクラス
//CSVファイルからの読み込み
public class ItemDataC
{
    private string id;          //アイテムID   
    private string itemName;    //名称
    private string description; //説明
    //private Sprite icon;        //アイテムのアイコン
    private int buyingPrice;    //買値
    private int sellingPrice;   //売値

    public string Id
    {
        get { return id; }
        set { id = value; }
    }
    public string ItemName
    {
        get { return itemName; }
        set { itemName = value; }
    }
    public string Description
    {
        get { return description; }
        set { description = value; }
    }
    //public Sprite Icon
    //{
    //    get { return icon; }
    //    set { icon = value; }
    //}
    public int BuyingPrice
    {
        get { return buyingPrice; }
        set { buyingPrice = value; }
    }
    public int SellingPrice
    {
        get { return sellingPrice; }
        set { sellingPrice = value; }
    }
}


public struct ItemData
{
    private string id;          //アイテムID   
    private string itemName;    //名称
    private string description; //説明
    private int grade;          //グレード

    //追加するステータス
    private float maxHp;        //最大体力
    private float attack;       //攻撃力
    private float swordAttack;  //近距離攻撃力
    private float shotAttack;   //遠距離攻撃力
    private float block;        //防御力
    private float criChance;    //クリティカル率
    private float criDamage;    //クリティカルダメージ

    public string Id
    {
        get { return id; }
        set { id = value; }
    }
    public string ItemName
    {
        get { return itemName; }
        set { itemName = value; }
    }
    public string Description
    {
        get { return description; }
        set { description = value; }
    }
    public int Grade
    {
        get { return grade; }
        set { grade = value; }
    }


    public float MaxHp
    {
        get { return maxHp; }
        set { maxHp = value; }
    }
    public float Attack
    {
        get { return attack; }
        set { attack = value; }
    }
    public float SwordAttack
    {
        get { return swordAttack; }
        set { swordAttack = value; }
    }
    public float ShotAttack
    {
        get { return shotAttack; }
        set { shotAttack = value; }
    }
    public float Block
    {
        get { return block; }
        set { block = value; }
    }
    public float CriChance
    {
        get { return criChance; }
        set { criChance = value; }
    }
    public float CriDamage
    {
        get { return criDamage; }
        set { criDamage = value; }
    }
}