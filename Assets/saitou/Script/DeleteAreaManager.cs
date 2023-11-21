using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAreaManager : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        //Œ•‚Æ‚ÌÚG
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("“G‚Ìíœ");

            Destroy(other);
        }
    }
}
