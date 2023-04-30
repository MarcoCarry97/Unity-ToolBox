using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorData : ScriptableObject
{
    [SerializeField]
    private int start;

    [SerializeField]
    private int end;

    public int Start { get { return start; } set { start = value; } }
    public int End { get { return end; } set { end = value; } }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
