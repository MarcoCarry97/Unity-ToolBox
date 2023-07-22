using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomData : ScriptableObject
{
    [SerializeField]
    private int id;

    [SerializeField]
    private Pair center;

    [SerializeField]
    private Pair trueCenter;

    [SerializeField]
    private Pair size;

    [SerializeField]
    private List<ExpansionData> expansions;

    public int Id { get { return id; } set { id = value; } }

    public Pair Center { get { return center;} set { center = value; } }

    public Pair TrueCenter { get { return trueCenter; } set { trueCenter = value; } }

    public Pair Size { get { return size;} set { size = value; } }

    public List<ExpansionData> Expansions { get { return expansions; } set { expansions = value; } }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
