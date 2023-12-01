using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{

    public Text HP;

    PlayerStatusManager playerStatusManager;

    // Start is called before the first frame update
    void Start()
    {
        //スクリプトの取得
        playerStatusManager = LoadManagerScene.GetPlayerStatusManager();
    }

    // Update is called once per frame
    void Update()
    {
        //最大体力と現在の体力を表示
        HP.text = "HP " + playerStatusManager.status.CurrentHP + " / " + playerStatusManager.MaxHP();
    }
}
