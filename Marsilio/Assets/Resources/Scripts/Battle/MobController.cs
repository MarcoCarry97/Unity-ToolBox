using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using static UnityEngine.GraphicsBuffer;

public abstract class MobController : MonoBehaviour, IComparable
{
    public enum Status
    {
        Normal,
        Sleeping,
        Poisoned,
        Paralized,
        
    }

    private Status status;

    public Status MobStatus
    {
        get { return status; }
        set { status = value; }
    }

    private MobInfo info;

    public MobInfo Info
    {
        get { return info; }
        set
        {
            info = value;
            modificableStats = info.Stats.Clone() as MobStats;
        }
    }


    private MobStats modificableStats;

    public MobStats ModificableStats
    {
        get { return modificableStats; }
        set 
        { modificableStats = value; }
    }

    public MoveSet MoveSet
    {
        get { return info.MoveSet; }
        protected set { info.MoveSet= value; }
    }

    public static int Compare(MobController mob1, MobController mob2)
    {
        int res = 0;
        if (mob1.modificableStats.agility > mob2.modificableStats.agility)
            res = 1;
        else if (mob1.modificableStats.agility == mob2.modificableStats.agility)
            res = 0;
        else res = -1;
        return res;
    }
    public int CompareTo(object obj)
    {
        MobController mob1 = this;
        MobController mob2 = obj as MobController;
        return Compare(mob1, mob2);
    }

    public abstract void ChooseMove();

    public void DoMove(Move move,MobController target)
    {
        move.Apply(this, target);
    }

    public abstract void ChooseTargets();

    public abstract void DoTurn();

}
