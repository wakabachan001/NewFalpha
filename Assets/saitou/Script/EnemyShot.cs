using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public float EffectTime = 0.2f;//攻撃エフェクトが残る時間

    public float speed = -0.1f; //手裏剣の速度
    public float lange = -3.0f;  //飛距離

    private float time = 0;//時間計測用

    private void FixedUpdate()
    {
        //座標更新
        transform.position += (transform.up * speed);

        //飛距離と消える時間が同じになるようにする
        time += speed;

        //timeが飛距離上になると
        if (time <= lange)
        {
            Destroy(gameObject);//このオブジェクトを削除
        }
    }
}
