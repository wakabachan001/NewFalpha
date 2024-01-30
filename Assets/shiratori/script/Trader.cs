using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public  enum Tabs
{
    BUY,
    UPGRADE
}

//表示データ
public struct BuyData
{
    public string id;
    public int grade;
    public Text price;
    public Text itemname;
    public Text disc;
}

public class Trader : MonoBehaviour
{
    // ItemSelectUIを格納する変数
    // インスペクターからゲームオブジェクトを設定する
    [SerializeField] GameObject TradeUI;
    //[SerializeField] GameObject[] Item = new GameObject[12];


    //GameObject datainfo;
    ItemManager itemmanager = new ItemManager();
    PlayerItemManager PIM = new PlayerItemManager();
    ItemIcon itemicon = new ItemIcon();

    PlayerManager playerManager;
    ItemUIManager itemUI;
    Sounds sounds;

    //購入アイテムアイコン
    public GameObject[] TradeItem = new GameObject[6];
    public Text[] textamount = new Text[6];
    public Text[] textname = new Text[6];
    public Text[] textdisc = new Text[6];
    public string[] ItemId = new string[6];

    //アップグレードアイコン
    public GameObject[] UpgradeItem = new GameObject[8];
    private GameObject[] UpgradeFrame = new GameObject[8];
    private BuyData[] UpgradeItemData = new BuyData[8];

    private int curTab; //現在のタブ

    private void Start()
    {
        // datainfo = GameObject.Find("DataInfo");
        itemmanager = LoadManagerScene.GetItemManager();

        PIM = LoadManagerScene.GetPlayerItemManager();
        itemicon = LoadManagerScene.GetItemIcon();

        GameObject obj = GameObject.Find("Player");
        playerManager = obj.GetComponent<PlayerManager>();

        GameObject canv = GameObject.Find("Canvas");
        itemUI = canv.GetComponent<ItemUIManager>();

        GameObject soun = GameObject.Find("SoundObject");
        sounds = soun.GetComponent<Sounds>();

        for (int i = 0; i < 8; i++)
        {
            foreach (Transform child in UpgradeItem[i].transform)
            { 
                switch (child.name)
                {
                    case "Name":
                        UpgradeItemData[i].itemname = child.GetComponent<Text>();
                        break;
                    case "Disc":
                        UpgradeItemData[i].disc = child.GetComponent<Text>();
                        break;
                    case "Price":
                        UpgradeItemData[i].price = child.GetComponent<Text>();
                        break;
                    case "Frame":
                        UpgradeFrame[i] = child.gameObject;
                        break;
                    default:
                        Debug.Log("アイコン内のオブジェクトを取得できませんでした");
                        break;
                }
            }
        }
    }

    public void ActiveTradeUI()
    {
        //アイコンの有効化
        foreach(GameObject obj in TradeItem)
        {
            obj.GetComponent<TradeClick>().Active();
        }
        foreach (GameObject obj in UpgradeItem)
        {
            obj.GetComponent<TradeClick>().Active();
        }

        curTab = (int)Tabs.BUY;

        TradeUI.SetActive(true);
        playerManager.dontMove = true;

        ItemId = PIM.GetRandomItem(6, false);

        for ( int i = 0; i < 6; i++)
        {
            if (ItemId[i] != null)
            {
                TradeItem[i].GetComponent<Image>().sprite = itemicon.SearchImage(ItemId[i]);
                textname[i].text = itemmanager.GetItemData(ItemId[i], 0, (int)ItemElement.NAME);
                textdisc[i].text = itemmanager.GetItemData(ItemId[i], 0, (int)ItemElement.DESCRIPTION);
                textamount[i].text = itemmanager.GetBuyingPrice(0).ToString();
            }
            //アイテムが見つからなかった場合
            else
            {
                TradeItem[i].GetComponent<Image>().sprite = itemicon.Empty();
                textname[i].text = "NoName";
                textdisc[i].text = " ";
                textamount[i].text = "0";
            }

        }
        GetHaveItemData();
        for (int i = 0; i < 8; i++)
        {
            if (UpgradeItemData[i].id != null)
            {
                //画像の取得
                UpgradeItem[i].GetComponent<Image>().sprite = itemicon.SearchImage(UpgradeItemData[i].id);
                UpgradeFrame[i].GetComponent<Image>().sprite = itemicon.SearchFrame(UpgradeItemData[i].grade);
            }
            //アイテムが見つからなかった場合
            else
            {
                UpgradeItem[i].GetComponent<Image>().sprite = itemicon.Empty();
                UpgradeItemData[i].itemname.text = "NoName";
                UpgradeItemData[i].disc.text = " ";
                UpgradeItemData[i].price.text = "0";
            }

        }
    }

    public void CloseUI()
    {
        TradeUI.SetActive(false);
        playerManager.dontMove = false;
        sounds.MenuCloseSE();//SE 閉じる
    }

    public void OpenUI()
    {
        TradeUI.SetActive(true);
        playerManager.dontMove = true;
    }

    // ActiveItemSelectUI で表示されているアイテムを選択
    public bool TradeChoice(int objectname, int tab)
    {
        Debug.Log("ステータス上昇" + objectname);

        switch (tab)
        {
            case (int)Tabs.BUY:

                //購入関数を呼び出し、購入できたなら
                if (PIM.BuyingItem(ItemId[objectname]) == true)
                {
                    itemUI.ChangeIcon();//所持アイテムアイコン更新
                    GetHaveItemData();
                    sounds.BuySE();//SE 購入
                    return true;
                }
                else
                {
                    sounds.ClickSE();//SE クリック
                    return false;
                }

            case (int)Tabs.UPGRADE:
                //購入関数を呼び出し、購入できたなら
                if (PIM.BuyingItem(UpgradeItemData[objectname].id) == true)
                {
                    itemUI.ChangeIcon();//所持アイテムアイコン更新
                    sounds.BuySE();//SE 購入
                    return true;
                }
                else
                {
                    sounds.ClickSE();//SE クリック
                    return false;
                }
            default:
                return false;
        }
    }


    //所持アイテム情報の取得関数
    public void GetHaveItemData()
    {
        //所持アイテムID取得
        string[] haveitem = new string[PIM.maxItem];
        haveitem = PIM.GetHaveItem();

        foreach(string str in haveitem)
        {
            Debug.Log(str);
        }

        //所持アイテムのidから他要素を求める
        for (int i = 0; i < PIM.maxItem; i++)
        {
            if (haveitem[i] != null)
            {
                UpgradeItemData[i].id = haveitem[i];

                UpgradeItemData[i].grade = PIM.GetHaveGrade(haveitem[i]) + 1;

                UpgradeItemData[i].itemname.text = itemmanager.GetItemData(
                    haveitem[i], 
                    UpgradeItemData[i].grade, 
                    (int)ItemElement.NAME);

                UpgradeItemData[i].disc.text = itemmanager.GetItemData(
                    haveitem[i], 
                    UpgradeItemData[i].grade, 
                    (int)ItemElement.DESCRIPTION);

                UpgradeItemData[i].price.text = itemmanager.GetBuyingPrice(UpgradeItemData[i].grade).ToString();
            }
        }
    }
}
