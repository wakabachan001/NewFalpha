using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    GameObject Player;
    public  float movelength = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;

        if(Input.GetKeyDown("left"))
        {
            position.x -= movelength;
        }
        if(Input.GetKeyDown("right"))
        {
            position.x += movelength;
        }
        if (Input.GetKeyDown("down"))
        {
            position.y -= movelength;
        }
        if (Input.GetKeyDown("up"))
        {
            position.y += movelength;
        }

        transform.position = position;
    }
}
