using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//ManagerSceneのスクリプト参照ユーティリティクラス
public static class LoadManagerScene
{
    //ManagerSceneの取得
    static Scene scene = SceneManager.GetSceneByName("ManagerScene");

    //ItemManager取得関数
    public static ItemManager GetItemManager()
    {
        scene = SceneManager.GetSceneByName("ManagerScene");

        //ヒエラルキーの最上位のオブジェクトが取得できる
        foreach (var rootGameObject in scene.GetRootGameObjects())
        {
            return rootGameObject.GetComponent<ItemManager>();   
        }
        return null;
    }
    //PlayerItemManager取得関数
    public static PlayerItemManager GetPlayerItemManager()
    {
        scene = SceneManager.GetSceneByName("ManagerScene");

        //ヒエラルキーの最上位のオブジェクトが取得できる
        foreach (var rootGameObject in scene.GetRootGameObjects())
        {
            return rootGameObject.GetComponent<PlayerItemManager>();
        }
        return null;
    }
    //PlayerStatusManager取得関数
    public static PlayerStatusManager GetPlayerStatusManager()
    {
        scene = SceneManager.GetSceneByName("ManagerScene");

        //ヒエラルキーの最上位のオブジェクトが取得できる
        foreach (var rootGameObject in scene.GetRootGameObjects())
        {
            return rootGameObject.GetComponent<PlayerStatusManager>();
        }
        return null;
    }
    //ItemIcon取得関数
    public static ItemIcon GetItemIcon()
    {
        scene = SceneManager.GetSceneByName("ManagerScene");

        //ヒエラルキーの最上位のオブジェクトが取得できる
        foreach (var rootGameObject in scene.GetRootGameObjects())
        {
            return rootGameObject.GetComponent<ItemIcon>();
        }
        return null;
    }
}