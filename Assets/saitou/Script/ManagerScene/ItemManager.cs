using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//全アイテムの管理クラス
//基本的に他オブジェクトからアクセス出来ないようにしたい
public class ItemManager : MonoBehaviour
{
    //全アイテムデータList
    private List<ItemDataC> ItemData = new List<ItemDataC>();

    private TextAsset csvFile; // CSVファイル
    private List<string[]> csvData = new List<string[]>(); // CSVファイルの中身を入れるリスト

    private PlayerItemManager playerItemManager;

    void Start()
    {
        playerItemManager = GetComponent<PlayerItemManager>();

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

            //アイテムデータにデータを追加
            ItemData.Add(LoadItem);
        }
        Debug.Log("アイテムデータを作成しました");
    }

    //名前を返す関数
    public string GetName(string id)
    {
        //アイテムデータを全て探す
        for (int i = 0; i < ItemData.Count; i++)
        {
            //IDが一致したなら
            if (ItemData[i].Id == id)
            {
                return ItemData[i].ItemName;
            }
        }
        //引数のIDがアイテムデータに存在しないなら
        Debug.Log("!指定したアイテムの名前が見つかりません");
        return null;
    }

    //説明文を返す関数
    public string GetDescription(string id)
    {
        //アイテムデータを全て探す
        for (int i = 0; i < ItemData.Count; i++)
        {
            //IDが一致したなら
            if (ItemData[i].Id == id)
            {
                return ItemData[i].Description;
            }
        }
        //引数のIDがアイテムデータに存在しないなら
        Debug.Log("!指定したアイテムの説明文が見つかりません");
        return null;
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
    public string GetRandomItem()
    {
        //0～全アイテムの種類のランダムな数値を取得
        int r = Random.RandomRange(0, ItemData.Count);

        //その数値から、IDを返す
        return ItemData[r].Id;
    }
    //引数のidと被らないオーバーロード numは個数
    public string[] GetRandomItem(int num = 1)
    {
        string[] ans = new string[num];//返り値用配列

        //アイテムID一覧の生成
        List<string> itemId = new List<string>();
        for(int i = 0; i<ItemData.Count; i++)
        {
            itemId.Add(ItemData[i].Id);
        }

        //引数のIDと被っている要素を削除
        for (int i = 0; i < playerItemManager.havingItem.Count; i++) 
        {
            itemId.Remove(playerItemManager.havingItem[i]);
        }

        for (int i = 0; i < num; i++)
        {
            //itemIdの中身があるかチェック
            if (itemId.Count != 0)
            {
                //0～全アイテムの種類のランダムな数値を取得
                int r = Random.RandomRange(0, itemId.Count);

                ans[i] = itemId[r];//返り値用配列にランダムなIDを代入

                itemId.RemoveAt(r);//代入したIDを削除
            }
            else
            {
                //エラー
                Debug.Log("!未所持のアイテムが見つかりません");
                return null;
            }
        }
        //その数値から、IDを返す
        return ans;
    }
}

