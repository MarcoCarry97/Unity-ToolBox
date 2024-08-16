using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "ScriptableObjects/Battle/Move/Status")]
public class StatusMove : Move
{
    public MobController.Status status;

    public override void Apply(MobController executor, MobController target)
    {
        executor.MobStatus = status;
    }
}
