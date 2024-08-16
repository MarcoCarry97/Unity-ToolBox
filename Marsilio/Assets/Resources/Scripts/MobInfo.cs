using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="MobInfo", menuName ="ScriptableObjects/MobInfo")]
public class MobInfo : ScriptableObject
{
    [SerializeField]
    private string characterName;

    public string Name { get { return characterName; } }

    [SerializeField]
    private Sprite sprite;

    public Sprite MobSprite { get { return sprite; } }

    [SerializeField]
    private RuntimeAnimatorController animate;
    public RuntimeAnimatorController MobAnimation { get { return animate; } }

    [SerializeField]
    private MobStats stats;

    public MobStats Stats {
        get { return stats; }  
        set { stats = value; }
    }

    [SerializeField]
    private MoveSet moveset;

    public MoveSet MoveSet {
        get { return moveset; }
        set
        {
            moveset = value;
        }
    }
}
