using UnityEngine;
public class DamageTXTmanager : MonoBehaviour
{
    [SerializeField] private GameObject _damageobj;
    [SerializeField] private Canvas _canvas;
    Vector2 pos ;
    public void TakeDamage(float damageval,float x,float y)
    {
            pos = new Vector2(x, y);
            DamageTXT damageIns = Instantiate(_damageobj , pos , Quaternion.identity , _canvas.transform ).GetComponent<DamageTXT>();
            damageIns.SetDamageText(damageval);    
    }
}