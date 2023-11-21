using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TresureBoxManager : MonoBehaviour
{
    private bool open = false;//�󔠂��J���Ă��邩

    public Sprite closeImg;//�����摜
    public Sprite openImg; //�J�����摜

    private SpriteRenderer spriteRenderer;

    public GameObject itemObj;//�A�C�e���I�u�W�F�N�g

    // Start is called before the first frame update
    void Start()
    {
        //SpriteRenderer���擾
        spriteRenderer = GetComponent<SpriteRenderer>();

        //�����̉摜������摜�ɂ���
        spriteRenderer.sprite = closeImg;
    }
    
    //�󔠂��J����֐�
    public void OpenBox()
    {
        open = true;
        Debug.Log("�󔠂��J����");

        //�J�����摜�ɕς���
        spriteRenderer.sprite = openImg;

        //�A�C�e���Ȃǂ��擾����
        Instantiate(itemObj, transform.position - transform.up, Quaternion.identity);
    }
}
