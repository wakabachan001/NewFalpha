using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemObjectManager : MonoBehaviour
{
    public string itemId = "str_up";    //このオブジェクトのアイテムID
    public float destroyTime = 0.1f;    //消えるまでの時間

    PlayerItemManager playerItemManager;
    ItemManager itemManager;
    ItemUIManager itemUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Playerタグとの接触時
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("アイテム獲得");

            //スクリプトを取得
            playerItemManager = LoadManagerScene.GetPlayerItemManager();
            itemManager = LoadManagerScene.GetItemManager();

            GameObject obj = GameObject.Find("Canvas");
            itemUI = obj.GetComponent<ItemUIManager>();


            //ランダムなアイテムのIDを取得
            itemId = itemManager.GetRandomItem();

            Debug.Log(itemId + "を取得");

            //このオブジェクトのアイテムIDを取得させる
            playerItemManager.AddItem(itemId);

            //アイコン更新
            itemUI.ChangeIcon();

            //このオブジェクトを削除
            Destroy(gameObject, destroyTime);
        }
    }
}

