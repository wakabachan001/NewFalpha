using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//アイコン画像を管理し、探すクラス
public class ItemIcon : MonoBehaviour
{
    //アイテムアイコン画像を入れる配列
    [SerializeField] private Sprite[] iconImage = new Sprite[20];
    [SerializeField] private Sprite emptyIconImage;
    
    //アイコン探索関数
    public Sprite SearchImage(string id)
    {
        switch(id)
        {
            //IDにアイテムのIDを書いていく
            case "Attack":
                return iconImage[0];
                break;
            case "Revenge":
                return iconImage[1];
                break;
            case "SelfHarm":
                return iconImage[2];
                break;
            case "Health":
                return iconImage[3];
                break;
            case "HealthTreat":
                return iconImage[4];
                break;
            case "ArmorPlate":
                return iconImage[5];
                break;
            case "CRITRate":
                return iconImage[6];
                break;
            case "CRITDmg":
                return iconImage[7];
                break;
            case "Throwable2":
                return iconImage[8];
                break;
            case "Fencing2":
                return iconImage[9];
                break;
            case "Fencing1":
                return iconImage[10];
                break;
            case "Throwable1":
                return iconImage[11];
                break;
            case "Collector":
                return iconImage[12];
                break;
            case "FirstAttack":
                return iconImage[13];
                break;
            case "MoneyTalent":
                return iconImage[14];
                break;
            default:
                Debug.Log("!アイテム画像が見つかりません");
                return emptyIconImage;    
        }
    }
    //空のアイコン取得関数
    public Sprite Empty()
    {
        return emptyIconImage;//空アイコンを返す
    }
}
