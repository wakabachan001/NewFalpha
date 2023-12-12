using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioClip Clickbutton;
    public AudioClip Attack;
    public AudioClip Shot;
    public AudioClip Enemydeath;
    public AudioClip GameClear;
    public AudioClip GameOver;
    public AudioClip menuclose;
    public AudioClip buy;
    public AudioClip treasure_chest;
    public AudioClip warp;
    public AudioClip playerDamage;
    public AudioClip move;

    public AudioSource stage1;
    public AudioSource stage2;
    public AudioSource stage3;
    public AudioSource titleBGM;
    public AudioSource bossBGM;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void Stage1BGM()
    {
        //ステージ１以外を止めてステージ１を流す
        stage2.Stop();
        stage3.Stop();

        stage1.Play();

    }
    public void Stage2BGM()
    {
        //ステージ２以外を止めてステージ３を流す
        stage1.Stop();
        stage3.Stop();

        stage2.Play();

    }
    public void Stage3BGM()
    {
        //ステージ３以外を止めてステージ３を流す
        stage2.Stop();
        stage1.Stop();

        stage3.Play();

    }
    public void TitleBGM()
    {
        //タイトルBGMを流す
        titleBGM.Play();

    }
    public void BossBGM()
    {
        //ボスBGM以外を止めてボスBGMを流す
        stage1.Stop();
        stage2.Stop();
        stage3.Stop();

        bossBGM.Play();

    }

    public void StopBGM()
    {
        //すべてのBGMをストップ
        stage1.Stop();
        stage2.Stop();
        stage3.Stop();
        bossBGM.Stop();
        titleBGM.Stop();

    }


    public void ClickSE()
    {
            //クリックボタン
        audioSource.PlayOneShot(Clickbutton);

    }
    public void AttackSE()
    {
        //近距離攻撃
        audioSource.PlayOneShot(Attack);

    }
    public void ShotSE()
    {
        //遠距離攻撃
        audioSource.PlayOneShot(Shot);

    }
    public void EnemyDeathSE()
    {
        //敵死亡サウンド
        audioSource.PlayOneShot(Enemydeath);

    }
    public void GameClearSE()
    {
        //ゲームクリア
        audioSource.PlayOneShot(GameClear);

    }
    public void GameOverSE()
    {
        //ゲームオーバー
        audioSource.PlayOneShot(GameOver);

    }
    public void MenuCloseSE()
    {
        //メニュークローズボタン
        audioSource.PlayOneShot(menuclose);

    }
    public void BuySE()
    {
        //商人からお買い物ボタン
        audioSource.PlayOneShot(buy);

    }
    public void Treasure_ChestSE()
    {
        //宝箱開けた時の音
        audioSource.PlayOneShot(treasure_chest);

    }
    public void WarpSE()
    {
        //ワープポイントに入ったときの音
        audioSource.PlayOneShot(warp);

    }
    public void DamageSE()
    {
        //プレイヤーがダメージを受けた時の音
        audioSource.PlayOneShot(playerDamage);

    }
    public void MoveSE()
    {
        //プレイヤーが移動した時の音
        audioSource.PlayOneShot(move);

    }
}
