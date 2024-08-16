using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ToolBox;
using UnityEngine;
using System;
using Unity.VisualScripting.FullSerializer;
using ToolBox.Extensions;
using ToolBox.Core;
using static UnityEngine.GraphicsBuffer;


class BattleException : Exception
{
    public new string Message { get; private set; }
    public BattleException(string message)
    {
        this.Message = message;
    }

    public BattleException()
    {
        this.Message = "Something is setted wroungly in the battle system!";
    }
}

public class BattleSystem : MonoBehaviour
{

    public List<AlliedController> AlliedParty { get { return GetParty<AlliedController>(); } }
    public List<EnemyController> EnemyParty { get { return GetParty<EnemyController>(); } }

    public enum State
    {
        Loading,
        Preparing,
        Init,
        During,
        Victory,
        GameOver
    }

    private State state;

    private Coroutine initCoroutine;

    public MobController currentMob;

    private GameObject alliedSpawnPoints;

    private GameObject enemySpawnPoints;

    public void SetState(State state)=>this.state= state;

    public State GetState() => state;

    private List<T> GetParty<T>() where T: MobController
    {
        List<T> party=GameObject.FindObjectsOfType<T>().ToList<T>();
        return party.Select(x=>x).Where(x=>x.ModificableStats.health!=0).ToList();
    }

    // Start is called before the first frame update
    void Start()
    {

        state = State.Loading;
        alliedSpawnPoints = this.transform.GetChild(0).gameObject;
        enemySpawnPoints = this.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Preparing:
                PreparingState();
                break;
            case State.Init:
                InitState();
                break;
            case State.During:
                DuringState();
                break;
            case State.Victory:
                VictoryState();
                break;
            case State.GameOver:
                GameOverState();
                break;
        }
    }

    private void PreparingState()
    {
        BattleController battle = GameController.Instance.Battle;
        Spawn<AlliedController>(battle.AlliedParty,alliedSpawnPoints);
        Spawn<EnemyController>(battle.EnemyParty,enemySpawnPoints);
        GameController.Instance.Gui.ActivePanel("BattlePanel");
        state = State.Init;
    }

    private void Spawn<T>(Party party,GameObject spawnPoints) where T:MobController
    {
        int index = 0;
        foreach (MobInfo info in party)
        {
            GameObject game = Resources.Load<GameObject>("Prefabs/BattleUnit");
            GameObject instance = Instantiate(game);
            SpriteRenderer renderer = instance.GetComponent<SpriteRenderer>();
            renderer.sprite = info.MobSprite;
            Animator animator = instance.GetComponent<Animator>();
            animator.runtimeAnimatorController = info.MobAnimation;
            instance.name = info.Name;
            T mob = instance.AddComponent<T>();
            mob.Info = info;
            renderer.flipX = mob is AlliedController;
            GameObject spawn = spawnPoints.transform.GetChild(index).gameObject;
            instance.transform.position=spawn.transform.position;
            //spawn to spawnpoint index
            index++;
        }
    }

    private void InitState()
    {
        
        if(initCoroutine==null)
        {
            if (AlliedParty.Count == 0 || EnemyParty.Count == 0)
                throw new BattleException("Remember to attach the components AlliedController and EnemyController to your prefabs!");
            state = State.During;
            initCoroutine = StartCoroutine(BattlePhase());
            
        }
    }

    private IEnumerator BattlePhase()
    {
        while(state==State.During)
        {
            List<MobController> turns = GameObject.FindObjectsOfType<MobController>().ToList<MobController>();
            turns=turns.Select(x=>x).Where(x=>x.ModificableStats.health!=0).ToList();
            turns.Sort((MobController a, MobController b)=>-a.CompareTo(b));
            foreach (MobController mob in turns)
                print("TURNS: " + mob.name);
            for(int i=0;i<turns.Count && state==State.During;i++)
            {
                GameController.Instance.Battle.ChosenMove = null;
                GameController.Instance.Battle.ChosenTarget = null;
                MobController turn = turns[i];
                print("Turn: "+turn.name);
                currentMob = turn;
                print("MOVE");
                yield return DoTurn(turn);
                Move move = GameController.Instance.Battle.ChosenMove;
                MobController target = GameController.Instance.Battle.ChosenTarget;

                turn.DoMove(move, target);
                print("Move done");
                yield return new WaitForSeconds(2);
                CheckTargets();
                print("target checked");
                print("NEXT " + state);
                if (EnemyParty.Count == 0) state = State.Victory;
                if (AlliedParty.Count == 0) state = State.GameOver;
            }
        }
    }

    private void CheckTargets()
    {
        MobController target = GameController.Instance.Battle.ChosenTarget;
        if (target.ModificableStats.health == 0)
        {
            Die(target);
            target.enabled = false;
        }
    }

    private void Die(MobController mob)
    {
        Vector3 angles = mob.transform.rotation.eulerAngles;
        angles.z = 90;
        mob.transform.rotation = Quaternion.Euler(angles);
    }

    private IEnumerator DoTurn(MobController turn)
    {
        turn.DoTurn();
        BattleController battleC = GameController.Instance.Battle;
        return new WaitUntil(() => battleC.ChosenMove != null && battleC.ChosenTarget != null);
    }

    private IEnumerator ChooseTargets(MobController turn)
    {
        turn.ChooseTargets();
        BattleController battleC=GameController.Instance.Battle;
        return new WaitUntil(()=>battleC.ChosenTarget!=null);
    }

    private IEnumerator MobTurn(MobController mob)
    {
        mob.ChooseMove();
        BattleController battleC=GameController.Instance.Battle;
        return new WaitUntil(() => battleC.ChosenMove!=null);
    }

    private void DuringState()
    {
        
    }

    private void VictoryState()
    {
        GameController.Instance.Gui.DeactivePanel();
        GameController.Instance.Gui.ActivePanel("VictoryPanel");
    }

    private void GameOverState()
    {
        GameController.Instance.Gui.DeactivePanel();
        GameController.Instance.Gui.ActivePanel("GameOverPanel");
    }
    

}
