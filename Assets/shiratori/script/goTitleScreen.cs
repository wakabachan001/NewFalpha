using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goTitleScreen : MonoBehaviour
{
    Sounds sounds;
    public GameObject text;

    public bool clearScene = true;
    public bool gameoverScene = false;
    public float keyOnSec = 5.0f; //タイトルに戻れるようになるまでの時間

    private bool onAnykey = false;

    private void Start()
    {
        GameObject obj = GameObject.Find("SoundObject");
        sounds = obj.GetComponent<Sounds>();

        StartCoroutine(KeyOn());

        if (clearScene == true)
            sounds.GameClearSE();//SE クリア
        if (gameoverScene == true)
            sounds.GameOverSE(); //SE ゲームオーバー
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKey && onAnykey == true)
        {
            SceneManager.LoadScene("TitleScreen");
        }
    }

    private IEnumerator KeyOn()
    {
        text.SetActive(false);

        onAnykey = false;

        yield return new WaitForSeconds(keyOnSec);

        text.SetActive(true);//テキストを表示

        onAnykey = true;     //キーの有効化
    }
}
