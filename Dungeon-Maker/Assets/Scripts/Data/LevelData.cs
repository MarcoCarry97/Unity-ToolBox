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

    [SerializeField]
    private List<DecorationData> decorations;

    private DecorationData stairs;

    private DecorationData startPoint;

    public int Init_Room{ get { return init_room; } set { init_room = value; } }
    public List<RoomData> Rooms { get { return rooms; } set { rooms = value; } }
    public List<DoorData> Doors { get { return doors; } set { doors = value; } }

    public List<DecorationData> Decorations { get { return decorations; } set { decorations = value; } }

    public DecorationData StartPoint{ get { return startPoint; } set { startPoint = value; } }
    public DecorationData Stairs{ get { return stairs; } set { stairs = value; } }

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

    public List<DecorationData> GetDecorationsOfRoom(RoomData room)
    {
        List<DecorationData> decs=new List<DecorationData>();
        foreach(DecorationData data in decorations)
        {
            if(data.Room.Equals(room.Id))
                decs.Add(data);
        }
        return decs;
    }

    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}
