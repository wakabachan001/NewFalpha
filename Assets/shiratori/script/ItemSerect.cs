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
    GameObject datainfo;
    ItemManager itemmanager = new ItemManager();
    PlayerItemManager PIM = new PlayerItemManager();

    public Text[] textname = new Text[3];
    public Text[] textdisc = new Text[3];
    public string[] ItemId = new string[3];
    //　TextMeshProUGUI   メッシュプろを使う際はこっち
    private void Start()
    {
        datainfo = GameObject.Find("DataInfo");
        itemmanager = datainfo.GetComponent<ItemManager>();

        PIM = datainfo.GetComponent<PlayerItemManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ActiveItemSelectUI();
        }
    }

    private int RandSelect;
    public void ActiveItemSelectUI()
    {
        ItemSelectUI.SetActive(true);

        for (RandSelect = 0; RandSelect < 3; RandSelect++)
        {
            ItemId[RandSelect] = itemmanager.GetRandomItem();
            textname[RandSelect].text = itemmanager.GetName(ItemId[RandSelect]);
            textdisc[RandSelect].text = itemmanager.GetDescription(ItemId[RandSelect]);
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