using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable
{
    public bool IsSelected();

    public void Select();

    public bool IsSelectable();
}
