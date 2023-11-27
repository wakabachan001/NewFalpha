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
    }

    public void ActiveTradeUI()
    {
        TradeUI.SetActive(true);

        ItemId = itemmanager.GetRandomItem(6);

        for ( int i = 0; i < 6; i++)
        {
           
            TradeItem[i].GetComponent<Image>().sprite = itemicon.SearchImage(ItemId[i]);
            textname[i].text = itemmanager.GetName(ItemId[i]);
            textdisc[i].text = itemmanager.GetDescription(ItemId[i]);
            textamount[i].text = itemmanager.GetBuyingPrice(ItemId[i]).ToString();

        }

        //string Id =itemmanager.GetRandomItem();
        //string name = itemmanager.GetName(Id);
        //string discription = itemmanager.GetDescription(Id);

        //itemname = name;
        // ランダムに3つのアイテムをアクティブにする
        //ActivateRandomItems(3);
    }

    public void CloseUI()
    {
        TradeUI.SetActive(false);
    }

    // ActiveItemSelectUI で表示されているアイテムを選択
    public void TradeChoice(int objectname)
    {
        PIM.BuyingItem(ItemId[objectname]); Debug.Log("ステータス上昇" + objectname);
        TradeUI.SetActive(false);
    }

    //void ActivateRandomItems(int count)
    //{
    //    // すべてのアイテムを非アクティブにする
    //    foreach (var item in Item)
    //    {
    //        item.SetActive(false);
    //    }

    //    List<int> selectedIndices = new List<int>();

    //    //ランダムなアイテムを選択
    //    while (selectedIndices.Count < count)
    //    {
    //        int randomIndex = Random.Range(0, Item.Length);
    //        if (!selectedIndices.Contains(randomIndex))
    //        {
    //            selectedIndices.Add(randomIndex);
    //        }
    //    }

    //    // 選択されたアイテムをアクティブにする
    //    foreach (int index in selectedIndices)
    //    {
    //        if (index >= 0 && index < Item.Length)
    //        {
    //            Item[index].SetActive(true);
    //        }
    //    }
    //}
}
