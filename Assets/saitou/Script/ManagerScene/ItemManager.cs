using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//アイテムデータの要素
public enum ItemElement
{
    ID,
    NAME,
    DESCRIPTION,
    GRADE,
    MAXHP,
    ATTACK,
    SWORD,
    SHOT,
    BLOCK,
    CRICHANCE,
    CRIDAMAGE,
    MONEY
}

//全アイテムの管理クラス
//基本的に他オブジェクトからアクセス出来ないようにしたい
public class ItemManager : MonoBehaviour
{
    
    //全アイテムデータList
    //private List<ItemDataC> ItemData = new List<ItemDataC>();

    private List<ItemDataC> ItemData = new List<ItemDataC>();

    private TextAsset csvFile; // CSVファイル
    private List<string[]> csvData = new List<string[]>(); // CSVファイルの中身を入れるリスト

    private PlayerItemManager playerItemManager;

    void Start()
    {
        {
        //    playerItemManager = GetComponent<PlayerItemManager>();

        //    csvFile = Resources.Load("ItemData") as TextAsset; // ResourcesにあるCSVファイルを格納
        //    StringReader reader = new StringReader(csvFile.text); // TextAssetをStringReaderに変換

        //    while (reader.Peek() != -1)
        //    {
        //        string line = reader.ReadLine(); // 1行ずつ読み込む
        //        csvData.Add(line.Split(',')); // csvDataリストに追加する 
        //    }
        //    for (int i = 0; i < 5; i++)
        //        Debug.Log(csvData[1][i]);

        //    //2行目からデータを読み込み
        //    for (int i = 1; i < csvData.Count; i++)
        //    {
        //        ItemDataC LoadItem = new ItemDataC();
        //        LoadItem.Id = csvData[i][0];
        //        LoadItem.ItemName = csvData[i][1];
        //        LoadItem.Description = csvData[i][2];
        //        LoadItem.BuyingPrice = int.Parse(csvData[i][3]);
        //        LoadItem.SellingPrice = int.Parse(csvData[i][4]);

        //        //アイテムデータにデータを追加
        //        ItemData.Add(LoadItem);
        //    }
        //    Debug.Log("アイテムデータを作成しました");
        }

        playerItemManager = GetComponent<PlayerItemManager>();


        csvFile = Resources.Load("ItemData_v3") as TextAsset; // ResourcesにあるCSVファイルを格納
        StringReader reader = new StringReader(csvFile.text); // TextAssetをStringReaderに変換

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine(); // 1行ずつ読み込む
            csvData.Add(line.Split(',')); // csvDataリストに追加する 
        }

        int row = 1;//csvの行

