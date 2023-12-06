using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class BossManager : MonoBehaviour
{
    [SerializeField] private float maxHP = 100.0f;       //最大体力
    [SerializeField] private int money = 100;            //落とすお金
    [SerializeField] private float attackDamage1 = 10.0f;//攻撃1のダメージ
    [SerializeField] private float attackDamage2 = 10.0f;//攻撃2のダメージ

    public Color mainColor = new Color(1f, 1f, 1f, 1f);     //通常時
    public Color damageColor = new Color(1f, 0.6f, 0.6f, 1f); //被ダメージ時

    private float takeDamage;   //被ダメージ

    private StatusData status;    //敵ステータスクラス
    private StatusCalc statusCalc = new StatusCalc();     //ダメージ計算クラス
    Sounds sounds;

    PlayerStatusManager playerStatusManager;//PlayerStatusManagerスクリプト
    GameObject obj;//DataInfo用

    void Start()
    {
        Debug.Log("ボス初期化");
        //PlayerStatusManagerの取得
        playerStatusManager = LoadManagerScene.GetPlayerStatusManager();

        GameObject soun = GameObject.Find("SoundObject");
        sounds = soun.GetComponent<Sounds>();

        //ステータス初期化
        status = new StatusData(maxHP, money, attackDamage1, attackDamage2);

        Debug.Log("ボス初期化完了");

        //mainColor = gameObject.GetComponent<Color>();
    }

    //他collider接触時
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D: " + other.gameObject.name);

        //剣との接触
        if (other.gameObject.tag == "Sword")
        {
            //プレイヤーの近距離攻撃ダメージを調べる
            takeDamage = playerStatusManager.AttackDamageCalc();

            //HP計算
            status.CurrentHP = statusCalc.HPCalc(status.CurrentHP, takeDamage);

            StartCoroutine(DamageEfect());

            Debug.Log("剣のダメージ : " + takeDamage);
        }
        //手裏剣との接触
        if (other.gameObject.tag == "Syuriken")
        {
            //プレイヤーの遠距離攻撃ダメージを調べる
            takeDamage = playerStatusManager.AttackDamageCalc();

            //HP計算
            status.CurrentHP = statusCalc.HPCalc(status.CurrentHP, takeDamage);

            StartCoroutine(DamageEfect());

            Debug.Log("手裏剣のダメージ : " + takeDamage);
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
            //CreateMapスクリプトを探す
            CreateMap createMap;
            GameObject obj = GameObject.Find("Main Camera");
            createMap = obj.GetComponent<CreateMap>();

            //ボスが倒されたとき関数
            createMap.BossDead();

            sounds.EnemyDeathSE();//SE 敵が倒れた

            //gameObject.SetActive(true);
            Destroy(gameObject);
            Debug.Log("ボスが倒れた");

                ////　ボスHPが０になると次のステージに行くためのマスが表示される
                //if (BossHP <= 0)
                //{
                //    Instantiate(NextStageTiles, new Vector2(5, 20), Quaternion.identity);
                //}
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
