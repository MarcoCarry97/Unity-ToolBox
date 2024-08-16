using System.Collections;
using System.Collections.Generic;
using TMPro;
using ToolBox.GUI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using ToolBox.Core;

public class ChooseTargetPanel : Panel
{
    void Start()
    {
        BattleSystem battle = GameObject.FindObjectOfType<BattleSystem>();
        MoveSet moveset = battle.currentMob.MoveSet;
        int index = 0;
        foreach (EnemyController enemy in battle.EnemyParty)
        {
            Button button = GameObject.Find($"Target{index}").GetComponent<Button>();
            button.transform.GetChild(0).GetComponent<Image>().sprite= enemy.GetComponent<SpriteRenderer>().sprite;
            button.onClick.AddListener(OnAttack(enemy));
            index++;
        }

    }

    private UnityAction OnAttack(EnemyController enemy)
    {
        return () =>
        {
            GameController.Instance.Battle.ChosenTarget = enemy;
            GameController.Instance.Gui.DeactivePanel();
        };
    }

}
