using Mazer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.TerrainTools;

namespace Mazer.Editor
{
    [CustomEditor(typeof(AspProgram),true)]
    [CanEditMultipleObjects]
    public class AspProgramEditor : UnityEditor.Editor
    {
        SerializedProperty content;

        private void OnEnable()
        {
            content=serializedObject.FindProperty("content");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(content);
            serializedObject.ApplyModifiedProperties();
        }
    }


}
