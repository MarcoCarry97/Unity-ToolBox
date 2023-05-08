using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prolog;
public class DoorData : ScriptableObject
{
    [SerializeField]
    private int start;

    [SerializeField]
    private int end;

    [SerializeField]
    private string orientation;

    public int Start { get { return start; } set { start = value; } }
    public int End { get { return end; } set { end = value; } }
    public string Orientation { get { return orientation; } set { orientation = value; } }

    public (int, int) GetOrientationValues()
    {
        int x = 0, y = 0;
        if (orientation.Equals("east"))
            x = 1;
        else if (orientation.Equals("west"))
            x = -1;
        else if (orientation.Equals("south"))
            y = -1;
        else if (orientation.Equals("north"))
            y = 1;
        return (x, y);
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
