using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アイコン画像を管理し、探すクラス
public class ItemIcon : MonoBehaviour
{
    //アイテムアイコン画像を入れる配列
    [SerializeField] private Sprite[] iconImage = new Sprite[20];
    [SerializeField] private Sprite[] frameImage = new Sprite[3];

    [SerializeField] private Sprite emptyIconImage;
    
    //アイコン探索関数
    public Sprite SearchImage(string id)
    {
        switch(id)
        {
            //IDにアイテムのIDを書いていく
            case "Attack":
                return iconImage[0];
            case "ArmorPlate":
                return iconImage[1];
            case "Health":
                return iconImage[2];
            case "CRITRate":
                return iconImage[3];
            case "CRITDmg":
                return iconImage[4];
            case "Fencing1":
                return iconImage[5];
            case "Fencing2":
                return iconImage[6];
            case "Throwable1":
                return iconImage[7];
            case "Throwable2":
                return iconImage[8];
            case "Revenge":
                return iconImage[9];
            case "Collector":
                return iconImage[10];
            case "FirstAttack":
                return iconImage[11];
            case "SelfHarm":
                return iconImage[12];
            case "MoneyTalent":
                return iconImage[13];
            default:
                Debug.Log("!アイテム画像が見つかりません");
                return emptyIconImage;    
        }
    }
    //対応する枠を返す関数
    public Sprite SearchFrame(int grade)
    {
        return frameImage[grade];
    }
    //空のアイコン取得関数
    public Sprite Empty()
    {
        return emptyIconImage;//空アイコンを返す
    }
}
