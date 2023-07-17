using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class RuleTileExtensions
{
    public static void Refresh(this RuleTile ruletile,Vector3Int position, Tilemap tilemap)
    {
        ruletile.RefreshTile(position, tilemap);

        Tilemap baseTilemap = tilemap.GetComponent<Tilemap>();

        //ruletile.ReleaseDestroyedTilemapCacheData(); // Prevent memory leak

        //if (ruletile.IsTilemapUsedTilesChange(baseTilemap, out var neighborPositionsSet))
         //   neighborPositionsSet = ruletile.CachingTilemapNeighborPositions(baseTilemap);

        var neighborPositionsRuleTile = ruletile.neighborPositions;
        foreach (Vector3Int offset in neighborPositionsRuleTile)
        {
            Vector3Int offsetPosition = ruletile.GetOffsetPositionReverse(position, offset);
            TileBase tile = tilemap.GetTile(offsetPosition);
            RuleTile ruleTile = null;

            if (tile is RuleTile rt)
                ruleTile = rt;
            else if (tile is RuleOverrideTile ot)
                ruleTile = ot.m_Tile;

            if (ruleTile != null)
                if (ruleTile == ruletile || ruleTile.neighborPositions.Contains(offset))
                    ruletile.Refresh(offsetPosition, tilemap);
        }

    }

}
