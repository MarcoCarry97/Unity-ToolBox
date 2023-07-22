using it.unical.mat.embasp.languages;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpansionData : ScriptableObject
{
    [SerializeField]
    private Pair center;

    [SerializeField]
    private Pair trueCenter;

    [SerializeField]
    private Pair size;

    public Pair Center { get { return center; } set { center = value; } }

    public Pair TrueCenter { get { return trueCenter; } set { trueCenter = value; } }

    public Pair Size { get { return size; } set { size = value; } }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
