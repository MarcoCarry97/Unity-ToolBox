using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using ToolBox.Extensions.Editor;
using ToolBox.Editor.DialogueSystem.Nodes;
using ToolBox.Editor.DialogSystem.Windows;
using System;

namespace ToolBox.Editor.DialogueSystem
{
    public class DialogueSystemGraphView : GraphView
    {

        private DialogueSystemWindow dialogueWindow;
        private DialogueSystemSearchWindow searchWindow;

        public DialogueSystemGraphView(DialogueSystemWindow window)
        {
            dialogueWindow= window;
            AddSearchWindow();
            AddGrid();
            AddStyle();
            AddManipulators();
            
        }

        private void AddGrid()
        {
            GridBackground grid = new GridBackground();
            grid.StretchToParentWidth();
            grid.StretchToParentSize();
            Insert(0, grid);
        }

        private void AddStyle()
        {
            StyleSheet style = (StyleSheet)EditorGUIUtility.Load("DialogueSystem/Style/DialogueSystemStyle.uss");
            styleSheets.Add(style);
        }

        private void AddManipulators()
        {
            float min = ContentZoomer.DefaultMinScale;
            float max = ContentZoomer.DefaultMaxScale;

            this.AddManipulator(SetupZoomer(min, max));
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            this.AddManipulator(CreateContextualMenu());

            this.AddManipulator(CreateGroupContextualMenu());
        }

        private void AddSearchWindow()
        {
            if(searchWindow==null)
            {
                searchWindow=ScriptableObject.CreateInstance<DialogueSystemSearchWindow>();
                searchWindow.Initialize(this);

            }
            nodeCreationRequest = (context) =>
            {
                SearchWindow.Open(new SearchWindowContext(
                    context.screenMousePosition),
                    searchWindow
                );
            };
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
           List<Port> CompatiblePorts = new List<Port>();
            foreach(Port port in ports)
            {
                if(port!=startPort &&
                    port.node!=startPort.node &&
                    port.direction != startPort.direction)
                {
                    CompatiblePorts.Add(port);
                }
            }
            return CompatiblePorts;
        }

        private IManipulator SetupZoomer(float min, float max)
        {
            IManipulator zoom = new ContentZoomer();
            return zoom;
        }

        private ContextualMenuManipulator CreateContextualMenu()
        {
            ContextualMenuManipulator man = new ContextualMenuManipulator(
                (menuEvent) =>
                {
                    menuEvent.menu.AppendAction("Add Choice",
                        (actionEvent) =>
                        {
                            Vector2 pos = GetLocalMousePosition(actionEvent.eventInfo.localMousePosition);
                            ChoiceNode node = CreateNode<ChoiceNode>(pos);
                            this.AddElement(node);
                        }
                    );
               
                    menuEvent.menu.AppendAction("Add Dialogue",
                        (actionEvent) =>
                        {
                            Vector2 pos = GetLocalMousePosition(actionEvent.eventInfo.localMousePosition);
                            DialogueNode node = CreateNode<DialogueNode>(pos);
                            this.AddElement(node);
                        }
                    );
                }
            );

            return man;
        }

        private ContextualMenuManipulator CreateGroupContextualMenu()
        {
            ContextualMenuManipulator man = new ContextualMenuManipulator(
                (menuEvent) =>
                {
                menuEvent.menu.AppendAction("Add Group",
                        (actionEvent)=>
                        {
                            Vector2 pos = GetLocalMousePosition(actionEvent.eventInfo.localMousePosition);
                            this.AddElement(CreateGroupNode(pos));
                        });
                }
            );
            return man;
        }

        

        public T CreateNode<T>(Vector2 position) where T:BaseNode
        {
            T node = Activator.CreateInstance<T>();
            node.Initialize(this,position);
            node.Draw();
            return node;
        }

        public Group CreateGroupNode(Vector2 position)
        {
            Group node = new Group()
            {
                title = "Dialogue Group"
            };
            node.SetPosition(new Rect(position, Vector2.zero));
            return node;
        }

        public Vector2 GetLocalMousePosition(Vector2 position, bool isSearchWindow=false)
        {
            Vector2 mousePos = position;
            if(isSearchWindow)
            {
                mousePos -= dialogueWindow.position.position;
            }
            Vector2 localMousePos=contentViewContainer.WorldToLocal(mousePos);
            return localMousePos;
        }

        public void DisconnectPorts(VisualElement container)
        {
            foreach(Port port in container.Children())
            {
                if(port.connected)
                {
                    port.DisconnectAll();
                    DeleteElements(port.connections);
                }
            }
        }
    }
}
