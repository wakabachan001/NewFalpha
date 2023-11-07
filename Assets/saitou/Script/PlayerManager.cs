using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // Start is called before the first frame update
    public class PlayerManager : MonoBehaviour
    {
    public float speed = 1.0f;      //移動距離
    public float shotSpeed = 0.2f;  //手裏剣の速度
    public float playerHP = 4.0f;   //プレイヤーの体力
    public float barrierCur = 2.0f;  //現在のバリア値
    public float takesDamage = 2.0f;  //被ダメージ
    
    public float effectLimit;       //近距離攻撃の判定が残る時間
    public float shotLimit = 3.5f;  //遠距離攻撃の飛距離の上限
    public float shotLange;        //遠距離攻撃の飛距離
    public float swordDamage = 2.0f;     //近距離攻撃ダメージ
    public float syurikenDamage = 1.5f;  //遠距離攻撃ダメージ

    public float leftLimit = 1.0f;  //侵入できる左の限界
    public float rightLimit = 5.0f; //侵入できる右の限界
    public float upLimit = 20.0f;   //侵入できる上の限界

    bool onAttack = false;      //近距離攻撃フラグ
    bool onShot = false;        //遠距離攻撃フラグ
    bool onBottomColumn = true; //下列にいるかどうか
    private float time; //時間計測用

    public GameObject AttackEffect;  //近距離攻撃
    public GameObject ShotEffect;    //遠距離攻撃
    public GameObject ghostPrefab;   //残像用のプレハブ
    public HPBar hpbar;              //HPBarスクリプト
    public BarrierManager barrierbar;//BarrierManagerスクリプト

    void Start()
    {
        barrierCur = 2.0f;
    }

    // Update is called once per frame

    void Update()
    {
        //プレイヤー座標の取得
        Vector2 position = transform.position;

        //移動(場外にいかないようにする)
        if ((Input.GetKeyDown("left") ||
            Input.GetKeyDown(KeyCode.A)) &&
            position.x > leftLimit)
        {
            CloneAfterimage();
            position.x -= speed;
        }
        if ((Input.GetKeyDown("right") ||
            Input.GetKeyDown(KeyCode.D)) &&
            position.x < rightLimit)
        {
            CloneAfterimage();
            position.x += speed;
        }
        if ((Input.GetKeyDown("up") ||
            Input.GetKeyDown(KeyCode.W)) &&
            position.y < upLimit)
        {
            CloneAfterimage();
            position.y += speed;
            onBottomColumn = false;
        }
        if ((Input.GetKeyDown("down") ||
            Input.GetKeyDown(KeyCode.S)) &&
            !onBottomColumn)
        {
            CloneAfterimage();
            position.y -= speed;
            //ボスエリアより手前なら
            if(transform.position.y < 19.0f)
                onBottomColumn = true;  //後退時に下列にいることにする
        }
        

        transform.position = position;  //座標の更新

        //近距離攻撃
        if (Input.GetKeyDown(KeyCode.Space) && !onAttack && !onShot)//攻撃開始時(Spaceキーを押すと攻撃開始)
        {
            //プレイヤーの前方に攻撃エフェクトのクローン生成
            Instantiate(AttackEffect, transform.position + transform.up, Quaternion.identity);

            time = 0.0f;        //時間のリセット
            onAttack = true;    //攻撃フラグオン
        }

        //遠距離攻撃
        if (Input.GetKeyDown(KeyCode.LeftShift) && !onAttack && !onShot)//攻撃開始時(Spaceキーを押すと攻撃開始)
        {
            //現在いる列によって飛距離を変更
            if (onBottomColumn)
                shotLange = shotLimit;
            else
                shotLange = shotLimit - 1.0f;

            //プレイヤーの位置に手裏剣のクローン生成
            Instantiate(ShotEffect, transform.position, Quaternion.identity);

            time = 0.0f;        //時間のリセット
            onShot = true;      //攻撃フラグオン
        }

    }

        private void FixedUpdate()
        {
        //近距離攻撃処理
        if (onAttack)
        {
            //１秒で1.0f増やす
            time += 0.02f;

            //timeが指定した時間以上になると
            if (time >= effectLimit)
            {
                //攻撃フラグを下げる
                onAttack = false;
            }
        }

        //遠距離攻撃処理
        if (onShot)
        {
            //timeを速度と同じだけ増やす
            time += shotSpeed;

            //timeが指定した時間以上になると
            if (time >= shotLange)
            {
                //攻撃フラグを下げる
                onShot = false;
            }
        }
        barrierbar.UpdateBarrier();
        }

    //敵などとの接触時のダメージ判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //接触タグが敵の攻撃か、敵本体ならHPを減らす
        if(collision.gameObject.tag == "EnemyAttack"|| collision.gameObject.tag == "Enemy")
        {
            Debug.Log("ダメージを食らった");
            playerHP -= ( takesDamage / barrierCur );   //プレイヤーの体力を減らす（後で右を変更）

            //HPBarの呼び出し
            hpbar.UpdateHP(playerHP);

            PlayerDead();       //プレイヤーが倒れるかチェック
        }
    }

    //プレイヤーがやられたとき関数
    void PlayerDead()
    {
        if (playerHP <= 0)
        {
            Debug.Log("やられた");
            Destroy(gameObject, 0.4f);
        }
    }

    //残像生成関数
    void CloneAfterimage()
    {
        //元のオブジェクトの位置に残像を生成
        GameObject ghost =
            Instantiate(ghostPrefab, transform.position, transform.rotation);
    }
}



