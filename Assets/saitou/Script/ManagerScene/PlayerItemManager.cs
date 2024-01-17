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

    //アイテム効果用
    private float addMaxHP;         //最大体力＋

    private float increaseAttack;   //攻撃（割合）＊

    private float increaseBlock;    //防御（割合）＊
    private float addCriticalDamage;  //会心ダメージ
    private int addCriticalChance;    //会心率
    private float takeDamage;         //自傷ダメージ
    private float addMoney;           //獲得金額
    private float addSword;           //近距離攻撃ダメージ*
    private float addShot;            //遠距離攻撃ダメージ*
    private float plusShot;

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
        //変数のリセット
        addMaxHP = 1;
        increaseAttack = 1;
        increaseBlock = 1;
        addCriticalDamage = 1;
        addCriticalChance = 0;
        addMoney = 1;
        addShot = 1;
        addSword = 1;
        plusShot = 0;

        //所持アイテムの効果を計算
        foreach (KeyValuePair<string, int> haveitem in havingItem)
        {
            //アイテムの効果は今後ここに増やす それぞれ関数で分けてもいいかも
            switch (haveitem.Key)
            {
                case "Attack":
                    increaseAttack += 0.12f;
                    break;
                case "Revenge":
                    if(playerStatusManager.HPper() < 0.20f)
                    {
                        increaseAttack += 0.70f;
                    }
                    break;
                case "SelfHarm":
                    //持っている時に遠距離攻撃が自傷効果をもつ 捨てたときの動作未実装
                    playerStatusManager.onSelfHarm = true;
                    plusShot += playerStatusManager.MaxHP() * 0.1f;
                    break;
                case "Health":
                    //最大体力20%増加
                    addMaxHP += 0.20f;
                    break;
                case "HealthTreat":
                    //敵を倒すと最大体力の3%分回復
                    playerStatusManager.onHealthTreat = true;
                    break;
                case "ArmorPlate":
                    increaseBlock += -0.04f;
                    break;
                case "CRITRate":
                    addCriticalChance += 10;
                    break;
                case "CRITDmg":
                    addCriticalDamage += 0.30f;
                    break;
                case "Throwable2":
                    addSword += -0.10f;
                    addShot  += 0.20f;
                    break;
                case "Fencing2":
                    addShot  += -0.10f;
                    addSword += 0.20f;        
                    break;
                case "Fencing1":
                    addSword += 0.10f;
                    break;
                case "Throwable1":
                    addShot += 0.10f;
                    break;
                case "Collector":
                    increaseAttack += (havingItem.Count * 0.02f);
                    break;
                case "FirstAttack":
                    increaseAttack += 0.20f - (havingItem.Count * 0.01f);
                    break;
                case "MoneyTalent":
                    addMaxHP += -0.50f;
                    addMoney += 1.0f;
                    break;
                default:
                    Debug.Log("!アイテム効果が見つかりません");
                    break;
            }
            //プレイヤーステータスクラス内の計算クラスに代入
            
            playerStatusManager.statusCalc.IncreaseAttack = increaseAttack;
            playerStatusManager.statusCalc.IncreaseBlock = increaseBlock;
            playerStatusManager.statusCalc.AddCriticalChance = addCriticalChance;
            playerStatusManager.statusCalc.AddCriticalDamage = addCriticalDamage;
            playerStatusManager.addMaxHP = addMaxHP;
            playerStatusManager.addMoney = addMoney;
            playerStatusManager.addAttackDamage = addSword;
            playerStatusManager.addShotDamage = addShot;
            playerStatusManager.plusShotDamage = plusShot;

            //playerStatusManager.RoadHP();
        }
    }

    //アイテム取得関数 すでに所持していたらfalseを返す
    public bool AddItem(string id)
    {
        if (id != null)
        {
            if(havingItem.Count >= maxItem)//最大所持数以上なら
            {
                Debug.Log("アイテムが最大です");
                return false;
            }
            //IDを所持していたら
            if (havingItem.ContainsKey(id))
            {
                //すでに所持していたなら何もしない
                Debug.Log(id + "をすでに所持しています");
                //アップグレード関数に移動したい
                UpgradeItem(id);
                return false;

            }
            //未所持なら
            havingItem.Add(id, 0);//所持アイテムにidを追加
            Debug.Log(id + "を獲得しました");
            return true;
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
                havingItem[id]++;
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
                int price = itemManager.GetBuyingPrice(havingItem[id]);

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
        for (int i = 0; i < itemManager.GetCount(); i++)
        {
            itemId.Add(itemManager.GetID(i));
        }

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
                        return null;
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

    //所持アイテムデバッグ表示関数
    public void CheckHaveItem()
    {
        if (havingItem.Count == 0)
            Debug.Log("アイテムを所持していません");

        foreach (KeyValuePair<string, int> haveitem in havingItem){  
                Debug.Log("アイテムID : "+haveitem.Key+" グレード : "+haveitem.Value);
        }

    }
}
