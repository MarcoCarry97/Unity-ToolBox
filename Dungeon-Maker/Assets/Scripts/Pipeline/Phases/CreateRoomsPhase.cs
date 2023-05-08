using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamplePhase : GenerativePipelinePhase
{
    [SerializeField]
    private string program;

    [SerializeField]
    private string fileName;

    protected override string doComputation(string input)
    {
        input=toAspRules(input);
        ClingoProcess process = new ClingoProcess();
        process.Start(program, fileName,input);
        string result = process.Output;
        return result;
    }

    private string toAspRules(string input)
    {
        return input.Replace(" ", ".");
    }
}
