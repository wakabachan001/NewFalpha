using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーン関連用

public class SceneChange : MonoBehaviour
{
    public string[] stageScene = new string[3];  //ステージそれぞれのシーン名
    public string clearScene;   //クリアシーン
    public string gameoverScene;//ゲームオーバーシーン

    private string nextStage;   //切り替えるシーンを入れる
    private int stageCount = 0; //何ステージ目か（最初は１ステージ目）

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void NextStage()
    {
        //stageCountが3未満の場合、次ステージに切り替え
        if (stageCount < 3)
        {
            //nextStageを現在何ステージ目かによって変える
            nextStage = stageScene[stageCount];

            //シーンの切り替え
            Debug.Log(nextStage + "に切り替えます");
            SceneManager.LoadScene(nextStage);
            Debug.Log(nextStage + "切り替えました");

            //ステージカウントを1進める
            stageCount++;
        }
        else
        {
            //クリア画面に移行
            SceneManager.LoadScene(clearScene);
        }
    }
}
