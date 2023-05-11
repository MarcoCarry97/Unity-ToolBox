using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;
using Newtonsoft.Json;

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
            if(index>0)
            {
                yield return new WaitUntil(() => phases[index-1].IsOutputAvailable());
                var s=phases[index - 1].Output;
                //input = phases[index - 1].Output;
            }
            yield return StartCoroutine(phase.Compute(input));
            if(index<phases.Count-1)
                yield return phases[index+1].IsFinished();
            else
            {
                yield return new WaitUntil(() => phase.Output!=null);
                output= phase.Output;
                phase.Output = null;
            }
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
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
        if(model!=null && buildCoroutine==null)
        {
            print("Create dungeon");
            buildCoroutine = StartCoroutine(BuildDungeon(model));
            output = null;
        }
    }

    private IEnumerator BuildDungeon(string model)
    {
        DungeonData dungeon=JsonConvert.DeserializeObject<DungeonData>(model);
        GenerateTiles(dungeon,0);
        yield return null;
    }

    private void GenerateTiles(DungeonData data, int index)
    {
        List<RoomData> visited = new List<RoomData>();
        LevelData level = data.Levels[index];
        RoomData initRoom = level.GetRoom(level.Init_Room);
        int x = initRoom.Center.X;
        int y = initRoom.Center.Y;
        RecursiveGeneration(level, initRoom, x, y, visited);
        //Next = false;
        buildCoroutine = null;
    }

    private void RecursiveGeneration(LevelData level, RoomData room, int x, int y, List<RoomData> visited)
    {
        if (!visited.Contains(room))
        {
            print("Generating " + room.Id);
            DrawRoomTiles(level, room, x, y);
            visited.Add(room);
            List<DoorData> doors = level.GetDoorsOfRoom(room);
            foreach (DoorData door in doors)
            {
                DrawDoorTiles(x, y, room, door);
                (int, int) ret = UpdateCoords(level, door);
                int upX = ret.Item1*distanceBetweenRooms;
                int upY = ret.Item2*distanceBetweenRooms;
                RoomData nextRoom = level.GetRoom(door.End);
                RecursiveGeneration(level, nextRoom, x + upX, y + upY, visited);

            }


        }
    }

    private (int, int) UpdateCoords(LevelData level, DoorData door)
    {
        RoomData r1 = level.GetRoom(door.Start);
        RoomData r2 = level.GetRoom(door.End);
        (int, int) muls = door.GetOrientationValues();
        int mulX = muls.Item1, mulY = muls.Item2;
        int addendX = (1 + (r1.Size.X + r2.Size.X) / 2) * mulX;
        int addendY = (1 + (r1.Size.Y + r2.Size.Y) / 2) * mulY;
        return (addendX, addendY);
    }

    private void DrawRoomTiles(LevelData level, RoomData room, int x, int y)
    {
        int sizeX = room.Size.X / 2;
        int sizeY = room.Size.Y / 2;
        //Vector3Int pos = new Vector3Int(x, y);
        //tilemap.SetTile(pos, tile);
        for (int i = -sizeX; i <= sizeX; i++)
            for (int j = -sizeY; j <= sizeY; j++)
            {
                Vector3Int pos = new Vector3Int(x + i, y + j);
                //Vector3Int pos=new Vector3Int(room.Center.X,room.Center.Y);

                tilemap.SetTile(pos, tile);
            }
        Vector3Int center = new Vector3Int(x,y);
        tilemap.SetTile(center, centerTile);
        /*List<DoorData> doors = level.GetDoorsOfRoom(room);
        foreach (DoorData door in doors)
        {
            DrawDoorTiles(x, y, room, door);
        }*/
    }

    private void DrawDoorTiles(int x, int y, RoomData room, DoorData door)
    {
        (int, int) doorVals = door.GetOrientationValues();
        for(int i=0;i<distanceBetweenRooms*2;i++)
        {
            int doorX = x + (i + room.Size.X / 2) * doorVals.Item1;
            int doorY = y + (i + room.Size.Y / 2) * doorVals.Item2;
            Vector3Int pos = new Vector3Int(doorX, doorY);
            tilemap.SetTile(pos, tile);
        }
    }
}
