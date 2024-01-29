using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
shiratori
    [SerializeField] public float maxHP = 100.0f;       //�ő�̗�
    [SerializeField] private int money = 100;            //���Ƃ�����
    [SerializeField] private float attackDamage1 = 10.0f;//�U��1�̃_���[�W
    [SerializeField] private float attackDamage2 = 10.0f;//�U��2�̃_���[�W
    main

    public Color mainColor = new Color(1f, 1f, 1f, 1f);       //通常時
    public Color damageColor = new Color(1f, 0.6f, 0.6f, 1f); //被ダメージ時

    private float takeDamage;   //被ダメージ

    public StatusData status;    //敵ステータスクラス
    private StatusCalc statusCalc = new StatusCalc();  //ダメージ計算クラス
    Sounds sounds;

    PlayerStatusManager playerStatusManager;//PlayerStatusManagerスクリプト

    DamageTXTmanager dtxtm;//ダメージテキストマネージャースクリプト格納用
    float x, y;

    void Start()
    {

        //Debug.Log("敵初期化");

        dtxtm = FindObjectOfType<DamageTXTmanager>();// dtxtmにスクリプトを保管



        //DataInfoのPlayerStatusManagerを取得
        playerStatusManager = LoadManagerScene.GetPlayerStatusManager();

        GameObject soun = GameObject.Find("SoundObject");
        sounds = soun.GetComponent<Sounds>();

        //ステータス初期化
        status = new StatusData(maxHP, money, attackDamage1, attackDamage2);

        //Debug.Log("敵初期化完了");
    }

    //他collider接触時
    void OnTriggerEnter2D(Collider2D other)
    {
        //剣との接触
        if(other.gameObject.tag == "Sword")
        {
            //プレイヤーの近距離攻撃ダメージを調べる
            takeDamage = playerStatusManager.AttackDamageCalc();

            // takeDamageをint型に変換して値を渡す
            dtxtm.TakeDamage((int)takeDamage, transform.position.x, transform.position.y + 0.5f);

            //HP計算
            status.CurrentHP = statusCalc.HPCalc(status.CurrentHP, takeDamage);

            StartCoroutine( DamageEfect());

            Debug.Log("剣のダメージ : " + takeDamage);
        }
        //手裏剣との接触
        if (other.gameObject.tag == "Syuriken")
        {
            //プレイヤーの遠距離攻撃ダメージを調べる
            takeDamage = playerStatusManager.ShotDamageCalc();

            // takeDamageをint型に変換して値を渡す
            dtxtm.TakeDamage((int)takeDamage, transform.position.x, transform.position.y + 0.5f) ;

            //HP計算
            status.CurrentHP = statusCalc.HPCalc(status.CurrentHP, takeDamage);

            StartCoroutine(DamageEfect());

            Debug.Log("手裏剣のダメージ : " + takeDamage);
        }
        if (other.gameObject.tag == "DeleteArea")
        {
            Destroy(gameObject);
        }
        //倒れるか調べる
        EnemyDead();
    }

    //倒れるか調べる関数
    void EnemyDead()
    {
        //敵HPが0以下なら、このオブジェクトを消す
        if (status.CurrentHP <= 0.0f)
        {
            //プレイヤーの所持金を増やす
            playerStatusManager.GettingMoney(status.Money);

            if(playerStatusManager.onHealthTreat == true)
            {
                playerStatusManager.HT();
            }
            sounds.EnemyDeathSE();//SE 敵が倒れた

            //タグがBossなら
            if (gameObject.tag == "Boss")
            {
                //CreateMapスクリプトを探す
                CreateMap createMap;
                GameObject obj = GameObject.Find("Main Camera");
                createMap = obj.GetComponent<CreateMap>();

                //ボスが倒されたとき関数
                createMap.BossDead();
            }

            Destroy(gameObject);
            Debug.Log("敵が倒れた");
        }
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