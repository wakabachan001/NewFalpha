using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Next_Stage : MonoBehaviour
{
    public string NextStage;
    public string targetName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("�^�C���ɐڐG");
        if (collision.gameObject.name == targetName) 
        {
            Debug.Log("�V�[���؂�ւ��܂�");
            SceneManager.LoadScene(NextStage);
            Debug.Log("�V�[���؂�ւ���sita");
        }

    }
}
