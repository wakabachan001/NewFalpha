using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//マップ生成用クラス
//カメラにアタッチ
public class CreateMap : MonoBehaviour
{
    public bool onNext = true;  //次マップに進んだときtrue
    private int mapCount = 0;   //何マップ目かどうか
    private int shopRand;       //商人出現乱数用
    private int mapWidth = 5;   //マップの幅
    public int mapHeight = 22;
    public float cameraPosX = 2;    //カメラの初期位置
    public float cameraPosY = 2;
    private float cameraPosZ = -10;
    public int mapEnemyMinY = 4;  //敵が生成される範囲
    public int mapEnemyMaxY = 19;

    public GameObject floorTiles;
    public GameObject[] enemyObj = new GameObject[2];//生成する敵オブジェクト
    public int[] enemyCount = new int[2]; //生成する敵の数

    //オブジェクトの位置情報を保存する変数
    private Transform boardHolder;

    private int start;
    private int end;
    Vector2 pos;

    // Start is called before the first frame update
    void Start()
    {
        shopRand = Random.RandomRange(1, 4);
    }

    // Update is called once per frame
    void Update()
    {
        if (onNext)
        {
            //onNextがオンになったとき、クローン全削除
            mapCount++;
            if(mapCount < 5)
            {
                //マップ生成
                MapCreate();
            }
            else
            {
                //ボスマップ生成
            }
            onNext = false;
        }
    }

    //マップ生成関数
    private void MapCreate()
    {
        BoardSetup();

        start = mapEnemyMinY * mapWidth;
        end = mapEnemyMaxY * (mapWidth + 1);

        List<int> mapNum = new List<int>();

        for (int i = start; i < end; i++)
            mapNum.Add(i);

        for (int i = 0; i < enemyCount.Length; i++)
        {
            while (enemyCount[i]-- > 0)
            {
                int index = Random.RandomRange(0, mapNum.Count);

                int rand = mapNum[index];
                //ここでmapNum[index]を削除する

                pos.x = rand % 5;
                pos.y = rand / 5;

                //敵クローン生成 pos
                Instantiate(enemyObj[i], pos, Quaternion.identity);
            }

        }

        //諸々生成

        //カメラを移動
        transform.position = new Vector3(cameraPosX, cameraPosY, cameraPosZ);
    }

    //床を配置
    void BoardSetup()
    {
        // Boardというオブジェクトを作成し、transform情報をboardHolderに保存
        boardHolder = new GameObject("Board").transform;

        // 幅5マス
        for (int x = 0; x < mapWidth; x++)
        {
            //　ｙ＝１～２０をループ
            for (int y = 0; y < mapHeight; y++)
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
}
