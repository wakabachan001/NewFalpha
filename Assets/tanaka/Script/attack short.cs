using UnityEngine;
using UnityEngine.Tilemaps;

public class attackshort : MonoBehaviour
{
    public Tilemap tilemap;

    public TileBase attackTile;

    public void
        Setattackshort(Vector3Int currentPosition)
    {
        
        //マス指定
        Vector3Int upTile    = currentPosition    + new Vector3Int( 0, 1, 0);
        Vector3Int leftTile  = currentPosition    + new Vector3Int(-1, 1, 0);
        Vector3Int rightTile = currentPosition    + new Vector3Int( 1, 1, 0);

        tilemap.SetTile(rightTile, attackTile);

    }

    private void FixedUpdate()
    {
        if
        (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }

    }

    void Attack()
    {


    }
}

//このマスに触れた敵を排除


//追加要素
//当たり判定
//攻撃モーション
