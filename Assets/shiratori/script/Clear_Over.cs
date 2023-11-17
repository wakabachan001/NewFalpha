using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear_Over : MonoBehaviour
{
    public void GameClear()
    {
        SceneManager.LoadScene("GameClear");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
