using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(fileName ="Sample",menuName ="Scriptable Objects/Pipeline Phases/Sample Phase")]
public class SamplePhase : GenerativePipelinePhase
{


    public override IEnumerator Compute(string input)
    {
        finished = false;
        input=toAspRules(input);



        if (!input.Equals("")) input += ".";
        string args = string.Format("{0}/Clingo/{1} [{2}] {3}", Application.dataPath, fileName, input, 1);
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
