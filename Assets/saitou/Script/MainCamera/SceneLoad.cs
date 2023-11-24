using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//ManagerSceneロードクラス
public class SceneLoad : MonoBehaviour
{
    private static bool Loaded { get; set; }

    void Awake()
    {
        if (Loaded) return;

        Loaded = true;
        SceneManager.LoadScene("ManagerScene", LoadSceneMode.Additive);
    }

}
