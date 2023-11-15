using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // Start is called before the first frame update
    public class PlayerManager : MonoBehaviour
    {
    public float swordDamage;
    public float syurikenDamage;//後で削除（BossManagerをいじる）

    public float speed = 1.0f;      //移動距離
    public float shotSpeed = 0.2f;  //手裏剣の速度
    public float takesDamage = 2.0f;  //被ダメージ
    
    public float effectLimit;       //近距離攻撃の判定が残る時間
    public float shotLimit = 3.5f;  //遠距離攻撃の飛距離の上限
    public float shotLange;        //遠距離攻撃の飛距離
    public float attackCooltime = 0.3f;//近距離攻撃クールタイム
    public float shotCooltime = 0.3f;  //遠距離攻撃クールタイム

    public float leftLimit = 1.0f;  //侵入できる左の限界
    public float rightLimit = 5.0f; //侵入できる右の限界
    public float upLimit = 20.0f;   //侵入できる上の限界

    bool onAttack = false;      //近距離攻撃フラグ
    bool onShot = false;        //遠距離攻撃フラグ
    bool onBottomColumn = true; //下列にいるかどうか
    private float time; //時間計測用

    Vector2 position; //プレイヤーの座標用
    public GameObject AttackEffect;  //近距離攻撃
    public GameObject ShotEffect;    //遠距離攻撃
    public GameObject ghostPrefab;   //残像用のプレハブ
    public BarrierManager barrierbar;//BarrierManagerスクリプト
    private GameObject dataInfo;     //DataInfoオブジェクト
    private PlayerStatusManager playerStatus;//PlayerStatusManagerスクリプト

    void Start()
    {
        //DataInfoオブジェクトのPlayerStatusManagerを取得
        dataInfo = GameObject.Find("DataInfo");
        playerStatus = dataInfo.GetComponent<PlayerStatusManager>();

        //プレイヤー座標の取得
        position = transform.position;
    }

    // Update is called once per frame

    void Update()
    {
        

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
        

        

        //近距離攻撃
        if (Input.GetKeyDown(KeyCode.Space) && !onAttack && !onShot)//攻撃開始時(Spaceキーを押すと攻撃開始)
        {
            //プレイヤーの前方に攻撃エフェクトのクローン生成
            Instantiate(AttackEffect, transform.position + transform.up, Quaternion.identity);

            //フラグ管理用コルーチン呼び出し
            StartCoroutine(AttackFlag());
        }

        //遠距離攻撃
        if (Input.GetKeyDown(KeyCode.LeftShift) && !onAttack && !onShot)//攻撃開始時(Spaceキーを押すと攻撃開始)
        {
            //現在いる列によって飛距離を変更
            if (onBottomColumn)
                shotLange = shotLimit;
            else
                shotLange = shotLimit - 1.0f;

            //フラグ管理用コルーチン呼び出し
            StartCoroutine(ShotFlag());

            //プレイヤーの位置に手裏剣のクローン生成
            Instantiate(ShotEffect, transform.position, Quaternion.identity);
        }

        //前方を調べる
        if (Input.GetKeyDown(KeyCode.E) &&
            !onAttack && !onShot)
        {
            Debug.Log("調べる");

            //上方を調べる
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1.0f);

            //調べた物が宝箱なら
            if (hit.collider.CompareTag("TreasureBox"))
            {
                Debug.Log("宝箱だ！");
                //宝箱のスクリプトを実行
            }
        }
    }

    private void FixedUpdate()
    {
        transform.position = position;  //座標の更新

        barrierbar.UpdateBarrier();
    }

    //敵などとの接触時のダメージ判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //接触タグが敵の攻撃か、敵本体ならHPを減らす
        if(collision.gameObject.tag == "EnemyAttack"|| collision.gameObject.tag == "Enemy")
        {
            //被ダメージ関数を呼び、falseが返ってきたなら ( HPが0以下でfalse )
            if (playerStatus.TakeDamage(takesDamage) == false)
            {
                //プレイヤーが倒れる
                PlayerDead();
            }                
        }
    }

    //近距離攻撃フラグ管理
    private IEnumerator AttackFlag()
    {
        onAttack = true;

        //待機
        yield return new WaitForSeconds(attackCooltime);

        onAttack = false;
    }
    //遠距離攻撃フラグ管理
    private IEnumerator ShotFlag()
    {
        onShot = true;

        //待機
        yield return new WaitForSeconds(shotCooltime);

        onShot = false;
    }

    //プレイヤーがやられたとき関数
    void PlayerDead()
    {
        Debug.Log("やられた");
        Destroy(gameObject, 0.4f);
    }

    //残像生成関数
    void CloneAfterimage()
    {
        //元のオブジェクトの位置に残像を生成
        GameObject ghost =
            Instantiate(ghostPrefab, transform.position, transform.rotation);
    }

}



