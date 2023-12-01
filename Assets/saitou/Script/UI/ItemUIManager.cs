using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIManager : MonoBehaviour
{
    public bool onChange = false; 

    //インスペクター上に表示されない（そもそもコンパイルエラー）
    [SerializeField] private float iconFirstPosX;   //アイコンの初期位置X（左上）
    [SerializeField] private float iconFirstPosY;   //アイコンの初期位置Y（左上）
    [SerializeField] private float iconPos;         //アイコン位置調整用
    [SerializeField] private GameObject iconPrefab; //生成するPrefab    
    
    [SerializeField]
    private Transform iconParent; //その親オブジェクト
    private GameObject[] iconObj = new GameObject[20]; //クローンしたオブジェクト

    PlayerItemManager playerItemManager;
    ItemIcon itemIcon;

    // Start is called before the first frame update
    void Start()
    {
        int column = 0;
        int row = 0;

        //コンパイルエラーはおそらくこれのせい
        //スクリプトの取得
        playerItemManager = LoadManagerScene.GetPlayerItemManager();
        itemIcon = LoadManagerScene.GetItemIcon();

        //Iconをまとめる用のオブジェクトを生成し、Canvasの子にする
        //iconParent = new GameObject("Icon").transform;
        //iconParent.SetParent(gameObject.transform);

        //クローン（アイコン）生成
        for(int i = 0; i < iconObj.Length; i++)
        {
            Debug.Log("アイコン作成");

            //子としてImageを持つPrefabをクローン
            iconObj[i] = Instantiate(iconPrefab, //クローンするオブジェクト
                new Vector2(iconParent.position.x + iconFirstPosX + (iconPos * column),
                            iconParent.position.y + iconFirstPosY + (iconPos * -row)),//leftInfo始点の位置
                Quaternion.identity,
                iconParent);//回転

            if (column < 3)
            {
                column++;
            }
            else
            {
                column = 0;
                row++;
            }
        }
    }

    //アイコン更新関数
    public void ChangeIcon()
    {
        Debug.Log("アイテムアイコン更新");
        Sprite iconImage; //探した画像を入れる用の変数

        //アイコンの画像を所持アイテムによって変える
        for (int i = 0; i < playerItemManager.havingItem.Count; i++)
        {
            //所持アイテムから画像を探す
            iconImage = itemIcon.SearchImage(playerItemManager.havingItem[i]);

            //エラーでなければ
            if (iconImage != null)
            {
                //クローンの画像を変える
                iconObj[i].GetComponent<Image>().sprite = iconImage;
            }
        }
    }

    private void Update()
    {
        if (onChange)
        {
            ChangeIcon();
            onChange = false;
        }
    }
}
