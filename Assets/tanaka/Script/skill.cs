using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class skill : MonoBehaviour
{
    public GameObject OriginObject;//オリジナルのオブジェクト
    public GameObject CloneObject;//クローンするオブジェクト

    private bool skillused = false;
    private bool buttonOn = true;  //ボタンが押せるかどうか

    private Button button;
    private Color normalColor  = Color.white; //ノーマルカラー
    private Color pressedlColor = Color.gray; //プレスドカラー（押されているとき）


    public float right = 1.0f;
    public float time = 5.0f;
    public float cooldownTime = 10.0f;

    GameObject manager;
    PlayerItemManager playeritem;

    private void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(ChangeColor);

        playeritem = LoadManagerScene.GetPlayerItemManager();
    }

    public void OnClick()
    {

        if (!skillused)
        {
            playeritem.onskill = true;
            StartCoroutine(Startskill());

            GameObject a;
            GameObject b;

            //クローン作成　元にするオブジェクトの左右に子として生成
            a = Instantiate(CloneObject, OriginObject.transform.position + (transform.right * right), Quaternion.identity, OriginObject.transform);
            b = Instantiate(CloneObject, OriginObject.transform.position + (transform.right * right * -1.0f), Quaternion.identity, OriginObject.transform);

            //元オブジェクトから見てどの位置に固定するか
            a.GetComponent<PlayerClone>().clonePos = new Vector3(1,  0, 0);
            b.GetComponent<PlayerClone>().clonePos = new Vector3(-1, 0, 0);

            //5秒後に消す
            StartCoroutine(DestroyClone(a, b));
            //Destroy(a, time);
            //Destroy(b, time);
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

    IEnumerator DestroyClone(GameObject a, GameObject b)
    {
        yield return new WaitForSeconds(time);

        Destroy(a);
        Destroy(b);

        playeritem.onskill = false;
    }
}
