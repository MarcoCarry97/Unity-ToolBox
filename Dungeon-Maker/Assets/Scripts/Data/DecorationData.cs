using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationData : ScriptableObject
{
    private string type;

    public string Type
    {
        get { return type; }
        set { type = value; }
    }

    private int index;

    public int Index
    {
        get { return index; }
        set { index = value; }
    }

    private int room;

    public int Room
    {
        get { return room; }
        set { room = value; }
    }

    private Pair position;

    public Pair Position
    {
        get { return position; }
        set { position = value; }
    }


}
