
//using BonaJson;
using Newtonsoft.Json.Linq;
using it.unical.mat.embasp.@base;
using it.unical.mat.embasp.languages.asp;
using it.unical.mat.embasp.platforms.desktop;
using it.unical.mat.embasp.specializations.clingo.desktop;
using Mazer.Data;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using Mazer.Utils;
using Mazer.Utils.Distance;

namespace Mazer.Generators
{
    

    public class ClingoMazer : MonoBehaviour, IMazer
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

        [SerializeField]
        private string firstPhase;

        [SerializeField]
        private List<string> beginPhases;

        [SerializeField]
        private List<string> lastPhases;




        public World World { get { return tilemap; } set { tilemap = value; } }

        public IEnumerator Generate()
        {
            JObject json=new JObject();
            List<string> inputs = DoPhase("", firstPhase, numLevels, spaceSize);
            yield return new WaitForEndOfFrame();
            inputs=DoMorePhases(inputs,beginPhases,numLevels,spaceSize);
            yield return new WaitForEndOfFrame();
            List<string> result = DoMorePhases(inputs, lastPhases, 1, 1);
            yield return new WaitForEndOfFrame();
            JArray levelsInJson = MazerUtils.SerializeLevels(result);
            json["status"] = "SAT";
            json["levels"] = levelsInJson;
            Dungeon = JsonConvert.DeserializeObject<DungeonData>(json.ToString());
        }

        private List<string> DoPhase(string input,string phase, int numLevels,int spaceSize)
        {
            List<string> models=new List<string>();
            for(int i=0;i<numLevels;i++)
            {
                List<string> tmpModels = Solve(input, phase, numLevels, numRooms, maxRoomSize, distanceBetweenRooms, maxPathLength, spaceSize, numTraps, numTreasures, numItems, numEnemiesSpawnPoints, randomStart,models);
                models.Add(tmpModels[UnityEngine.Random.Range(0, tmpModels.Count)]);
            }
            return models;
        }

        public List<string> DoMorePhases(List<string> inputs, List<string> phases,int numLevels,int spaceSize)
        {
            List<string> models=new List<string>();
            for(int i=0;i<inputs.Count;i++)
            {
                string input = inputs[i];
                for(int j=0; j<phases.Count;j++)
                {
                    string phase = phases[j];
                    List<string> result=DoPhase(input,phase,1,spaceSize);
                    input = GetRandomModels(result, 1)[0];
                }
                models.Add(input);
            }
            return models;
        }

        private List<string> Solve(string input, string phase,int numLevels,int numRooms,int maxRoomSize,int distanceBetweenRooms,int maxPathLength,int spaceSize,int numTraps, int numTreasures,int numItems,int numEnemies,bool randomStart,List<string> previousLevels=null)
        {
            string execName = $"{Application.dataPath}/Mazer/Solvers/Windows/clingo";
            print(execName);
            Handler handler = new DesktopHandler(new ClingoDesktopService(execName));
            List<OptionDescriptor> options = GetOptions(numLevels, numRooms, maxRoomSize, distanceBetweenRooms, maxPathLength, spaceSize, numTraps, numTreasures, numItems, numEnemies, randomStart);
            foreach (OptionDescriptor option in options)
                handler.AddOption(option);
            string path = Application.dataPath + "\\Mazer\\Generator\\LogicPrograms\\" + phase;
            InputProgram program = new ASPInputProgram();
            program.AddProgram(input + ReadFile(path));
            //program.AddProgram("point(0,0).");
            handler.AddProgram(program);
            it.unical.mat.embasp.@base.Output output = handler.StartSync();
            print(output.OutputString);
            if (output.Equals(""))
                throw new MazerException("Starting generation", output);
            JObject json = JObject.Parse(output.OutputString);
            string result = (json["Result"] as JToken).Value<string>();
            if (result.Equals("UNSATISFIABLE"))
                throw new MazerException(phase, output);
            List<string> bestModels = ExtractMostDifferentModels(json,numLevels,previousLevels);
            if (bestModels.Count == 0)
                throw new MazerException(phase, output);
            return bestModels;
        }

