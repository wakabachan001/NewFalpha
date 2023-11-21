using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class BossManager : MonoBehaviour
{
    public float BossHP;       //�G�I�u�W�F�N�gHP
    public GameObject NextStageTiles;

    //public GameObject ClearText;//�N���A�e�L�X�g

    //��collider�ڐG��
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerManager playermanager;
        GameObject obj = GameObject.Find("Player");
        playermanager = obj.GetComponent<PlayerManager>();

        Debug.Log("OnTriggerEnter2D: " + other.gameObject.name);

        //���Ƃ̐ڐG
        if (other.gameObject.tag == "Sword")
        {
            Debug.Log("���̃_���[�W");
            BossHP -= playermanager.swordDamage; //HP�����_���[�W�����炷
        }
        //�藠���Ƃ̐ڐG
        if (other.gameObject.tag == "Syuriken")
        {
            Debug.Log("�藠���̃_���[�W");
            BossHP -= playermanager.syurikenDamage; //HP���藠���_���[�W�����炷
        }
        //�|��邩���ׂ�
        EnemyDead();
    }

    //�|��邩���ׂ�֐�
    void EnemyDead()
    {
        //�GHP��0�ȉ��Ȃ�A���̃I�u�W�F�N�g������
        if (BossHP <= 0.0f)
        {
            //GameObject obj = GameObject.Find("ClearText");
            //obj.SetActive(true);

            //gameObject.SetActive(true);
            Destroy(gameObject);
            Debug.Log("�{�X���|�ꂽ");

                //�@�{�XHP���O�ɂȂ�Ǝ��̃X�e�[�W�ɍs�����߂̃}�X���\�������
                if (BossHP <= 0)
                {
                    Instantiate(NextStageTiles, new Vector2(5, 20), Quaternion.identity);
                }
        }
    }
}
