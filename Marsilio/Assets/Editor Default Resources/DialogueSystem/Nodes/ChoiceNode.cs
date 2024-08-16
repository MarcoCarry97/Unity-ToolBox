using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using ToolBox.Editor.DialogueSystem.Utilities;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace ToolBox.Editor.DialogueSystem.Nodes
{
    public class ChoiceNode : BaseNode
    {
        [SerializeField]
        protected List<string> choices;

        public List<string> Choices
        {
            get { return choices; }
        }

        public ChoiceNode() { }

        public override void Initialize(DialogueSystemGraphView graphView,Vector3 position)
        {
            base.Initialize(graphView, position);
            dialogueName = "Choice Node";
            choices=new List<string>();
        }

        public override void Draw()
        {
            base.Draw();

            Button addButton = Utils.CreateButton("Add", OnAdd);
            titleContainer.Add(addButton);
            List<VisualElement> elements = CreateChoiceTiles(choices);
            elements.Add(addButton);
            Foldout choicesFoldout = Utils.CreateFoldout("Choices", elements);
            outputContainer.Add(choicesFoldout);
            RefreshExpandedState();
        }

        private List<VisualElement> CreateChoiceTiles(List<string> choices)
        {
            List<VisualElement> elements = new List<VisualElement>();
            foreach (string choice in choices)
            {
                VisualElement choiceTile = CreateChoiceTile(choice, OnDelete);
                elements.Add(choiceTile);
            }
            return elements;
        }

        private VisualElement CreateChoiceTile(string choice, VisualAction action)
        {
            TextField textField = Utils.CreateTextField("Choice Field", OnChoiceTextChanged);
            Port port = InstantiateOutput("");
            Button button = Utils.CreateButton("X", action(port));
            port.Add(textField);
            port.Add(button);
            return port;
        }

        private void OnAdd()
        {
            choices.Add("New Choice");
            Foldout foldout = outputContainer.ElementAt(0) as Foldout;
            Button button = foldout.ElementAt(foldout.childCount - 1) as Button;
            Port element = CreateChoiceTile("New Choice",OnDelete) as Port;      
            
            
            foldout.Remove(button);
            foldout.Add(element);
            foldout.Add(button);
            
            RefreshExpandedState();
        }

        private Action OnDelete(VisualElement element)
        {
            return () =>
            {
                Port port = element as Port;
                Foldout foldout=outputContainer.ElementAt(0) as Foldout;
                int index = foldout.IndexOf(port);
                choices.RemoveAt(index);
                DisconnectPorts();
                foldout.RemoveAt(index);
                RefreshExpandedState();
            };
        }

        private void OnChoiceTextChanged(ChangeEvent<string> evt)
        {
            int index = choices.IndexOf(evt.previousValue);
            choices[index] = evt.newValue;
        }

    }
}
