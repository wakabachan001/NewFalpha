using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TresureBoxManager : MonoBehaviour
{
    private bool open = false;//宝箱が開いているか

    public Sprite closeImg;//閉じた画像
    public Sprite openImg; //開いた画像

    private SpriteRenderer spriteRenderer;

    public GameObject itemObj;//アイテムオブジェクト

    private ItemSerect itemSerect;

    // Start is called before the first frame update
    void Start()
    {
        //SpriteRendererを取得
        spriteRenderer = GetComponent<SpriteRenderer>();

        //ItemSerectスクリプトを探す
        GameObject obj = GameObject.Find("UImanager");
        itemSerect = obj.GetComponent<ItemSerect>();

        //初期の画像を閉じた画像にする
        spriteRenderer.sprite = closeImg;

        open = false;
    }
    
    //宝箱を開ける関数
    public void OpenBox()
    {
        //空いていないなら
        if (open == false)
        {
            open = true;
            Debug.Log("宝箱が開いた");

            //開けた画像に変える
            spriteRenderer.sprite = openImg;    

            //宝箱用UIを表示する
            itemSerect.ActiveItemSelectUI();
        }
        else
        {
            Debug.Log("すでに開いています");
            itemSerect.OpenUI();
        }
    }
}
