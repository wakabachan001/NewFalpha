using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpManager : MonoBehaviour
{
    public GameObject checkText;  //表示するテキスト
    Transform canvas;            //キャンバス
    GameObject ui;                //クローン管理用

    CreateMap createMap;          //マップ生成スクリプト

    bool ontext = false;          //textが表示されているかどうか

    void OnTriggerEnter2D(Collider2D other)
    {
        //プレイヤーと接触
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("ワープゾーンに入った");

            //CreateMapスクリプトを取得
            GameObject obj = GameObject.Find("Main Camera");
            createMap = obj.GetComponent<CreateMap>();

            //CanvasのTransformを取得
            GameObject parent = GameObject.Find("Canvas");
            canvas = parent.transform;

            ui = Instantiate(checkText, canvas.position + transform.up*20f, Quaternion.identity, canvas);

            ontext = true;   
        }
    }

    private void Update()
    {
        if (ontext)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //マップ生成フラグをオン
                createMap.onNext = true;

                Destroy(ui);
                ontext = false;
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(ui);
                ontext = false;
            }
        }
    }
}
