using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    public Text money;

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
        money.text = "Money : " + playerStatusManager.status.Money;
    }
}
