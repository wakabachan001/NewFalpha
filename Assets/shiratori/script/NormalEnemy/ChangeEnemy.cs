using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEnemy : MonoBehaviour
{
    public Transform PlayerTransform;
    public Transform Enemy04Transform;

    public float coolTime = 2.0f;//�U���̃N�[���^�C��
    private float time = 0.0f;//���Ԍv���p

    private bool moveOn = false;//�s���\�t���O


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�A�N�e�B�u�G���A���ɓ�������
        if (collision.gameObject.tag == "ActiveArea")
        {
            moveOn = true;//�s���\�t���O���I��
            Debug.Log("�s���\");
        }
    }

    void Swap()
    {
        Vector2 tempPos = new Vector2( PlayerTransform.position.x, PlayerTransform.position.y + 1f );

        //PlayerTransform.position = Enemy04Transform.position;
        Enemy04Transform.position = tempPos;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (moveOn)
        {
            time += 0.02f;//1�b��1������

            //time���N�[���^�C���𒴂�����
            if (time >= coolTime)
            {
                Swap();
                time = 0.0f;
            }
        }
    }
}