using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class DungeonMaker : MonoBehaviour
{
    [Range(2,100)]
    [SerializeField]
    private int numLevels;

    [Range(1,10)]
    [SerializeField]
    private int numRooms;

    [Range(3,10)]
    [SerializeField]
    private int maxRoomSize;

    [Range(1, 5)]
    [SerializeField]
    private int corridorSize;

    [Range(1,50)]
    [SerializeField]
    private int distanceBetweenRooms;

    [Range(1,10)]
    [SerializeField]
    private int maxPathLength;

    [Range(0, 10)]
    [SerializeField]
    private int numTreasures;

    [Range(1, 1000)]
    [SerializeField]
    private int spaceSize;
    
    [Range(0,10)]
    [SerializeField]
    private int numTraps;

    [Range(0, 10)]
    [SerializeField]
    private int numItems;

    [SerializeField]
    private bool randomStart;

    public DungeonData Dungeon { get; private set; }

    private World tilemap;

    [SerializeField]
    private RuleTile tile;

    [SerializeField]
    private Tile trapTile;

    [SerializeField]
    private Tile stairsTile;

    [SerializeField]
    private Tile treasurePrefab;

    [SerializeField]
    private Tile keyPrefab;

    [SerializeField]
    private Tile enemySpawnTile;

    public IEnumerator Generate()
    {
        Process process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = GetArgs(),
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                WorkingDirectory = $"{Application.dataPath}/Clingo/Generator"
            }
        };
        process.Start();
        yield return new WaitUntil(() => !process.HasExited);
        string result = process.ReadStandardOutput();
        string error = process.ReadStandardError();
        UnityEngine.Debug.LogError(error);
        print("Exit Code: "+process.ExitCode);
        print("Result: " + result);
        DungeonData dungeon = JsonConvert.DeserializeObject<DungeonData>(result);
        print(dungeon.ToString());
        Dungeon = dungeon;
        Build(0);
        
    }

    public string GetArgs()
    {
        if (maxRoomSize <= corridorSize)
            maxRoomSize = corridorSize + 3;
        string execPath = $"{Application.dataPath}/Clingo/Generator/maker_main.py";
        string res=$"{execPath} " +
            $"--levels={numLevels} " +
            $"--rooms={numRooms} " +
            $"--size={maxRoomSize} " +
            $"--path={maxPathLength} " +
            $"--distance={distanceBetweenRooms} " +
            $"--space={spaceSize} " +
            $"--num_trap={numTraps} " +
            $"--num_treasure={numTreasures} " +
            $"--num_item={numItems} "+
            $"--corr_size={corridorSize} ";
        res += randomStart ? " --rand_init" : "";
        print(res);
        return res;
    }

    public void Build(int index)
    {
        tilemap.ClearAllTiles();
        List<RoomData> visited = new List<RoomData>();
        LevelData level = Dungeon.Levels[index];
        RoomData initRoom = level.GetRoom(level.Init_Room);
        int x = initRoom.Center.X;
        int y = initRoom.Center.Y;
        RecursiveBuild(level, initRoom, x, y, visited);
    }

    private void Start()
    {
        tilemap=GameObject.FindAnyObjectByType<World>();
    }

    private int GetMaxRoomSize(DungeonData data, int index)
    {
        LevelData level = data.Levels[index];
        int distance = Mathf.Max(level.Rooms[0].Size.X, level.Rooms[0].Size.Y);
        foreach (RoomData room in level.Rooms)
        {
            int max = Mathf.Max(room.Size.X, room.Size.Y);
            if (max > distance)
                distance = max;
        }
        return distance;
    }

    private void RecursiveBuild(LevelData level, RoomData room, int x, int y, List<RoomData> visited)
    {
        if (!visited.Contains(room))
        {
            print("Room: " + room.Id);
            visited.Add(room);
            DrawRoomTiles(room,level);
            foreach (DoorData door in level.GetDoorsOfRoom(room))
            {
                DrawCorridorTiles(level, door);
                (int, int) up = door.GetOrientationValues(5);
                print(up);
                int upX = up.Item1;
                int upY = up.Item2;
                RoomData dest = level.GetRoom(door.End);
                RecursiveBuild(level, dest, x + upX, y + upY, visited);
            }
        }
    }

    private void DrawRoomTiles(RoomData room, LevelData level)
    {
        int halfSizeX = room.Size.X / 2;
        int halfSizeY = room.Size.Y / 2;
        int subtractor = (corridorSize % 2 == 0 && room.Size.X==corridorSize && room.Size.Y==corridorSize) ? 1 : 0;
        for (int i = -halfSizeX; i <= halfSizeX; i++)
            for (int j = -halfSizeY; j <= halfSizeY; j++)
            {
                int x = room.TrueCenter.X + i - subtractor;
                int y = room.TrueCenter.Y + j - subtractor;
                Vector3Int pos = new Vector3Int(x, y);
                tilemap.SetTile(pos,tile,0);
            }
        DrawDecorations(room, level);
    }

    private void DrawDecorations(RoomData room, LevelData level)
    {
        foreach(DecorationData dec in level.GetDecorationsOfRoom(room))
        {
            int x = dec.Position.X + room.TrueCenter.X - room.Size.X / 2;
            int y = dec.Position.Y + room.TrueCenter.Y - room.Size.Y / 2;
            Vector3Int pos=new Vector3Int(x,y);
            string type = dec.Type;
            Tile typeTile;
            if (type.Equals("trap"))
                typeTile = trapTile;
            else if (type.Equals("treasure"))
                typeTile = treasurePrefab;
            else if (type.Equals("enemy"))
                typeTile = enemySpawnTile;
            else typeTile = keyPrefab;
            tilemap.SetTile(pos, typeTile, 1);
        }
    }

    private void DrawCorridorTiles(LevelData level, DoorData door)
    {
        RoomData start = level.GetRoom(door.Start);
        RoomData end = level.GetRoom(door.End);
        (int, int) values = door.GetOrientationValues(distanceBetweenRooms);
        int increment = (end.Center.X - start.Center.X + end.Center.Y - start.Center.Y) / distanceBetweenRooms;
        if (door.Orientation.Equals("north") || door.Orientation.Equals("south"))
            for (int i = start.Center.Y; i != end.Center.Y; i += increment)
                for(int j=0;j<corridorSize;j++)
                    AddTile(tile, start.Center.X+j-corridorSize/2, i);
        else for (int i = start.Center.X; i != end.Center.X; i += increment)
                for (int j = 0; j < corridorSize; j++)
                    AddTile(tile, i, start.Center.Y+j-corridorSize/2);
    }

    private void AddTile(RuleTile tile, int x, int y)
    {
        Vector3Int pos = new Vector3Int(x, y);
        tilemap.SetTile(pos, tile,0);
    }

    private void AddTile(Tile tile, int x, int y)
    {
        Vector3Int pos = new Vector3Int(x, y);
        tilemap.SetTile(pos, tile, 0);
    }
}
