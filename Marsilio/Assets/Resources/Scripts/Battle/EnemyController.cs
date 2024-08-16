using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ToolBox.Core;

public class EnemyController : MobController
{
    public override void ChooseMove()
    {
        GameController.Instance.Battle.ChosenMove = randomMove();
    }

    public override void ChooseTargets()
    {
        Party allieds = GameController.Instance.Battle.AlliedParty;
        int num = Random.Range(0, allieds.CurrentParty.Count-1);
        MobController mob = GameObject.Find(allieds[num].Name).GetComponent<MobController>();
        GameController.Instance.Battle.ChosenTarget = mob;
    }

    private Move randomMove()
    {
        return MoveSet[Random.Range(0,MoveSet.moves.Count)];
    }

    public override void DoTurn()
    {
        ChooseMove();
        ChooseTargets();
    }
}
