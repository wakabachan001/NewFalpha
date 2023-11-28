using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAntiHoming : MonoBehaviour
{
    GameObject playerObj;
    PlayerManager player;
    Transform playerTransform;

    private Camera mainCamera;
    public float speed = 1f;

    private bool moveOn = false;//行動可能フラグ
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //アクティブエリア内に入ったら
        if (collision.gameObject.tag == "ActiveArea")
        {
            moveOn = true;//行動可能フラグをオン
            Debug.Log("行動可能");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<PlayerManager>();
        playerTransform = playerObj.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 position = transform.position;

        if (moveOn)
        {
            if (position.y <= 20)// Y軸　移動制限
            {
                //  距離を一定に保つと動かない
                if(transform.position.y - playerTransform.position.y == 2) { ; }
                //　奥に逃げる
                else if (transform.position.y - playerTransform.position.y <= 2)
                {
                    position.y += speed; 
                    if (position.y > 20) position.y -= speed;
                }
                //　離れすぎると近づいてくる
                else if (transform.position.y - playerTransform.position.y >= 2)
                {
                    position.y -= speed;
                }
            }
      
            if (position.x > 0 && position.x < 6) // X軸　移動制限
            {
                //　右に逃げる
                if (playerTransform.position.x==transform.position.x -1)
                {
                    position.x += speed;
                    if (position.x > 5) position.x -= speed;
                }
                // 左に逃げる
                if (playerTransform.position.x == transform.position.x + 1)
                {
                    position.x -= speed;
                    if (position.x < 1) position.x += speed;
                }

                // X座標が同じとき左右のどっちかに逃げる
                if (transform.position.x == playerTransform.position.x) 
                {
                    int randomNumber = Random.Range(0, 2);  //ランダムな値を生成

                    if (randomNumber == 0 && position.x < 5)
                    {
                        position.x += speed;    //右に移動
                    }
                    else if (randomNumber != 0 && position.x > 1) 
                    {
                        position.x -= speed;    //左に移動
           
                    }
                }
            }
        }
        

        transform.position = position;  //座標の更新
    }
}
