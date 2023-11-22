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

    public GameObject floorTiles;//床オブジェクト

    public GameObject player;    //プレイヤーオブジェクト
    public GameObject tresureBox;//宝箱オブジェクト
    public GameObject warpZone;  //ワープゾーンオブジェクト
    public GameObject shop;      //商人オブジェクト
    public GameObject boss;      //ボスオブジェクト
    public GameObject[] enemyObj = new GameObject[2];//生成する敵オブジェクト
    public int[] enemyNum = new int[2];              //生成する敵の数
    private int[] enemyCount = new int[2]; //カウント用

    //オブジェクトの位置情報を保存する変数
    private Transform boardHolder;

    PlayerManager playerManager;

    [SerializeField]
    private int start;
    [SerializeField]
    private int end;
    Vector2 pos;

    // Start is called before the first frame update
    void Start()
    {
        shopRand = Random.RandomRange(1, 4);
        GameObject obj = GameObject.Find("Player");
        playerManager = obj.GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (onNext)
        {
            mapCount++;

            //onNextがオンになったとき、クローン全削除
            DestroyClone();

            //1マップ目だけ地面の生成
            if(mapCount == 1)
            {
                //床生成
                BoardSetup();
            }

            if(mapCount < 5)
            {
                //マップ生成
                MapCreate();
            }
            else
            {
                //ボスマップ生成
                BossMapCreate();
            }
            onNext = false;
        }
    }

    //マップ生成関数
    private void MapCreate()
    {
        Debug.Log("マップ生成");

        //マスの位置に対応した数値
        start = mapEnemyMinY * mapWidth;
        end = ((mapEnemyMaxY+1) * mapWidth)-1;

        //カウント用の変数を初期化
        for (int i = 0; i < enemyNum.Length; i++)
            enemyCount[i] = enemyNum[i];

        //ランダム用リスト
        List<int> mapNum = new List<int>();
        for (int i = start; i < end; i++)
            mapNum.Add(i);

        //敵クローン生成
        for (int i = 0; i < enemyCount.Length; i++)
        {
            while (enemyCount[i]-- > 0)
            {
                //ランダムな値を取得
                int index = Random.RandomRange(0, mapNum.Count);
                int rand = mapNum[index];

                //取得した値に対応した座標を設定
                pos.x = rand % 5;
                pos.y = rand / 5;

                //ランダムが重複しないように、リストから削除
                mapNum.RemoveAt(index);

                //敵クローン生成 pos
                Instantiate(enemyObj[i], pos, Quaternion.identity);
            }

        }
        //諸々生成
        Instantiate(tresureBox, new Vector2(2f, 21f), Quaternion.identity);
        Instantiate(warpZone, new Vector2(4f, 21f), Quaternion.identity);
        Instantiate(shop, new Vector2(1f, 21f), Quaternion.identity);//商人は後で制限する
        //Instantiate(player, new Vector2(2f, 1f), Quaternion.identity);
        

        //カメラを移動
        transform.position = new Vector3(cameraPosX, cameraPosY, cameraPosZ);

        //プレイヤーを移動　制限を変更
        playerManager.ResetPos(new Vector2(2f, 1f));
        playerManager.upLimit = mapHeight - 1;
        playerManager.backLimitArea = mapHeight - 3;
    }
    //プレイヤーの移動範囲未設定　倒されたときさらに変化させたい
    //ボスマップ生成関数
    private void BossMapCreate()
    {
        Debug.Log("ボスマップ生成");
        //奥行10で床を生成する
        mapHeight = 10;
        BoardSetup();

        //オブジェクトを生成
        Instantiate(boss, new Vector2(2f, 7f), Quaternion.identity);
        Instantiate(shop, new Vector2(1f, 3f), Quaternion.identity);   

        //カメラを移動
        transform.position = new Vector3(cameraPosX, cameraPosY, cameraPosZ);

        //プレイヤーを移動 制限を変更
        playerManager.ResetPos(new Vector2(2f, 1f));
        playerManager.upLimit = mapHeight - 3;
        playerManager.backLimitArea = mapHeight - 5;
    }
    //ボスが倒されたとき関数
    public void BossDead()
    {
        Debug.Log("ボス撃破時");
        Instantiate(tresureBox, new Vector2(2f, 9f), Quaternion.identity);
        Instantiate(warpZone, new Vector2(4f, 9f), Quaternion.identity);

        //プレイヤーの制限を変更
        playerManager.upLimit += 2;
        playerManager.backLimitArea += 3;
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

    //クローン全削除関数
    void DestroyClone()
    {
        //敵削除
        var enemyClones = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(var clone in enemyClones)
        {
            Destroy(clone);
        }

        //宝箱削除
        var boxClones = GameObject.FindGameObjectsWithTag("TreasureBox");
        foreach (var clone in boxClones)
        {
            Destroy(clone);
        }

        //shop削除
        var shopClones = GameObject.FindGameObjectsWithTag("Trader");
        foreach (var clone in shopClones)
        {
            Destroy(clone);
        }

        //ワープゾーン削除
        var warpClones = GameObject.FindGameObjectsWithTag("WarpZone");
        foreach (var clone in warpClones)
        {
            Destroy(clone);
        }

        //ボスマップだけ動く
        if(mapCount == 5)
        {
            //地面削除
            var floorClones = GameObject.FindGameObjectsWithTag("floorTiles");
            foreach (var clone in floorClones)
            {
                Destroy(clone);
            }
        }
    }
}
