using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEnemy : MonoBehaviour
{
    public Transform object1;
    public Transform object2;
    // Start is called before the first frame update
    void Swap()
    {
        Vector3 tempPos = object1.position;
        object1.position = object2.position;
        object2.position = tempPos;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Swap();
        }
    }
}
