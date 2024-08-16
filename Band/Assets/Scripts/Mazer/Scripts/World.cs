using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class World : MonoBehaviour
{
    private Tilemap baseMap;
    private Tilemap level0;
    private Tilemap level1;

    [SerializeField]
    private Tile baseTile;

    public int Size { get; set; }



    private void Start()
    {
        baseMap = this.transform.GetChild(0).GetComponent<Tilemap>();
        level0 = this.transform.GetChild(1).GetComponent<Tilemap>();
        level1 = this.transform.GetChild(2).GetComponent<Tilemap>();
    }

    public void ClearAllTiles()
    {
        print("Base: " + baseMap);
        int halfSize = Size / 2;
        for (int i = -halfSize; i <= halfSize; i++)
            for (int j = -halfSize; j <= halfSize; j++)
                baseMap.SetTile(new Vector3Int(i, j, 0), baseTile);
        level0.ClearAllTiles();
        level1.ClearAllTiles();
    }

    public void SetTile(Vector3Int position, RuleTile tile, int level)
    {
        Tilemap levelMap = null;
        Tile t=new Tile();
        t.sprite = tile.m_DefaultSprite;
        if (level == 0) levelMap = level0;
        else levelMap = level1;
        levelMap.SetTile(position,t);
        baseMap.SetTile(position, null);
        tile.ReloadTile(position, levelMap);
    }

    public void SetTile(Vector3Int position, Tile tile, int level)
    {
        if (level == 0) level0.SetTile(position, tile);
        else level1.SetTile(position, tile);
        baseMap.SetTile(position, null);
    }
}
