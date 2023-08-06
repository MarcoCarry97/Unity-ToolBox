using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using static ToolBox.Interaction.InteractionFlag;
using ToolBox.Core;

public class Functions : MonoBehaviour
{
    public void OpenConversationView()
    {

        GameObject canvas = GameObject.FindGameObjectWithTag("GUI");
        if(canvas.transform.childCount==0)
        {
            GameObject game = Resources.Load<GameObject>("Prefabs/ConversationView");
            GameObject.Instantiate(game, canvas.transform);
            Debug.Log("ok");
        }
    }

    public void IsFinished(Flag flag)
    {
        flag.Value = GameObject.FindGameObjectWithTag("GUI")
            .transform.childCount > 0;
        print("FLAG VALUE " + flag.Value);
    }

    public void BeginBattle()
    {
        Enemy enemy=this.GetComponent<Enemy>();
        PartyController partyC = GameController.Instance.Party;
        GameController.Instance.Battle.Begin(partyC.party, enemy.EnemyParty);
    }
}
