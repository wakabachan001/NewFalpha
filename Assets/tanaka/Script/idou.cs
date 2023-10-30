using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    // Start is called before the first frame update
    public class idou : MonoBehaviour
    {
    public float speed = 0.5f;
    public float shotSpeed = 0.2f;

    bool onAttack = false;
    bool onShot = false;
    bool onBottomColumn = true;

    public float EffectLimit;
    private float ShotLimit = 3;

    private float time;

    public GameObject AttackEffect;
    public GameObject ShotEffect;

    void Start()
    {

    }

    // Update is called once per frame

    void Update()
    {
        Vector2 position = transform.position;

        if (Input.GetKeyDown("left"))
        {
            position.x -= speed;
         }
        if (Input.GetKeyDown("right"))
        {
            position.x += speed;
        }
        if (Input.GetKeyDown("up"))
        {
            position.y += speed;
            onBottomColumn = false;
        }
        if (Input.GetKeyDown("down"))
        {
            position.y -= speed;
            onBottomColumn = true;
        }

        transform.position = position;

        if (Input.GetKeyDown(KeyCode.Space) && !onAttack && !onShot)//攻撃開始時(Spaceキーを押すと攻撃開始)
        {
            AttackEffect.gameObject.SetActive(true);
            time = 0.0f;
                
            AttackEffect.transform.position = this.transform.position + transform.up;
            onAttack = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !onAttack && !onShot)//攻撃開始時(Spaceキーを押すと攻撃開始)
        {
            if (onBottomColumn)
                ShotLimit = 3;
            else
                ShotLimit = 2;

            ShotEffect.gameObject.SetActive(true);
            time = 0.0f;

            ShotEffect.transform.position = this.transform.position;
            onShot = true;
        }



    }

        private void FixedUpdate()
        {
        if (onAttack)
        {
            time += 0.02f;
            //time *= Time.deltaTime;
            if (time >= EffectLimit)
            {
                AttackEffect.gameObject.SetActive(false);
                onAttack = false;
            }
        }

        if (onShot)
        {
            time += shotSpeed;

            Vector3 shotpos = ShotEffect.transform.position;

            shotpos += new Vector3(0, shotSpeed, 0);

            ShotEffect.transform.position = shotpos;

            //time *= Time.deltaTime;
            if (time >= ShotLimit)
            {
                ShotEffect.gameObject.SetActive(false);
                onShot = false;
            }
        }

    }
    }



