using Band.Gui.Widgets;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Band.Editor.GUI.Widgets
{
    [CustomEditor(typeof(ExtendedButton))]
    public class ExtendedButtonEditor : ButtonEditor
    {
       private SerializedProperty selectProperty;
       private SerializedProperty deselectProperty;
       private SerializedProperty audioClickProperty;
       private SerializedProperty audioSelectProperty;

        protected override void OnEnable()
        {
            base.OnEnable();
            selectProperty = serializedObject.FindProperty("m_OnSelect");
            deselectProperty = serializedObject.FindProperty("m_OnDeselect");
            audioClickProperty = serializedObject.FindProperty("clickClip");
            audioSelectProperty = serializedObject.FindProperty("selectClip");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUILayout.PropertyField(selectProperty);
            EditorGUILayout.PropertyField(deselectProperty);
            EditorGUILayout.PropertyField(audioClickProperty);
            EditorGUILayout.PropertyField(audioSelectProperty);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
