using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrierManager : MonoBehaviour
{
    [SerializeField] private Image BarrierBarcurrent;
    private float barrierMax = 100.0f; //バリアゲージの長さ
    [SerializeField] private float barrierDec = 3.0f;   //1秒間に減らすバリアゲージ

    [SerializeField]
    private float barrierCur; //現在のバリア
    private bool on75 = false;//バリアが75%以下になったか
    private bool on50 = false;//バリアが50%以下になったか
    private bool on25 = false;//バリアが25%以下になったか
    private bool on0 = false; //バリアが0以下になったか

    PlayerStatusManager playerStatusManager;

    private void Start()
    {
        //PlayerStatusManagerを取得
        playerStatusManager = LoadManagerScene.GetPlayerStatusManager();

        //現在のバリアの初期化
        barrierCur = barrierMax;     
    }
    private void FixedUpdate()
    {
        barrierCur -= barrierDec * 0.02f; //1秒でbarrierDec分更新

        //現在のバリアの割合分、ゲージを変える
        BarrierBarcurrent.fillAmount = barrierCur / barrierMax;

        //バリアが75%以下になったとき一度だけ
        if (barrierCur == 75.0f && !on75)
        {
            on75 = true;//フラグを立てて動かないようにする

            //Playerのバリア値の変化
            playerStatusManager.ChangeBarrier(2);

            Debug.Log("バリアが半分以下");
        }
        //バリアが半分以下になったとき一度だけ
        if (barrierCur == 50.0f && !on50)
        {
            on50 = true;//フラグを立てて動かないようにする

            //Playerのバリア値の変化
            playerStatusManager.ChangeBarrier(2);

            Debug.Log("バリアが半分以下");
        }
        //バリアが25%以下になったとき一度だけ
        if (barrierCur == 25.0f && !on25)
        {
            on25 = true;//フラグを立てて動かないようにする

            //Playerのバリア値の変化
            playerStatusManager.ChangeBarrier(2);

            Debug.Log("バリアが半分以下");
        }
        //バリアが0以下になったとき一度だけ
        if (barrierCur == 0.0f && !on0)
        {
            on0 = true;//フラグを立てて動かないようにする

            //Playerのバリア値の変化
            playerStatusManager.ChangeBarrier(4);

            Debug.Log("バリアが0以下");
        }
        //リセットフラグがオンになったとき
        if(playerStatusManager.onResetHpBr == true)
        {
            //バリアゲージの初期化
            barrierCur = barrierMax;
            //フラグの初期化
            on75 = false;
            on50 = false;
            on25 = false;
            on0  = false;

            //フラグを下げる
            playerStatusManager.onResetHpBr = false;
        }
    }
}
