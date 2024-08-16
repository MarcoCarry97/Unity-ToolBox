using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static Antlr4.Runtime.Atn.SemanticContext;
using static UnityEngine.RuleTile.TilingRuleOutput;

public static class RuleTileExtensions
{
   public static void RefreshTiles(this RuleTile tile,Vector3Int position, Tilemap tilemap)
    {

        Dictionary<Vector3Int,TileBase> neighbors= GetNeighbors(tile,position, tilemap);
        KeyValuePair<Vector3Int, TileBase> current = new KeyValuePair<Vector3Int, TileBase>(position, tile);
        UpdateCurrentTile(tile, current, tilemap);
        foreach (KeyValuePair<Vector3Int, TileBase> neighbor in neighbors)
            UpdateCurrentTile(tile, neighbor, tilemap);
        
    }

    private static Dictionary<Vector3Int,TileBase> GetNeighbors(RuleTile ruletile, Vector3Int position, Tilemap tilemap)
    {
        
        Dictionary<Vector3Int,TileBase> dict= new Dictionary<Vector3Int,TileBase>();
        
        int index = 0;
        foreach(Vector3Int pos in ruletile.neighborPositions)
        {
            if (pos.x == 0 && pos.y == 0)
                continue;
            Vector3Int neighPos = position + pos;
            Tile tile = tilemap.GetTile(neighPos) as Tile;
            dict[neighPos] = tile;
            index++;
        }

        return dict;
    }

    private static void UpdateCurrentTile(RuleTile tile, KeyValuePair<Vector3Int,TileBase> current, Tilemap tilemap)
    {
        
        Tile tilebase = current.Value as Tile;
        if (tilebase == null)
            return;
        else if (tilebase.sprite == null)
            return;
        //Tile newTile = GetTileFromRules(tile,current,tilemap);
        Tile newTile = GetTileFromDistance(tile,current,tilemap);
        if (newTile.sprite == null)
            newTile.sprite = tile.m_DefaultSprite;
        tilemap.SetTile(current.Key, newTile);
        Debug.Log("Current Pos: " + current.Key + " " + current.Value?.ToString());
    }

    private static Dictionary<Vector3Int,int> GetNeighborMatrix(RuleTile tile, KeyValuePair<Vector3Int,TileBase> current, Tilemap tilemap)
    {
        Dictionary<Vector3Int, int> dict = new Dictionary<Vector3Int, int>();
        foreach(Vector3Int pos in tile.neighborPositions)
        {
            if (pos.x == 0 && pos.y == 0)
                continue;
            Vector3Int position = current.Key + pos;
            int no = RuleTile.TilingRuleOutput.Neighbor.NotThis;
            int ok = RuleTile.TilingRuleOutput.Neighbor.This;
            dict[position] = tilemap.GetTile(position) == null ? no : ok;
        }
        return dict;
    }

    private static int IsThereATile(Vector3Int pos, Tilemap tilemap)
    {
        TileBase tile = tilemap.GetTile(pos);
        return tile == null ? RuleTile.TilingRuleOutput.Neighbor.NotThis : RuleTile.TilingRuleOutput.Neighbor.This;
        //return tile == null ? 0 : RuleTile.TilingRuleOutput.Neighbor.This;
    }

    private static Tile GetTileFromRules(RuleTile ruletile, KeyValuePair<Vector3Int,TileBase> current, Tilemap tilemap)
    {
        List<RuleTile.TilingRule> rules = ruletile.m_TilingRules;
        Dictionary<Vector3Int, int> currentDict = GetNeighborMatrix(ruletile, current, tilemap);
        Tile tile = new Tile();
        RuleTile.TilingRule rule=rules[0];
        bool end = false;
        for(int i=0;i<rules.Count && !end;i++)
        {
            RuleTile.TilingRule otherRule = rules[i];
            Dictionary<Vector3Int, int> ruleDict = ToDict(ruletile,current,otherRule);
            end = CompareMatrixes(currentDict,ruleDict);
            if (end)
                rule = otherRule;
        }
        tile.sprite = rule.m_Sprites[0];
        return tile;
    }

