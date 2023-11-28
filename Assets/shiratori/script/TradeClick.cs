using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeClick : MonoBehaviour
{
    GameObject traderOBJ;
    Trader trader = new Trader();

    private string objname, objname2;

    private void Start()
    {
        traderOBJ = GameObject.Find("TradeUImanager");
        trader = traderOBJ.GetComponent<Trader>();
    }

    public void Onclick()
    {
        objname = this.name;

        //　文字列の最後から１文字を抽出する
        objname2 = objname.Substring(objname.Length - 1);

        //　抽出した文字列を整数に変換して渡す
        trader.TradeChoice(int.Parse(objname2));
    }
}
