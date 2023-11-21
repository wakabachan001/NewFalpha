using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy02AI : MonoBehaviour
{
    public float movelength = 0.02f;
    float hanpuku = -1.0f;

    //bool onAttack = false;      //近距離攻撃フラグ
    //private float time; //時間計測用
    //public GameObject EnemyAttackEffect; //近距離攻撃
    //public float EffectLimit;       //近距離攻撃の判定が残る時間

    GameObject cameraObj;
    cameraMove camera;
    Transform cameraTransform;

    //GameObject playerObj;
    //PlayerManager player;
    //Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
        camera = cameraObj.GetComponent<cameraMove>();
        cameraTransform = cameraObj.transform;

        //playerObj = GameObject.FindGameObjectWithTag("Player");
        //player = playerObj.GetComponent<PlayerManager>();
        //playerTransform = playerObj.transform;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Vector2 position = transform.position;


        if (cameraTransform.position.y + 2 >= position.y && cameraTransform.position.y - 2.5 <= position.y)
        {

            //　敵０２を左右に移動させる----------------------------------------------------------------
            transform.position = new Vector2(position.x += movelength, position.y);

            if (transform.position.x > 4)
            {
                movelength *= hanpuku;
            }
            if (transform.position.x < 0)
            {
                movelength *= hanpuku;
            }
            // -----------------------------------------------------------------------------------------

            //if (playerTransform.position.x < transform.position.x + 0.5 &&
            //    playerTransform.position.x > transform.position.x - 0.5 &&
            //    playerTransform.position.y < transform.position.y - 1.0 && !onAttack)
            //{
            //    Debug.Log("敵が攻撃");

            //    //攻撃エフェクトの有効化
            //    EnemyAttackEffect.gameObject.SetActive(true);

            //    //プレイヤーの１マス前に攻撃エフェクトを移動
            //    EnemyAttackEffect.transform.position = this.transform.position + transform.up * -1.0f;

            //    time = 0.0f;        //時間のリセット
            //    onAttack = true;    //攻撃フラグオン

            //}

            ////近距離攻撃処理
            //if (onAttack)
            //{
            //    //１秒で1.0f増やす
            //    time += 0.02f;

            //    //timeが指定した時間以上になると
            //    if (time >= EffectLimit)
            //    {
            //        //オブジェクトの無効化
            //        EnemyAttackEffect.gameObject.SetActive(false);
            //        //攻撃フラグを下げる
            //        onAttack = false;
            //    }
            //}

        }
    }
}
