using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MoveSet",menuName ="ScriptableObjects/Battle/MoveSet")]
public class MoveSet : ScriptableObject, IEnumerable<Move>
{
    public List<Move> moves;

    public List<Move> learnable;

    [Range(0, 20)]
    public int limit;

    public Move this[int index] 
    {
        get { return moves[index]; }
        set { moves[index] = value; }
    }

    public IEnumerator<Move> GetEnumerator()
    {
        return moves.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return moves.GetEnumerator();
    }
}
