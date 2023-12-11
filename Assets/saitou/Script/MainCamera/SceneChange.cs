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

    public float changeTime = 0.3f;//シーンチェンジ時差


    public void StageClear()
    {
        //クリア画面に移行
        StartCoroutine(ChangeCooltime(changeTime, clearScene));
    }
    public void GameOver()
    {
        //ゲームオーバー画面に移行
        StartCoroutine(ChangeCooltime(changeTime, gameoverScene));
    }
    public void GameStart()
    {
        //メインゲーム画面に移行
        StartCoroutine(ChangeCooltime(changeTime, gameScene));
    }
    public void Title()
    {
        //タイトル画面に移行
        SceneManager.LoadScene(titleScene);
    }


    //時差シーンチェンジコルーチン
    private IEnumerator ChangeCooltime(float sec, string scene)
    {
        yield return new WaitForSeconds(sec);//引数だけ待つ

        SceneManager.LoadScene(scene);//シーン切り替え
    }
}
