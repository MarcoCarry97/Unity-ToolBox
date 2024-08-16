using System;
using UnityEngine;

[CreateAssetMenu(fileName ="Stats",menuName ="ScriptableObjects/Battle/Stats")]
public class MobStats : ScriptableObject , ICloneable
{ 
    public const int MinValue = 0;
    public const int MaxValue= 999;

    [Range(0,999)]
    public int maxHealth;

    [Range(0, 999)]
    public int health;

    [Range(0, 999)]
    public int attack;

    [Range(0, 999)]
    public int defense;

    [Range(0, 999)]
    public int specialAttack;

    [Range(0, 999)]
    public int specialDefense;

    [Range(0, 999)]
    public int agility;

    [Range(0, 999)]
    public int fortune;

    [Range(0, 100)]
    public int precision;

    [Range(0, 100)]
    public int elusion;

    [Range(1, 100)]
    public int level;
    
    public object Clone()
    {
        MobStats stats=ScriptableObject.CreateInstance<MobStats>();
        stats.maxHealth=maxHealth;
        stats.health=health;
        stats.attack=attack;
        stats.defense=defense;
        stats.specialAttack = specialAttack;
        stats.specialDefense = specialDefense;
        stats.agility = agility;
        stats.precision=precision;
        stats.elusion=elusion;
        stats.fortune=fortune;
        stats.level = level;
        return stats;
    }
}
