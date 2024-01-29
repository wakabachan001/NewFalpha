using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpUI : MonoBehaviour
{
    public Text HP;

    GameObject Boss;
    EnemyManager enemyManager;

    //CreateMapスクリプトを探す
    CreateMap createMap;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = GameObject.Find("Main Camera");
        createMap = obj.GetComponent<CreateMap>();
    }

    // Update is called once per frame
    void Update()
    {

        if (createMap.isMakeBoss == true) 
        {
            Boss = GameObject.FindWithTag("Boss");
            enemyManager = Boss.GetComponent<EnemyManager>();
        }

        //最大体力と現在の体力を表示
        HP.text = "HP " + (int)enemyManager.status.CurrentHP + " / " + (int)enemyManager.maxHP;
    }
}
