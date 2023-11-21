using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����A�C�e���Ǘ��N���X
public class PlayerItemManager : MonoBehaviour
{
    //�v���C���[�����A�C�e�����X�g
    private List<string> havingItem = new List<string>();
    private float addMaxHP;         //�ő�̗́{
    private float addAttack;        //�U���{
    private float increaseAttack;   //�U���i�����j��
    private float addBlock;         //�h��+
    private float increaseBlock;    //�h��i�����j��

    private float addCriticalDamage;    //��S�_���[�W
    private float addCriticalChance;    //��S��
    private float takeDamage;           //�����_���[�W
    private int addMoney;             //�������ǉ�

    [SerializeField] private float iconPosX;//�A�C�R���̏����ʒuX�i����j
    [SerializeField] private float iconPosY;//�A�C�R���̏����ʒuY�i����j


    //���X�N���v�g�̃C���X�^���X
    PlayerStatusManager playerStatusManager;
    ItemManager itemManager;

    //�A�C�e���A�C�R���p�I�u�W�F�N�g
    GameObject itemIcon;

    // Start is called before the first frame update
    void Start()
    {
        //���I�u�W�F�N�g�̕ʃX�N���v�g���擾
        playerStatusManager = GetComponent<PlayerStatusManager>();
        itemManager = GetComponent<ItemManager>();
    }

    private void FixedUpdate()
    {
        //�ϐ��̃��Z�b�g
        addMaxHP = 0;
        addAttack = 0;
        increaseAttack = 1;
        addBlock = 0;
        increaseBlock = 1;
        addCriticalDamage = 0;
        addCriticalChance = 0;
        addMoney = 0;

        //�����A�C�e���̌��ʂ��v�Z
        for (int i = 0; i < havingItem.Count; i++)
        {
            //�A�C�e���̌��ʂ͍��ケ���ɑ��₷ ���ꂼ��֐��ŕ����Ă���������
            switch (havingItem[i])
            {
                case "str_up":
                    addAttack += 2.0f;
                    break;
                case "maxhp_up":
                    addMaxHP += 100.0f;
                    break;
                default:
                    Debug.Log("!�A�C�e�����ʂ�������܂���");
                    break;
            }
            //�v���C���[�X�e�[�^�X�N���X���̌v�Z�N���X�ɑ��
            playerStatusManager.statusCalc.AddAttack = addAttack;
            playerStatusManager.statusCalc.IncreaseAttack = increaseAttack;
            playerStatusManager.statusCalc.AddBlock = addBlock;
            playerStatusManager.statusCalc.IncreaseBlock = increaseBlock;
            playerStatusManager.statusCalc.AddCriticalChance = addCriticalChance;
            playerStatusManager.statusCalc.AddCriticalDamage = addCriticalDamage;
            playerStatusManager.statusCalc.AddMoney = addMoney;
        }
    }

    //�A�C�e���擾�֐�
    public void AddItem(string id)
    {
        //�擾�A�C�e���������A�C�e�����ɂ��邩���ׂ�
        for (int i = 0; i < havingItem.Count; i++)
            if (havingItem[i] == id)
            {
                //���łɏ������Ă����Ȃ牽�����Ȃ�
                Debug.Log(id + "�����łɏ������Ă��܂�");
                break;
            }
            else
            {
                //�������Ȃ�
                havingItem.Add(id);//�����A�C�e����id��ǉ�
            }
    }
    //�A�C�e���p���֐�
    public void RemoveItem(string id)
    {
        havingItem.Remove(id);//�����A�C�e������w���id���폜
    }

    //�A�C�e���w���֐�
    public void BuyingItem(string id)
    {
        //�w�����i���擾
        int price = itemManager.GetSellingPrice(id);

        //������������Ă���Ȃ�
        if (playerStatusManager.playerStatus.Money >= price)
        {
            //���i���A�����������炷
            playerStatusManager.GettingMoney(price * -1);
            AddItem(id);    //�A�C�e�����l��
        }
    }
    //�A�C�e�����p�֐�
    public void SellingItem(string id)
    {
        //�w�����i���擾
        int price = itemManager.GetSellingPrice(id);

        //���i���A�������𑝂₷
        playerStatusManager.GettingMoney(price);
        RemoveItem(id);     //�A�C�e�����l��
    }

    //�����A�C�e���̃A�C�R����\������֐�
    private void MakeIcon()
    {
        for (int i = 0; i < havingItem.Count; i++)
        {
            //�q�Ƃ���Image������Prefab���N���[��
            //�N���[�������I�u�W�F�N�g
        }
    }
}
