using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace ToolBox.GUI.Utils
{
    [CreateAssetMenu(fileName = "Conversation", menuName = "ScriptableObjects/ConversationData")]
    public class Conversation :ScriptableObject, IEnumerable
    {
        public List<Dialogue> dialogues;


        public Conversation()
        {

        }

        public void AddDialogue(string speaker, string text)
        {
            dialogues.Add(new Dialogue(speaker, text));
        }

        public IEnumerator GetEnumerator()
        {
            foreach(Dialogue dialogue in dialogues)
                yield return dialogue;
        }
    }

}