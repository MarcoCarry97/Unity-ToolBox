using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "ScriptableObjects/Battle/Move/Attack")]
public class AttackMove : Move
{

    public override void Apply(MobController executor, MobController target)
    {
        int damage=0;
        if (Type == MoveType.Special)
            damage = calcDamage(Value, executor.ModificableStats.attack, target.ModificableStats.defense);
        else damage = calcDamage(Value, executor.ModificableStats.specialAttack, target.ModificableStats.specialDefense);
        if (damage < 0)
            damage = 0;
        bool hit = calcHit(executor, target);
        if (hit)
        {
            int multiplier = calcFortuneHit(executor);
            target.ModificableStats.health -= damage;
            if (target.ModificableStats.health < 0)
                target.ModificableStats.health = 0;
        }
        else Debug.Log("MISSED!!!");
    }

    private int calcDamage(int value, int attack, int defense)
    {
        return value + attack - defense;
    }

    private bool calcHit(MobController executor, MobController target)
    {
        int prec = Precision + executor.ModificableStats.precision - target.ModificableStats.elusion;
        return Random.Range(0, 100) <= prec;
    }

    private int calcFortuneHit(MobController executor)
    {
        int num = Random.Range(MobStats.MinValue, MobStats.MaxValue);
        int res = 1;
        if (num < executor.ModificableStats.fortune / 8)
            res = 4;
        else if (num < executor.ModificableStats.fortune)
            res = 2;
        return res;
    }
}