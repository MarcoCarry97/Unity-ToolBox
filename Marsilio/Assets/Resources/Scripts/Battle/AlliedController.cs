
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolBox.Core;

public class AlliedController : MobController
{

    public override void ChooseMove()
    {
        GameController.Instance.Gui.ActivePanel("AlliedTurnPanel");
        GameController.Instance.Battle.ChosenMove = null;
    }

    public override void ChooseTargets()
    {
        GameController.Instance.Gui.ActivePanel("ChooseTargetPanel");
        GameController.Instance.Battle.ChosenTarget= null;
    }

    public override void DoTurn()
    {
        ChooseMove();
    }
}
