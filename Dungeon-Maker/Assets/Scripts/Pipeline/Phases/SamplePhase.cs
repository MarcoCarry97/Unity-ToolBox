using Antlr4.Runtime.Misc;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Windows;

[CreateAssetMenu(fileName ="Sample",menuName ="Scriptable Objects/Pipeline Phases/Sample Phase")]
public class SamplePhase : GenerativePipelinePhase
{
    [SerializeField]
    private string genRoom;

    [SerializeField]
    private string genCorridor;

    [SerializeField]
    private string genDecoration;


    private string toAbsolutePath(string file)
    {
        return string.Format("{0}/Clingo/{1}", Application.dataPath, file);
    }

    public override IEnumerator Compute(string input)
    {
        finished = false;
        input=toAspRules(input);




        if (!input.Equals("")) input += ".";
        string pathRoom = toAbsolutePath(genRoom);
        string pathCorridor= toAbsolutePath(genCorridor);   
        string pathDecoration= toAbsolutePath(genDecoration);
        string args = $" {pathRoom} {pathCorridor} {pathDecoration} 1";
        string filePath = string.Format("{0}/Clingo/Generator/{1}", Application.dataPath, program);
        args = $"{filePath} {args}";
        Process process = new Process
        {

            StartInfo = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = args,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };
        process.Start();
        yield return new WaitUntil(() => process.HasExited);
        string result = process.StandardOutput.ReadToEnd();




        output = result;
        finished= true;
    }

    private string toAspRules(string input)
    {
        return input.Replace(" ", ".");
    }
}
