using Mazer.Data;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace Mazer.Generators
{
    public class DungeonMaker : MonoBehaviour
    {
        [Range(2, 100)]
        [SerializeField]
        private int numLevels;

        public int NumberOfLevels { get { return numLevels; } }

        [Range(1, 10)]
        [SerializeField]
        private int numRooms;

        public int NumberOfRoomsPerLevel { get { return numRooms; } }

        [Range(3, 10)]
        [SerializeField]
        private int maxRoomSize;

        public int MaxRoomSize { get { return maxRoomSize; } }

        [Range(1, 5)]
        [SerializeField]
        private int corridorSize;

        public int CorridorSize { get { return corridorSize; } }

        [Range(1, 50)]
        [SerializeField]
        private int distanceBetweenRooms;

        public int DistanceBetweenRooms { get { return distanceBetweenRooms; } }

        [Range(1, 10)]
        [SerializeField]
        private int maxPathLength;

        public int MaxPathLength { get { return maxPathLength; } }

        [Range(0, 10)]
        [SerializeField]
        private int numTreasures;

        [Range(1, 1000)]
        [SerializeField]
        private int spaceSize;

        public int SpaceSize { get { return spaceSize; } }

        [Range(0, 10)]
        [SerializeField]
        private int numTraps;

        [Range(0, 10)]
        [SerializeField]
        private int numItems;

        public int NumberOfItemsPerRoom { get { return numLevels; } }

        [SerializeField]
        [Range(0, 10)]
        private int numEnemiesSpawnPoints;

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

        public World World { get { return tilemap; } set { tilemap = value; } }

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
                    WorkingDirectory = $"{Application.dataPath}/Mazer/Generator"
                }
            };
            process.Start();
            //process.WaitForExit();
            string result = process.ReadStandardOutput();
            string error = process.ReadStandardError();
            if (!error.Equals(""))
                UnityEngine.Debug.LogError(error);
            print(result);
            DungeonData dungeon = JsonConvert.DeserializeObject<DungeonData>(result);
            Dungeon = dungeon;
            yield break;
            //yield return (Build(0));
        }


        public string GetArgs()
        {
            if (maxRoomSize <= corridorSize)
                maxRoomSize = corridorSize + 3;
            string execPath = $"{Application.dataPath}/Mazer/Generator/maker_main.py";
            string res = $"{execPath} " +
                $"--levels={numLevels} " +
                $"--rooms={numRooms} " +
                $"--size={maxRoomSize} " +
                $"--path={maxPathLength} " +
                $"--distance={distanceBetweenRooms} " +
                $"--space={spaceSize} " +
                $"--num_trap={numTraps} " +
                $"--num_treasure={numTreasures} " +
                $"--num_item={numItems} " +
                $"--corr_size={corridorSize} ";
            res += randomStart ? " --rand_init" : "";
            print(res);
            return res;
        }

        /*public void Build(int index)
        {
            List<RoomData> visited = new List<RoomData>();
            LevelData level = Dungeon.Levels[index];
            tilemap.Size = GetSize(level);
            tilemap.ClearAllTiles();
            RoomData initRoom = level.GetRoom(level.Init_Room);
            int x = initRoom.Center.X;
            int y = initRoom.Center.Y;
            RecursiveBuild(level, initRoom, x, y, visited);
        }*/

        public IEnumerator Build(int index)
        {
            LevelData level = Dungeon.Levels[index];
            return Build(level);
        }

        public IEnumerator Build(LevelData level)
        {
            List<RoomData> visited = new List<RoomData>();
            tilemap.Size = GetSize(level);
            tilemap.ClearAllTiles();
            yield return new WaitForEndOfFrame();
            RoomData initRoom = level.GetRoom(level.Init_Room);
            int x = initRoom.Center.X;
            int y = initRoom.Center.Y;
            yield return RecursiveBuild(level, initRoom, x, y, visited);
            yield return DrawStairs(level);
        }

        private int GetSize(LevelData level)
        {
            int farPoint = Math.Max(Math.Abs(level.Rooms[0].TrueCenter.X), Math.Abs(level.Rooms[0].Center.Y));
            int maxSize = Math.Max(Math.Abs(level.Rooms[0].Size.X), Math.Abs(level.Rooms[0].Size.Y));
            for (int i = 1; i < level.Rooms.Count; i++)
            {
                int tmpFar = Math.Max(Math.Abs(level.Rooms[0].TrueCenter.X), Math.Abs(level.Rooms[i].Center.Y));
                int tmpSize = Math.Max(Math.Abs(level.Rooms[0].Size.X), Math.Abs(level.Rooms[i].Size.Y));
                if (farPoint < tmpFar)
                    farPoint = tmpFar;
                if (tmpSize > maxSize)
                    maxSize = tmpSize;
            }
            return farPoint * maxSize / 2;
        }

        private void Start()
        {
            // tilemap=GameObject.FindAnyObjectByType<World>();
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

        /*private void RecursiveBuild(LevelData level, RoomData room, int x, int y, List<RoomData> visited)
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
        }*/

        private IEnumerator RecursiveBuild(LevelData level, RoomData room, int x, int y, List<RoomData> visited)
        {
            if (!visited.Contains(room))
            {
                //print("Room: " + room.Id);
                visited.Add(room);
                yield return StartCoroutine(DrawRoomTiles(room, level));
                foreach (DoorData door in level.GetDoorsOfRoom(room))
                {
                    yield return StartCoroutine(DrawCorridorTiles(level, door));
                    (int, int) up = door.GetOrientationValues(5);
                    int upX = up.Item1;
                    int upY = up.Item2;
                    RoomData dest = level.GetRoom(door.End);
                    yield return StartCoroutine(RecursiveBuild(level, dest, x + upX, y + upY, visited));
                }
            }
        }

        private IEnumerator DrawRoomTiles(RoomData room, LevelData level)
        {
            int halfSizeX = room.Size.X / 2;
            int halfSizeY = room.Size.Y / 2;
            int subtractor = (corridorSize % 2 == 0 && room.Size.X == corridorSize && room.Size.Y == corridorSize) ? 1 : 0;
            for (int i = -halfSizeX; i <= halfSizeX; i++)
                for (int j = -halfSizeY; j <= halfSizeY; j++)
                {
                    int x = room.TrueCenter.X + i - subtractor;
                    int y = room.TrueCenter.Y + j - subtractor;
                    Vector3Int pos = new Vector3Int(x, y);
                    tilemap.SetTile(pos, tile, 0);
                    yield return new WaitForEndOfFrame();
                }
            foreach (ExpansionData expansion in room.Expansions)
                yield return StartCoroutine(DrawExpansion(expansion, room.TrueCenter, room.Size));
            yield return StartCoroutine(DrawDecorations(room, level));
        }

        private IEnumerator DrawExpansion(ExpansionData expansion, Pair center, Pair size)
        {
            int halfSizeX = expansion.Size.X / 2;
            int halfSizeY = expansion.Size.Y / 2;

            for (int i = -halfSizeX; i <= halfSizeX; i++)
                for (int j = -halfSizeY; j <= halfSizeY; j++)
                {
                    int x = expansion.TrueCenter.X + i;
                    int y = expansion.TrueCenter.Y + j;
                    Vector3Int pos = new Vector3Int(x, y);
                    tilemap.SetTile(pos, tile, 0);
                    yield return new WaitForEndOfFrame();
                }
        }

        private IEnumerator DrawDecorations(RoomData room, LevelData level)
        {
            foreach (DecorationData dec in level.GetDecorationsOfRoom(room))
            {
                int x = dec.Position.X;
                int y = dec.Position.Y;
                //int x = dec.Position.X;
                //int y = dec.Position.Y;
                Vector3Int pos = new Vector3Int(x, y);
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
                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator DrawCorridorTiles(LevelData level, DoorData door)
        {
            RoomData start = level.GetRoom(door.Start);
            RoomData end = level.GetRoom(door.End);
            (int, int) values = door.GetOrientationValues(distanceBetweenRooms);
            int increment = (end.Center.X - start.Center.X + end.Center.Y - start.Center.Y) / distanceBetweenRooms;
            if (door.Orientation.Equals("north") || door.Orientation.Equals("south"))
                for (int i = start.Center.Y; i != end.Center.Y; i += increment)
                    for (int j = 0; j < corridorSize; j++)
                    {
                        AddTile(tile, start.Center.X + j - corridorSize / 2, i);
                        yield return new WaitForEndOfFrame();
                    }
            else for (int i = start.Center.X; i != end.Center.X; i += increment)
                    for (int j = 0; j < corridorSize; j++)
                    {
                        AddTile(tile, i, start.Center.Y + j - corridorSize / 2);
                        yield return new WaitForEndOfFrame();
                    }
        }

        private IEnumerator DrawStairs(LevelData level)
        {
            Vector3Int pos = new Vector3Int(level.Stairs.Position.X, level.Stairs.Position.Y, 0);
            print("Stairs: " + pos);
            tilemap.SetTile(pos, stairsTile, 1);
            yield return new WaitForEndOfFrame();
        }

        private void AddTile(RuleTile tile, int x, int y)
        {
            Vector3Int pos = new Vector3Int(x, y);
            tilemap.SetTile(pos, tile, 0);
        }

        private void AddTile(Tile tile, int x, int y)
        {
            Vector3Int pos = new Vector3Int(x, y);
            tilemap.SetTile(pos, tile, 0);
        }
    }
}
