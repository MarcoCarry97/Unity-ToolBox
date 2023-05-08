using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenerativePipelinePhase : ScriptableObject
{

    [SerializeField]
    private string fileName;

    private ClingoProcess process;

    public string Output { get; private set; }

    protected abstract string doComputation(string input);

    public bool IsFinished()
    {
        return !process.IsFinished();
    }

    public void Compute(string input)
    {
        Output = doComputation(input);
    }

    public bool IsOutputAvailable()
    {
        return Output!= null;
    }


}
