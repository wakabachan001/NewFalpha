using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy02AI : MonoBehaviour
{
    public float movelength = 0.02f;
    float hanpuku = -1.0f;

    //bool onAttack = false;      //�ߋ����U���t���O
    //private float time; //���Ԍv���p
    //public GameObject EnemyAttackEffect; //�ߋ����U��
    //public float EffectLimit;       //�ߋ����U���̔��肪�c�鎞��

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

            //�@�G�O�Q�����E�Ɉړ�������----------------------------------------------------------------
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
            //    Debug.Log("�G���U��");

            //    //�U���G�t�F�N�g�̗L����
            //    EnemyAttackEffect.gameObject.SetActive(true);

            //    //�v���C���[�̂P�}�X�O�ɍU���G�t�F�N�g���ړ�
            //    EnemyAttackEffect.transform.position = this.transform.position + transform.up * -1.0f;

            //    time = 0.0f;        //���Ԃ̃��Z�b�g
            //    onAttack = true;    //�U���t���O�I��

            //}

            ////�ߋ����U������
            //if (onAttack)
            //{
            //    //�P�b��1.0f���₷
            //    time += 0.02f;

            //    //time���w�肵�����Ԉȏ�ɂȂ��
            //    if (time >= EffectLimit)
            //    {
            //        //�I�u�W�F�N�g�̖�����
            //        EnemyAttackEffect.gameObject.SetActive(false);
            //        //�U���t���O��������
            //        onAttack = false;
            //    }
            //}

        }
    }
}