    private static bool CompareMatrixes(Dictionary<Vector3Int,int> current, Dictionary<Vector3Int,int> rule)
    {

        if(rule.Keys.Count !=current.Keys.Count)
            return false;
        foreach(Vector3Int key in  current.Keys)
        {
            if (!rule.ContainsKey(key))
                return false;
            else if (rule[key] != current[key])
                return false;
        }
        return true;
    }

    private static Dictionary<Vector3Int,int> ToDict(RuleTile tile,KeyValuePair<Vector3Int,TileBase> current, RuleTile.TilingRule rule)
    {
        Dictionary<Vector3Int, int> dict = new Dictionary<Vector3Int, int>();
        foreach(Vector3Int pos in tile.neighborPositions)
        {
            if (pos.x == 0 && pos.y == 0)
                continue;
            Vector3Int position = current.Key + pos;
            dict[position] = rule.m_Neighbors[rule.m_NeighborPositions.IndexOf(pos)];
        }
        return dict;
    }

    private static Tile GetTileFromDistance(RuleTile ruletile, KeyValuePair<Vector3Int, TileBase> current, Tilemap tilemap)
    {
        List<RuleTile.TilingRule> rules = ruletile.m_TilingRules;
        Dictionary<Vector3Int, int> currentDict = GetNeighborMatrix(ruletile, current, tilemap);
        Tile tile = new Tile();
        RuleTile.TilingRule bestRule = null;
        int distance = int.MaxValue;

        for(int i = 0; i < rules.Count; i++)
        {
            RuleTile.TilingRule rule= rules[i];
            Dictionary<Vector3Int, int>  ruleDict = ToDict(ruletile,current, rule);
            int dist = ComputeDistance(currentDict,ruleDict);
            if(dist<distance)
            {
                bestRule = rule;
                distance = dist;
            }
        }
        tile.sprite = bestRule.m_Sprites[0];
        return tile;
    }

    private static int ComputeDistance(Dictionary<Vector3Int,int> current, Dictionary<Vector3Int,int> rule)
    {

        int distance = current.Keys.Count != rule.Keys.Count ? 1 : 0;
        foreach(Vector3Int key in current.Keys)
        {
            if (!rule.ContainsKey(key))
                distance += 1;
            else if (current[key] != rule[key])
                distance += 1;
        }
        return distance;
    }

    public static void ReloadTile(this RuleTile ruletile, Vector3Int position, Tilemap tilemap)
    {
        UpdateTile(ruletile,position,tilemap);
        foreach (Vector3Int neighPos in ruletile.neighborPositions)
            UpdateTile(ruletile, position + neighPos, tilemap);
    }

    private static void UpdateTile(RuleTile ruletile, Vector3Int position, Tilemap tilemap)
    {
        if (tilemap.GetTile(position) == null)
            return;
        int dontCare = 0;
        int no = RuleTile.TilingRuleOutput.Neighbor.NotThis;
        int ok = RuleTile.TilingRuleOutput.Neighbor.This;
        Tile newTile = new Tile();
        newTile.sprite = ruletile.m_DefaultSprite;
        for(int j=0;j<ruletile.m_TilingRules.Count;j++)
        {
            RuleTile.TilingRule rule = ruletile.m_TilingRules[j];
            bool noProblem = true;
            for(int i=0;i<rule.m_NeighborPositions.Count && noProblem;i++)
            {
                Vector3Int neighPos = rule.m_NeighborPositions[i];
                int neighIndex = rule.m_NeighborPositions.IndexOf(neighPos);
                int neighStatus= rule.m_Neighbors[neighIndex];
                TileBase oldTile = tilemap.GetTile(position + neighPos);
                if (neighStatus != dontCare)
                {
                    if (neighStatus == no)
                    {
                        if (oldTile != null)
                            noProblem = false;
                    }
                    else if (oldTile == null)
                        noProblem = false;
                }  
            }
            if(noProblem)
            {
                newTile.sprite = rule.m_Sprites[0];
            }
        }
        tilemap.SetTile(position, newTile);
    }
    
}
