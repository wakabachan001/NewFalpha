using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shitakara : MonoBehaviour
{
    GameObject playerObj;
    public GameObject enemy; // 敵のゲームオブジェクト
    public GameObject attackEffect; // 攻撃エフェクト
    public float attackRange = 1.5f;
    public float attackCooldown = 2.5f; // 攻撃のクールダウン時間
    public float timeSinceLastAttack = 0f; // 最後に攻撃した時間

    Vector3 playerCenterPosition;


    private void Start()
    {
        playerObj = GameObject.Find("player");
        
    }
    void Update()
    {
        // 時間の経過を追跡
        timeSinceLastAttack += Time.deltaTime ;

        //クールダウン時間が経過したら攻撃
        if ( timeSinceLastAttack >= attackCooldown)
        {
            Attack();
            timeSinceLastAttack = 0f; // 攻撃したのでクールダウンをリセット
        }
    }

    void Attack()
    {
        if (enemy != null)
        {
            // プレイヤーの中心位置を取得
            playerCenterPosition = playerObj.transform.position;
           //Invoke("sisyou", 0.5f);
            // 攻撃エフェクトを生成
            if (attackEffect != null)
            {
                Instantiate(attackEffect, playerCenterPosition, Quaternion.identity);
                // ここに攻撃エフェクトの発生音などを追加する

                // 攻撃範囲内の敵にダメージを与える処理などをここに追加する
            }
        }
    }

    void sisyou()
    {
        // 攻撃範囲内かどうかを判定
        if (Vector3.Distance(playerObj.transform.position, playerCenterPosition) <= attackRange)
        {
            // 攻撃エフェクトを生成
            if (attackEffect != null)
            {
                Instantiate(attackEffect, playerCenterPosition, Quaternion.identity);
                // ここに攻撃エフェクトの発生音などを追加する

                // 攻撃範囲内の敵にダメージを与える処理などをここに追加する
            }
        }
    }
   
}
