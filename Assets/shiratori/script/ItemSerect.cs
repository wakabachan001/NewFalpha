using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSerect : MonoBehaviour
{
    // ItemSelectUIを格納する変数
    // インスペクターからゲームオブジェクトを設定する
    [SerializeField] GameObject ItemSelectUI;
    [SerializeField] GameObject[] Item = new GameObject[12];

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            ActiveItemSelectUI();
        }
    }

    public void ActiveItemSelectUI()
    {
        ItemSelectUI.SetActive(true);

        // ランダムに3つのアイテムをアクティブにする
        ActivateRandomItems(3);
    }

    void ActivateRandomItems(int count)
    {
        // すべてのアイテムを非アクティブにする
        foreach (var item in Item)
        {
            item.SetActive(false);
        }

        List<int> selectedIndices = new List<int>();

        //ランダムなアイテムを選択
        while (selectedIndices.Count < count)
        {
            int randomIndex = Random.Range(0, Item.Length);
            if (!selectedIndices.Contains(randomIndex))
            {
                selectedIndices.Add(randomIndex);
            }
        }

        // 選択されたアイテムをアクティブにする
        foreach (int index in selectedIndices)
        {
            if (index >= 0 && index < Item.Length)
            {
                Item[index].SetActive(true);
            }
        }
    }
}
