using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditorInternal;
using UnityEngine;

public class DungeonMaker : MonoBehaviour
{
    [Range(1,100)]
    [SerializeField]
    private int numLevels;

    [Range(1,10)]
    [SerializeField]
    private int numRooms;

    [Range(3,10)]
    [SerializeField]
    private int maxRoomSize;

    [Range(1,10)]
    [SerializeField]
    private int distanceBetweenRooms;

    [Range(1,10)]
    [SerializeField]
    private int maxPathLength;

    [SerializeField]
    private bool useRandomizedInitialPosition;

    [SerializeField]
    private string roomFile;

    [SerializeField]
    private string corridorFile;

    [SerializeField]
    private string decorationFile;

    private List<DungeonData> dungeons;

    public IEnumerator Generate()
    {
        string args = GetArgs();
        Process process = new Process();
        process.StartInfo.FileName = "python";
        process.StartInfo.Arguments = args;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow= true;
        process.Start();
        yield return new WaitUntil(() => process.HasExited);
        string result = process.ReadStandardOutput();
        dungeons.Add(JsonConvert.DeserializeObject<DungeonData>(result));
    }

    public string GetArgs()
    {
        string genRooms = $"{Application.dataPath}/Clingo/{roomFile}.lp";
        string genCorridors = $"{Application.dataPath}/Clingo/{corridorFile}.lp";
        string genDecorations = $"{Application.dataPath}/Clingo/{decorationFile}.lp";
        string res=$"--room_file {genRooms} --corr_file {genCorridors} --dec_file {genDecorations} --levels {numLevels} --rooms {numRooms} --size {maxRoomSize} --path {maxPathLength} --distance {distanceBetweenRooms}";
        res += useRandomizedInitialPosition ? "--rand_init" : "";
        return res;
    }

}
