using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyurikenManager : MonoBehaviour
{
    public float EffectTime = 0.2f;//攻撃エフェクトが残る時間

    private float speed;  //手裏剣の速度
    private float lange;  //飛距離

    private float time = 0;//時間計測用



    private void Start()
    {
        //PlayerManagerを取得
        PlayerManager playermanager;
        GameObject obj = GameObject.Find("Player");
        playermanager = obj.GetComponent<PlayerManager>();

        //lange,speedをplayerから取得
        lange = playermanager.ShotLange;
        speed = playermanager.shotSpeed;
    }

    private void FixedUpdate()
    {
        //座標更新
        transform.position += (transform.up * speed);

        //飛距離と消える時間が同じになるようにする
        time += speed;

        //timeが飛距離上になると
        if (time >= lange)
        {
            Destroy(gameObject);//このオブジェクトを削除
        }
    }
}
