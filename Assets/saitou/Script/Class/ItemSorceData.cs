using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSorceData : MonoBehaviour
{
}

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