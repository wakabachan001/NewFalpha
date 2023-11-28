using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShitakaraAttackEffect : MonoBehaviour
{
    public float EffectTime = 0.2f;
    public float time = 0;

    private void FixedUpdate()
    {
        time += 0.02f;

        if (time >= EffectTime)
        {
            Destroy(gameObject);
        }
    }
}
