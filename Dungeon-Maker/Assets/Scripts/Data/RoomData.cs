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
    private Pair size;

    public int Id { get { return id; } set { id = value; } }

    public Pair Center { get { return center;} set { center = value; } }

    public Pair Size { get { return size;} set { size = value; } }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
