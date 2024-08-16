using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using ToolBox.GUI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using ToolBox.Core;

public class ChooseMovePanel : Panel
{
    // Start is called before the first frame update
    void Start()
    {
        BattleSystem battle = GameObject.FindObjectOfType<BattleSystem>();
        MoveSet moveset = battle.currentMob.MoveSet;
        int index = 0;
        foreach (Move move in moveset)
        {
            Button button = GameObject.Find($"Attack{index}").GetComponent<Button>();
            button.GetComponentInChildren<TextMeshProUGUI>().text = move.Name;
            button.onClick.AddListener(OnAttack(move));
            index++;
        }

    }

    private UnityAction OnAttack(Move move)
    {
        return () =>
        {
            GameController.Instance.Battle.ChosenMove = move;
            GameController.Instance.Gui.DeactivePanel();
            GameController.Instance.Gui.ActivePanel("ChooseTargetPanel");
        };
    }

}
