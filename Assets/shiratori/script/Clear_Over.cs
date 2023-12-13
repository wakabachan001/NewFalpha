using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clear_Over : MonoBehaviour
{
    public bool isGameOver ;
    public bool isGameClear ;

    private void Start()
    {
        isGameOver = false;
        isGameClear = false;
    }

    public void GameClear()
    {
        DelayGameClear();
    }

    public void GameOver()
    {
        DelayGameOver();
    }

    public IEnumerator DelayGameOver()
    {
        isGameOver = true;
        yield return new WaitForSeconds(10.0f);
        SceneManager.LoadScene("GameOver");
    }

    public IEnumerator DelayGameClear()
    {
        isGameClear = true;
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("GameClear");
    }
}
