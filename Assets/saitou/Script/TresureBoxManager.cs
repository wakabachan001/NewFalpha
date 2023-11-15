using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TresureBoxManager : MonoBehaviour
{
    public Sprite closeImg;//•Â‚¶‚½‰æ‘œ
    public Sprite openImg; //ŠJ‚¢‚½‰æ‘œ

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        //SpriteRenderer‚ğæ“¾
        spriteRenderer = GetComponent<SpriteRenderer>();

        //‰Šú‚Ì‰æ‘œ‚ğ•Â‚¶‚½‰æ‘œ‚É‚·‚é
        spriteRenderer.sprite = closeImg;
    }
    
    //•ó” ‚ğŠJ‚¯‚éŠÖ”
    public void OpenBox()
    {
        //ŠJ‚¯‚½‰æ‘œ‚É•Ï‚¦‚é
        spriteRenderer.sprite = openImg;
    }
}
