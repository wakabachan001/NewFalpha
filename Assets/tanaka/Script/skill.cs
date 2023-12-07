using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class skill : MonoBehaviour
{
    public GameObject OriginObjct;//オリジナルのオブジェクト
    public GameObject CloneObject;//クローンするオブジェクト

    private bool skillused = false;
    private bool buttonOn = true;  //ボタンが押せるかどうか

    private Button button;
    private Color normalColor  = Color.white; //ノーマルカラー
    private Color pressedlColor = Color.gray; //プレスドカラー（押されているとき）


    public float right = 1.0f;
    public float time = 5.0f;
    public float cooldownTime = 10.0f;



    private void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(ChangeColor);
    }

    public void OnClick()
    {

        if (!skillused)
        {
            StartCoroutine(Startskill());

            GameObject a;
            GameObject b;

            //現在地を取得
            Vector3 currentPositione = transform.position;


            //クローン作成
            a = Instantiate(CloneObject, OriginObjct.transform.position + (transform.right * right), Quaternion.identity);

            //クローン作成
            b = Instantiate(CloneObject, OriginObjct.transform.position + (transform.right * right * -1.0f), Quaternion.identity);

            //5秒後に消す
            Destroy(a, time);
            Destroy(b, time);
        }
    }

    private void ChangeColor()
    {
        if (buttonOn == true)//ボタンが押せる状態なら
        {
            buttonOn = false;
            //ボタンのカラーを変更
            gameObject.GetComponent<Image>().color = pressedlColor;

            //ボタンを再度元のカラーに戻すためのコルーチンを呼び出す例
            StartCoroutine(ResetColor());
        }
    }
    private IEnumerator ResetColor()
    {
        
        yield return new WaitForSeconds(cooldownTime);//10秒後に元のカラーに戻す

        gameObject.GetComponent<Image>().color = normalColor;//ボタンのカラーを戻す

        buttonOn = true;
    }
    IEnumerator Startskill()
    {
        skillused = true;

        yield return new WaitForSeconds(cooldownTime);

        skillused = false;
    }
}
