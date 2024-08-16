using System.Collections;
using System.Collections.Generic;
using ToolBox.GUI;
using UnityEngine;

public class BattlePanel : Panel
{
    [SerializeField]
    private List<LifeBar> partyLives;

    [SerializeField]
    private List<LifeBar> enemyLives;

    // Start is called before the first frame update
    void Start()
    {
        BattleSystem battle=GameObject.FindObjectOfType<BattleSystem>();
        if(battle!=null)
        {
            if( battle.GetState()==BattleSystem.State.Init)
            {
                AssignLives(partyLives, battle.AlliedParty);
                AssignLives(enemyLives, battle.EnemyParty);
                DeactiveLifebarsNull(partyLives);
                DeactiveLifebarsNull(enemyLives);
            }
        }
    }

    private void AssignLives(List<LifeBar> lives,List<AlliedController> mobs)
    {
        for (int i = 0; i < mobs.Count; i++)
            lives[i].Mob = mobs[i];
    }

    private void AssignLives(List<LifeBar> lives, List<EnemyController> mobs)
    {
        for (int i = 0; i < mobs.Count; i++)
            lives[i].Mob = mobs[i];
    }

    private void DeactiveLifebarsNull(List<LifeBar> lives)
    {
        for(int i=0;i<lives.Count;i++)
            if (lives[i].Mob==null)
                lives[i].gameObject.SetActive(false);
    }
}
