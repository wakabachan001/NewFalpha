using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BoardManager boardScript;
    //テストとしてレベルを３にしておく
    private int level = 3;

    //Awake:Sceneを移動した時即座に実行される
    private void Awake()
    {
        //BoardManager取得
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }

    void InitGame()
    {
        //BoardManagerのSetupSceneメソッドを実行
        boardScript.SetupScene(level);
    }
}
