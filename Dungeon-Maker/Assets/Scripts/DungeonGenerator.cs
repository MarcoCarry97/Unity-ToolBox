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

    Tilemap tilemap;

    private Coroutine genCoroutine;

    private DungeonData dungeon;

    private int dungeonIndex;

    public bool Next { get; set; }

    private void Start()
    {
        tilemap= GameObject.FindObjectOfType<Tilemap>();
        Next = false;
        dungeonIndex= 0;
        Generate("gen_room.lp", 5);
    }

    private void Update()
    {
        if(Next)
        {
            tilemap.ClearAllTiles();
            GenerateTiles(dungeon, dungeonIndex);
            dungeonIndex = (dungeonIndex + 1) % dungeon.Levels.Count;
        }
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
                //Arguments= "C:\\Users\\marco\\Desktop\\gen_room.lp 2 --verbose=0 --outf=2",
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
        print(res);
        dungeon = JsonConvert.DeserializeObject<DungeonData>(res);
        print(dungeon);
        Next = true;

    }

    private void GenerateTiles(DungeonData data,int index)
    {
        List<RoomData> visited = new List<RoomData>();
        LevelData level = data.Levels[index];
        RoomData initRoom = level.GetRoom(level.Init_Room);
        int x = initRoom.Center.X;
        int y = initRoom.Center.Y;
        RecursiveGeneration(level,initRoom,x,y,visited);
        Next= false;
    }

    private void RecursiveGeneration(LevelData level,RoomData room, int x, int y, List<RoomData> visited)
    {
        if (!visited.Contains(room))
        {
            print("Generating " + room.Id);
            DrawRoomTiles(level, room,x,y);
            visited.Add(room);
            List<DoorData> doors = level.GetDoorsOfRoom(room);
            foreach (DoorData door in doors)
            {
                DrawDoorTiles(x, y, room, door);
                (int,int) ret=UpdateCoords(level,door);
                int upX = ret.Item1;
                int upY =ret.Item2;
                RoomData nextRoom = level.GetRoom(door.End);
                RecursiveGeneration(level, nextRoom,x+upX,y+upY, visited);
                
            }

            
        }
    }

    private (int,int) UpdateCoords(LevelData level,DoorData door)
    {
        RoomData r1= level.GetRoom(door.Start);
        RoomData r2 = level.GetRoom(door.End);
        (int,int) muls=door.GetOrientationValues();
        int mulX=muls.Item1, mulY=muls.Item2;
        int addendX = (1+(r1.Size.X + r2.Size.X)/2)*mulX;
        int addendY = (1+(r1.Size.Y + r2.Size.Y)/2)*mulY;
        return (addendX,addendY);
    }

    private void DrawRoomTiles(LevelData level,RoomData room,int x, int y)
    {
        int sizeX = room.Size.X/2;
        int sizeY = room.Size.Y/2;
        //Vector3Int pos = new Vector3Int(x, y);
        //tilemap.SetTile(pos, tile);
        for(int i=-sizeX;i<=sizeX;i++)
            for(int j=-sizeY;j<=sizeY;j++)
            {
                Vector3Int pos=new Vector3Int(x+i,y+j);
                //Vector3Int pos=new Vector3Int(room.Center.X,room.Center.Y);
                
                tilemap.SetTile(pos, tile);
            }
        List<DoorData> doors = level.GetDoorsOfRoom(room);
        foreach(DoorData door in doors)
        {
            DrawDoorTiles(x,y,room,door);
        }
    }

    private void DrawDoorTiles(int x,int y,RoomData room,DoorData door)
    {
        (int,int) doorVals=door.GetOrientationValues();
        int doorX=x+(1+room.Size.X/2)*doorVals.Item1;
        int doorY=y+(1+room.Size.Y/2)*doorVals.Item2;
        Vector3Int pos=new Vector3Int(doorX,doorY);
        tilemap.SetTile(pos,tile);
    }
}