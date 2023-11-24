using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelectClick : MonoBehaviour
{
    GameObject selectOBJ;
    ItemSerect itemselect = new ItemSerect();

    private string objname, objname2;

    private void Start()
    {
        selectOBJ = GameObject.Find("UImanager");
        itemselect = selectOBJ.GetComponent<ItemSerect>();
    }

    public void Onclick()
    {
        objname = this.name;

        //　文字列の最後から１文字を抽出する
        objname2 = objname.Substring(objname.Length - 1);

        //　抽出した文字列を整数に変換して渡す
        itemselect.ItemChoice(int.Parse(objname2));
    }
}
