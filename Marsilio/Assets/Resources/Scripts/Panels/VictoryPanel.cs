using System.Collections;
using System.Collections.Generic;
using ToolBox.GUI;
using UnityEngine;
using ToolBox.Core;
using ToolBox.Control.Explorer2D;

public class VictoryPanel : Panel
{ 
    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.Commands.SetState(InputController.State.VictoryControl);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
