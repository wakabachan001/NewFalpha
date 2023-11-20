using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMap : MonoBehaviour
{
    public bool onNext = true; //次マップに進んだときtrue
    private int mapCount = 0;   //何マップ目かどうか
    private int shopRand;       //商人出現乱数用
    private int mapWidth = 5;   //マップの幅
    public int mapEnemyMinY = 4;  //敵が生成される範囲
    public int mapEnemyMaxY = 19;
    public int[] enemyCount = new int[2]; //生成される敵の数

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
            }

        }    
        
        //諸々生成
    }
}
