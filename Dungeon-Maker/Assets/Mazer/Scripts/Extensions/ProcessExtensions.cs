using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public static class ProcessExtensions
{
    public static string ReadStandardOutput(this Process process)
    {
        string res = "";
        while (!process.StandardOutput.EndOfStream)
        {
            res += process.StandardOutput.ReadLine();
        }
        return res;
    }

    public static string ReadStandardError(this Process process)
    {
        string res = "";
        while (!process.StandardError.EndOfStream)
        {
            res += process.StandardError.ReadLine();
        }
        return res;
    }
}