using UnityEngine;
using UnityEngine.UI;
public class DamageTXT : MonoBehaviour
{
    private float _displaytime = 0.4f;
    private float _timer;
    private Text _txt;
    private Vector3 pos;
    private void Awake()
    {
        _txt = GetComponent<Text>();
        pos = transform.position;
    }
    void FixedUpdate()
    {
        _timer += Time.deltaTime;
        if (_timer > _displaytime)
            Destroy(gameObject);//displaytimeÇ≈ê›íËÇµÇΩéûä‘Ç…Ç»ÇÈÇ∆ï\é¶Çè¡Ç∑

        pos.y += 0.01f;
        transform.position = pos;
    }
    public void SetDamageText(float value)
    {
        _txt.text = value.ToString();
    }
}