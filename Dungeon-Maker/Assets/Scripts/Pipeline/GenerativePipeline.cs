using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;
using Newtonsoft.Json;
using System.Linq;

public class GenerativePipeline : MonoBehaviour
{
    [SerializeField]
    private List<GenerativePipelinePhase> phases;

    private string output;

    private Tilemap tilemap;

    [SerializeField]
    private Tile tile;

    [SerializeField]
    private Tile centerTile;

    [SerializeField]
    [Range(5,10)]
    private int distanceBetweenRooms;

    private int distance;

    private List<DungeonData> dungeons;

    private int current = -1;

    public bool Next { get; set; }

    public string Output
    {
        get
        {
            string val = output;
            output = null;
            return output;
        }
    }

    private Coroutine buildCoroutine;

    private void CreatePhaseJobs()
    {
        for(int i=0; i<phases.Count; i++)
        {
            StartCoroutine((PhaseJob(i)));
        }
    }

    private IEnumerator PhaseJob(int index)
    {
        
        GenerativePipelinePhase phase = phases[index];
        bool end = false;
        while(!end)
        {
            string input = "";
            yield return StartCoroutine(phase.Compute(input));
            yield return new WaitUntil(() => phase.Output!=null);
            output= phase.Output;
            DungeonData dungeon = JsonConvert.DeserializeObject<DungeonData>(output);
            dungeons.Add(dungeon);
            phase.Output = null;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        dungeons = new List<DungeonData>();
        tilemap = GameObject.FindObjectOfType<Tilemap>();
        if (phases.Count!=0)
        {
            CreatePhaseJobs();
        }
    }

    // Update is called once per frame
    void Update()
    {
        print("Update");
        string model = output;
        if(model!=null && buildCoroutine==null && Next)
        {
            print("Create dungeon");
            buildCoroutine = StartCoroutine(BuildDungeon(model));
            output = null;
        }
    }

    private IEnumerator BuildDungeon(string model)
    {
        
        
        current = (current + 1) % dungeons.Count;
        DungeonData dungeon = dungeons[current];
        GenerateTiles(dungeon,0);
        yield return null;
    }

    private void GenerateTiles(DungeonData data, int index)
    {
        distance = GetMaxRoomSize(data, index)*2;
        List<RoomData> visited = new List<RoomData>();
        LevelData level = data.Levels[index];
        RoomData initRoom = level.GetRoom(level.Init_Room);
        int x = initRoom.Center.X;
        int y = initRoom.Center.Y;
        RecursiveGeneration(level, initRoom, x, y, visited);
        //Next = false;
        buildCoroutine = null;
    }

    private int GetMaxRoomSize(DungeonData data, int index)
    {
        LevelData level = data.Levels[index];
        int distance = Mathf.Max(level.Rooms[0].Size.X, level.Rooms[0].Size.Y);
        foreach(RoomData room in level.Rooms)
        {
            int max=Mathf.Max(room.Size.X,room.Size.Y);
            if (max > distance)
                distance = max;
        }
        return distance;
    }

    private void RecursiveGeneration(LevelData level, RoomData room, int x, int y, List<RoomData> visited)
    {
        if (!visited.Contains(room))
        {
            print("Room: " + room.Id);
            visited.Add(room);
            DrawRoomTiles(room);
            foreach(DoorData door in level.GetDoorsOfRoom(room))
            {
                DrawCorridorTiles(level,door);
                (int, int) up = door.GetOrientationValues(distance);
                print(up);
                int upX = up.Item1;
                int upY = up.Item2;
                RoomData dest = level.GetRoom(door.End);
                RecursiveGeneration(level, dest, x + upX, y + upY, visited);
            }
        }
    }

    private void DrawRoomTiles(RoomData room)
    {
        int halfSizeX = room.Size.X / 2;
        int halfSizeY = room.Size.Y / 2;
        for(int i=-halfSizeX;i<=halfSizeX;i++)
            for(int j = -halfSizeY; j <= halfSizeY; j++)
            {
                int x = room.Center.X + i;
                int y = room.Center.Y + j;
                Vector3Int pos = new Vector3Int(x,y);
                tilemap.SetTile(pos, tile);
            }
    }

    private void DrawCorridorTiles(LevelData level,DoorData door)
    {
        RoomData start=level.GetRoom(door.Start);
        RoomData end=level.GetRoom(door.End);
        (int,int) values=door.GetOrientationValues(distance);
        int increment=(end.Center.X - start.Center.X + end.Center.Y -start.Center.Y)/5;
        if (door.Orientation.Equals("north") || door.Orientation.Equals("south"))
            for (int i = start.Center.Y; i != end.Center.Y; i += increment)
                AddTile(tile, start.Center.X, i);
        else for (int i = start.Center.X; i != end.Center.X; i += increment)
            AddTile(tile, i, start.Center.Y);
    }
   
    private void AddTile(Tile tile,int x, int y)
    {
        Vector3Int pos = new Vector3Int(x,y);
        tilemap.SetTile(pos, tile);
    }

}
