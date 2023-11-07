using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//アイテムのソースデータ
[CreateAssetMenu(menuName = "SaitoAssets/ItemSourceData")]
public class ItemSourceData : ScriptableObject
{
    //アイテム識別用id
    [SerializeField] private string _id;
    //idを取得
    public string id
    {
        get { return _id; }
    }

    //アイテムの名前
    [SerializeField] private string _itemName;
    //アイテム名を取得
    public string itemName
    {
        get { return _itemName; }
    }

    //アイテムの見た目
    [SerializeField] private Sprite _sprite;
    //アイテムの見た目を取得
    public Sprite sprite
    {
        get { return _sprite; }
    }

    //アイテムの説明
    [SerializeField] private string _itemDescription;
    //アイテムの説明を取得
    public string itemDescription
    {
        get { return _itemDescription; }
    }

    //買値
    [SerializeField] private int _buyingPrice;
    //買値を取得
    public int buyingPrice
    {
        get { return _buyingPrice; }
    }

}

