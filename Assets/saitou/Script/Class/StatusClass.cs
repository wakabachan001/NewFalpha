using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusClass : MonoBehaviour
{
}

//�X�e�[�^�X�Ǘ��N���X
public class StatusData
{
    //�R���X�g���N�^
    public StatusData()
    {

    }
    protected float maxHP;  //�ő�HP   
    protected int money;    //������
    protected List<float> attackDamage = new List<float>();//�U���̃_���[�W


    public float MaxHP
    {
        get { return maxHP; }
        set { maxHP = value; }
    }
    public int Money
    {
        get { return money; }
        set { money = value; }
    }
    public void SetAttackDamage(int n, float damage)
    {
        attackDamage.Insert(n, damage);
    }
    public float GetAttackDamage(int n)
    {
        return attackDamage[n];
    }

    //��S�_���[�W�@��S���@��ǉ��\��
}

//�v���C���[�X�e�[�^�X�Ǘ��N���X
public class PlayerStatusData : StatusData
{
    //�R���X�g���N�^
    public PlayerStatusData()
    {

    }
    private float maxBarrier;//�ő�o���A�l

    public float MaxBarrier
    {
        get { return maxBarrier; }
        set { maxBarrier = value; }
    }
}


//�X�e�[�^�X�v�Z�N���X
public class StatusCalc
{
    private float addAttack;      //�ǉ��_���[�W(+)
    private float increaseAttack; //�����_���[�W(*)
    private float addBlock;         //�h��+
    private float increaseBlock;    //�h��i�����j��

    private float addCriticalDamage;    //��S�_���[�W
    private float addCriticalChance;    //��S��
    private int addMoney;               //�������ǉ�

    //�R���X�g���N�^
    public StatusCalc()
    {
        //������
        addAttack = 0f;
        increaseAttack = 1f;
        addBlock = 0f;
        increaseBlock = 1f;

        addCriticalDamage = 0f;
        addCriticalChance = 0f;
        addMoney = 0;
    }
    //�_���[�W�v�Z�֐�
    public float DamageCalc(float damage)
    {
        //1~100�̃����_��
        int dice = Random.RandomRange(1, 101);

        //�����_���̒l���A�N���e�B�J�����ȉ��Ȃ�
        if( dice <= 2 + addCriticalChance)
        {
            //�N���e�B�J���_���[�W��������
            return (damage + addAttack) * increaseAttack * addCriticalDamage;
        }
        else
        {
            //�ʏ�̃_���[�W
            return (damage + addAttack) * increaseAttack;
        }     
    }
    //�̗͌v�Z�֐�
    public float HPCalc(float hp, float damage, float barrier = 1.0f)
    {
        return hp - ((damage - addBlock)* increaseBlock * barrier);
    }
    //�̗͉񕜊֐�
    public float HealHP(float maxhp, float hp, float heal)
    {
        //�񕜂��čő�̗͂𒴂���Ȃ�
        if (hp + heal >= maxhp)
        {
            //���݂̗̑͂��ő�̗͂Ɠ����ɂ���
            return maxhp;
        }
        else
        {
            //���݂̗̑͂��񕜂���
            return hp + heal;
        }
    }
    //�擾���z�v�Z�֐�
    public int MoneyCalc(int money)
    {
        return money + addMoney;
    }

    //�v���p�e�B
    public float AddAttack
    {
        get { return addAttack; }
        set { addAttack = value; }
    }
    public float IncreaseAttack
    {
        get { return increaseAttack; }
        set { increaseAttack = value; }
    }
    public float AddBlock
    {
        get { return addBlock; }
        set { addBlock = value; }
    }
    public float IncreaseBlock
    {
        get { return increaseBlock; }
        set { increaseBlock = value; }
    }
    public float AddCriticalDamage
    {
        get { return addCriticalDamage; }
        set { addCriticalDamage = value; }
    }
    public float AddCriticalChance
    {
        get { return addCriticalChance; }
        set { addCriticalChance = value; }
    }
    public int AddMoney
    {
        get { return addMoney; }
        set { addMoney = value; }
    }
}