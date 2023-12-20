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

    public float upLimit = 21.0f;       //侵入できる上の限界
    public float backLimitArea = 19.0f; //後退に制限をつける範囲(以下) CreateMapで適宜更新

    public Vector3 clonePos; //クローン元から見てどの位置か

    bool onAttack = false;          //近距離攻撃フラグ
    bool onShot = false;            //遠距離攻撃フラグ
    bool onBottomColumn = false;    //下列にいるかどうか


    private float time; //時間計測用

    public GameObject AttackEffect;  //近距離攻撃
    public GameObject ShotEffect;    //遠距離攻撃
    GameObject baseObj; //クローン元の親オブジェクト

    PlayerManager playerManager;

    void Start()
    {
        GameObject obj = GameObject.Find("Player");
        playerManager = obj.GetComponent<PlayerManager>();


        //親オブジェクトの取得
        baseObj = transform.parent.gameObject;
    }

    // Update is called once per frame

    void Update()
    {
        //フラグチェック
        if (!onAttack && !onShot)
        {


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
    }

    private void FixedUpdate()
    {
        gameObject.transform.position = baseObj.transform.position + clonePos;
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