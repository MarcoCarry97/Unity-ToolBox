using System.Collections;
using System.Collections.Generic;
using ToolBox.GUI;
using UnityEngine;
using UnityEngine.UI;
using ToolBox.Core;

public class AlliedTurnPanel : Panel
{
    private Button attack;
    private Button items;
    private Button guard;
    private Button run;
    private BattleSystem battle;
    private MobController current;

    private GuardMove guardMove;

    private Move itemMove;

    void Start()
    {
        attack=this.transform.GetChild(0).GetComponent<Button>();
        items= this.transform.GetChild(1).GetComponent<Button>();
        guard = this.transform.GetChild(2).GetComponent<Button>();
        run = this.transform.GetChild(3).GetComponent<Button>();
        attack.onClick.AddListener(Attack);
        guard.onClick.AddListener(Guard);
        items.onClick.AddListener(Items);
        run.onClick.AddListener(Run);
        battle = GameObject.FindObjectOfType<BattleSystem>();
        attack.Select();
        guardMove=ScriptableObject.CreateInstance<GuardMove>();
        guardMove.Name = "Guard";
    }

    private void Update()
    {
        current = battle.currentMob;
        if (current != null)
            print("current mob in battle: "+current.name);
    }

    private void Attack()
    {
        GameController.Instance.Gui.DeactivePanel();
        GameController.Instance.Gui.ActivePanel("ChooseMovePanel");
    }

    private void Guard()
    {
        StartCoroutine(GuardState());
        GameController.Instance.Battle.ChosenMove = guardMove;
        GameController.Instance.Battle.ChosenTarget = current;
        GameController.Instance.Gui.DeactivePanel();
    }

    private void Items()
    {
        
    }

    private void Run()
    {
        GameController.Instance.Gui.Clear();
        GameController.Instance.Battle.Terminate();
    }

    private IEnumerator GuardState()
    {
        yield return new WaitUntil(()=>!current.name.Equals(battle.currentMob.name));
        current.Info.Stats.defense *= 2;
        yield return new WaitUntil(() => current.name.Equals(battle.currentMob.name));
        current.Info.Stats.defense /= 2;
    }

}
