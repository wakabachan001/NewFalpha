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
    private bool halfOn = false;//バリアが半分以下になったか
    private bool zeroOn = false;//バリアが0以下になったか

    GameObject dataInfo;
    PlayerStatusManager playerStatusManager;

    private void Awake()
    {
        //PlayerManagerを探す
        dataInfo = GameObject.Find("DataInfo");
        playerStatusManager = dataInfo.GetComponent<PlayerStatusManager>();

        //現在のバリアの初期化
        barrierCur = barrierMax;
        
    }
    public void UpdateBarrier()
    {
        barrierCur -= barrierDec * 0.02f; //1秒でbarrierDec分更新

        //現在のバリアの割合分、ゲージを変える
        BarrierBarcurrent.fillAmount = barrierCur / barrierMax;

        //バリアが半分以下になったとき一度だけ
        if(barrierCur / barrierMax < 0.5f && !halfOn)
        {
            halfOn = true;//フラグを立てて動かないようにする

            //Playerのバリア値の変化
            playerStatusManager.CurrentBarrier = 1.5f;

            Debug.Log("バリアが半分以下");
        }
        //バリアが0以下になったとき一度だけ
        if (barrierCur / barrierMax < 0.0f && !zeroOn)
        {
            zeroOn = true;//フラグを立てて動かないようにする

            //Playerのバリア値の変化
            playerStatusManager.CurrentBarrier = 0.5f;

            Debug.Log("バリアが0以下");
        }
    }
}
