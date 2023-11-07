using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarrierManager : MonoBehaviour
{
    [SerializeField] private Image BarrierBarcurrent;

    public float barrierMax = 100.0f; //バリアの最大
    public float barrierDec = 2.0f;   //1秒間に減らすバリアゲージ
    [SerializeField]
    private float barrierCur; //現在のバリア
    private bool halfOn = false;//バリアが半分以下になったか
    private bool zeroOn = false;//バリアが0以下になったか

    GameObject playerObj;
    PlayerManager playermanager;

    private void Awake()
    {
        //PlayerManagerを探す
        playerObj = GameObject.Find("Player");
        playermanager = playerObj.GetComponent<PlayerManager>();

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
            playermanager.barrierCur = 1.5f;

            Debug.Log("バリアが半分以下");
        }
        //バリアが0以下になったとき一度だけ
        if (barrierCur / barrierMax < 0.0f && !zeroOn)
        {
            zeroOn = true;//フラグを立てて動かないようにする

            //Playerのバリア値の変化
            playermanager.barrierCur = 0.5f;

            Debug.Log("バリアが0以下");
        }
    }
}
