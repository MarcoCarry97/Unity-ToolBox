using System;
using System.Collections;
using System.Collections.Generic;
using ToolBox.Editor.DialogueSystem;
using ToolBox.Editor.DialogueSystem.Nodes;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace ToolBox.Editor.DialogSystem.Windows
{
    public enum NodeType
    {
        Dialogue,
        Choice
    }

    public class DialogueSystemSearchWindow : ScriptableObject, ISearchWindowProvider
    {

        private DialogueSystemGraphView graphView;
        private Texture2D identIcon;

        public void Initialize(DialogueSystemGraphView view)
        {
            graphView= view;
            identIcon = new Texture2D(1, 1);
            identIcon.SetPixel(0, 0, Color.clear);
            identIcon.Apply();
        }


        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> entries = new List<SearchTreeEntry>()
            {
                new SearchTreeGroupEntry(new GUIContent("Create Element",identIcon)),
                new SearchTreeGroupEntry(new GUIContent("DialogueNode"),1),
                new SearchTreeEntry(new GUIContent("ChoiceNode",identIcon))
                {
                   level=2,
                   userData=NodeType.Dialogue
                },
                new SearchTreeEntry(new GUIContent("ChoiceNode", identIcon))
                {
                   level=2,
                   userData=NodeType.Choice
                },
                new SearchTreeGroupEntry(new GUIContent("DialogueGroup", identIcon),1),
                new SearchTreeEntry(new GUIContent("Single group", identIcon))
                {
                    level=2,
                    userData=new Group()
                }
            };
            return entries;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            Vector3 pos = graphView.GetLocalMousePosition(context.screenMousePosition);
            bool res=false;
            BaseNode node;
            switch(SearchTreeEntry.userData)
            {
                case NodeType.Dialogue:
                    node = graphView.CreateNode<DialogueNode>(pos);
                    graphView.AddElement(node);
                    res = true;
                    break;
                case NodeType.Choice:
                    node = graphView.CreateNode<ChoiceNode>(pos);
                    graphView.AddElement(node);
                    res = true;
                    break;
                case Group _:
                    Group group = graphView.CreateGroupNode(pos);
                    graphView.AddElement(group);
                    res = true;
                    break;
            }
            return res;
        }
    }
}
