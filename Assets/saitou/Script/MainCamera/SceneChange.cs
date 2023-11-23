using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーン関連用

public class SceneChange : MonoBehaviour
{
    public string gameScene;    //メインゲームシーン
    public string titleScene;   //タイトルシーン
    public string clearScene;   //クリアシーン
    public string gameoverScene;//ゲームオーバーシーン

    public void StageClear()
    {
        //クリア画面に移行
        SceneManager.LoadScene(clearScene);
    }
    public void GameOver()
    {
        //ゲームオーバー画面に移行
        SceneManager.LoadScene(gameoverScene);
    }
    public void GameStart()
    {
        //メインゲーム画面に移行
        SceneManager.LoadScene(gameScene);
    }
    public void Title()
    {
        //タイトル画面に移行
        SceneManager.LoadScene(titleScene);
    }
}
