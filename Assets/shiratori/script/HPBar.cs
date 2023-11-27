using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image hpBarcurrent;
    
    PlayerStatusManager playerStatusManager;//PlayerManagerスクリプト

    //private float currentHealth;//現在の体力

    void Start()
    {
        //PlayerManagerの読み込み
        playerStatusManager = LoadManagerScene.GetPlayerStatusManager();
    }

    //体力更新関数
    public void Update()
    {
        //現在の体力の割合から見た目を変える
        hpBarcurrent.fillAmount = playerStatusManager.HPper();
    }
}
