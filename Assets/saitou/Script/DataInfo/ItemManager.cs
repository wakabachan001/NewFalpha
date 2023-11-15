using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//全アイテムの管理クラス
public class ItemManager : MonoBehaviour
{
    //全アイテムデータList
    private List<ItemDataC> ItemData = new List<ItemDataC>();

    private TextAsset csvFile; // CSVファイル
    private List<string[]> csvData = new List<string[]>(); // CSVファイルの中身を入れるリスト

    void Start()
    {
        csvFile = Resources.Load("ItemData") as TextAsset; // ResourcesにあるCSVファイルを格納
        StringReader reader = new StringReader(csvFile.text); // TextAssetをStringReaderに変換

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine(); // 1行ずつ読み込む
            csvData.Add(line.Split(',')); // csvDataリストに追加する 
        }
        for (int i = 0; i < 5; i++)
            Debug.Log(csvData[1][i]);

        //2行目からデータを読み込み
        for (int i = 1; i < csvData.Count; i++)
        {
            ItemDataC LoadItem = new ItemDataC();
            LoadItem.Id = csvData[i][0];
            LoadItem.ItemName = csvData[i][1];
            LoadItem.Description = csvData[i][2];
            LoadItem.BuyingPrice = int.Parse(csvData[i][3]);
            LoadItem.SellingPrice = int.Parse(csvData[i][4]);

            //アイテムデータに仮データを追加
            //!現在ItemDataリストに何かを代入すると、他要素も更新されてしまうバグあり
            //各要素の初期化を行わなければならないぽい    

            ItemData.Add(LoadItem);

            //ItemData[i - 1] = new ItemDataC();


        }

        for (int i = 0; i < ItemData.Count; i++)
        {
            Debug.Log(i);

            Debug.Log(ItemData[i].Id);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    //購入価格を返す関数
    public int GetBuyingPrice(string id)
    {
        //アイテムデータを全て探す
        for (int i = 0; i < ItemData.Count; i++)
        {
            //IDが一致したなら
            if (ItemData[i].Id == id)
            {
                return ItemData[i].BuyingPrice;
            }
        }
        //引数のIDがアイテムデータに存在しないなら
        Debug.Log("!指定したアイテムの購入価格が見つかりません");
        return 0;
    }
    //売却価格を返す関数
    public int GetSellingPrice(string id)
    {
        //アイテムデータを全て探す
        for (int i = 0; i < ItemData.Count; i++)
        {
            //IDが一致したなら
            if (ItemData[i].Id == id)
            {
                return ItemData[i].SellingPrice;
            }
        }
        //引数のIDがアイテムデータに存在しないなら
        Debug.Log("!指定したアイテムの売却価格が見つかりません");
        return 0;
    }

    //ランダムアイテム指定関数
    //後々オーバーロードでレアリティを指定できるようにしてもいい
    public string GetRandomItem()
    {
        //0～全アイテムの種類のランダムな数値を取得
        int r = Random.RandomRange(0, ItemData.Count);

        //その数値から、IDを返す
        return ItemData[r].Id;
    }
}

