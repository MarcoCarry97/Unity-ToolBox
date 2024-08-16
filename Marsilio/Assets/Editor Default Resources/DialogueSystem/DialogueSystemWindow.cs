using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ToolBox.Editor.DialogueSystem
{
    public class DialogueSystemWindow : EditorWindow
    {
        [MenuItem("Window/Dialogues/Dialogue System Window")]
        public static void ShowExample()
        {
            DialogueSystemWindow window = EditorWindow.GetWindow<DialogueSystemWindow>();
            window.titleContent.text = "Dialogue System Window";
        }

        private void OnEnable()
        {
            AddGraphView();
        }

        private void AddGraphView()
        {
            DialogueSystemGraphView graphView = new DialogueSystemGraphView(this);
            graphView.StretchToParentSize();
            graphView.StretchToParentWidth();
            rootVisualElement.Add(graphView);

        }
    }
}
