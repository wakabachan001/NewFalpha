using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour
{
    [SerializeField] private Image BosshpBarcurrent;

    EnemyManager enemyManager;//PlayerManagerスクリプト
    GameObject objboss;

    //private float currentHealth;//現在の体力

    void Start()
    {
        
    }

    //体力更新関数
    public void FixedUpdate()
    {
        objboss = GameObject.FindWithTag("Boss");
        //PlayerManagerの読み込み
        enemyManager = objboss.GetComponent<EnemyManager>();

        //現在の体力の割合から見た目を変える
        BosshpBarcurrent.fillAmount = enemyManager.status.CurrentHP / enemyManager.maxHP;
    }
}
