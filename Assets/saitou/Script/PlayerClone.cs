using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Start is called before the first frame update
public class PlayerClone : MonoBehaviour
{
    public float speed = 1.0f;      //移動距離
    public float shotSpeed = 0.2f;  //手裏剣の速度
    public float takesDamage = 2.0f;  //被ダメージ

    public float effectLimit;       //近距離攻撃の判定が残る時間
    public float shotLimit = 3.5f;  //遠距離攻撃の飛距離の上限
    public float shotLange;        //遠距離攻撃の飛距離
    public float attackCooltime = 0.3f;//近距離攻撃クールタイム
    public float shotCooltime = 0.3f;  //遠距離攻撃クールタイム

    public float upLimit = 21.0f;   //侵入できる上の限界
    public float backLimitArea = 19.0f; //後退に制限をつける範囲(以下) CreateMapで適宜更新

    bool onAttack = false;          //近距離攻撃フラグ
    bool onShot = false;            //遠距離攻撃フラグ
    bool onBottomColumn = false;    //下列にいるかどうか


    private float time; //時間計測用

    Vector2 position; //プレイヤーの座標用
    public GameObject AttackEffect;  //近距離攻撃
    public GameObject ShotEffect;    //遠距離攻撃

    PlayerManager playerManager;

    void Start()
    {
        GameObject obj = GameObject.Find("Player");
        playerManager = obj.GetComponent<PlayerManager>();

        //プレイヤー座標の取得
        position = transform.position;
    }

    // Update is called once per frame

    void Update()
    {
        //移動不可フラグを調べる
        if (playerManager.dontMove == false)
        {
            //移動(場外にいかないようにする)
            if (Input.GetKeyDown("left") ||
                Input.GetKeyDown(KeyCode.A))
            {

                position.x -= speed;
            }
            if (Input.GetKeyDown("right") ||
                Input.GetKeyDown(KeyCode.D))
            {

                position.x += speed;
            }
            if ((Input.GetKeyDown("up") ||
                Input.GetKeyDown(KeyCode.W)) &&
                position.y < upLimit)
            {

                position.y += speed;

                onBottomColumn = false;
            }
            if ((Input.GetKeyDown("down") ||
                Input.GetKeyDown(KeyCode.S)) &&
                !onBottomColumn)
            {
                position.y -= speed;
                //ボスエリアより手前なら
                if (transform.position.y <= backLimitArea)
                    onBottomColumn = true;  //後退時に下列にいることにする
            }
        }


        //近距離攻撃
        //if (Input.GetKeyDown(KeyCode.Space) && !onAttack && !onShot)//攻撃開始時(Spaceキーを押すと攻撃開始)
        if (Input.GetMouseButtonDown(1))//右クリックが押されたとき

        {
            //プレイヤーの前方に攻撃エフェクトのクローン生成
            Instantiate(AttackEffect, transform.position + transform.up, Quaternion.identity);

            //フラグ管理用コルーチン呼び出し
            StartCoroutine(AttackFlag());
        }

        //遠距離攻撃
        //if (Input.GetKeyDown(KeyCode.LeftShift) && !onAttack && !onShot)//攻撃開始時(Spaceキーを押すと攻撃開始)
        if (Input.GetMouseButtonDown(0))//左クリックが押されたとき
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
    }

    private void FixedUpdate()
    {
        transform.position = position;  //座標の更新
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
}