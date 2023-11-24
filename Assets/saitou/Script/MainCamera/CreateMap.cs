using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//マップ生成用クラス
//カメラにアタッチ
public class CreateMap : MonoBehaviour
{
    public bool onNext = true;  //次マップに進んだときtrue
    [SerializeField]
    private int mapCount = 1;   //何マップ目かどうか
    [SerializeField]
    private int stageCount = 1; //何ステージ目かどうか
    private int shopRand;       //商人出現乱数用
    private int mapWidth = 5;   //マップの幅
    public int nomalmapHeight = 22;//通常マップの縦幅
    public int bossMapHeight = 10;  //ボスマップの縦幅
    private int mapHeight;
    public float cameraPosX = 2;    //カメラの初期位置
    public float cameraPosY = 2;
    private float cameraPosZ = -10;
    public int mapEnemyMinY = 4;  //敵が生成される範囲
    public int mapEnemyMaxY = 19;

    public GameObject[] floorTiles = new GameObject[3];//床オブジェクト

    public GameObject player;    //プレイヤーオブジェクト
    public GameObject tresureBox;//宝箱オブジェクト
    public GameObject warpZone;  //ワープゾーンオブジェクト
    public GameObject shop;      //商人オブジェクト
    public GameObject[] boss = new GameObject[3];         //ボスオブジェクト
    public GameObject[] stage1Enemy = new GameObject[2];  //敵オブジェクト (２次元配列でやっていたが、インスペクター上で変更できないため断念)
    public GameObject[] stage2Enemy = new GameObject[2];  
    public GameObject[] stage3Enemy = new GameObject[2];
    private GameObject[] enemyObj = new GameObject[2];    //実際に生成する敵オブジェクト
    public int[] enemyNum = new int[2];    //生成する敵の数
    private int[] enemyCount = new int[2]; //カウント用

    //オブジェクトの位置情報を保存する変数
    private Transform boardHolder;

    PlayerManager playerManager;
    PlayerStatusManager playerStatusManager;

    private int start;
    private int end;
    Vector2 pos;


    // Start is called before the first frame update
    void Start()
    {
        //スクリプトの取得
        playerStatusManager = LoadManagerScene.GetPlayerStatusManager();

        GameObject obj = GameObject.Find("Player");
        playerManager = obj.GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //onNextを受け取ったとき、ステージが3以下なら
        if (onNext && stageCount <= 3)
        {

            //onNextがオンになったとき、クローン全削除
            DestroyClone();

            //1マップ目だけ地面の生成
            if(mapCount == 1)
            {
                //HP,バリアの初期化
                playerStatusManager.ResetHpBr();

                //縦幅の設定
                mapHeight = nomalmapHeight;
                //床生成
                BoardSetup();

                //商人が何マップ目に出現するか決める(ボス手前は固定)
                shopRand = Random.RandomRange(1, 4);
            }

            //マップ生成
            if(mapCount < 5)
            {
                PicEnemy();
                //マップ生成
                MapCreate();
            }
            else
            {
                //縦幅の設定
                mapHeight = bossMapHeight;
                //ボスマップ生成
                BossMapCreate();

                //ボスマップのクリア後、次ステージの最初になるようにする
                stageCount++;
                mapCount = 0;
            }

            onNext = false;
            mapCount++;
        }
        else
        {
            //クリア
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
        //現在のマップが商人出現マップなら
        if (mapCount == shopRand)
        {
            Instantiate(shop, new Vector2(1f, 21f), Quaternion.identity);
        }       

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
        BoardSetup();

        //オブジェクトを生成
        Instantiate(boss[stageCount], new Vector2(2f, 7f), Quaternion.identity);
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
        if (stageCount <= 3)
        {
            //最終ステージのボスマップは宝箱を生成しない
            Instantiate(tresureBox, new Vector2(2f, 9f), Quaternion.identity);
        }

        Instantiate(warpZone, new Vector2(4f, 9f), Quaternion.identity);

        //プレイヤーの制限を変更
        playerManager.upLimit += 2;
        playerManager.backLimitArea += 3;
    }

    //床を配置
    void BoardSetup()
    {
        Debug.Log("床生成");
        // Boardというオブジェクトを作成し、transform情報をboardHolderに保存
        boardHolder = new GameObject("Board").transform;

        // 幅5マス
        for (int x = 0; x < mapWidth; x++)
        {
            //　ｙ＝１～２０をループ
            for (int y = 0; y < mapHeight; y++)
            {
                GameObject toInstantiate = floorTiles[stageCount];


                //床を生成し、instance変数に格納
                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f),
                            Quaternion.identity) as GameObject;

                //生成したinstanceをBoardオブジェクトの子オブジェクトとする
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    //生成する敵を決める関数
    void PicEnemy()
    {
        //生成する敵を、ステージによって変える
        //敵の種類を増やす場合ランダムを使う
        if (stageCount == 1)
        {
            enemyObj[0] = stage1Enemy[0];
            enemyObj[1] = stage1Enemy[1];
        }
        if (stageCount == 2)
        {
            enemyObj[0] = stage2Enemy[0];
            enemyObj[1] = stage2Enemy[1];
        }
        if (stageCount == 3)
        {
            enemyObj[0] = stage3Enemy[0];
            enemyObj[1] = stage3Enemy[1];
        }

        for (int i= 0;i<enemyNum.Length;i++)
        {
            enemyNum[i] = Random.RandomRange(5, 15);
        }
    }

    //クローン全削除関数
    void DestroyClone()
    {
        Debug.Log("クローン削除");
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

        //ボスマップと最初のマップだけ動く
        if(mapCount == 5 || mapCount == 1)
        {
            Debug.Log("地面削除");
            //地面削除
            var floorClones = GameObject.FindGameObjectsWithTag("floorTiles");
            foreach (var clone in floorClones)
            {
                Destroy(clone);
            }
        }
    }
}
