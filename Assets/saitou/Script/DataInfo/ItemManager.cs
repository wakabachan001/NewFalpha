using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//�S�A�C�e���̊Ǘ��N���X
public class ItemManager : MonoBehaviour
{
    //�S�A�C�e���f�[�^List
    private List<ItemDataC> ItemData = new List<ItemDataC>();

    private TextAsset csvFile; // CSV�t�@�C��
    private List<string[]> csvData = new List<string[]>(); // CSV�t�@�C���̒��g�����郊�X�g

    void Start()
    {
        csvFile = Resources.Load("ItemData") as TextAsset; // Resources�ɂ���CSV�t�@�C�����i�[
        StringReader reader = new StringReader(csvFile.text); // TextAsset��StringReader�ɕϊ�

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine(); // 1�s���ǂݍ���
            csvData.Add(line.Split(',')); // csvData���X�g�ɒǉ����� 
        }
        for (int i = 0; i < 5; i++)
            Debug.Log(csvData[1][i]);

        //2�s�ڂ���f�[�^��ǂݍ���
        for (int i = 1; i < csvData.Count; i++)
        {
            ItemDataC LoadItem = new ItemDataC();
            LoadItem.Id = csvData[i][0];
            LoadItem.ItemName = csvData[i][1];
            LoadItem.Description = csvData[i][2];
            LoadItem.BuyingPrice = int.Parse(csvData[i][3]);
            LoadItem.SellingPrice = int.Parse(csvData[i][4]);

            //�A�C�e���f�[�^�ɉ��f�[�^��ǉ�
            //!����ItemData���X�g�ɉ�����������ƁA���v�f���X�V����Ă��܂��o�O����
            //�e�v�f�̏��������s��Ȃ���΂Ȃ�Ȃ��ۂ�    

            ItemData.Add(LoadItem);

            //ItemData[i - 1] = new ItemDataC();


        }

        for (int i = 0; i < ItemData.Count; i++)
        {
            Debug.Log(i);

            Debug.Log(ItemData[i].Id);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    //���O��Ԃ��֐�
    public string GetName(string id)
    {
        //�A�C�e���f�[�^��S�ĒT��
        for (int i = 0; i < ItemData.Count; i++)
        {
            //ID����v�����Ȃ�
            if (ItemData[i].Id == id)
            {
                return ItemData[i].ItemName;
            }
        }
        //������ID���A�C�e���f�[�^�ɑ��݂��Ȃ��Ȃ�
        Debug.Log("!�w�肵���A�C�e���̖��O��������܂���");
        return null;
    }

    //��������Ԃ��֐�
    public string GetDescription(string id)
    {
        //�A�C�e���f�[�^��S�ĒT��
        for (int i = 0; i < ItemData.Count; i++)
        {
            //ID����v�����Ȃ�
            if (ItemData[i].Id == id)
            {
                return ItemData[i].Description;
            }
        }
        //������ID���A�C�e���f�[�^�ɑ��݂��Ȃ��Ȃ�
        Debug.Log("!�w�肵���A�C�e���̐�������������܂���");
        return null;
    }

    //�w�����i��Ԃ��֐�
    public int GetBuyingPrice(string id)
    {
        //�A�C�e���f�[�^��S�ĒT��
        for (int i = 0; i < ItemData.Count; i++)
        {
            //ID����v�����Ȃ�
            if (ItemData[i].Id == id)
            {
                return ItemData[i].BuyingPrice;
            }
        }
        //������ID���A�C�e���f�[�^�ɑ��݂��Ȃ��Ȃ�
        Debug.Log("!�w�肵���A�C�e���̍w�����i��������܂���");
        return 0;
    }
    //���p���i��Ԃ��֐�
    public int GetSellingPrice(string id)
    {
        //�A�C�e���f�[�^��S�ĒT��
        for (int i = 0; i < ItemData.Count; i++)
        {
            //ID����v�����Ȃ�
            if (ItemData[i].Id == id)
            {
                return ItemData[i].SellingPrice;
            }
        }
        //������ID���A�C�e���f�[�^�ɑ��݂��Ȃ��Ȃ�
        Debug.Log("!�w�肵���A�C�e���̔��p���i��������܂���");
        return 0;
    }

    //�����_���A�C�e���w��֐�
    //��X�I�[�o�[���[�h�Ń��A���e�B���w��ł���悤�ɂ��Ă�����
    public string GetRandomItem()
    {
        //0�`�S�A�C�e���̎�ނ̃����_���Ȑ��l���擾
        int r = Random.RandomRange(0, ItemData.Count);

        //���̐��l����AID��Ԃ�
        return ItemData[r].Id;
    }
}

