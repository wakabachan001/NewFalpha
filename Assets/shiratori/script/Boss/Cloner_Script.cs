using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloner_Script : MonoBehaviour
{   
    PlayerManager _player;
    GameObject playerObj;//プレイヤーを探す用
    public GameObject enemyPrefab; // 分身する敵のプレハブ
    public int numberOfClones = 3; // 分身する敵の数
    public float cloneDistance = 1.5f; // 分身する敵の間隔

    bool positionflag = true;

    Vector2 position; //Boss02座標]
    Vector2 playerPOS;
    
    Vector2[] clonePosition = new Vector2[2];//clonePositionの初期化
    GameObject[] clone = new GameObject[2];// clone生成用変数を初期化
   

    void Start()
    {
        //Boss02座標の取得
        position = transform.position;
        playerObj = GameObject.Find("Player");//プレイヤーを探す
        _player = playerObj.GetComponent<PlayerManager>();
        SpawnClones();
    }

    private void FixedUpdate()
    {

        if(transform.position.x != playerObj.transform.position.x&&positionflag)
        {
            StartCoroutine(PositionChange());
        }

        //プレイヤーの１マス前にいるとき
        if( transform.position.x == playerObj.transform.position.x &&
            transform.position.y == playerObj.transform.position.y -1 )
        {

        }
    }


    void SpawnClones()
    {
        
        for (int i = 0; i < numberOfClones; i++)
        {
            clonePosition[i] = new Vector2(transform.position.x + i * cloneDistance -1, transform.position.y);
            clone[i] = Instantiate(enemyPrefab, clonePosition[i], Quaternion.identity);Debug.Log("生成");
            clone[i].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // サイズを1/2倍に変更する
             // クローンに対する追加の設定などを行う場合は、ここで行います
        }
        
    }

    private IEnumerator PositionChange()
    {
        positionflag = false;
        //待機
        yield return new WaitForSeconds(0.7f);

        //Boss本体　プレイヤーの x位置 に移動
        position.x = playerObj.transform.position.x;
        transform.position = position;

        //clone 座標更新
        for (int i = 0; i < numberOfClones; i++) 
        {
            //　クローン座標変数にボス本体の座標を代入
            clonePosition[i] = new Vector2(transform.position.x + i * cloneDistance - 1, transform.position.y);

            //　クローンに変数の座標を代入
            clone[i].transform.position = clonePosition[i];

            Debug.Log(i + "番目");
        }

        yield return new WaitForSeconds(0.35f);
        positionflag = true;
    }
}
