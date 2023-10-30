using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    public float EffectTime = 0.2f;//攻撃エフェクトが残る時間

    private float time = 0;//時間計測用

    private void FixedUpdate()
    {
        //１秒で1.0f増やす
        time += 0.02f;

        //timeが指定した時間以上になると
        if (time >= EffectTime)
        {
            Destroy(gameObject);//このオブジェクトを削除
        }
    }
}
