using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BoardManager boardScript;
    //�e�X�g�Ƃ��ă��x�����R�ɂ��Ă���
    //private int level = 3;

    //Awake:Scene���ړ������������Ɏ��s�����
    private void Awake()
    {
        //BoardManager�擾
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }

    void InitGame()
    {
        //BoardManager��SetupScene���\�b�h�����s
        boardScript.SetupScene();
    }
}
