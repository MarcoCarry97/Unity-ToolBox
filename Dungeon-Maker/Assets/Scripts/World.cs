using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class World : MonoBehaviour
{
    private Tilemap level0;
    private Tilemap level1;

    private void Start()
    {
        level0 = this.transform.GetChild(0).GetComponent<Tilemap>();
        level1 = this.transform.GetChild(1).GetComponent<Tilemap>();
    }

    public void ClearAllTiles()
    {
        level0.ClearAllTiles();
        level1.ClearAllTiles();
    }

    public void SetTile(Vector3Int position, Tile tile, int level)
    {
        if (level == 0) level0.SetTile(position, tile);
        else level1.SetTile(position, tile);
    }
}
