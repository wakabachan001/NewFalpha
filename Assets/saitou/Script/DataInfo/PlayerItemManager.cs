using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//所持アイテム管理クラス
public class PlayerItemManager : MonoBehaviour
{
    //プレイヤー所持アイテムリスト
    private List<string> havingItem = new List<string>();
    private float addDamage;
    private float increaseDamage;
    private float addMaxHP;

    [SerializeField] private float iconPosX;//アイコンの初期位置X（左上）
    [SerializeField] private float iconPosY;//アイコンの初期位置Y（左上）


    //他スクリプトのインスタンス
    PlayerStatusManager playerStatusManager;
    ItemManager itemManager;

    //アイテムアイコン用オブジェクト
    GameObject itemIcon;

    // Start is called before the first frame update
    void Start()
    {
        //同オブジェクトの別スクリプトを取得
        playerStatusManager = GetComponent<PlayerStatusManager>();
        itemManager = GetComponent<ItemManager>();
    }

    private void FixedUpdate()
    {
        //変数のリセット　後で初期値を変数化
        addDamage = 0;
        addMaxHP = 0;
        increaseDamage = 1;

        //所持アイテムの効果を計算
        for (int i = 0; i < havingItem.Count; i++)
        {
            //アイテムの効果は今後ここに増やす それぞれ関数で分けてもいいかも
            switch (havingItem[i])
            {
                case "str_up":
                    addDamage += 2.0f;
                    break;
                case "maxhp_up":
                    addMaxHP += 100.0f;
                    break;
                default:
                    Debug.Log("!所持アイテムが見つかりません");
                    break;
            }
            //プレイヤーステータスクラス内の計算クラスに代入
            playerStatusManager.statusCalc.AddDamage = addDamage;
            playerStatusManager.statusCalc.IncreaseDamage = increaseDamage;
        }
    }

    //アイテム取得関数
    public void AddItem(string id)
    {
        //取得アイテムが所持アイテム中にあるか調べる
        for (int i = 0; i < havingItem.Count; i++)
            if (havingItem[i] == id)
            {
                //すでに所持していたなら何もしない
                Debug.Log(id + "をすでに所持しています");
                break;
            }
            else
            {
                //未所持なら
                havingItem.Add(id);//所持アイテムにidを追加
            }
    }
    //アイテム廃棄関数
    public void RemoveItem(string id)
    {
        havingItem.Remove(id);//所持アイテムから指定のidを削除
    }

    //アイテム購入関数
    public void BuyingItem(string id)
    {
        //購入価格を取得
        int price = itemManager.GetSellingPrice(id);

        //所持金が足りているなら
        if (playerStatusManager.playerStatus.Money >= price)
        {
            //価格分、所持金を減らす
            playerStatusManager.GettingMoney(price * -1);
            AddItem(id);    //アイテムを獲得
        }
    }
    //アイテム売却関数
    public void SellingItem(string id)
    {
        //購入価格を取得
        int price = itemManager.GetSellingPrice(id);

        //価格分、所持金を増やす
        playerStatusManager.GettingMoney(price);
        RemoveItem(id);     //アイテムを獲得
    }

    //所持アイテムのアイコンを表示する関数
    private void MakeIcon()
    {
        for (int i = 0; i < havingItem.Count; i++)
        {
            //子としてImageを持つPrefabをクローン
            //クローンしたオブジェクト
        }
    }
}
