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

    // Start is called before the first frame update
    void Start()
    {
        //SpriteRendererを取得
        spriteRenderer = GetComponent<SpriteRenderer>();

        //初期の画像を閉じた画像にする
        spriteRenderer.sprite = closeImg;
    }
    
    //宝箱を開ける関数
    public void OpenBox()
    {
        open = true;
        Debug.Log("宝箱が開いた");

        //開けた画像に変える
        spriteRenderer.sprite = openImg;

        //アイテムなどを取得する
        //Instantiate(itemObj, transform.position - transform.up, Quaternion.identity);

    }
}
