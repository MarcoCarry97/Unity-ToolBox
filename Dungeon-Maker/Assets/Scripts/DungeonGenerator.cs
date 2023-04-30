using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Runtime.ExceptionServices;
using UnityEngine.Tilemaps;
using System.Text.RegularExpressions;
using static Unity.Burst.Intrinsics.X86.Avx;
using System;

public class DungeonGenerator : MonoBehaviour
{
    [SerializeField]
    private TileBase tile;

    private Coroutine genCoroutine;

    private void Start()
    {
        Generate("pcg2.lp", 5);
    }

    private void Generate(string fileName, int numSamples)
    {
        if (genCoroutine == null)
            genCoroutine = StartCoroutine(GenCoroutine(fileName, numSamples));
    }

    private IEnumerator GenCoroutine(string fileName, int numSamples)
    {

        string args = string.Format("{0}/Clingo/{1} {2}", Application.dataPath, fileName, numSamples);
        string filePath = string.Format("{0}/Clingo/Generator/main.py", Application.dataPath);
        args = $"{filePath} {args}";
        Process process = new Process
        {

            StartInfo = new ProcessStartInfo
            {
                FileName = "python",
                //Arguments= "C:\\Users\\marco\\Desktop\\pcg.lp 2 --verbose=0 --outf=2",
                Arguments = args,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };
        process.Start();
        yield return new WaitUntil(() => !process.HasExited);
        string res = "";
        while (!process.StandardOutput.EndOfStream)
        {
            string piece = process.StandardOutput.ReadLine();
            res += piece;
        }

        DungeonData data = JsonConvert.DeserializeObject<DungeonData>(res);

        GenerateTiles(data);


    }

    private void GenerateTiles(DungeonData data)
    {
        bool[] visited = new bool[data.Levels[0].Rooms.Count];
        foreach(RoomData room in data.Levels[0].Rooms)
        {
            RecursiveGeneration(data.Levels[0],room, visited);
        }
    }

    private void RecursiveGeneration(LevelData level,RoomData room, bool[] visited)
    {
        if (!visited[room.Id])
        {
            
            DrawTiles(level, room);
            List<DoorData> doors = level.GetDoorsOfRoom(room);
            foreach(DoorData d in doors)
            {
                print("Next: "+d.End);
            }
            foreach (DoorData door in doors)
            {
                print("Next");
                RoomData nextRoom = level.GetRoom(door.End);
                print("Door to " + door.End);
                RecursiveGeneration(level, nextRoom, visited);
            }
            visited[room.Id] = true;
        }
    }

    private void DrawTiles(LevelData level,RoomData room)
    {
        Tilemap tilemap=GameObject.FindObjectOfType<Tilemap>();
        Vector3Int pos = new Vector3Int(room.Center.X, room.Center.Y);
        tilemap.SetTile(pos, tile);
    }
}