using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PrintTrap",menuName ="ScriptableObjects/Traps/PrintTrap")]
public class PrintTrap : Trap
{

    public override void OnEffect()
    {
        Debug.Log("It works!");
    }
}
