using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public static class ProcessExtensions
{
    public static string ReadStandardOutput(this Process process)
    {
        return process.StandardOutput.ReadToEnd();
    }
}
