using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace ToolBox.Editor.DialogueSystem.Utilities
{
    public delegate Action VisualAction(VisualElement element);

    public static class Utils 
    {
       public static TextField CreateTextField(string text=null,EventCallback<ChangeEvent<string>> onValueChanged=null)
        {
            TextField textField = new TextField
            {
                value = text,
            };
            if(onValueChanged!= null )
                textField.RegisterValueChangedCallback( onValueChanged );
            return textField;
        }

        public static TextField CreateTextArea(string text = null, EventCallback<ChangeEvent<string>> onValueChanged = null)
        {
            TextField textArea = CreateTextField(text, onValueChanged);
            textArea.multiline= true;
            return textArea;
        }

        public static Foldout CreateFoldout(string name, List<VisualElement> list)
        {
            Foldout foldout = new Foldout()
            {
                text = name
            };
            foreach(VisualElement element in list)
                foldout.Add(element);
            return foldout;
        }

        public static Foldout CreateFoldout(string name, params VisualElement[] list)
        {
            return CreateFoldout(name,list.ToList<VisualElement>());
        }

        public static Button CreateButton(string text,Action onCLick)
        {
            Button button = new Button()
            {
                text = text
            };
            button.clicked += onCLick;
            return button;
        }

        
    }

    
}
