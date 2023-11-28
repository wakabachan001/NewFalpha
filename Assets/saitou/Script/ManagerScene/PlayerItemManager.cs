using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//所持アイテム管理クラス
public class PlayerItemManager : MonoBehaviour
{
    //プレイヤー所持アイテムリスト
    public List<string> havingItem = new List<string>();

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
        for (int i = 0; i < havingItem.Count; i++)
        {
            //アイテムの効果は今後ここに増やす それぞれ関数で分けてもいいかも
            switch (havingItem[i])
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
                    plusShot += playerStatusManager.statusCalc.MaxHPCalc(playerStatusManager.status.MaxHP) 
                                 * 0.1f;
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
                    addMoney += 2.0f;
                    break;
                default:
                    Debug.Log("!アイテム効果が見つかりません");
                    break;
            }
            //プレイヤーステータスクラス内の計算クラスに代入
            playerStatusManager.statusCalc.AddMaxHP = addMaxHP;
            playerStatusManager.statusCalc.IncreaseAttack = increaseAttack;
            playerStatusManager.statusCalc.IncreaseBlock = increaseBlock;
            playerStatusManager.statusCalc.AddCriticalChance = addCriticalChance;
            playerStatusManager.statusCalc.AddCriticalDamage = addCriticalDamage;
            playerStatusManager.addMoney = addMoney;
            playerStatusManager.addAttackDamage = addSword;
            playerStatusManager.addShotDamage = addShot;
            playerStatusManager.plusShotDamage = plusShot;
            playerStatusManager.RoadHP();
        }
    }

    //アイテム取得関数
    public void AddItem(string id)
    {
        //取得アイテムが所持アイテム中にあるか調べる
        for (int i = 0; i < havingItem.Count; i++)
        {
            if (havingItem[i] == id)
            {
                //すでに所持していたなら何もしない
                Debug.Log(id + "をすでに所持しています");
                return;
            }
        }
        //未所持なら
        havingItem.Add(id);//所持アイテムにidを追加
        Debug.Log(id + "を獲得しました");
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
        int price = itemManager.GetBuyingPrice(id);

        //所持金が足りているなら
        if (playerStatusManager.status.Money >= price)
        {
            //価格分、所持金を減らす
            playerStatusManager.GettingMoney(price * -1);
            AddItem(id);    //アイテムを獲得

            Debug.Log(id + "を購入 : " + price);
        }
        else
        {
            Debug.Log("お金が足りません");
        }
    }
    //アイテム売却関数 今のところ使う予定なし
    public void SellingItem(string id)
    {
        //購入価格を取得
        int price = itemManager.GetSellingPrice(id);

        //価格分、所持金を増やす
        playerStatusManager.GettingMoney(price);
        RemoveItem(id);     //アイテムを獲得

        Debug.Log(id + "を売却");
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

    //所持アイテムデバッグ表示関数
    public void CheckHaveItem()
    {
        if (havingItem.Count == 0)
            Debug.Log("アイテムを所持していません");

        for(int i = 0; i < havingItem.Count;i++)
        {
            Debug.Log(i + " : " + havingItem[i]);
        }

    }
}
