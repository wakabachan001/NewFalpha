using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image hpBarcurrent;
    private float maxHealth;//�ő�̗�

    //private float currentHealth;//���݂̗̑�

    void Awake()
    {
        //PlayerManager�̓ǂݍ���
        PlayerStatusManager playerStatusManager;
        GameObject obj = GameObject.Find("DataInfo");
        playerStatusManager = obj.GetComponent<PlayerStatusManager>();

        //�ő�̗͂�PlayermStatusManager����Q��
        maxHealth = playerStatusManager.MaxHP;

        ////���݂�HP��������
        //currentHealth = maxHealth;
    }

    //�̗͍X�V�֐�
    public void UpdateHP(float currentHealth)
    {
        //currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        hpBarcurrent.fillAmount = currentHealth / maxHealth;
    }
}
