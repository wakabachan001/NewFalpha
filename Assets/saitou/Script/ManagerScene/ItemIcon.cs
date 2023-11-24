using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アイコン画像を管理し、探すクラス
public class ItemIcon : MonoBehaviour
{
    //アイテムアイコン画像を入れる配列
    [SerializeField] private Sprite[] iconImage = new Sprite[20];
    
    //アイコン探索関数
    public Sprite SearchImage(string id)
    {
        switch(id)
        {
            //IDにアイテムのIDを書いていく
            case "ID":
                return iconImage[0];
            
            default:
                Debug.Log("!アイテム画像が見つかりません");
                return null;    
        }
    }
}
