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

    [SerializeField]
    [Range(100, 1000)]
    private int size;

    private void Start()
    {
        baseMap = this.transform.GetChild(0).GetComponent<Tilemap>();
        level0 = this.transform.GetChild(1).GetComponent<Tilemap>();
        level1 = this.transform.GetChild(2).GetComponent<Tilemap>();
    }

    public void ClearAllTiles()
    {
        int halfSize = size / 2;
        for (int i = -halfSize; i <= halfSize; i++)
            for (int j = -halfSize; j <= halfSize; j++)
                baseMap.SetTile(new Vector3Int(i, j, 0), baseTile);
        level0.ClearAllTiles();
        level1.ClearAllTiles();
    }

    public void SetTile(Vector3Int position, RuleTile tile, int level)
    {
        RuleTile t = ScriptableObject.Instantiate<RuleTile>(tile);
        Tilemap levelMap = null;
        if (level == 0) levelMap = level0;
        else levelMap = level1;
        levelMap.SetTile(position, t);
        baseMap.SetTile(position, null);
        //t.Refresh(position, levelMap);
    }

    public void SetTile(Vector3Int position, Tile tile, int level)
    {
        if (level == 0) level0.SetTile(position, tile);
        else level1.SetTile(position, tile);
        baseMap.SetTile(position, null);
    }
}
