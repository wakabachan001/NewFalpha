using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSorceData : MonoBehaviour
{
}

//�A�C�e���f�[�^�N���X
//CSV�t�@�C������̓ǂݍ���
public class ItemDataC
{
    private string id;          //�A�C�e��ID   
    private string itemName;    //����
    private string description; //����
    //private Sprite icon;        //�A�C�e���̃A�C�R��
    private int buyingPrice;    //���l
    private int sellingPrice;   //���l

    public string Id
    {
        get { return id; }
        set { id = value; }
    }
    public string ItemName
    {
        get { return itemName; }
        set { itemName = value; }
    }
    public string Description
    {
        get { return description; }
        set { description = value; }
    }
    //public Sprite Icon
    //{
    //    get { return icon; }
    //    set { icon = value; }
    //}
    public int BuyingPrice
    {
        get { return buyingPrice; }
        set { buyingPrice = value; }
    }
    public int SellingPrice
    {
        get { return sellingPrice; }
        set { sellingPrice = value; }
    }
}