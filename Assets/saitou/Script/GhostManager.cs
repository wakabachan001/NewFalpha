using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : MonoBehaviour
{
    public float visibility = 0.5f;//透明度

    public float num = 0.02f;//1fで減る透明度

    private void FixedUpdate()
    {
        SpriteRenderer ghostRenderer = GetComponent<SpriteRenderer>();//SpriteRendererを読み込み

        //透明度を変更
        ghostRenderer.color = new Color(ghostRenderer.color.r, ghostRenderer.color.g, ghostRenderer.color.b, visibility);

        //透明度用数値を減らす
        visibility -= num;

        //透明度が０になったらクローンを削除
        if (visibility <= 0.0f)
            Destroy(gameObject, 0.0f);
    }
}
