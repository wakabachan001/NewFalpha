using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class antiHoming : MonoBehaviour
{
    GameObject playerObj;
    PlayerManager player;
    Transform playerTransform;

    private Camera mainCamera;
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<PlayerManager>();
        playerTransform = playerObj.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;

        if (position.y <= 20)// Y���@�ړ�����
        {
            //  ���������ɕۂƓ����Ȃ�
            if(transform.position.y - playerTransform.position.y == 2) { ; }
            //�@���ɓ�����
            else if (transform.position.y - playerTransform.position.y <= 2)
            {
                position.y += speed; 
                if (position.y > 20) position.y -= speed;
            }
            //�@���ꂷ����Ƌ߂Â��Ă���
            else if (transform.position.y - playerTransform.position.y >= 2)
            {
                position.y -= speed;
            }
        }
      
        if (position.x > 0 && position.x < 6) // X���@�ړ�����
        {
            //�@�E�ɓ�����
            if (playerTransform.position.x==transform.position.x -1)
            {
                position.x += speed;
                if (position.x > 5) position.x -= speed;
            }
            // ���ɓ�����
            if (playerTransform.position.x == transform.position.x + 1)
            {
                position.x -= speed;
                if (position.x < 1) position.x += speed;
            }

            // X���W�������Ƃ����E�̂ǂ������ɓ�����
            if (transform.position.x == playerTransform.position.x) 
            {
                int randomNumber = Random.Range(0, 2);  //�����_���Ȓl�𐶐�

                if (randomNumber == 0 && position.x < 5)
                {
                    position.x += speed;    //�E�Ɉړ�
                }
                else if (randomNumber != 0 && position.x > 1) 
                {
                    position.x -= speed;    //���Ɉړ�
           
                }
            }
        }

        transform.position = position;  //���W�̍X�V
    }
}