        private List<string> ExtractMostDifferentModels(JObject json,int numLevels,List<string> previousLevels)
        {
            Dictionary<int, List<string>> dict = ExtractAllModels(json);
            List<string> models = MazerUtils.ConvertToListOfStrings(dict);
            List<string> result = new List<string>();
            if (previousLevels.Count == 0)
                result.Add(GetRandomModels(models, 1)[0]);
            else for(int i=0;i<models.Count;i++)
            {
                    string model= models[i];
                    int distance = 0;
                    string bestModel = GetRandomModels(models, 1)[0];
                    foreach(string previousLevel in previousLevels)
                    {
                        int dist = 0;
                        foreach(Func<string, string,int> metric in DistanceMetrics.Metrics)
                        {
                            dist += metric(model, previousLevel);
                        }
                        if(distance<=dist)
                        {
                            bestModel = model;
                            distance = dist;
                        }
                    }
                    if(bestModel!=null)
                    {
                        result.Add(bestModel);
                        models.Remove(bestModel);
                    }
            }
            return result;
        }

        private List<string> GetRandomModels(List<string> models,int numLevels)
        {
            List<string> list= new List<string>();
            for(int i=0;i<numLevels;i++)
            {
                string model = models[UnityEngine.Random.Range(0, models.Count)];
                models.Remove(model);
                list.Add(model);
            }
            return list;
        }

        private Dictionary<int, List<string>> ExtractAllModels(JObject json)
        {
            Dictionary<int, List<string>> dict = new Dictionary<int, List<string>>();
            JToken token = json["Call"][0]["Witnesses"];
            if(token == null)
                throw new MazerException();
            List<JToken> listModels = token.ToList<JToken>();
            for(int i = 0; i < listModels.Count;i++)
            {
                dict[i] = new List<string>();
                
                JToken jsonModel = listModels[i]["Value"];
                for (int j = 0; j < jsonModel.Count(); j++)
                    dict[i].Add(jsonModel[j].ToString());
            }
            return dict;
        }

        private string ReadFile(string path)
        {
            StreamReader sr = new StreamReader(path);
            string cont = "";
            while(!sr.EndOfStream)
            {
                cont += sr.ReadLine()+"\n";
            }
            sr.Close();
            return cont;
        }

        private List<OptionDescriptor> GetOptions(int numLevels, int numRooms, int maxRoomSize, int distanceBetweenRooms, int maxPathLength, int spaceSize, int numTraps, int numTreasures, int numItems, int numEnemies,bool randomStart)
        {
            List<OptionDescriptor> options = new List<OptionDescriptor>();
            if (maxRoomSize <= corridorSize)
                maxRoomSize = corridorSize + 3;
            int xStart = 0, yStart=0;
            if(randomStart)
            {
                xStart=UnityEngine.Random.Range(-numRooms*2+1,numRooms*2-1)*distanceBetweenRooms;
                yStart=UnityEngine.Random.Range(-numRooms*2+1,numRooms*2-1)*distanceBetweenRooms;
                
            }
            //Options
            options.Add(CreateOption("--models", "=", numLevels*spaceSize));
            options.Add(CreateOption("--verbose","=",1));
            //options.Add(CreateOption("--quiet","=",1));
            options.Add(CreateOption("--outf","=",2));
            //options.Add(CreateOption("--text"));
            //options.Add(CreateOption("--lparse-rewrite"));
            //options.Add(CreateOption("--ilearnt","=","forget"));
            //options.Add(CreateOption("--iheuristic","=","forget"));
            //options.Add(CreateOption("--stats"));
            //options.Add(CreateOption("--quiet"));
            //options.Add(CreateOption("--seed", "=", UnityEngine.Random.Range(-numRooms * 2 + 1, numRooms * 2 - 1)));

            //Constants
            options.Add(CreateOption("-c num_rooms","=", numRooms));
            options.Add(CreateOption("-c max_size", "=", maxRoomSize));
            options.Add(CreateOption("-c corr_dim", "=", corridorSize));
            options.Add(CreateOption("-c distance", "=", distanceBetweenRooms));
            options.Add(CreateOption("-c num_trap", "=", numTraps));
            options.Add(CreateOption("-c num_treasure", "=", numTreasures));
            options.Add(CreateOption("-c num_item", "=", numItems));
            options.Add(CreateOption("-c num_enemy", "=", numEnemies));
            options.Add(CreateOption("-c x_start","=", xStart));
            options.Add(CreateOption("-c y_start","=", yStart));

            return options;
        }

        private OptionDescriptor CreateOption(string name,string separator, object value)
        {
            OptionDescriptor option = new OptionDescriptor();
            option.Separator = " ";
            if (value != null)
                option.AddOption($"{name}{separator}{value.ToString()}");
            else option.AddOption($"{name}");
            return option;
        }

        private OptionDescriptor CreateOption(string name)
        {
            OptionDescriptor option = new OptionDescriptor();
            option.Separator =null;
            option.AddOption (name);
            return option;
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
            RoomData initRoom = level.GetRoom(level.Initial_Room);
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
