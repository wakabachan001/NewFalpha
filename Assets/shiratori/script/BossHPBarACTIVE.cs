using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHPBarACTIVE : MonoBehaviour
{
    [SerializeField] GameObject BossHPBar;
    public void ActiveBossHPBar()
    {
        BossHPBar.SetActive(true);
    }

    public void InActiveBossHPBar()
    {
        BossHPBar.SetActive(false);
    }
}
