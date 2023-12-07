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

    PlayerManager playerManager;
    ItemUIManager itemUI;
    Sounds sounds;

    public GameObject[] Item = new GameObject[3];
    public Text[] textname = new Text[3];
    public Text[] textdisc = new Text[3];
    public string[] ItemId = new string[3];
    //　TextMeshProUGUI   メッシュプろを使う際はこっち

    private bool getItem = false; //この宝箱でアイテムを取得したか
    private void Start()
    {
        //datainfo = GameObject.Find("DataInfo");
        itemmanager = LoadManagerScene.GetItemManager();

        PIM = LoadManagerScene.GetPlayerItemManager();
        itemicon = LoadManagerScene.GetItemIcon();

        GameObject obj = GameObject.Find("Player");
        playerManager = obj.GetComponent<PlayerManager>();

        GameObject canv = GameObject.Find("Canvas");
        itemUI = canv.GetComponent<ItemUIManager>();

        GameObject soun = GameObject.Find("SoundObject");
        sounds = soun.GetComponent<Sounds>();

        getItem = false;
    }

    public void ActiveItemSelectUI()
    {
        sounds.Treasure_ChestSE();//宝箱を開けた音
        ItemSelectUI.SetActive(true);
        playerManager.dontMove = true;

        ItemId = itemmanager.GetRandomItem(3) ;

        for (int i = 0; i < 3; i++)
        {
            if (ItemId[i] != null)
            {
                Item[i].GetComponent<Image>().sprite = itemicon.SearchImage(ItemId[i]);
                textname[i].text = itemmanager.GetName(ItemId[i]);
                textdisc[i].text = itemmanager.GetDescription(ItemId[i]);
            }
            //アイテムが見つからなかった場合
            else
            {
                
                Item[i].GetComponent<Image>().sprite = itemicon.Empty();
                textname[i].text = "NoName";
                textdisc[i].text = " ";
            }

        }

    }

    public void CloseUI()
    {
        ItemSelectUI.SetActive(false);
        playerManager.dontMove = false;
        sounds.MenuCloseSE();//SE 閉じる
    }

    public void OpenUI()
    {
        //まだこの宝箱からアイテムを取得していないなら
        if (getItem == false)
        {
            ItemSelectUI.SetActive(true);
            playerManager.dontMove = true;
        }
    }

    // ActiveItemSelectUI で表示されているアイテムを選択
    public void ItemChoice(int objectname)
    {
        Debug.Log("ステータス上昇" + objectname);

        //アイテム追加関数を呼び出して、取得出来たら
        if (PIM.AddItem(ItemId[objectname]) == true)
        {
            getItem = true;
            sounds.ClickSE();
            itemUI.ChangeIcon();//所持アイテムアイコン更新

            CloseUI();//UIを閉じる
        }
        
    }

}