using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // Start is called before the first frame update
    public class PlayerManager : MonoBehaviour
    {
    public float swordDamage;
    public float syurikenDamage;//後で削除（BossManagerをいじる）

    public float speed = 1.0f;       //移動距離
    public float shotSpeed = 0.2f;   //手裏剣の速度
    public float takesDamage = 2.0f; //被ダメージ
    
    public float effectLimit;       //近距離攻撃の判定が残る時間
    public float shotLimit = 3.5f;  //遠距離攻撃の飛距離の上限
    public float shotLange;         //遠距離攻撃の飛距離
    public float attackCooltime = 0.3f;//近距離攻撃クールタイム
    public float shotCooltime = 0.3f;  //遠距離攻撃クールタイム

    public float leftLimit = 0.0f;  //侵入できる左の限界
    public float rightLimit = 4.0f; //侵入できる右の限界
    public float upLimit = 21.0f;   //侵入できる上の限界
    public float backLimitArea = 19.0f; //後退に制限をつける範囲(以下) CreateMapで適宜更新
    public float invincibleTime = 0.5f; //無敵時間

    public bool dontMove = false;    //移動できなくするフラグ
    private string lastMove = "down";//最後に動いた方向
    bool onAttack = false;           //近距離攻撃フラグ
    bool onShot = false;             //遠距離攻撃フラグ
    bool onBottomColumn = false;     //下列にいるかどうか
    bool invincible = false;         //無敵フラグ

    private float time; //時間計測用

    //色
    Color mainColor   = new Color(1f, 1f, 1f, 1f);     //通常時
    Color damageColor = new Color(1f, 0.6f, 0.6f, 1f); //被ダメージ時
    Color inviColor   = new Color(1f, 1f, 1f, 0.5f);   //無敵時

    Vector2 position; //プレイヤーの座標用
    public GameObject AttackEffect;  //近距離攻撃
    public GameObject ShotEffect;    //遠距離攻撃
    public GameObject ghostPrefab;   //残像用のプレハブ
    private GameObject camera;       //Main Cameraオブジェクト

    private PlayerStatusManager playerStatus;//PlayerStatusManagerスクリプト
    private TresureBoxManager tresureBox;    //TresureBoxManagerスクリプト
    private TraderManager traderManager;     //TraderManagerスクリプト
    private SceneChange sceneChange;         //SceneChangeスクリプト
    private Sounds sounds;

    void Start()
    {
        playerStatus = LoadManagerScene.GetPlayerStatusManager();

        camera = GameObject.Find("Main Camera"); //カメラの取得

        GameObject obj = GameObject.Find("SoundObject");
        sounds = obj.GetComponent<Sounds>();

        //プレイヤー座標の取得
        position = transform.position;
    }

    // Update is called once per frame

    void Update()
    {
        //移動不可フラグを調べる
        if (dontMove == false)
        {
            //移動(場外にいかないようにする)
            if ((Input.GetKeyDown("left") ||
                Input.GetKeyDown(KeyCode.A)) &&
                position.x > leftLimit)
            {
                CloneAfterimage();
                position.x -= speed;
                lastMove = "left";
            }
            if ((Input.GetKeyDown("right") ||
                Input.GetKeyDown(KeyCode.D)) &&
                position.x < rightLimit)
            {
                CloneAfterimage();
                position.x += speed;
                lastMove = "right";
            }
            if ((Input.GetKeyDown("up") ||
                Input.GetKeyDown(KeyCode.W)) &&
                position.y < upLimit)
            {
                CloneAfterimage();
                position.y += speed;
                lastMove = "up";

                //カメラの移動もこっちでする
                if (onBottomColumn == false && transform.position.y < backLimitArea)
                {
                    MoveCamera();//カメラの座標更新
                }
                onBottomColumn = false;
            }
            if ((Input.GetKeyDown("down") ||
                Input.GetKeyDown(KeyCode.S)) &&
                !onBottomColumn)
            {
                CloneAfterimage();
                position.y -= speed;
                lastMove = "down";
                //ボスエリアより手前なら
                if (transform.position.y <= backLimitArea)
                    onBottomColumn = true;  //後退時に下列にいることにする
            }
        }

        //フラグチェック
        if (!dontMove && !onAttack && !onShot)
        {

            //近距離攻撃
            //if (Input.GetKeyDown(KeyCode.Space))//攻撃開始時(Spaceキーを押すと攻撃開始)
            if(Input.GetMouseButtonDown(1))//右クリックが押されたとき
            {
                //プレイヤーの前方に攻撃エフェクトのクローン生成
                Instantiate(AttackEffect, transform.position + transform.up, Quaternion.identity);

                //フラグ管理用コルーチン呼び出し
                StartCoroutine(AttackFlag());
            }

            //遠距離攻撃
            //if (Input.GetKeyDown(KeyCode.LeftShift))//攻撃開始時(Spaceキーを押すと攻撃開始)
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

            //前方を調べる
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.DrawRay(transform.position + (transform.up * 0.5f), transform.up * 0.8f, Color.green, 0.5f);

                Debug.Log("調べる");

                //プレイヤーの上から、上方を調べる
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up * 1.5f, 0.8f);

                if (hit.collider != null)
                {
                    if (hit.collider)
                    {
                        Debug.Log(hit.collider.gameObject.name);
                    }

                    //調べた物が宝箱なら
                    if (hit.collider.CompareTag("TreasureBox"))
                    {
                        Debug.Log("宝箱だ");

                        //スクリプトを取得
                        tresureBox = hit.collider.gameObject.GetComponent<TresureBoxManager>();

                        tresureBox.OpenBox();//宝箱のスクリプトを実行
                    }
                    //調べた物が商人なら
                    else if (hit.collider.CompareTag("Trader"))
                    {
                        Debug.Log("商人だ");

                        //スクリプトを取得
                        traderManager = hit.collider.gameObject.GetComponent<TraderManager>();

                        traderManager.OpenShop();//商人のスクリプトを実行
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        transform.position = position;  //座標の更新
    }

    //敵などとの接触時のダメージ判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //無敵時間でなければ
        if (invincible == false)
        {
            //接触タグが敵の攻撃なら
            if (collision.gameObject.tag == "EnemyAttack")
            {
                sounds.DamageSE();//SE 被ダメージ
                StartCoroutine( DamageEfect());//被ダメージエフェクト

                takesDamage = collision.GetComponent<EffectData>().damage;//当たった攻撃からダメージを取得

                //被ダメージ関数を呼び、falseが返ってきたなら ( HPが0以下でfalse )
                if (playerStatus.TakeDamage(takesDamage) == false)
                {
                    //プレイヤーが倒れる
                    PlayerDead();
                }

            }
            //接触タグが敵本体なら
            if (collision.gameObject.tag == "Enemy")
            {
                sounds.DamageSE();//SE 被ダメージ

                takesDamage = 10f;//ぶつかったときのダメージは固定

                //被ダメージ関数を呼び、falseが返ってきたなら ( HPが0以下でfalse )
                if (playerStatus.TakeDamage(takesDamage) == false)
                {
                    //プレイヤーが倒れる
                    PlayerDead();
                }
                StartCoroutine( Knockback());
                //無敵時間
                StartCoroutine(OnInvincible(invincibleTime));
            }
        }
    }

    //近距離攻撃フラグ管理
    private IEnumerator AttackFlag()
    {
        onAttack = true;

        //SE 近距離攻撃
        sounds.AttackSE();
        

        //待機
        yield return new WaitForSeconds(attackCooltime);

        onAttack = false;
    }
    //遠距離攻撃フラグ管理
    private IEnumerator ShotFlag()
    {
        onShot = true;

        //SE 遠距離攻撃
        sounds.ShotSE();

        //待機
        yield return new WaitForSeconds(shotCooltime);

        onShot = false;
    }

    //プレイヤーがやられたとき関数
    void PlayerDead()
    {
        Debug.Log("やられた");

        sounds.GameOverSE();//SE ゲームオーバー

        Destroy(gameObject, 0.4f);

        //SeneChangeスクリプトを探し、ゲームオーバーシーンに移行
        sceneChange = camera.GetComponent<SceneChange>();
        sceneChange.GameOver();
    }

    //残像生成関数
    void CloneAfterimage()
    {
        //元のオブジェクトの位置に残像を生成
        GameObject ghost =
            Instantiate(ghostPrefab, transform.position, transform.rotation);

        sounds.MoveSE();//SE 移動
    }

    public void ResetPos(Vector2 pos)
    {
        position = pos;
    }
    //プレイヤーのけぞり関数
    public IEnumerator Knockback()
    {
        Debug.Log("のけぞり");

        dontMove = true;

        //色を変更
        gameObject.GetComponent<SpriteRenderer>().color = inviColor;

        //SE 被ダメージ

        yield return new WaitForSeconds(0.1f);

        //最後に移動した方向と逆方向のベクトルを指定
        switch (lastMove)
        {
            case "left":
                position += Vector2.right;
                break;
            case "right":
                position += Vector2.left;
                break;
            case "up":
                position += Vector2.down;
                onBottomColumn = true;
                break;
            case "down":
                position += Vector2.up;
                onBottomColumn = false;
                break;
            default:
                Debug.Log("!最終移動方向が見つかりません。");
                break;
        }
        //連続でのけぞった場合、ステージ外に出そうなので中身を消す
        lastMove = null;

        transform.position = position;  //座標の更新   

        yield return new WaitForSeconds(0.3f);

        dontMove = false;
        
    }
    //一定時間無敵コルーチン 引数秒無敵
    public IEnumerator OnInvincible(float sec)
    {
        invincible = true;  //無敵になる
        
        //色変更
        //gameObject.GetComponent<SpriteRenderer>().color = inviColor;       

        yield return new WaitForSeconds(sec);

        invincible = false; //無敵解除
        //色を戻す
        gameObject.GetComponent<SpriteRenderer>().color = mainColor;
    }
    //カメラの座標更新
    public void MoveCamera()
    {
        camera.transform.position = new Vector3(2, transform.position.y + 1.6f, -10);
    }
    //被ダメージエフェクト
    public IEnumerator DamageEfect()
    {
        //色変更
        gameObject.GetComponent<SpriteRenderer>().color = damageColor;

        //SE　被ダメージ

        yield return new WaitForSeconds(0.1f);

        //色を戻す
        gameObject.GetComponent<SpriteRenderer>().color = mainColor;
    }
}



