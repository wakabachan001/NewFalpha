using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//using TMPro;

public class ItemSerect : MonoBehaviour
{

    // ItemSelectUIを格納する変数
    // インスペクターからゲームオブジェクトを設定する
    [SerializeField] GameObject ItemSelectUI;
    [SerializeField] private EventSystem eventSystem;
    //[SerializeField] GameObject[] Item = new GameObject[12];

    GameObject SelectButton;
    GameObject FindID;
    //GameObject datainfo;
    ItemManager itemmanager = new ItemManager();
    PlayerItemManager PIM = new PlayerItemManager();
    ItemIcon itemicon = new ItemIcon();

    public GameObject[] Item = new GameObject[3];
    public Text[] textname = new Text[3];
    public Text[] textdisc = new Text[3];
    public string[] ItemId = new string[3];
    //　TextMeshProUGUI   メッシュプろを使う際はこっち
    private void Start()
    {
        //datainfo = GameObject.Find("DataInfo");
        itemmanager = LoadManagerScene.GetItemManager();

        PIM = LoadManagerScene.GetPlayerItemManager();
        itemicon = LoadManagerScene.GetItemIcon();
    }

    public void ActiveItemSelectUI()
    {
        ItemSelectUI.SetActive(true);

        ItemId = itemmanager.GetRandomItem(3) ;

        for (int i = 0; i < 3; i++)
        {
             
            Item[i].GetComponent<Image>().sprite = itemicon.SearchImage(ItemId[i]);
            textname[i].text = itemmanager.GetName(ItemId[i]);
            textdisc[i].text = itemmanager.GetDescription(ItemId[i]);

        }

        //string Id =itemmanager.GetRandomItem();
        //string name = itemmanager.GetName(Id);
        //string discription = itemmanager.GetDescription(Id);

        //itemname = name;
        // ランダムに3つのアイテムをアクティブにする
        //ActivateRandomItems(3);
    }

    // ActiveItemSelectUI で表示されているアイテムを選択
    public void ItemChoice(int objectname)
    {

        PIM.AddItem(ItemId[objectname]); Debug.Log("ステータス上昇"+objectname);
        ItemSelectUI.SetActive(false);
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