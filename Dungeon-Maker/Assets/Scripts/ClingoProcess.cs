using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Windows;

public class ClingoProcess
{
    private Process process=new Process();

    public string Output { get; private set; }

    public ClingoProcess() { }

    public IEnumerator Start(string program, string fileName, string input, int numOfSamples=1)
    {
        if (!input.Equals("")) input += ".";
        string args = string.Format("{0}/Clingo/{1} [{2}] {3}", Application.dataPath, fileName, input, numOfSamples);
        string filePath = string.Format("{0}/Clingo/Generator/{1}", Application.dataPath, program);
        args = $"{filePath} {args}";
        UnityEngine.Debug.Log(args);
        process = new Process
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
        Output = process.StandardOutput.ReadToEnd();
        UnityEngine.Debug.Log("Process output: " + Output);
    }

    private void tbody(object obj)
    {
        Tuple<string, string, string, int> data = obj as Tuple<string, string,string, int>;
        string program=data.Item1;
        string fileName=data.Item2;
        string input=data.Item3;
        int numOfSamples = data.Item4;
       
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
        bool end = false;
        if (process == null)
            end = true;
        else end = process.HasExited;
        return end;
    }


}
