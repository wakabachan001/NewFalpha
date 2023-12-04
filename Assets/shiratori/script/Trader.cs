using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Trader : MonoBehaviour
{
    // ItemSelectUIを格納する変数
    // インスペクターからゲームオブジェクトを設定する
    [SerializeField] GameObject TradeUI;
    //[SerializeField] GameObject[] Item = new GameObject[12];

    GameObject SelectButton;
    GameObject FindID;

    //GameObject datainfo;
    ItemManager itemmanager = new ItemManager();
    PlayerItemManager PIM = new PlayerItemManager();
    ItemIcon itemicon = new ItemIcon();

    PlayerManager playerManager;
    ItemUIManager itemUI;

    public GameObject[] TradeItem = new GameObject[6];
    public Text[] textamount = new Text[6];
    public Text[] textname = new Text[6];
    public Text[] textdisc = new Text[6];
    public string[] ItemId = new string[6];
    //　TextMeshProUGUI   メッシュプろを使う際はこっち
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
    }

    public void ActiveTradeUI()
    {
        TradeUI.SetActive(true);
        playerManager.dontMove = true;

        ItemId = itemmanager.GetRandomItem(6);

        for ( int i = 0; i < 6; i++)
        {
            if (ItemId[i] != null)
            {
                TradeItem[i].GetComponent<Image>().sprite = itemicon.SearchImage(ItemId[i]);
                textname[i].text = itemmanager.GetName(ItemId[i]);
                textdisc[i].text = itemmanager.GetDescription(ItemId[i]);
                textamount[i].text = itemmanager.GetBuyingPrice(ItemId[i]).ToString();
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

    }

    public void CloseUI()
    {
        TradeUI.SetActive(false);
        playerManager.dontMove = false;
    }

    public void OpenUI()
    {
        TradeUI.SetActive(true);
        playerManager.dontMove = true;
    }

    // ActiveItemSelectUI で表示されているアイテムを選択
    public void TradeChoice(int objectname)
    {
        PIM.BuyingItem(ItemId[objectname]); Debug.Log("ステータス上昇" + objectname);
        itemUI.ChangeIcon();//所持アイテムアイコン更新
    }

}
