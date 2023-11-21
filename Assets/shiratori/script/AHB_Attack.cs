using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AHB_Attack : MonoBehaviour
{
    public float clonepos = -1.0f;//�N���[�������ʒu�����p
    public float coolTime = 2.0f;//�U���̃N�[���^�C��
    private float time = 0.0f;//���Ԍv���p

    private bool moveOn = false;//�s���\�t���O

    public GameObject AttackEffect;//�N���[������I�u�W�F�N�g

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("������");
        //�A�N�e�B�u�G���A���ɓ�������
        if (collision.gameObject.tag == "ActiveArea")
        {
            moveOn = true;//�s���\�t���O���I��
            Debug.Log("�s���\");
        }
    }

    private void FixedUpdate()
    {
        if (true)
        {
            time += 0.02f;//1�b��1������

            //time���N�[���^�C���𒴂�����
            if (time >= coolTime)
            {
                Debug.Log("�U��");
                //�U������
                // Boss�� X+1,Y-2 �̈ʒu�ɍU��
                Vector2 position1 = new Vector2(transform.position.x + 1.0f, transform.position.y - 2.0f);
                Instantiate(AttackEffect, position1, Quaternion.identity);

                // Boss�� X-1,Y-2 �̈ʒu�ɍU��
                Vector2 position2 = new Vector2(transform.position.x - 1.0f, transform.position.y - 2.0f);
                Instantiate(AttackEffect, position2, Quaternion.identity);

                time = 0.0f;
            }
        }
    }
}