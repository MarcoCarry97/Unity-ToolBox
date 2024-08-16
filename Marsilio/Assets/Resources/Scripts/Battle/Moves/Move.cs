
using UnityEngine;




public abstract class Move : ScriptableObject
{
    public enum MoveType
    {
        Physic,
        Special,
        Status
    }

    public string Name;

    public int Value;

    public MoveType Type;

    public int Precision;
    public abstract void Apply(MobController executor, MobController target);
}
