using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolBox.GUI.Utils
{
    [CreateAssetMenu(fileName ="Dialogue",menuName = "ScriptableObjects/DialogueData")]
    public class Dialogue : ScriptableObject,IEnumerable
    {
        [SerializeField]
        public string Speaker;

        [SerializeField]
        public string Text;


        public Dialogue(string speaker, string text)
        {
            Speaker = speaker;
            Text = text;
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)Text).GetEnumerator();
        }
    }
}
