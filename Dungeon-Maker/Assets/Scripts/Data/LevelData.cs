using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[Serializable]
public class LevelData : ScriptableObject
{

    [SerializeField]
    private List<RoomData> rooms;

    [SerializeField]
    private List<DoorData> doors;

    public List<RoomData> Rooms { get { return rooms; } set { rooms = value; } }
    public List<DoorData> Doors { get { return doors; } set { doors = value; } }

    public List<DoorData> GetDoorsOfRoom(RoomData room)
    {
        List<DoorData> doors=new List<DoorData>();
        foreach(DoorData door in doors)
        {
            if(door.Start==room.Id)
            { 
                doors.Add(door);
            }
        }
        return doors;
    }

    public RoomData GetRoom(int id)
    { 
        foreach(RoomData room in Rooms)
        {
            if (room.Id == id)
                return room;
        }
        return null;

    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
