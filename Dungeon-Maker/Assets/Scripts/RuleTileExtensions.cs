using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class RuleTileExtensions
{

    public static void SetRuleTile(this Tilemap tilemap, Vector3Int position, RuleTile tile)
    {
        List<RuleTile.TilingRule> rules = tile.m_TilingRules;
        tilemap.SetTile(position, tile);
        for(int i=-1; i<=1;i++)
        {
            for(int j=-1; j<=1;j++)
            {
                Vector3Int otherPos= new Vector3Int(position.x + i, position.y + j, position.z);
                if(tilemap.HasTile(otherPos))
                {
                    TileBase otherTile= tilemap.GetTile(otherPos);
                    foreach(RuleTile.TilingRule rule in rules)
                    {
                        Vector3Int pos = new Vector3Int(i, j, 0);
                        var dict = rule.GetNeighbors();
                        Debug.Log(dict.ToString());
                        if(dict.ContainsKey(pos))
                        {
                            int neighbor = dict[pos];
                            if (!tile.RuleMatch(neighbor, otherTile))
                            {
                                Tile newTile = new Tile();
                                newTile.sprite = rule.m_Sprites[0];
                                tilemap.SetTile(otherPos, newTile);
                            }
                            else break;
                        }
                    }
                }
            }
        }
    }
}
