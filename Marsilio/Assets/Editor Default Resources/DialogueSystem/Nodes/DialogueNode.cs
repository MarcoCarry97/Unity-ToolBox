using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace ToolBox.Editor.DialogueSystem.Nodes
{
    public class DialogueNode : BaseNode
    {
        public DialogueNode() { }

        public override void Initialize(DialogueSystemGraphView graphView,Vector3 position)
        {
            base.Initialize(graphView,position);
            dialogueName = "Dialogue Node";
            dialogueText = "Text";
        }

        public override void Draw()
        {
            base.Draw();
            Port output = InstantiateOutput("Next Dialogue");
            outputContainer.Add(output);
            RefreshExpandedState();
        }
    }
}

