using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class BossManager : MonoBehaviour
{
    public float enemyHP;       //敵オブジェクトHP

    //public GameObject ClearText;//クリアテキスト

    //他collider接触時
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerManager playermanager;
        GameObject obj = GameObject.Find("Player");
        playermanager = obj.GetComponent<PlayerManager>();

        Debug.Log("OnTriggerEnter2D: " + other.gameObject.name);

        //剣との接触
        if (other.gameObject.tag == "Sword")
        {
            Debug.Log("剣のダメージ");
            enemyHP -= playermanager.swordDamage; //HPを剣ダメージ分減らす
        }
        //手裏剣との接触
        if (other.gameObject.tag == "Syuriken")
        {
            Debug.Log("手裏剣のダメージ");
            enemyHP -= playermanager.syurikenDamage; //HPを手裏剣ダメージ分減らす
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
            GameObject obj = GameObject.Find("ClearText");
            obj.SetActive(true);

            gameObject.SetActive(true);
            //Destroy(gameObject);
            Debug.Log("ボスが倒れた");
        }
    }
}
