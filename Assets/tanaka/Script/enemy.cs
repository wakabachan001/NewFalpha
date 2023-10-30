using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    Animator animator;    //アニメオブジェクト

    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();  //アニメコンポーネントの取得
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("aaa");
        animator.SetTrigger("Death");   //倒れるアニメに移行
    }
}