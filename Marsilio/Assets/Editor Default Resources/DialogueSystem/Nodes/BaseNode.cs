using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;
using ToolBox.Editor.DialogueSystem.Utilities;

namespace ToolBox.Editor.DialogueSystem.Nodes
{
    public abstract class BaseNode : Node
    {
        private DialogueSystemGraphView graphView;

        [SerializeField]
        protected string dialogueName;

        public string DialogueName
        {
            get { return name; }
        }

        [SerializeField]
        protected string dialogueText;

        public string DialogueText
        {
            get { return dialogueText; }
        }
        
        public virtual void Initialize(DialogueSystemGraphView graphView, Vector3 position)
        {
            this.graphView= graphView;
            dialogueName = "New Dialogue";
            this.SetPosition(new Rect(position,Vector2.zero));
        }

        public virtual void Draw()
        {
            TextField textField =Utils.CreateTextField(dialogueName,OnDialogueNameChanged);
            mainContainer.Insert(0, textField);
            Port input = InstantiateInput("Dialogue Connection");
            inputContainer.Add( input);       
            TextField dialogueField = Utils.CreateTextArea(dialogueText, OnDialogueTextChanged);
            Foldout textFoldOut = Utils.CreateFoldout("Dialogue Text", dialogueField);
            extensionContainer.Add(textFoldOut);
        }

        private void OnDialogueNameChanged(ChangeEvent<string> evt)
        {
            dialogueName = evt.newValue;
        }

        private void OnDialogueTextChanged(ChangeEvent<string> evt)
        {
            dialogueName = evt.newValue;
        }

        protected Port InstantiateInput(string name)
        {
            Port input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
            input.portName= name;
            return input;
        }

        protected Port InstantiateOutput(string name)
        {
            Port output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
            output.portName= name;
            return output;
        }

        protected void DisconnectPorts()
        {
            Foldout foldout=outputContainer.ElementAt(0) as Foldout;
            foreach(VisualElement element in foldout.Children())
            {
                if(element is Port)
                {
                    Port port = element as Port;
                    if (port.connected)
                    {
                        graphView.DeleteElements(port.connections);
                    }
                }
            }
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            evt.menu.AppendAction("Disconnect Input Ports", (actionEvent) => graphView.DisconnectPorts(inputContainer));
            evt.menu.AppendAction("Disconnect Output Ports", (actionEvent) => graphView.DisconnectPorts(outputContainer.ElementAt(0)));
            base.BuildContextualMenu(evt);
        }
    }   
}
