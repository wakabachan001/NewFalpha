using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSerect : MonoBehaviour
{

    // ItemSelectUIを格納する変数
    // インスペクターからゲームオブジェクトを設定する
    [SerializeField] GameObject ItemSelectUI;
    //[SerializeField] GameObject[] Item = new GameObject[12];

    GameObject datainfo;
    ItemManager itemmanager = new ItemManager();

    public Text[] textname = new Text[3];
    public Text[] textdisc = new Text[3];
    //　TextMeshProUGUI   メッシュプろを使う際はこっち
    private void Start()
    {
        datainfo = GameObject.Find("DataInfo");
        itemmanager = datainfo.GetComponent<ItemManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            ActiveItemSelectUI();
        }
    }

    public void ActiveItemSelectUI()
    {
        ItemSelectUI.SetActive(true);

        textname[0].text = "asdjaisd";

        for (int i = 0; i < 3; i++)
        {
            string Id = itemmanager.GetRandomItem();
            textname[i].text = itemmanager.GetName(Id);
            textdisc[i].text = itemmanager.GetDescription(Id);
        }

        //string Id =itemmanager.GetRandomItem();
        //string name = itemmanager.GetName(Id);
        //string discription = itemmanager.GetDescription(Id);

        //itemname = name;
        // ランダムに3つのアイテムをアクティブにする
        //ActivateRandomItems(3);
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
