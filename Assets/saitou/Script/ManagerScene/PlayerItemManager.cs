using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//所持アイテム管理クラス
public class PlayerItemManager : MonoBehaviour
{
    //プレイヤー所持アイテムリスト(ID, グレード)
    //public List<string> havingItem = new List<string>();
    public Dictionary<string, int> havingItem = new Dictionary<string, int>();

    public int maxItem = 8;//アイテムの最大所持数

    ItemDataS plusEffect;   //足し合わせる効果データ
    ItemDataS resetEffect; //上の効果データの初期化用
     
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

        //リセット用の数値を初期化（出来れば宣言時にしたかった）
        {
            resetEffect.Id          = null;
            resetEffect.ItemName    = null;
            resetEffect.Description = null;
            resetEffect.Grade       = 0;

            resetEffect.MaxHp       = 1;
            resetEffect.Attack      = 1;
            resetEffect.SwordAttack = 1;
            resetEffect.ShotAttack  = 1;
            resetEffect.Block       = 1;
            resetEffect.CriChance   = 0;
            resetEffect.CriDamage   = 1;
            resetEffect.AddMoney    = 1;
        }
    }

    //必要時だけの呼び出しでいいのでは？
    public void GetItemEffect(ref ItemDataS refdata)
    {
        //変数のリセット
        plusEffect = resetEffect;



        //グレードによって効果を変更したい
        //所持アイテムの効果を計算
        foreach (KeyValuePair<string, int> haveitem in havingItem)
        {
            //特殊な条件などがある効果の場合の処理　その他はdefaultで
            switch (haveitem.Key)
            {
                case "Revenge":
                    switch(haveitem.Value){
                        case 0:
                            if (playerStatusManager.HPper() < 0.20f)
                            {
                                plusEffect.Attack += 0.40f;
                            }
                            break;
                        case 1:
                            if (playerStatusManager.HPper() < 0.20f)
                            {
                                plusEffect.Attack += 0.50f;
                            }
                            break;
                        case 2:
                            if (playerStatusManager.HPper() < 0.20f)
                            {
                                plusEffect.Attack += 0.70f;
                            }
                            break;
                    }
                    break;

                case "SelfHarm":          
                    playerStatusManager.onSelfHarm = true;

                    switch (haveitem.Value){
                        case 0:
                            plusEffect.ShotAttack += 1;
                            break;
                        case 1:
                            plusEffect.ShotAttack += 1.2f; 
                            break;
                        case 2:
                            plusEffect.ShotAttack += 1.4f;
                            break;
                    }
                    break;
               
                case "Collector":
                    switch (haveitem.Value){
                        case 0:
                            plusEffect.Attack += (havingItem.Count * 0.02f);
                            break;
                        case 1:
                            plusEffect.Attack += (havingItem.Count * 0.03f);
                            break;
                        case 2:
                            plusEffect.Attack += (havingItem.Count * 0.04f);
                            break;
                    }
                    break;

                case "FirstAttack":
                    switch (haveitem.Value){
                        case 0:
                            plusEffect.Attack += 0.16f - (havingItem.Count * 0.02f); 
                            break;
                        case 1:
                            plusEffect.Attack += 0.40f - (havingItem.Count * 0.04f);
                            break;
                        case 2:
                            plusEffect.Attack += 0.64f - (havingItem.Count * 0.06f);
                            break;
                    }
                    
                    break;
                default:
                    Debug.Log("!アイテム効果が見つかりません");

                    //特殊効果等が無い場合、効果を足し合わせる
                    itemManager.PlusEffect(ref plusEffect, haveitem.Key, haveitem.Value);

                    break;
            }
            //playerStatusManager.plusShotDamage = plusShot;

            //playerStatusManager.RoadHP();
        }
        //取得した効果を引数に代入
        refdata = plusEffect;
    }

    //アイテム取得関数 すでに所持していたらfalseを返す
    public bool AddItem(string id)
    {
        if (id != null)
        {          
            //IDを所持していたら
            if (havingItem.ContainsKey(id))
            {
                //アップグレード上限なら
                if(havingItem[id] > 2)
                {
                    //すでに所持していたなら何もしない
                    Debug.Log(id + "をすでに所持しています");

                    return false;
                }  
                //アップグレード関数に移動したい
                UpgradeItem(id);
                return true;
            }
            //未所持なら
            if (havingItem.Count >= maxItem)//最大所持数以上なら
            {
                Debug.Log("アイテムが最大です");
                return false;
            }
            else
            {
                havingItem.Add(id, 0);//所持アイテムにidを追加
                Debug.Log(id + "を獲得しました");
                return true;
            }
        }
        return false;
    }

    //アイテムアップグレード関数
    public void UpgradeItem(string id)
    {
        //IDを所持していたら
        if (havingItem.ContainsKey(id))
        {
            if (havingItem[id] < 3)
            {
                havingItem[id]++;
                Debug.Log(id + " : をアップデートしました");
            }
            else
                Debug.Log(id + " : はアップデート出来ません");
        }
        else
        {
            Debug.Log("!所持していません");
        }
    }

    //アイテム廃棄関数
    public void RemoveItem(string id)
    {
        havingItem.Remove(id);//所持アイテムから指定のidを削除
    }

    //アイテム購入関数
    public bool BuyingItem(string id)
    {
        if (id != null)
        {
            //そのアイテムを所持しているか
            if (!havingItem.ContainsKey(id))//未所持の場合
            {
                //購入価格を取得
                int price = itemManager.GetBuyingPrice(0);

                //所持金が足りているなら                                                       
                if (playerStatusManager.status.Money >= price)
                {
                    if (havingItem.Count >= maxItem)//最大所持数以上なら
                    {
                        Debug.Log("アイテムが最大です");
                        return false;
                    }
                    //アイテムを獲得 
                    AddItem(id);

                    //価格分、所持金を減らす
                    playerStatusManager.UseMoney(price);

                    Debug.Log(id + "を購入 : " + price);
                    return true;

                }
                else
                {
                    Debug.Log("お金が足りません");
                    return false;
                }
            }
            else//所持済の場合
            {
                //購入価格を取得
                int price = itemManager.GetBuyingPrice(havingItem[id] + 1);

                //所持金が足りているなら                                                       
                if (playerStatusManager.status.Money >= price)
                {
                    //アイテムをアップグレード
                    UpgradeItem(id);

                    //価格分、所持金を減らす
                    playerStatusManager.UseMoney(price);
                    return true;
                }
                else
                {
                    Debug.Log("お金が足りません");
                    return false;
                }
            }
        }
        return false;
    }


    //ランダムアイテム指定関数(取得する数(返り値の要素数), グレード1,2も出現するか)
    public string[] GetRandomItem(int num = 1, bool ongrade = true)
    {
        string[] ans = new string[num];//返り値用配列

        Debug.Log("IDは" + itemManager.GetID(1));
        //アイテムID一覧の生成
        List<string> itemId = new List<string>();

        if (havingItem.Count < maxItem)//最大所持数未満なら
        {
            //全アイテムIDをitemIdに入れる
            for (int i = 0; i < itemManager.GetCount(); i++)
            {
                itemId.Add(itemManager.GetID(i));
            }
        }
        else//最大まで所持しているなら
        {
            //所持アイテム全てをitemIdに入れる
            foreach (KeyValuePair<string, int> haveitem in havingItem)
            {
                itemId.Add(haveitem.Key);
            }
        }

        //デバッグ用
        for(int i = 0;i<itemId.Count;i++)
        {
            Debug.Log("ID : "+itemId[i]);
        }

        //所持アイテムをすべて探す
        foreach (KeyValuePair<string, int> haveitem in havingItem)
        {
            switch(ongrade)
            {
                case true:
                    //グレードが2(これ以上アップグレードできない)なら候補から外す
                    if (haveitem.Value == 2)
                        itemId.Remove(haveitem.Key);
                    break;

                case false:
                    if (havingItem.Count >= maxItem)//最大所持数以上なら
                    {
                        Debug.Log("アイテムが最大です");
                        return null;//nullで返す
                    }
                    //所持アイテムを候補から外す
                    itemId.Remove(haveitem.Key);

                    break;
            }
        }

        //返り値を決める
        for (int i = 0; i < num; i++)
        {
            //itemIdの中身があるかチェック
            if (itemId.Count != 0)
            {
                //0～全アイテムの種類のランダムな数値を取得
                int r = Random.RandomRange(0, itemId.Count);

                ans[i] = itemId[r];//返り値用配列にランダムなIDを代入

                itemId.RemoveAt(r);//代入したIDを削除
            }
            else
            {
                //エラー
                Debug.Log("!未所持のアイテムが見つかりません");
                ans[i] = null;
            }
        }
        return ans;
    }

    //所持アイテムのグレードを取得する関数
    public int GetHaveGrade(string id)
    {
        //引数idを所持しているか
        if (havingItem.ContainsKey(id))
        {
            return havingItem[id];//所持しているidのグレードを返す
        }
        else
        {
            Debug.Log("アイテムを所持していません");
            return -1;
        }
    }

    //所持アイテムデバッグ表示関数
    public string[] GetHaveItem( )
    {
        string[] id = new string[8];
        int i = 0;

        foreach (KeyValuePair<string, int> haveitem in havingItem){
            id[i] = haveitem.Key;
            i++;
        }
        return id;
    }
}
