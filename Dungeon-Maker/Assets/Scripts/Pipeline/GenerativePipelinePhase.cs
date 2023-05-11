using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public abstract class GenerativePipelinePhase : ScriptableObject
{
    [SerializeField]
    protected string program;

    [SerializeField]
    protected string fileName;

    protected bool finished = false;

    protected ClingoProcess process;

    protected string output;

    public string Output { 
        get
        {
            return output;
        }
        set
        {
            output = value;
        }
    }

    public IEnumerator IsFinished()
    {
        return new WaitUntil(()=>finished);
    }

    public abstract IEnumerator Compute(string input);

    public bool IsOutputAvailable()
    {
        return output!= null;
    }


}
