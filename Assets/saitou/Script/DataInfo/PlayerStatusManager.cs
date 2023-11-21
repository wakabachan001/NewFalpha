using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�v���C���[�̃X�e�[�^�X�Ǘ��N���X
public class PlayerStatusManager : MonoBehaviour
{
    [SerializeField] private float maxHP;       //�ő�̗�
    private float currentHP;                //���݂̗̑�
    [SerializeField] private float maxBarrier;  //�ő�o���A
    private float currentBarrier;           //���݂̃o���A
    [SerializeField] private float attckDamage; //�ߋ����U���_���[�W
    [SerializeField] private float shotDamage;  //�������U���_���[�W
    [SerializeField] private int initialMoney;  //����������

    public PlayerStatusData playerStatus = new PlayerStatusData(); //�v���C���[�̃X�e�[�^�X�i�[�p
    public StatusCalc statusCalc = new StatusCalc();               //�X�e�[�^�X�v�Z�p

    public HPBar hpbar;              //HPBar�X�N���v�g

    public float MaxHP
    {
        get { return maxHP; }
    }
    public float MaxBarrier
    {
        get { return maxBarrier; }
    }
    public float CurrentBarrier
    {
        get { return currentBarrier; }
        set { currentBarrier = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //�X�e�[�^�X�̏�����
        playerStatus.MaxHP = maxHP;
        playerStatus.MaxBarrier = maxBarrier;
        playerStatus.SetAttackDamage(0, attckDamage);//attackDamage[0]������
        playerStatus.SetAttackDamage(1, shotDamage); //attackDamage[1]������
        playerStatus.Money = initialMoney;

        currentHP = maxHP;
        currentBarrier = maxBarrier;

        Debug.Log("maxHP" + playerStatus.MaxHP);
    }

    //�����l���֐�
    public void GettingMoney(int money)
    {
        Debug.Log("�����擾");
        playerStatus.Money += statusCalc.MoneyCalc(money);
    }

    //�_���[�W���󂯂�֐�
    public bool TakeDamage(float damage)
    {
        Debug.Log("�_���[�W��H�����");

        //HP�v�Z�֐����Ă�ŁA���ݑ̗͂��X�V
        currentHP = statusCalc.HPCalc(currentHP, damage, currentBarrier);

        //HPBar�̌Ăяo��
        hpbar.UpdateHP(currentHP);

        //HP���O�ȉ���������false��Ԃ�
        if (currentHP <= 0)
            return false;
        else
            return true;
    }

    //�ߋ����U���̃_���[�W�v�Z�֐�
    public float AttackDamageCalc()
    {
        //AttackDamage[0]�������Ƃ��āA�_���[�W�v�Z�֐����Ă�
        return statusCalc.DamageCalc(playerStatus.GetAttackDamage(0));
    }

    //�������U���̃_���[�W�v�Z�֐�
    public float ShotDamageCalc()
    {
        //AttackDamage[1]�������Ƃ��āA�_���[�W�v�Z�֐����Ă�
        return statusCalc.DamageCalc(playerStatus.GetAttackDamage(1));
    }
}

