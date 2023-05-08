using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerativePipeline : MonoBehaviour
{
    [SerializeField]
    private List<GenerativePipelinePhase> phases;

    private string output;
    public string Output
    {
        get
        {
            string val = output;
            output = null;
            return output;
        }
    }

    private Coroutine buildCoroutine;

    private void CreatePhaseJobs()
    {
        for(int i=0; i<phases.Count; i++)
        {
            StartCoroutine((PhaseJob(i)));
        }
    }

    private IEnumerator PhaseJob(int index)
    {
        GenerativePipelinePhase phase = phases[index];
        bool end = false;
        while(!end)
        {
            string input = "";
            if(index>0)
            {
                yield return new WaitUntil(() => phases[index-1].IsOutputAvailable());
                input = phases[index - 1].Output;
            }
            phase.Compute(input);
            if(index<phases.Count-1)
                yield return new WaitUntil(() => phases[index+1].IsFinished());
            else
            {
                output=phase.Output;
                yield return new WaitUntil(() => Output != null);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(phases.Count!=0)
        {
            CreatePhaseJobs();
        }
    }

    // Update is called once per frame
    void Update()
    {
        string model = Output;
        if(model!=null)
        {
            buildCoroutine = StartCoroutine(BuildDungeon(model));
        }
    }

    private IEnumerator BuildDungeon(string model)
    {
        print(model);
        yield return null;
    }


}
