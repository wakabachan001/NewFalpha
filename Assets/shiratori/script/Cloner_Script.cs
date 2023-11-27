using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloner_Script : MonoBehaviour
{
    public GameObject enemyPrefab; // 分身する敵のプレハブ
    public int numberOfClones = 3; // 分身する敵の数
    public float cloneDistance = 1.5f; // 分身する敵の間隔

    public float clonepos = -1.0f;//クローン生成位置調整用
    public float coolTime = 2.0f;//攻撃のクールタイム
    private float time = 0.0f;//時間計測用
    private bool moveOn = true;//行動可能フラグ
    public GameObject AttackEffect;//クローンするオブジェクト

    GameObject ClonerforClone;
    EnemyAttack attack = new EnemyAttack();

    void Start()
    {
        SpawnClones();
        ClonerforClone= GameObject.FindGameObjectWithTag("Cloner for clone");
        attack=ClonerforClone.GetComponent<EnemyAttack>();

        attack.moveOn = true;
    }

    private void FixedUpdate()
    {
       
    }

    void SpawnClones()
    {
        for (int i = 0; i < numberOfClones; i++)
        {
            Vector2 spawnPosition = new Vector2(transform.position.x + i * cloneDistance -1, transform.position.y);
            GameObject clone = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            clone.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f); // サイズを2倍に変更する例
             // クローンに対する追加の設定などを行う場合は、ここで行います
        }
    }
}
