using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PrintItem", menuName ="ScriptableObjects/Items/PrintItem")]
public class PrintItem : Item
{
    public override void OnEffect()
    {
        Debug.Log("It works!");
    }
}
