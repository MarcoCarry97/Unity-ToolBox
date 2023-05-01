using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[Serializable]
public class LevelData : ScriptableObject
{
    [SerializeField]
    private int init_room;

    [SerializeField]
    private List<RoomData> rooms;

    [SerializeField]
    private List<DoorData> doors;

    public int InitRoom{ get { return init_room; } set { init_room = value; } }
    public List<RoomData> Rooms { get { return rooms; } set { rooms = value; } }
    public List<DoorData> Doors { get { return doors; } set { doors = value; } }

    public List<DoorData> GetDoorsOfRoom(RoomData room)
    {
        List<DoorData> nextDoors=new List<DoorData>();
        foreach(DoorData door in Doors)
        {
            if(door.Start.Equals(room.Id))
            { 
                nextDoors.Add(door);
            }
        }
        return nextDoors;
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
