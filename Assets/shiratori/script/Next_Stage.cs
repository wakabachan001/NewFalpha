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
        Debug.Log("タイルに接触");
        if (collision.gameObject.name == targetName) 
        {
            Debug.Log("シーン切り替えます");
            SceneManager.LoadScene(NextStage);
            Debug.Log("シーン切り替えまsita");
        }

    }
}
