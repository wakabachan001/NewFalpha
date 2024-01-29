using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LongBullet : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject bullet;
    float speed;
    float bullet_count = 0;
    public float lifeTime = 1.0f;
    public float Ra    = 20; //Remaining_ammunition
    public float resetbullet = 20; 

    public Text ammoText;


    void Start()
    {
        speed = 10.0f;
        ammoText.text = "残弾:20";
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && bullet_count < Ra)
        {


            //弾（ゲームオブジェクト）の作成
            GameObject clone = Instantiate(bullet, transform.position, Quaternion.identity);

            //クリックした座標の取得（スクリーン座標からワールド座標に変換）
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //向きの生成（Z成分の除去と正規化）
            Vector3 shotForward = Vector3.Scale(mouseWorldPos - transform.position, new Vector3(1, 1, 0)).normalized;

            //弾に速度を与える
            clone.GetComponent<Rigidbody2D>().velocity = shotForward * speed;

            bullet_count++;

            UpdateAmmoText();

            Destroy(clone, lifeTime);


        }


    }

    void UpdateAmmoText()
    {
        //残弾を減らす処理
        ammoText.text = "残弾:" + (Ra - bullet_count);
    }

    void ResetBullet()
    {
      
        resetbullet = Ra;

        bullet_count = 0;

    }
}

