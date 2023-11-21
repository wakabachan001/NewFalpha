using UnityEngine;
using System;
//Listを使うため
using System.Collections.Generic;
//Randomを使うため
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{

    //カウント用のクラスを設定
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    // 20*5のゲームボードを作るので縦の段を２０横の列を５
    public int columns = 5;
    public int rows = 20;
    public float adjustment = 0.2f;//オブジェクト位置調整用

    public Count enemyCount = new Count(14, 20);

    public GameObject   floorTiles;
    public GameObject   BossTiles;
    public GameObject[] enemyTiles;

    //オブジェクトの位置情報を保存する変数
    private Transform boardHolder;

        //オブジェクトを配置できる範囲を表すリスト
        //Ｌｉｓｔは可変型の配列
        private List<Vector3> gridPositions = new List<Vector3>();

        //敵キャラを配置できる範囲を決定
        void InitialiseList()
        {
            //gridPositionをクリア
            gridPositions.Clear();
            //gridPositionにオブジェクトw配置可能範囲を指定
            //　ｘ＝１～５をループ
            for (int x = 1; x <= columns; x++) 
            {
                // ｙ＝５～１８をループ
                for (int y = 5; y < rows - 1; y++) 
                {
                    //　５*１８の範囲をgridPositionに指定
                    gridPositions.Add(new Vector3(x, y + adjustment, 0f));
                }
            }

        }

    //床を配置
    void BoardSetup()
    {
        // Boardというオブジェクトを作成し、transform情報をboardHolderに保存
        boardHolder = new GameObject("Board").transform;

        // ｘ＝１～８をループ
        for (int x = 1; x < columns + 1; x++)
        {
            //　ｙ＝１～２０をループ
            for (int y = 1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles;


                //床を生成し、instance変数に格納
                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f),
                            Quaternion.identity) as GameObject;

                //生成したinstanceをBoardオブジェクトの子オブジェクトとする
                instance.transform.SetParent(boardHolder);
            }
        }

       
    }

    Vector3 RandomPosition()
    {
        // ０～３６～ランダムで１つ決定し、位置情報を確定
        int randomIndex = Random.Range(0, gridPositions.Count);

        Vector3 randomPosition = gridPositions[randomIndex];
        //ランダムで決定した数値は削除
        gridPositions.RemoveAt(randomIndex);
        //確定した位置情報を返す
        return randomPosition;
    }

    void LayoutObjectAtRandom(GameObject[] tileArray,int minimum,int maximum)
    {
        //最低値～最大値＋１のランダム回数分だけループ
        int objectCount = Random.Range(minimum, maximum + 1);
        for (int i = 0; i < objectCount; i++)
        {
            //gridPositionから位置情報を一つ取得
            Vector3 randomPosition = RandomPosition();
            //引数tileArrayからランダムで一つ選択
            GameObject tileChoise = tileArray[Random.Range(0, tileArray.Length)];
            //ランダムで決定した種類・位置でオブジェクトを生成
            Instantiate(tileChoise, randomPosition, Quaternion.identity);
        }
    }

    //オブジェクトを配置していくメソッド
    //このクラス内唯一のpublicメソッド床を生成するタイミングでGameManagerから呼ばれる
    public void SetupScene()
    {

        //床を配置し、
        BoardSetup();
        //敵キャラを配置できる位置を決定し、
        InitialiseList();

        //Instantiate(生成したいGameObject, 位置, );
        Instantiate(BossTiles, new Vector2(3, rows + adjustment), Quaternion.identity);

        //敵キャラをランダムで配置し、
        LayoutObjectAtRandom(enemyTiles, enemyCount.minimum, enemyCount.maximum);

    }


}
