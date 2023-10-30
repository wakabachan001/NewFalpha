using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float enemyHP;       //敵オブジェクトHP

    

    //他collider接触時
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerManager playermanager;
        GameObject obj = GameObject.Find("Player");
        playermanager = obj.GetComponent<PlayerManager>();

        Debug.Log("OnTriggerEnter2D: " + other.gameObject.name);

        //剣との接触
        if(other.gameObject.tag == "Sword")
        {
            Debug.Log("剣のダメージ");
            enemyHP -= playermanager.SwordDamage; //HPを剣ダメージ分減らす
        }
        //手裏剣との接触
        if (other.gameObject.tag == "Syuriken")
        {
            Debug.Log("手裏剣のダメージ");
            enemyHP -= playermanager.SyurikenDamage; //HPを手裏剣ダメージ分減らす
        }
        //倒れるか調べる
        EnemyDead();
    }

    //倒れるか調べる関数
    void EnemyDead()
    {
        //敵HPが0以下なら、このオブジェクトを消す
        if (enemyHP <= 0.0f)
        {
            Destroy(gameObject);
            Debug.Log("敵が倒れた");
        }
    }
}