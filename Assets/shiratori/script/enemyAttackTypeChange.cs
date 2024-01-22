using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAttackTypeChange : MonoBehaviour
{
    EnemyAttack enemyattack;
    EnemyAntiHoming enemyantihoming;

    // Start is called before the first frame update
    void Start()
    {
        enemyattack = gameObject.GetComponent<EnemyAttack>();
        enemyantihoming = gameObject.GetComponent<EnemyAntiHoming>();
    }

    // Boss01 の攻撃切り替え
    public void Boss01AttackChange()
    {

        if (enemyattack.attackType == 0) 
        {
            if (enemyattack.time >= enemyattack.coolTime)
                StartCoroutine(ChangeAttackType());
        }
        if (enemyattack.attackType != 0) 
        {
            if (enemyattack.time >= enemyattack.coolTime)
                StartCoroutine(ChangeAttackType());
        }
    }

    // Boss03 の攻撃切り替え
    public void Boss03AttackChange()
    {      
            // Boss03　がプレイヤーの１ます上に居るなら
        if ( enemyattack.attackType != 0 && enemyantihoming.transform.position.y == enemyantihoming.playerObj.transform.position.y + 1 )
            enemyattack.attackType = 0;

            // Boss03　がプレイヤーの２ます上に居るなら
        if ( enemyattack.attackType != 1 && enemyantihoming.transform.position.y == enemyantihoming.playerObj.transform.position.y + 2 )
            enemyattack.attackType = 1;

    }

    private IEnumerator ChangeAttackType()
    {
        yield return new WaitForSeconds(0.001f);

        if (enemyattack.attackType == 0)
            enemyattack.attackType = 1;
        else
            enemyattack.attackType = 0;
    }
}
