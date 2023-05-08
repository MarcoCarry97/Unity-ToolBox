using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Rendering;

public class ClingoProcess
{
    private Process process;

    public string Output { get; private set; }

    public ClingoProcess() { }

    public void Start(string program, string fileName, string input, int numOfSamples=1)
    {
        string args = string.Format("{0}/Clingo/{1} {2} {3}", Application.dataPath, fileName,input, numOfSamples);
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
        while (!process.HasExited);
        Output = process.StandardOutput.ReadToEnd();
        process.Kill();
    }

    public void WaitForExit()
    {
        process.WaitForExit();
    }

    public void Stop()
    {
        process.Kill();
    }

    public bool IsFinished()
    {
        return process.HasExited;
    }


}
