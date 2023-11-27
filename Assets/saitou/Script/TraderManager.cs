using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderManager : MonoBehaviour
{
    public bool shopCount = false;  //すでにこのショップを開いたか 

    private Trader traderUI;

    private void Start()
    {
        shopCount = false;

        //ItemSerectスクリプトを探す
        GameObject obj = GameObject.Find("TradeUImanager");
        traderUI = obj.GetComponent<Trader>();
    }

    //取引開始関数
    public void OpenShop()
    {
        if (shopCount == false)
        {
            shopCount = true;

            //UIの初期化
            traderUI.ActiveTradeUI();
        }
        else
        {
            //UIを開く
            traderUI.OpenUI();
        }
    }
}
