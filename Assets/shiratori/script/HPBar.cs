using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image hpBarcurrent;
    private float maxHealth;//Å‘å‘Ì—Í

    //private float currentHealth;//Œ»İ‚Ì‘Ì—Í

    void Awake()
    {
        //PlayerManager‚Ì“Ç‚İ‚İ
        PlayerManager playermanager;
        GameObject obj = GameObject.Find("Player");
        playermanager = obj.GetComponent<PlayerManager>();

        //Å‘å‘Ì—Í‚ğPlayermManager‚©‚çQÆ
        maxHealth = playermanager.playerHP;

        ////Œ»İ‚ÌHP‚ğ‰Šú‰»
        //currentHealth = maxHealth;
    }

    //‘Ì—ÍXVŠÖ”
    public void UpdateHP(float currentHealth)
    {
        //currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        hpBarcurrent.fillAmount = currentHealth / maxHealth;
    }
}
