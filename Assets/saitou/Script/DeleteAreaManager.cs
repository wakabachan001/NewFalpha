using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAreaManager : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        //���Ƃ̐ڐG
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("�G�̍폜");

            Destroy(other);
        }
    }
}
