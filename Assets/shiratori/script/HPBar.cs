using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image hpBarcurrent;
    private float maxHP;        //最大体力
    private float currentHP;    //現在体力
    
    PlayerStatusManager playerStatusManager;//PlayerManagerスクリプト

    //private float currentHealth;//現在の体力

    void Start()
    {
        //PlayerManagerの読み込み
        playerStatusManager = LoadManagerScene.GetPlayerStatusManager();

        //最大体力をPlayermStatusManagerから参照
        maxHP = playerStatusManager.status.MaxHP;

        ////現在のHPを初期化
        //currentHealth = maxHealth;
    }

    //体力更新関数
    public void Update()
    {
        //現在の体力の割合から見た目を変える
        hpBarcurrent.fillAmount = playerStatusManager.HPper();
    }
}