        //2行目からデータを読み込み
        while (row < csvData.Count)
        {
            ItemDataC LoadItem = new ItemDataC();

            for (int j = 0; j < 3; j++)
            {
                //アイテム情報を取得
                LoadItem.grade[j].Id            = csvData[row][(int)ItemElement.ID];
                LoadItem.grade[j].ItemName      = csvData[row][(int)ItemElement.NAME];
                LoadItem.grade[j].Description   = csvData[row][(int)ItemElement.DESCRIPTION];
                LoadItem.grade[j].Grade         = int.Parse(  csvData[row][(int)ItemElement.GRADE]);
                LoadItem.grade[j].MaxHp         = float.Parse(csvData[row][(int)ItemElement.MAXHP]);
                LoadItem.grade[j].Attack        = float.Parse(csvData[row][(int)ItemElement.ATTACK]);
                LoadItem.grade[j].SwordAttack   = float.Parse(csvData[row][(int)ItemElement.SWORD]);
                LoadItem.grade[j].ShotAttack    = float.Parse(csvData[row][(int)ItemElement.SHOT]);
                LoadItem.grade[j].Block         = float.Parse(csvData[row][(int)ItemElement.BLOCK]);
                LoadItem.grade[j].CriChance     = float.Parse(csvData[row][(int)ItemElement.CRICHANCE]);
                LoadItem.grade[j].CriDamage     = float.Parse(csvData[row][(int)ItemElement.CRIDAMAGE]);
                LoadItem.grade[j].AddMoney      = float.Parse(csvData[row][(int)ItemElement.MONEY]);

                row++;
            }   
                //アイテムデータにデータを追加
                ItemData.Add(LoadItem);

        }
        Debug.Log("アイテムデータを作成しました");
    }

    //IDとグレードからアイテム情報を取得する関数(ID,グレード,返り値の要素)
    public string GetItemData(string id,int gra,int elem)
    {
        //グレードの値が正しくないなら
        if (gra < 0 || gra > 2)
        {            
            Debug.Log("!グレードの値が正しくないです");
            return null;
        }
        //要素の値が正しくないなら
        if(elem<0||elem>10)
        {
            Debug.Log("!要素の値が正しくないです");
            return null;
        }
        

        //アイテムデータを全て探す
        for (int i = 0; i < ItemData.Count; i++)
        {
            //IDが一致したなら
            if (ItemData[i].grade[0].Id == id)
            {
                switch (elem)
                {
                    case (int)ItemElement.NAME:
                        return ItemData[i].grade[gra].ItemName;

                    case (int)ItemElement.DESCRIPTION:
                        return ItemData[i].grade[gra].Description;

                    case (int)ItemElement.MAXHP:
                        return ItemData[i].grade[gra].MaxHp.ToString();

                    case (int)ItemElement.ATTACK:
                        return ItemData[i].grade[gra].Attack.ToString();

                    case (int)ItemElement.SWORD:
                        return ItemData[i].grade[gra].SwordAttack.ToString();

                    case (int)ItemElement.SHOT:
                        return ItemData[i].grade[gra].ShotAttack.ToString();

                    case (int)ItemElement.BLOCK:
                        return ItemData[i].grade[gra].Block.ToString();

                    case (int)ItemElement.CRICHANCE:
                        return ItemData[i].grade[gra].CriChance.ToString();

                    case (int)ItemElement.CRIDAMAGE:
                        return ItemData[i].grade[gra].CriDamage.ToString();

                    case (int)ItemElement.MONEY:
                        return ItemData[i].grade[gra].AddMoney.ToString();
                }               
            }
        }
        //引数のIDがアイテムデータに存在しないなら
        Debug.Log("!指定したアイテムのIDが見つかりません");
        return null;

    }


    //購入価格を返す関数 削除予定
    public int GetBuyingPrice(int gra)
    {
        switch (gra)
        {
            case 0:
                return 100;
            case 1:
                return 200;
            case 2:
                return 300;
            default:
                Debug.Log("!グレードの値がおかしいです");
                return 0;
        }  
    }

    //アイテムの要素数
    public int GetCount()
    {
        return ItemData.Count;
    }

    //引数の要素番目のIDを取得する関数
    public string GetID(int num)
    {
        if (num< ItemData.Count)
        {
            Debug.Log("IDを返す"+ ItemData[num].grade[0].Id);
            return ItemData[num].grade[0].Id;
        }
        else
        {
            Debug.Log("IDねえよ");
            return null;
        }
    }

    //アイテム効果を足す関数（ItemDataSの参照渡し,ID, グレード）
    public void PlusEffect(ref ItemDataS sum_effect, string id , int gra)
    {
        //一致するidを探す
        foreach(ItemDataC data in ItemData)
        {
            if(data.grade[0].Id == id)
            {
                //アイテム効果部分を足し合わせる
                sum_effect.MaxHp        += data.grade[gra].MaxHp;
                sum_effect.Attack       += data.grade[gra].Attack;
                sum_effect.SwordAttack  += data.grade[gra].SwordAttack;
                sum_effect.ShotAttack   += data.grade[gra].ShotAttack;
                sum_effect.Block        += data.grade[gra].Block;
                sum_effect.CriChance    += data.grade[gra].CriChance;
                sum_effect.CriDamage    += data.grade[gra].CriDamage;
                sum_effect.AddMoney     += data.grade[gra].AddMoney;

                return;
            }
        }

        Debug.Log("!アイテムidが見つかりません");
    }

    //ランダムアイテム指定関数　ほぼ未使用
    //public string GetRandomItem()
    //{
    //    //0～全アイテムの種類のランダムな数値を取得
    //    int r = Random.RandomRange(0, ItemData.Count);

    //    //その数値から、IDを返す
    //    return ItemData[r].grade[0].Id;
    //}
    //引数のidと被らないオーバーロード numは個数 havingItemの方に移行したい
    //public string[] GetRandomItem(int num = 1)
    //{
    //    string[] ans = new string[num];//返り値用配列

    //    //アイテムID一覧の生成
    //    List<string> itemId = new List<string>();
    //    for(int i = 0; i<ItemData.Count; i++)
    //    {
    //        itemId.Add(ItemData[i].grade[0].Id);
    //    }

    //    //引数のIDと被っている要素を削除
    //    for (int i = 0; i < playerItemManager.havingItem.Count; i++) 
    //    {
    //        itemId.Remove(playerItemManager.havingItem.grade[0]);
    //    }

    //    for (int i = 0; i < num; i++)
    //    {
    //        //itemIdの中身があるかチェック
    //        if (itemId.Count != 0)
    //        {
    //            //0～全アイテムの種類のランダムな数値を取得
    //            int r = Random.RandomRange(0, itemId.Count);

    //            ans[i] = itemId[r];//返り値用配列にランダムなIDを代入

    //            itemId.RemoveAt(r);//代入したIDを削除
    //        }
    //        else
    //        {
    //            //エラー
    //            Debug.Log("!未所持のアイテムが見つかりません");
    //            ans[i] = null;
    //        }
    //    }
    //    //その数値から、IDを返す
    //    return ans;
    //}

}

