using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private float maxHP = 100.0f;       //�ő�̗�
    [SerializeField] private int money = 100;            //���Ƃ�����
    [SerializeField] private float attackDamage1 = 10.0f;//�U��1�̃_���[�W
    [SerializeField] private float attackDamage2 = 10.0f;//�U��2�̃_���[�W

    private float currentHP;    //���݂�HP
    private float takeDamage;   //��_���[�W

    private StatusData enemyStatus = new StatusData();    //�G�X�e�[�^�X�N���X
    private StatusCalc statusCalc = new StatusCalc();     //�_���[�W�v�Z�N���X

    PlayerStatusManager playerStatusManager;//PlayerStatusManager�X�N���v�g
    GameObject obj;//DataInfo�p

    void Start()
    {
        Debug.Log("�G������");
        //�X�e�[�^�X���N���X�ŊǗ�
        enemyStatus.MaxHP = maxHP;
        enemyStatus.Money = money;
        enemyStatus.SetAttackDamage(0, attackDamage1);
        enemyStatus.SetAttackDamage(1, attackDamage2);

        Debug.Log("�G����������");

        currentHP = maxHP;
    }

    //��collider�ڐG��
    void OnTriggerEnter2D(Collider2D other)
    {
        //DataInfo��PlayerStatusManager���擾
        obj = GameObject.Find("DataInfo");
        playerStatusManager = obj.GetComponent<PlayerStatusManager>();

        Debug.Log("OnTriggerEnter2D: " + other.gameObject.name);

        //���Ƃ̐ڐG
        if(other.gameObject.tag == "Sword")
        {
            Debug.Log("���̃_���[�W");

            //�v���C���[�̋ߋ����U���_���[�W�𒲂ׂ�
            takeDamage = playerStatusManager.AttackDamageCalc();

            //HP�v�Z
            currentHP = statusCalc.HPCalc(currentHP, takeDamage);
        }
        //�藠���Ƃ̐ڐG
        if (other.gameObject.tag == "Syuriken")
        {
            Debug.Log("�藠���̃_���[�W");

            //�v���C���[�̉������U���_���[�W�𒲂ׂ�
            takeDamage = playerStatusManager.AttackDamageCalc();

            //HP�v�Z
            currentHP = statusCalc.HPCalc(currentHP, takeDamage);
        }
        //�|��邩���ׂ�
        EnemyDead();
    }

    //�|��邩���ׂ�֐�
    void EnemyDead()
    {
        //�GHP��0�ȉ��Ȃ�A���̃I�u�W�F�N�g������
        if (currentHP <= 0.0f)
        {
            //�v���C���[�̏������𑝂₷
            playerStatusManager.GettingMoney(enemyStatus.Money);

            Destroy(gameObject);
            Debug.Log("�G���|�ꂽ");
        }
    }
}